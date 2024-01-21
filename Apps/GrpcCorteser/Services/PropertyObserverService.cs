using Grpc.Core;
using GrpcCorteser;
using PropertyObserver;
using Google.Protobuf.WellKnownTypes;

namespace GrpcCorteser.Services;

public class PropertyObserverService : PropertyObserver.PropertyObserver.PropertyObserverBase
{
    private readonly ILogger<PropertyObserverService> _logger;
    private readonly IPropertyService _propertiesService;

    public PropertyObserverService(ILogger<PropertyObserverService> logger)
    {
        _logger = logger;
    }

    public override async Task<CheckPropertiesReply> CheckProperties(CheckPropertiesRequest request, ServerCallContext context)
    {
        // Mock implementation
        var reply = new CheckPropertiesReply
        {
            ProperyValue = "Mock CheckProperties method called"
        };
        return await Task.FromResult(reply);
    }

    public override async Task<CheckPropertiesReply> GetPropertiesForType(StringValue request, ServerCallContext context)
    {
        // Mock implementation
        var reply = new CheckPropertiesReply
        {
           ProperyValue = "Mock CheckProperties method called"
        };
        return await Task.FromResult(reply);
    }

    public override async Task GetPropertiesStream(Empty request, IServerStreamWriter<PropertyData> responseStream, ServerCallContext context)
    {
        // Mock implementation
        var i = 0;
            while(!context.CancellationToken.IsCancellationRequested && i <50)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(_propertiesService.GetPropertyData(i));
                i++;
            }
    }

    public override async Task<CheckPropertiesReply> GetPropertiesClientStream(IAsyncStreamReader<StreamMessage> requestStream, ServerCallContext context)
    {
        // Mock implementation
        var reply = new CheckPropertiesReply
        {
            ProperyType = "Mock CheckProperties method called"
        };
        await foreach (var message in requestStream.ReadAllAsync())
        {
            // Process the incoming messages
        };
        return reply;
    }

    public override async Task GetPropertiesDuplexStream(IAsyncStreamReader<StreamMessage> requestStream, IServerStreamWriter<PropertyData> responseStream, ServerCallContext context)
    {
        // Mock implementation
        await foreach (var message in requestStream.ReadAllAsync())
        {
            // Process the incoming messages
        }
    }

}
