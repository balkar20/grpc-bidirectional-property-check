// See https://aka.ms/new-console-template for more information
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcCorteserClient;

// The port number must match the port of the gRPC server.
using var channel = GrpcChannel.ForAddress("http://localhost:5000");
var client = new PropertyObserver.GreeterClient(channel);
var reply = await client.CheckPropertiesAsync(
                  new CheckPropertiesRequest 
                  { 
                    ProperyId = 1,
                    ProperyName = "Vlad",
                    ProperyType = "Vlad",
                    ProperyValue = "One"
                  });
Console.WriteLine("ProperyValue is : " + reply.ProperyValue);
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
