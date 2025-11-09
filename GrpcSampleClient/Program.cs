using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcServer;  // Adjust the namespace accordingly from your .proto file

class Program
{
    static async Task Main(string[] args)
    {

        var httpHandler = new HttpClientHandler();
        httpHandler.ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

        using var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions
        {
            HttpHandler = httpHandler
        });

        // Create the gRPC client from the generated code
        var client = new Greeter.GreeterClient(channel);

        // Call the SayHello RPC method on the server and await the response
        var reply = await client.SayHelloAsync(new HelloRequest { Name = "User" });

        // Print the server reply message to console
        Console.WriteLine("Reply from server: " + reply.Message);
        Console.WriteLine("Press any key to exit...");
        Console.ReadLine();

    }
}
