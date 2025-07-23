namespace Asset_Manager.Models  
{
    public class ActivityViewModel
    {
        public string Action { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public string AssetName { get; set; }
        public string StatusColor { get; set; }
        public string User { get; set; }
    }
}
