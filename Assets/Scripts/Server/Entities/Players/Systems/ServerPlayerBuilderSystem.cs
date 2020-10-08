using Plugins.ECSPowerNetcode.Features.NetworkEntities;
using Plugins.ECSPowerNetcode.Server.Entities;
using Protocol.Players.Components;
using Protocol.Players.Packets;
using Server.Entities.Players.Components;
using Unity.Entities;

namespace Server.Entities.Players.Systems
{
    public class ServerPlayerBuilderSystem : AServerNetworkEntityBuilderSystemT2<ServerPlayer, Player, ServerPlayerBuilderPacket>
    {
        protected override ServerPlayerBuilderPacket CreateTransferCommandForEntity(Entity entity, NetworkEntity networkEntity, ref ServerPlayer serverPlayer, ref Player player)
        {
            return new ServerPlayerBuilderPacket
            {
                networkConnectionId = serverPlayer.networkConnectionId,
                networkEntityId = networkEntity.networkEntityId,
                playerId = player.playerId
            };
        }
    }
}