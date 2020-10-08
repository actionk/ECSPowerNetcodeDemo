using Plugins.ECSEntityBuilder.Archetypes;
using Plugins.ECSEntityBuilder.Worlds;
using Protocol.Players.Archetypes;
using Unity.Entities;
using Unity.Transforms;

namespace Client.Entities.Players.Builders
{
    [Archetype(WorldType = WorldType.CLIENT)]
    public class ClientPlayerArchetype : PlayerArchetype
    {
        public override ComponentType[] CustomComponents => new ComponentType[]
        {
            typeof(CopyTransformToGameObject)
        };
    }
}