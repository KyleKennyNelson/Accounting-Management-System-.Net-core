using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using LKACSoftModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static api.Controllers.Hubs.NotificationsHub;
using static Azure.Core.HttpHeader;

namespace api.Controllers.Hubs
{
   // [Authorize]
    public class NotificationsHub : Hub<INotificationClient>
    {
        public static ConcurrentDictionary<string, List<Claim>> _connectedClients = new ConcurrentDictionary<string, List<Claim>>();
        private readonly ILKACSoft_NotificationRepository _notiRepo;
        private readonly IHubContext<NotificationsHub, NotificationsHub.INotificationClient> _hubContext;

        NotificationsHub(ILKACSoft_NotificationRepository notiRepo, IHubContext<NotificationsHub, NotificationsHub.INotificationClient> hubContext)
        {
            _notiRepo = notiRepo;
            _hubContext = hubContext;
        }

        [Authorize]
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            string? token = null;

            if (httpContext != null)
            {
                // Try to get the token from the Authorization header
                var authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    token = authHeader.Substring("Bearer ".Length).Trim();
                }
                // Or from the query string (for WebSocket connections)
                else if (httpContext.Request.Query.TryGetValue("access_token", out var accessToken))
                {
                    token = accessToken;
                }
            }

            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var claims = jwtToken.Claims.ToList();

                // Now you can use the claims as needed
                var userId = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                var roles = claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                var connectionId = Context.ConnectionId;
                // Check if the user is already connected
                if (claims != null && claims.Any())
                {
                    _connectedClients.TryAdd(connectionId, claims);
                }
                await Clients.Client(connectionId).ReceiveNotification(
                    $"Bạn đã kết nối đến trang thông báo {userId}");

                var notis = await _notiRepo.GetAllAsync(userId);

                if (notis == null || notis.Count == 0)
                {
                    await _hubContext.Clients.Client(connectionId)
                        .ReceiveNotification(
                            $"Không có thông báo nào cho bạn."
                        );
                }
                else
                {
                    await _hubContext.Clients.Client(connectionId)
                        .ReceiveNotification(
                            notis.Select(n => n.ToLKACSoft_NotificationDto())
                        );
                }

                await base.OnConnectedAsync();
            }
        }

        //public override Task OnDisconnectedAsync(Exception? exception)
        //{
        //    var connectionId = Context.ConnectionId;
        //    ConnectedClients.TryRemove(connectionId, out _);
        //    return base.OnDisconnectedAsync(exception);
        //}

        //public Dictionary<string, List<Claim>> GetConnectedClients()
        //{
        //    return ConnectedClients.ToDictionary(x => x.Key, x => x.Value);
        //}

        public interface INotificationClient
        {
            Task ReceiveNotification(string message);
            Task ReceiveNotification(object notification);
        }
    }
}
