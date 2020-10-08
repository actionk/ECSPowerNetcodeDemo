using Unity.Burst;
using Unity.Mathematics;
using Unity.NetCode;

namespace Protocol.Players.Packets
{
    [BurstCompile]
    public struct ClientPlayerMovementCommand : ICommandData
    {
        public uint Tick { get; set; }

        public uint playerId;
        public uint networkEntityId;
        public uint createdTick;
        public float3 position;
        public quaternion rotation;
        public int2 velocity;
    }
}