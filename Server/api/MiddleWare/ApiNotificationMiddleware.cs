using api.Controllers.Hubs;
using api.Interfaces.I_LKRepo;
using api.Mappers.LK_Mappers;
using api.Models;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Text.Json;
using static Azure.Core.HttpHeader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api.MiddleWare
{
    public class ApiNotificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHubContext<NotificationsHub, NotificationsHub.INotificationClient> _hubContext;
        //private readonly ILKACSoft_NotificationRepository _notiRepo;
        private readonly IServiceProvider _serviceProvider;

        public ApiNotificationMiddleware(RequestDelegate next, IHubContext<NotificationsHub, NotificationsHub.INotificationClient> hubContext,
            IServiceProvider serviceProvider)
            //ILKACSoft_NotificationRepository notiRepo)
        {
            _next = next;
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
            //_notiRepo = notiRepo;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //// 1. Capture the original response stream
            //var originalBodyStream = context.Response.Body;
            //using var memoryStream = new MemoryStream();
            //context.Response.Body = memoryStream;

            // 2. Let the request proceed
            await _next(context);

            // 3. Get the HTTP method and path
            var method = context.Request.Method.ToUpper();
            var path = context.Request.Path.Value?.ToLower();

            // 4. Only process for /api/tasks and PUT/POST
            if (path != null && (path.StartsWith("/api/tasks") || path.StartsWith("/api/tasks/UpdateTaskStatus")) && (method == "POST" || method == "PUT"))
            {

                // 6. Check if the operation was successful
                var isSuccess = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300;

                // 7. Notify only if success
                if (isSuccess)
                {
                    //// Read the response body as string
                    //memoryStream.Seek(0, SeekOrigin.Begin);
                    //string responseBody = await new StreamReader(memoryStream).ReadToEndAsync();

                    //// Try to extract TaskTitle from the response JSON
                    //string? taskTitle = null;
                    //try
                    //{
                    //    using var doc = JsonDocument.Parse(responseBody);
                    //    if (doc.RootElement.TryGetProperty("title", out var titleElement))
                    //    {
                    //        taskTitle = titleElement.GetString();
                    //    }
                    //}
                    //catch
                    //{
                    //    // Ignore JSON parsing errors, taskTitle will remain null
                    //}

                    //var adminConnectionIds = NotificationsHub._connectedClients
                    //    .Where(kvp => kvp.Value.Any(c => c.Type == "role" && c.Value == "NVGIAONHAN"))
                    //    .Select(kvp => kvp.Key)
                    //    .ToList();

                    //foreach (var connectionId in adminConnectionIds)
                    //{
                    //    await _hubContext.Clients.Client(connectionId)
                    //        .ReceiveNotification(
                    //            $"Tác vụ {(taskTitle != null ? $"{taskTitle}" : "[không xác định]")} {(method == "POST" ? "tạo" : "cập nhật")} thành công."
                    //        );
                    //}

                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var notiRepo = scope.ServiceProvider.GetRequiredService<ILKACSoft_NotificationRepository>();
                        // Use notiRepo as needed

                        string? firstConnectionId = NotificationsHub._connectedClients.FirstOrDefault().Key;

                        if (firstConnectionId == null)
                        {
                            // Handle the case where there are no connected clients
                            return;
                        }
                        string? nameId = null;

                        if (NotificationsHub._connectedClients.TryGetValue(firstConnectionId, out var claims))
                        {
                            nameId = claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
                        }

                        var notis = await notiRepo.GetByIdAsync(nameId);

                        if (notis != null)
                        {
                            await _hubContext.Clients.Client(firstConnectionId)
                                .ReceiveNotification(
                                    notis.ToLKACSoft_NotificationDto()
                                );
                        }
                    }
                }
            }

            //// 8. Copy the content back to the original stream
            //memoryStream.Seek(0, SeekOrigin.Begin);
            //await memoryStream.CopyToAsync(originalBodyStream);
            //context.Response.Body = originalBodyStream;
        }
    }
}
