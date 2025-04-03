namespace IvoCars.Models
{
    public class CarDeleteViewModel
    {
        public Guid? Id { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public int CarYear { get; set; }
        public string CarGearbox { get; set; }
        public string CarColor { get; set; }
        public int CarMileage { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

    }
}

