using Grpc.Core;
using Grpc.Net.Client;
using System.Threading.Tasks;

namespace RyGamingProviderClientLibrary
{
    internal static class Connection
    {
        private const string Address = "localhost:5001";
        public static string _token { get; set; }

        public static GrpcChannel Channel => CreateAuthenticatedChannel($"https://{Address}");

        private static GrpcChannel CreateAuthenticatedChannel(string address)
        {
            var credentials = CallCredentials.FromInterceptor((context, metadata) =>
            {
                if (!string.IsNullOrEmpty(_token))
                {
                    metadata.Add("Authorization", $"Bearer {_token}");
                }
                return Task.CompletedTask;
            });

            // SslCredentials is used here because this channel is using TLS.
            // Channels that aren't using TLS should use ChannelCredentials.Insecure instead.
            var channel = GrpcChannel.ForAddress(address, new GrpcChannelOptions
            {
                Credentials = ChannelCredentials.Create(new SslCredentials(), credentials)
            });
            return channel;
        }
    }
}
