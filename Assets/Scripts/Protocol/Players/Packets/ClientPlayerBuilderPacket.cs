using Plugins.ECSPowerNetcode.Client.Packets;
using Unity.Burst;
using Unity.NetCode;

namespace Protocol.Players.Packets
{
    [BurstCompile]
    public struct ServerPlayerBuilderPacket : INetworkEntityCopyRpcCommand, IRpcCommand
    {
        public uint NetworkEntityId => networkEntityId;

        public int networkConnectionId;
        public uint networkEntityId;
        public uint playerId;
    }
}