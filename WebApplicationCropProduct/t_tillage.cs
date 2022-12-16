namespace WebApplicationCropProduct
{
    public class t_tillage
    {
        public int id { get; set; }
        public int field { get; set; }
        public int tillage_operation { get; set; }
        public int machine { get; set; }
        public string date_start { get; set; }
        public string date_end { get; set; }
    }
}
