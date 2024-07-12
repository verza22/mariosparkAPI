namespace api.Models
{
    public class Widget
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public bool IsLeading { get; set; }
        public WidgetInfoType InfoType { get; set; }
        public WidgetType Type { get; set; }

        //dates
        public string DateFrom { get; set; }
        public DateType DateFromType { get; set; }
        public string DateTo { get; set; }
        public DateType DateToType { get; set; }

        //general config
        public int Position { get; set; }
        public int SizeX { get; set; }
        public int SizeY { get; set; }
        public string BgColor { get; set; }
    }
}
