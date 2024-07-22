using api.Models;
using System.Data;

namespace api.BusinessLogic.Interface
{
    public interface IWidgetBusinessLogic
    {
        List<Widget> GetWidgets(int userID);
        bool RemoveWidget(int widgetId);
        int AddOrUpdateWidget(Widget widget);
        int GetWidgetData(int widgetID, int storeID);
        DataTable GetWidgetDataList(int widgetID, int storeID);
        void UpdateWidgetPositions(int userID, string widgetIDs);
    }
}
