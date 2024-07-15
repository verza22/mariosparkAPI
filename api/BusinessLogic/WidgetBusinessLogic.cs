using api.BusinessLogic.Interface;
using api.DataAccess;
using api.Models;
using System.Data;

namespace api.BusinessLogic
{
    public class WidgetBusinessLogic : IWidgetBusinessLogic
    {
        private readonly WidgetDataAccess _widgetDataAccess;

        public WidgetBusinessLogic(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _widgetDataAccess = new WidgetDataAccess(connectionString);
        }

        public List<Widget> GetWidgets(int userID)
        {
            return _widgetDataAccess.GetWidgets(userID);
        }

        public bool RemoveWidget(int widgetId)
        {
            return _widgetDataAccess.RemoveWidget(widgetId);
        }

        public int AddOrUpdateWidget(Widget widget)
        {
            return _widgetDataAccess.AddOrUpdateWidget(widget);
        }

        public int GetWidgetData(int widgetID, int storeID)
        {
            return _widgetDataAccess.GetWidgetData(widgetID, storeID);
        }

        public DataTable GetWidgetDataList(int widgetID, int storeID)
        {
            return _widgetDataAccess.GetWidgetDataList(widgetID, storeID);
        }
    }
}
