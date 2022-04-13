using Grpc.Core;
using Grpc.Net.Client;
using RyGaming;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace GrpcGreeterClient
{
    internal class Program
    {
        // The port number(5001) must match the port of the gRPC server.
        private const string Address = "localhost:5001";

        private static string _token;

        private static async Task Main(string[] args)
        {
            var channel = CreateAuthenticatedChannel($"https://{Address}");
            var client = new RyGamer.RyGamerClient(channel);

            Console.WriteLine("gRPC Ticketer");
            Console.WriteLine();
            Console.WriteLine("Press a key:");
            Console.WriteLine("1: Login");
            Console.WriteLine("2: TopUp");
            Console.WriteLine("3: Withdraw");
            Console.WriteLine("4: Bet");
            Console.WriteLine("5: Exit");
            Console.WriteLine();

            var exiting = false;
            while (!exiting)
            {
                var consoleKeyInfo = Console.ReadKey(intercept: true);
                switch (consoleKeyInfo.KeyChar)
                {
                    case '1':
                        await Login(client);
                        break;

                    case '2':
                        await TopUp(client);
                        break;

                    case '3':
                        await Withdraw(client);
                        break;

                    case '4':
                        await Bet(client);
                        break;

                    case '5':
                        exiting = true;
                        break;
                }
            }

            Console.WriteLine("Exiting");
        }

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

        private static async Task<string> Authenticate()
        {
            Console.WriteLine($"Authenticating as {Environment.UserName}...");
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri($"https://{Address}/generateJwtToken?name={HttpUtility.UrlEncode(Environment.UserName)}"),
                Method = HttpMethod.Get,
                Version = new Version(2, 0)
            };
            var tokenResponse = await httpClient.SendAsync(request);
            tokenResponse.EnsureSuccessStatusCode();

            var token = await tokenResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Successfully authenticated.");
            return token;
        }

        private static async Task Login(RyGamer.RyGamerClient client)
        {
            // _token = await Authenticate();

            var request = new LoginRequest()
            {
                Name = "Win10",
                Password = "123"
            };

            var response = await client.LoginAsync(request);

            if (response.Success)
            {
                _token = response.Token;
            }

            Console.WriteLine("Login Message: " + response.Message);
        }

        private static async Task Withdraw(RyGamer.RyGamerClient client)
        {
            var request = new WalletWithdrawRequest()
            {
                WithdrawAmount = 30
            };

            var response = await client.WithdrawAsync(request);

            Console.WriteLine($"Withdraw Message: {response.Message} , Balance After : {response.BalanceAfter} ");
        }

        private static async Task TopUp(RyGamer.RyGamerClient client)
        {
            var request = new WalletTopUpRequest()
            {
                TopUpAmount = 100
            };

            var response = await client.TopUpAsync(request);

            Console.WriteLine($"TopUp Message: {response.Message} , Balance After : {response.BalanceAfter} ");
        }

        private static async Task Bet(RyGamer.RyGamerClient client)
        {
            var request = new BetRequest()
            {
                BetAmount = 50
            };

            var response = await client.BetAsync(request);

            Console.WriteLine($"Bet Message: {response.Message} , Balance After : {response.BalanceAfter} ");
        }
    }
}
