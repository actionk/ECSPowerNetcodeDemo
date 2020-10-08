using Unity.Entities;

namespace Server.Entities.Players.Components
{
    public struct ServerPlayer : IComponentData
    {
        public int networkConnectionId;
        public Entity networkConnection;
    }
}