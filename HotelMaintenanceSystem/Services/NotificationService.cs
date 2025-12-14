using Microsoft.AspNetCore.SignalR;
using HotelMaintenanceSystem.Hubs;

namespace HotelMaintenanceSystem.Services;

public class NotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public NotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendNotificationAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveNotification", message);
    }
}