using api.Models;

namespace api.BusinessLogic.Interface
{
    public interface IWidgetBusinessLogic
    {
        List<Widget> GetWidgets(int userID);
        bool RemoveWidget(int widgetId);
        int AddOrUpdateWidget(Widget widget);
    }
}
