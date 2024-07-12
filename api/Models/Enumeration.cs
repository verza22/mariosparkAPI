namespace api.Models
{
    public enum WidgetType
    {
        Kpi = 0,
        List = 1,
        Pie = 2,
        Column = 3
    }

    public enum WidgetInfoType
    {
        OrderTotal = 0,
        OrderCount = 1,
        HotelOrderTotal = 2,
        HotelOrderCount = 3,
        WaiterOrderTotal = 4,
        WaiterOrderCount = 5,
        CustomerCount = 6,
        ProductCount = 7,
        CategoryCount = 8,
        OtherCustomerCount = 9,
        OtherProductCount = 10
    }

    public enum DateType
    {
        Fixed = 0,
        Dynamic = 1
    }
}
