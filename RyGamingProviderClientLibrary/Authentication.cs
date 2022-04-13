using Grpc.Net.Client;
using RyGaming;
using System.Threading.Tasks;

namespace RyGamingProviderClientLibrary
{
    public class LoginResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
    }

    public class Authentication
    {
        private GrpcChannel _grpcChannel;

        public Authentication()
        {
            _grpcChannel = Connection.Channel;
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password)
        {
            var client = new RyGamer.RyGamerClient(_grpcChannel);

            var request = new LoginRequest()
            {
                Name = "Win10",
                Password = "123"
            };

            var response = await client.LoginAsync(request);

            if (response.Success)
            {
                Connection._token = response.Token;
            }

            return new LoginResponse()
            {
                Success = response.Success,
                Message = response.Message,
                Token = response.Token
            };
        }
    }
}
