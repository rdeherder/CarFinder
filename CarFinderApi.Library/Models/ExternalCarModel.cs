namespace CarFinderApi.Library.Models
{
    public class ExternalCarModel
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int HP { get; set; }
        public double Price { get; set; }
        public string Img_Url { get; set; }
    }
}
