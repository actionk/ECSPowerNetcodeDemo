using Unity.NetCode;

namespace Protocol.Players.Packets
{
    public struct ServerPlayerLoginResponsePacket : IRpcCommand
    {
        public uint playerId;
        public uint networkEntityId;
    }
}