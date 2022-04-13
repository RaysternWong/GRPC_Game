using RyGaming;
using System.Threading.Tasks;

namespace RyGamingProviderClientLibrary
{
    public class BetResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public double BalanceAfter { get; set; }
        public double WinLossAmount { get; set; }
    }

    public class GameBet
    {
        public BetResponse SingleBet(double betAmount)
        {
            return SingleBetAsync(betAmount).Result;
        }

        public async Task<BetResponse> SingleBetAsync(double betAmount)
        {
            var client = new RyGamer.RyGamerClient(Connection.Channel);

            var request = new BetRequest()
            {
                BetAmount = betAmount
            };

            var response = await client.BetAsync(request);

            return new BetResponse()
            {
                Success = response.Success,
                Message = response.Message,
                BalanceAfter = response.BalanceAfter,
                WinLossAmount = response.WinLossAmount
            };
        }
    }
}
