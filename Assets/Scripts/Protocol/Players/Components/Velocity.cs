using Plugins.ECSPowerNetcode.Features.Synchronization.Generic;
using Plugins.ECSPowerNetcode.Shared.Systems;
using Protocol.Players.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Networking.Transport;

[assembly: RegisterGenericComponentType(typeof(CopyEntityComponentRpcCommand<Velocity, VelocityConverter>))]

namespace Protocol.Players.Components
{
    public struct Velocity : IComponentData
    {
        public int2 value;
    }

    public struct VelocityConverter : ISyncEntityConverter<Velocity>
    {
        public Velocity velocity;
        public Velocity Value => velocity;

        public void Convert(Velocity value)
        {
            velocity = value;
        }

        public void Serialize(ref DataStreamWriter writer)
        {
            writer.WriteInt(velocity.value.x);
            writer.WriteInt(velocity.value.y);
        }

        public void Deserialize(ref DataStreamReader reader)
        {
            velocity.value = new int2(
                reader.ReadInt(),
                reader.ReadInt()
            );
        }
    }

    public class VelocityRpcCommandSender : RpcCommandSendSystem<
        CopyEntityComponentRpcCommand<Velocity, VelocityConverter>,
        CopyEntityComponentRpcCommand<Velocity, VelocityConverter>>
    {
    }
}