using Plugins.ECSEntityBuilder.Archetypes;
using Plugins.ECSPowerNetcode.Features.NetworkEntities;
using Protocol.Players.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Protocol.Players.Archetypes
{
    public abstract class PlayerArchetype : ICustomArchetypeDescriptor
    {
        public ComponentType[] Components => new ComponentType[]
        {
            typeof(Translation),
            typeof(Rotation),
            typeof(Scale),
            typeof(LocalToWorld),
            typeof(Velocity),

            // SHARED
            typeof(NetworkEntity),
            typeof(Player),
        };

        public abstract ComponentType[] CustomComponents { get; }
    }
}