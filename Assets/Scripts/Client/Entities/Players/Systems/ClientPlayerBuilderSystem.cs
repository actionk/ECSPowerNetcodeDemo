using Client.Entities.Players.Builders;
using Plugins.ECSPowerNetcode.Client.Entities;
using Plugins.ECSPowerNetcode.Client.Groups;
using Protocol.Players.Packets;
using Unity.Entities;

namespace Client.Entities.Players.Systems
{
    [UpdateInGroup(typeof(ClientGameSimulationSystemGroup))]
    public class ClientPlayerBuilderSystem : AClientNetworkEntityBuilderSystem<ServerPlayerBuilderPacket>
    {
        protected override void CreateNetworkEntity(uint networkEntityId, ServerPlayerBuilderPacket packet)
        {
            ClientPlayerBuilder
                .Create(packet)
                .Build(PostUpdateCommands);
        }

        protected override void SynchronizeNetworkEntity(Entity entity, ServerPlayerBuilderPacket packet)
        {
            // update the entity
        }
    }
}