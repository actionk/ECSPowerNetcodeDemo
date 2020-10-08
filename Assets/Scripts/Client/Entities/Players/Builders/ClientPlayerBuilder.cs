using Client.Entities.Players.Components;
using Plugins.ECSEntityBuilder;
using Plugins.ECSEntityBuilder.Components;
using Plugins.ECSEntityBuilder.Managers;
using Plugins.ECSPowerNetcode.Client;
using Plugins.ECSPowerNetcode.Client.Entities;
using Plugins.ECSPowerNetcode.Features.Synchronization.Transform;
using Protocol.Players.Components;
using Protocol.Players.Packets;
using Unity.Transforms;
using UnityEngine;

namespace Client.Entities.Players.Builders
{
    public class ClientPlayerBuilder : ClientNetworkEntityBuilder<ClientPlayerBuilder>
    {
        public static ClientPlayerBuilder Create(ServerPlayerBuilderPacket packet)
        {
            return new ClientPlayerBuilder(packet);
        }

        private ClientPlayerBuilder(ServerPlayerBuilderPacket packet) : base(packet.networkEntityId)
        {
            CreateFromArchetype<ClientPlayerArchetype>();
            AddComponentData(new Player {playerId = packet.playerId});
            SetComponentData(new Scale {Value = 1});

            SetName($"ClientPlayer_{packet.playerId}");

            var isLocalPlayer = packet.networkConnectionId == ClientManager.Instance.ConnectionToServer.networkConnectionId;
            if (isLocalPlayer)
            {
                AddComponent<LocalPlayer>();
                AddComponent<IgnoreTransformCopyingFromServer>();
            }
        }

        protected override void OnPreBuild(EntityManagerWrapper wrapper)
        {
            base.OnPreBuild(wrapper);

            var playerPrefab = Resources.Load<GameObject>("Prefabs/PlayerAvatar");
            var playerGameObject = GameObjectEntityManager.Instance.CreateFromPrefab(playerPrefab);

            AddComponentData(new ManagedGameObject {instanceId = playerGameObject.GetInstanceID()});
        }
    }
}