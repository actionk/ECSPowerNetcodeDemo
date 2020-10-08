using Plugins.ECSPowerNetcode.Server;
using Plugins.ECSPowerNetcode.Server.Components;
using Plugins.ECSPowerNetcode.Server.Packets;
using Protocol.Players.Packets;
using Server.Entities.Players.Builders;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Server.Entities.Players.Systems
{
    [UpdateInGroup(typeof(ServerSimulationSystemGroup))]
    public class ServerPlayerLoginSystem : ComponentSystem
    {
        private uint m_nextAvailablePlayerId;

        protected override void OnUpdate()
        {
            Entities
                .ForEach((Entity entity, ref ClientPlayerLoginRequestPacket command, ref ReceiveRpcCommandRequestComponent reqSrc) =>
                {
                    var playerId = ++m_nextAvailablePlayerId;
                    var networkConnectionId = (byte) EntityManager.GetComponentData<NetworkIdComponent>(reqSrc.SourceConnection).Value;

                    Debug.Log($"[Server] Player connected from {reqSrc.SourceConnection}:{networkConnectionId}. Assigning id: {playerId}");

                    var networkEntityId = ServerManager.Instance.NextNetworkEntityId;

                    new ServerPlayerBuilder(networkEntityId, networkConnectionId, reqSrc.SourceConnection, playerId)
                        .AddElementToBuffer(new TransferNetworkEntityToClient {clientConnection = reqSrc.SourceConnection})
                        .Build(PostUpdateCommands);

                    ServerToClientRpcCommandBuilder
                        .SendTo(reqSrc.SourceConnection, new ServerPlayerLoginResponsePacket
                        {
                            playerId = playerId,
                            networkEntityId = networkEntityId
                        })
                        .Build(PostUpdateCommands);

                    PostUpdateCommands.DestroyEntity(entity);
                });
        }
    }
}