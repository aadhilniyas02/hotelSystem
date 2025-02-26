using dealSystem.Data;
using dealSystem.Model;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace dealSystem.services
{

    public interface InterfaceService
    {
        Task<List<Deal>> GetDealsAsync(); 
        Task<Deal> FindDealByIdAsync(int id); 
        Task<Deal> AddDealAsync(DealDto dealToCreate); 
        Task<Deal> UpdateDealAsync(int id, DealDto dealToUpdate); 
        Task<bool> DeleteDealAsync(int id); 
        
    }

}