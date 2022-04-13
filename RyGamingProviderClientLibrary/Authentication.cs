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
        public Authentication()
        {
        }

        public LoginResponse Login(string userName, string password)
        {
            return LoginAsync(userName, password).Result;
        }

        public async Task<LoginResponse> LoginAsync(string userName, string password)
        {
            var client = new RyGamer.RyGamerClient(Connection.Channel);

            var request = new LoginRequest()
            {
                Name = userName,
                Password = password
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
