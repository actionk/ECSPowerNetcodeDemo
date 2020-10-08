//THIS FILE IS AUTOGENERATED BY GHOSTCOMPILER. DON'T MODIFY OR ALTER.
using AOT;
using Unity.Burst;
using Unity.Networking.Transport;
using Unity.Entities;
using Unity.Collections;
using Unity.NetCode;
using Unity.Transforms;
using Unity.Mathematics;
using Protocol.Players.Packets;


namespace Protocol.Generated
{
    [BurstCompile]
    public struct ProtocolPlayersPacketsServerPlayerLoginResponsePacketSerializer : IComponentData, IRpcCommandSerializer<Protocol.Players.Packets.ServerPlayerLoginResponsePacket>
    {
        public void Serialize(ref DataStreamWriter writer, in Protocol.Players.Packets.ServerPlayerLoginResponsePacket data)
        {
            writer.WriteUInt(data.playerId);
            writer.WriteUInt(data.networkEntityId);
        }

        public void Deserialize(ref DataStreamReader reader, ref Protocol.Players.Packets.ServerPlayerLoginResponsePacket data)
        {
            data.playerId = (uint) reader.ReadUInt();
            data.networkEntityId = (uint) reader.ReadUInt();
        }
        [BurstCompile]
        [MonoPInvokeCallback(typeof(RpcExecutor.ExecuteDelegate))]
        private static void InvokeExecute(ref RpcExecutor.Parameters parameters)
        {
            RpcExecutor.ExecuteCreateRequestComponent<ProtocolPlayersPacketsServerPlayerLoginResponsePacketSerializer, Protocol.Players.Packets.ServerPlayerLoginResponsePacket>(ref parameters);
        }

        static PortableFunctionPointer<RpcExecutor.ExecuteDelegate> InvokeExecuteFunctionPointer =
            new PortableFunctionPointer<RpcExecutor.ExecuteDelegate>(InvokeExecute);
        public PortableFunctionPointer<RpcExecutor.ExecuteDelegate> CompileExecute()
        {
            return InvokeExecuteFunctionPointer;
        }
    }
    class ProtocolPlayersPacketsServerPlayerLoginResponsePacketRpcCommandRequestSystem : RpcCommandRequestSystem<ProtocolPlayersPacketsServerPlayerLoginResponsePacketSerializer, Protocol.Players.Packets.ServerPlayerLoginResponsePacket>
    {
        [BurstCompile]
        protected struct SendRpc : IJobEntityBatch
        {
            public SendRpcData data;
            public void Execute(ArchetypeChunk chunk, int orderIndex)
            {
                data.Execute(chunk, orderIndex);
            }
        }
        protected override void OnUpdate()
        {
            var sendJob = new SendRpc{data = InitJobData()};
            ScheduleJobData(sendJob);
        }
    }
}