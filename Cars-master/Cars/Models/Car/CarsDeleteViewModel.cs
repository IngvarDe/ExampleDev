namespace Cars.Models.Car
{
    public class CarsDeleteViewModel
    {
        public Guid Id { get; set; }
        public string CarMake { get; set; }

        public int Year { get; set; }

        public string CarColor { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Modifieted { get; set; }
    }
}
