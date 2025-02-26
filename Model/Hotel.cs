

namespace dealSystem.Model
{
    public class Hotel
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required  double Rating { get; set; }

        public required string Description { get; set; }

        public int DealId { get; set; }

        public  Deal Deal { get; set; }
    }

}