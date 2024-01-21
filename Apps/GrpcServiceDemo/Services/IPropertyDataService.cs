using Google.Protobuf.WellKnownTypes;
using Grpc.Core;


namespace GrpcServiceDemo.Services
{
    public interface IPropertyDataService
    {
        Task<PropertyDataReply> GetPropertyData(ServerCallContext context);

        Task<PropertyDataReply> GetPropertyDataForDate(Timestamp date, ServerCallContext context);

         PropertyData GetPropertyData(int index);
    }
}
