namespace Ezzygate.Infrastructure.Notifications;

public interface INotificationClient
{
    Task<NotificationResult> SendNotificationAsync(NotificationData data);
}