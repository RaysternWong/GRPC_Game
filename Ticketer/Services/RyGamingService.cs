using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using RyGaming;
using System.Threading.Tasks;

namespace RyGamingProvider.Services
{
    public class RyGamingService : RyGaming.RyGamer.RyGamerBase
    {
        private readonly PlayerRepository _ticketRepository;

        public RyGamingService(PlayerRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public override Task<AvailableTicketsResponse> GetAvailableTickets(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new AvailableTicketsResponse
            {
                Count = _ticketRepository.GetAvailableTickets()
            });
        }

        [Authorize]
        public override Task<BuyTicketsResponse> BuyTickets(BuyTicketsRequest request, ServerCallContext context)
        {
            var user = context.GetHttpContext().User;

            return Task.FromResult(new BuyTicketsResponse
            {
                Success = _ticketRepository.BuyTickets(user.Identity.Name!, request.Count)
            });
        }
    }
}
