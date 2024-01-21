using Grpc.Core;
using GrpcServiceDemo;
using Google.Protobuf.WellKnownTypes;

namespace GrpcServiceDemo.Services
{
    public class PropertyDataService : IPropertyDataService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<PropertyDataService> _logger;

        public PropertyDataService(ILogger<PropertyDataService> logger)
        {
            _logger = logger;
        }

        public Task<PropertyDataReply> GetPropertyData(ServerCallContext context)
        {
            return Task.FromResult<PropertyDataReply>(GetPropertyData());
        }

        public Task<PropertyDataReply> GetPropertyDataForDate(Timestamp date, ServerCallContext context)
        {
            return Task.FromResult<PropertyDataReply>(GetWeather(date));
        }

        public PropertyData GetPropertyData(int index)
        {
            return PropertyDataService.GetStaticPropertyData(index);
        }

        private PropertyDataReply GetPropertyData()
        {
            var result = new PropertyDataReply();
            for (var index = 1; index <= 5; index++)
            {
                result.Result.Add(
                        PropertyDataService.GetStaticPropertyData(index)
                    );
            }
            return result;
        }

        private static PropertyData GetStaticPropertyData(int index)
        {
            return new PropertyData
            {
                Date = Timestamp.FromDateTime(DateTime.UtcNow.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                TemperatureF = (int)(32 + (Random.Shared.Next(-20, 55) / 0.5556))
            };
        }

        private PropertyDataReply GetWeather(Timestamp date)
        {
            var result = new PropertyDataReply();
            result.Result.Add(
                new PropertyData
                {
                    Date = date,
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    TemperatureF = (int)(32 + (Random.Shared.Next(-20, 55) / 0.5556))
                }
                );
            return result;
        }
    }
}
