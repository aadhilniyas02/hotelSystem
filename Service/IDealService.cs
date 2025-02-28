using dealSystem.Data;
using dealSystem.Model;
using System.Linq;
using dealSystem.ViewModel;
using Microsoft.EntityFrameworkCore;
using dealSystem.services;
using Npgsql.EntityFrameworkCore.PostgreSQL.Query.ExpressionTranslators.Internal;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http.HttpResults;



namespace dealSystem.services
{
    public class IDealService: InterfaceService
    {
        private readonly DealContext _context;
   
        public int Id { get; private set; }

        public IDealService(DealContext context)
        {
            _context = context;
        }

         public async Task<List<Deal>> GetDealsAsync()
        {
            try
            {
                var deals = await _context.DealTable
                        .Include(d => d.Hotels)
                        .ToListAsync();
                     return  deals;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            } 
        }
        

        public async Task<Deal> FindDealByIdAsync(int id)
        {
            try
            {
                var deals = await _context.DealTable
                        .Include(d => d.Hotels)
                        .FirstOrDefaultAsync(d => d.Id == id); 
                return deals;
            }
            catch (Exception e )
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }


        public async Task<Deal> AddDealAsync(DealDto dealToCreate)
        {
        
            try
                {
                    var deal = new Deal
                    {
                        Name = dealToCreate.Name,
                        Slug = dealToCreate.Slug,
                        Title = dealToCreate.Title,
                        Hotels = new List<Hotel>()
                    };

                            if (dealToCreate.Hotels != null && dealToCreate.Hotels.Any())
                            {

                            List<Hotel> newOne = new List<Hotel>();

                                    foreach (var hotelDto in dealToCreate.Hotels)
                                    {
                                        var hotelEntity = new Hotel
                                        {
                                            Name = hotelDto.Name,
                                            Rating = hotelDto.Rating,
                                            Description = hotelDto.Description,
                                        };

                                            newOne.Add(hotelEntity);
                                    }

                                    deal.Hotels = newOne;
                    
                            }
                        
                    _context.DealTable.Add(deal);
                    await _context.SaveChangesAsync();

                    return  deal;

                }

                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
            }
        }


        public async Task<Deal> UpdateDealAsync(int id, DealDto dealToUpdate)
        {
            try
            {
                var deal = await _context.DealTable
                        .Include(d => d.Hotels)
                        .FirstOrDefaultAsync(d => d.Id == id);

             if(deal == null)
                 throw new KeyNotFoundException("Deal not found");
            
             deal.Name = dealToUpdate.Name;
             deal.Slug = dealToUpdate.Slug;
             deal.Title = dealToUpdate.Title;

                if(dealToUpdate.Hotels != null && dealToUpdate.Hotels.Any())
                {
                    var newHotels = new List<Hotel>();

                    foreach (var hotelDto in dealToUpdate.Hotels)
                    {
                        var hotel = deal.Hotels.FirstOrDefault();
                        
                        if(hotel == null)
                        {
                            hotel = new Hotel
                            {
                                Name = hotelDto.Name,
                                Rating = hotelDto.Rating,
                                Description = hotelDto.Description,
                                DealId = deal.Id
                            };
                            _context.HotelTable.Add(hotel);
                        }
                        else
                        {
                            hotel.Name = hotelDto.Name;
                            hotel.Rating = hotelDto.Rating;
                            hotel.Description = hotelDto.Description;
                        }
                        newHotels.Add(hotel);
                    }
                    deal.Hotels = newHotels;
                }

            await _context.SaveChangesAsync();
            return deal; 
            }
            
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
        }
        

    
        public async Task<bool> DeleteDealAsync(int id)
        {
            try
            {
                var deal = await _context.DealTable.Include(d => d.Hotels).FirstOrDefaultAsync(d => d.Id ==id);
                    if (deal == null) return false;

                _context.DealTable.Remove(deal);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
          
        }

        public async Task<bool> DeleteHotelAsync(int hotelId)
        {
            var hotel = await _context.HotelTable.FindAsync(hotelId);
            if(hotel == null) return false;

            _context.HotelTable.Remove(hotel);
            await _context.SaveChangesAsync();
            return true;
        }

     
    }

}