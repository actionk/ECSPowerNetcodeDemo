using Plugins.ECSPowerNetcode.Client.Packets;
using Plugins.ECSPowerNetcode.Shared;
using Protocol.Players.Packets;
using UnityEngine;

namespace Client.Entities.Players.Systems
{
    public class ClientPlayerLoginResponseSystem : AClientReceiveRpcCommandSystem<ServerPlayerLoginResponsePacket>
    {
        protected override void OnCommand(ref ServerPlayerLoginResponsePacket packet, ConnectionDescription connectionToServer)
        {
            Debug.Log($"[Client] Player logged in [PlayerId={packet.playerId}, NetworkEntityId={packet.networkEntityId}]");

            // DO PLAYER INIT USING ServerPlayerLoginResponsePacket
        }
    }
}