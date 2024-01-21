using Grpc.Core;
using Google.Protobuf.WellKnownTypes;


namespace GrpcServiceDemo.Services
{
    public sealed class PropertyDataGrpcService : GrpcServiceDemo.PropertyDataService.PropertyDataServiceBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Focus", "Diafragm", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PropertyDataService> _logger;
        private readonly IPropertyDataService _propertyDataService;

        public PropertyDataGrpcService(ILogger<PropertyDataService> logger, IPropertyDataService propertyDataService)
        {
            _logger = logger;
            _propertyDataService = propertyDataService;
        }

        public override Task<PropertyDataReply> GetPropertyData(Empty request, ServerCallContext context)
        {
            return _propertyDataService.GetPropertyData(context);
        }

        public override Task<PropertyDataReply> GetPropertyDataForDate(Timestamp date, ServerCallContext context)
        {
            return _propertyDataService.GetPropertyDataForDate(date, context);
        }

        public override async Task GetPropertyDataStream(Empty request, IServerStreamWriter<PropertyData> responseStream, ServerCallContext context)
        {
            var i = 0;
            while(!context.CancellationToken.IsCancellationRequested && i <50)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(_propertyDataService.GetPropertyData(i));
                i++;
            }
        }

        public override async Task<PropertyDataReply> GetPropertyDataClientStream(IAsyncStreamReader<StreamMessage> requestStream, ServerCallContext context)
        {
            var response = new PropertyDataReply();
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                var i = requestStream.Current.Index;
                response.Result.Add(_propertyDataService.GetPropertyData(i));
            }
            return await Task.FromResult<PropertyDataReply>(response);
        }

        public override async Task GetPropertyDataDuplexStream(IAsyncStreamReader<StreamMessage> requestStream, IServerStreamWriter<PropertyData> responseStream, ServerCallContext context)
        {
            while (await requestStream.MoveNext() && !context.CancellationToken.IsCancellationRequested)
            {
                var i = requestStream.Current.Index;
                await Task.Delay(1000);
                await responseStream.WriteAsync(_propertyDataService.GetPropertyData(i));
            }
        }
    }
}
