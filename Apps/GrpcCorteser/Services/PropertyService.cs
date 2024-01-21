using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using PropertyObserver;

namespace GrpcCorteser.Services;
    public class PropertyService : IPropertyService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    
        private static readonly string[] PropertyTypes = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    
        private static readonly string[] PropertyNames = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        public Task<CheckPropertiesReply> CheckPropertiesForType(string type, ServerCallContext context)
        {
            return Task.FromResult<CheckPropertiesReply>(GetProperties(type));
        }

        public Task<CheckPropertiesReply> GetProperties(ServerCallContext context)
        {
            return Task.FromResult<CheckPropertiesReply>(GetProperties());
        }

        public PropertyData GetPropertyData(int index)
        {
            return GetProperty(1);
        }

        private CheckPropertiesReply GetProperties()
        {
            var result = new CheckPropertiesReply();
            
            return result;
        }

        private CheckPropertiesReply GetProperties(string propertyType)
        {
            var result = new CheckPropertiesReply();
            result.Result.Add(
                new CheckPropertiesReply
                {
                    Date = date,
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)],
                    TemperatureF = (int)(32 + (Random.Shared.Next(-20, 55) / 0.5556))
                });
            return result;
        }

        private static PropertyData GetProperty(int index)
        {
            return new PropertyData
            {
                ProperyId =  Random.Shared.Next(1, 1000),
                ProperyName = PropertyNames[Random.Shared.Next(Summaries.Length)],
                ProperyValue = Summaries[Random.Shared.Next(Summaries.Length)],
                ProperyType = PropertyTypes[Random.Shared.Next(PropertyTypes.Length)]
            };
        }

    }