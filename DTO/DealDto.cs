
namespace dealSystem.Model;

public class DealDto  
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public required string Title { get; set; }
    public IEnumerable<HotelDto> Hotels { get; set; } = new List<HotelDto>();
}