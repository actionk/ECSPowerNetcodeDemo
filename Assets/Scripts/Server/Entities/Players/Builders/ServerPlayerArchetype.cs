using Plugins.ECSEntityBuilder.Archetypes;
using Plugins.ECSEntityBuilder.Worlds;
using Plugins.ECSPowerNetcode.Features.Synchronization.Transform;
using Protocol.Players.Archetypes;
using Server.Entities.Players.Components;
using Unity.Entities;

namespace Server.Entities.Players.Builders
{
    [Archetype(WorldType = WorldType.SERVER)]
    public class ServerPlayerArchetype : PlayerArchetype
    {
        public override ComponentType[] CustomComponents => new ComponentType[]
        {
            typeof(ServerPlayer),
            typeof(SyncTransformFromServerToClient)
        };
    }
}