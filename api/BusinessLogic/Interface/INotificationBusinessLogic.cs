namespace api.BusinessLogic.Interface
{
    public interface INotificationBusinessLogic
    {
        string SendNotification(List<string> registrationTokens, string title, string body);
    }
}
