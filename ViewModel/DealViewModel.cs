using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;

namespace dealSystem.ViewModel
{

    public class DealViewModel
    {
        public required string Name { get; set; }
        public required string Slug { get; set; }
        public required string Title { get; set;}

        public required List<Hotel> Hotels { get; set; }
    }
}