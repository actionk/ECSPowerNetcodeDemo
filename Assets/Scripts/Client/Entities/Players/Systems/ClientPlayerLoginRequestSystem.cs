using Plugins.ECSPowerNetcode.Client.Groups;
using Plugins.ECSPowerNetcode.Client.Packets;
using Protocol.Players.Packets;
using Unity.Entities;
using Unity.NetCode;
using UnityEngine;

namespace Client.Entities.Players.Systems
{
    [UpdateInGroup(typeof(ClientRequestProcessingSystemGroup))]
    public class ClientPlayerLoginRequestSystem : ComponentSystem
    {
        private struct PlayerLoggedId : ISystemStateComponentData
        {
        }

        protected override void OnUpdate()
        {
            Entities
                .WithAll<NetworkStreamInGame>()
                .WithNone<PlayerLoggedId>()
                .ForEach(entity =>
                {
                    ClientToServerRpcCommandBuilder
                        .Send(new ClientPlayerLoginRequestPacket())
                        .Build(PostUpdateCommands);

                    PostUpdateCommands.AddComponent<PlayerLoggedId>(entity);

                    Debug.Log("[Client] Player tries to log in");
                });
        }
    }
}