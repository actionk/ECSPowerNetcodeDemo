using Plugins.ECSPowerNetcode.Client;
using Plugins.ECSPowerNetcode.Client.Components;
using Plugins.ECSPowerNetcode.Client.Groups;
using Plugins.ECSPowerNetcode.Features.NetworkEntities;
using Protocol.Players.Components;
using Protocol.Players.Packets;
using Unity.Entities;
using Unity.Mathematics;
using Unity.NetCode;
using Unity.Transforms;
using UnityEngine;

namespace Client.Entities.Players.Systems
{
    [UpdateInGroup(typeof(ClientGameSimulationSystemGroup))]
    public class ClientPlayerMovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            var deltaTime = Time.DeltaTime;

            Entities
                .WithAll<ClientToServerCommandHandler>()
                .WithNone<ClientPlayerMovementCommand>()
                .ForEach(entity => { PostUpdateCommands.AddBuffer<ClientPlayerMovementCommand>(entity); });

            Entities
                .WithAll<Player>()
                .ForEach((Entity entity, ref Player player, ref Velocity velocity, ref NetworkEntity networkEntity, ref Translation translation, ref Rotation rotation) =>
                {
                    velocity.value = int2.zero;

                    if (Input.GetKey(KeyCode.A))
                        velocity.value.x = -1;
                    if (Input.GetKey(KeyCode.D))
                        velocity.value.x = 1;
                    if (Input.GetKey(KeyCode.W))
                        velocity.value.y = 1;
                    if (Input.GetKey(KeyCode.S))
                        velocity.value.y = -1;

                    if (velocity.value.x == 0 && velocity.value.y == 0)
                        return;

                    translation.Value = translation.Value + new float3(velocity.value.x, 0, velocity.value.y) * deltaTime * 5.0f;

                    var tick = ClientManager.Instance.ServerTick;

                    var playerMoveInput = EntityManager.GetBuffer<ClientPlayerMovementCommand>(ClientManager.Instance.ConnectionToServer.commandHandlerEntity);
                    playerMoveInput.AddCommandData(new ClientPlayerMovementCommand
                    {
                        playerId = player.playerId,
                        networkEntityId = networkEntity.networkEntityId,
                        position = translation.Value,
                        rotation = rotation.Value.value,
                        velocity = velocity.value,
                        Tick = tick,
                        createdTick = tick
                    });

                    //Debug.Log($"[Client] send player movement for player {player.playerId} at tick {tick}");
                });
        }
    }
}