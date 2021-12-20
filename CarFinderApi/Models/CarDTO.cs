namespace CarFinderApi.Models
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public double Price { get; set; }
        public string Img_Url { get; set; }
    }
}
