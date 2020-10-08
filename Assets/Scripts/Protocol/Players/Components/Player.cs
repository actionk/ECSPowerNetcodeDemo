using Unity.Entities;

namespace Protocol.Players.Components
{
    public struct Player : IComponentData
    {
        public uint playerId;
    }
}