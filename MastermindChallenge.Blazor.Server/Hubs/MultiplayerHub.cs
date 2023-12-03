using MastermindChallenge.Blazor.Server.Services.Multiplayer;
using Microsoft.AspNetCore.SignalR;

namespace MastermindChallenge.Blazor.Server.Hubs
{
    public class MultiplayerHub : Hub
    {
        private readonly IMultiplayerService _multiplayerService;

        public MultiplayerHub(IMultiplayerService multiplayerService)
        {
            _multiplayerService = multiplayerService;
        }

        public async Task JoinSession(string sessionId)
        {
            var sessions = await _multiplayerService.GetSessionsAsync();
            var session = sessions.FirstOrDefault(session => session.SessionId == sessionId);
            if(session != null)
            {
                if (session.PlayerCount < 2)
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);

                    await Clients.GroupExcept(sessionId, Context.ConnectionId).SendAsync("SessionJoined");

                    //session.PlayerCount++;

                    // Update the session in the database
                }
                else
                {
                    await Groups.AddToGroupAsync(Context.ConnectionId, sessionId);
                    // Create session with specific ID
                    await _multiplayerService.CreateSession();
                }
            }
        }
    }
}
