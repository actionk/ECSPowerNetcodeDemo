using Plugins.ECSEntityBuilder.Worlds;
using Plugins.ECSPowerNetcode.Features.Synchronization;
using Plugins.ECSPowerNetcode.Server;
using Plugins.ECSPowerNetcode.Server.Components;
using Plugins.ECSPowerNetcode.Server.Groups;
using Protocol.Players.Components;
using Protocol.Players.Packets;
using Unity.Entities;
using Unity.NetCode;
using Unity.Transforms;

namespace Server.Entities.Players.Systems
{
    [UpdateInGroup(typeof(ServerRequestProcessingSystemGroup))]
    public class ServerPlayerMovementSystem : ComponentSystem
    {
        private uint m_lastProcessedTick;

        protected override void OnUpdate()
        {
            uint tick = EntityWorldManager.Instance.ServerTick;

            Entities
                .WithAll<ServerToClientCommandHandler>()
                .WithNone<ClientPlayerMovementCommand>()
                .ForEach(entity => { PostUpdateCommands.AddBuffer<ClientPlayerMovementCommand>(entity); });

            Entities.ForEach((DynamicBuffer<ClientPlayerMovementCommand> inputBuffer) =>
            {
                if (!inputBuffer.GetDataAtTick(tick, out var input))
                    return;

                if (input.playerId == 0 || input.createdTick == m_lastProcessedTick)
                    return;

                //Debug.Log($"[Server] Received player [{input.playerId}] movement. Position: {input.position}, rotation {input.rotation}, velocity {input.velocity}");

                var playerEntity = ServerManager.Instance.NetworkEntityManager.GetEntityByNetworkEntityId(input.networkEntityId);
                PostUpdateCommands.SetComponent(playerEntity, new Translation {Value = input.position});
                PostUpdateCommands.SetComponent(playerEntity, new Rotation {Value = input.rotation});
                PostUpdateCommands.SetComponent(playerEntity, new Velocity {value = input.velocity});
                PostUpdateCommands.AddComponent<Synchronize>(playerEntity);

                m_lastProcessedTick = input.createdTick;
            });
        }
    }
}