
namespace dealSystem.Model
{
    public class Deal
    {
        public int Id { get; set; }

        public required string Name { get; set;}

        public required string Slug { get; set; }

        public required string Title { get; set; }

        public IEnumerable<Hotel> Hotels { get; set;} = new List<Hotel>();
    }
}