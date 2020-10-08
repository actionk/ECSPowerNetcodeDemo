using Plugins.ECSPowerNetcode.Server.Entities;
using Protocol.Players.Components;
using Server.Entities.Players.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Server.Entities.Players.Builders
{
    public class ServerPlayerBuilder : ServerNetworkEntityBuilder<ServerPlayerBuilder>
    {
        public ServerPlayerBuilder(uint networkEntityId, int networkConnectionId, Entity networkConnection, uint playerId) : base(networkEntityId)
        {
            CreateFromArchetype<ServerPlayerArchetype>();
            SetComponentData(new Scale {Value = 1});
            SetComponentData(new Player
            {
                playerId = playerId
            });
            SetComponentData(new ServerPlayer
            {
                networkConnectionId = networkConnectionId,
                networkConnection = networkConnection,
            });
            SetName($"ServerPlayer {playerId}");
        }
    }
}