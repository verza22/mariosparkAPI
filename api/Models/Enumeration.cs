namespace api.Models
{
    public enum WidgetType
    {
        Kpi = 1,
        List = 2,
        Pie = 3,
        Column = 4
    }

    public enum WidgetInfoType
    {
        OrderTotal = 1,
        OrderCount = 2,
        HotelOrderTotal = 3,
        HotelOrderCount = 4,
        WaiterOrderTotal = 5,
        WaiterOrderCount = 6,
        CustomerCount = 7,
        ProductCount = 8,
        CategoryCount = 9,
        OtherCustomerCount = 10,
        OtherProductCount = 11
    }

    public enum DateType
    {
        Fixed = 0,
        Dynamic = 1
    }
}
