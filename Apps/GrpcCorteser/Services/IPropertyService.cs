using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using PropertyObserver;

namespace GrpcCorteser.Services
{
    public interface IPropertyService
    {
        Task<CheckPropertiesReply> GetProperties(ServerCallContext context);

        Task<CheckPropertiesReply> CheckPropertiesForType(string type, ServerCallContext context);

         PropertyData GetPropertyData(int index);
    }
}