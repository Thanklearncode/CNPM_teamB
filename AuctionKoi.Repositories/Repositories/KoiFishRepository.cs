using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Repositories
{
    public class KoiFishRepository : IKoiFishRepository
    {
        private readonly AuctionKoiContext _dbContext;

        public KoiFishRepository(AuctionKoiContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<KoiFish>> GetAllKoiFishAsync()
        {
            return await _dbContext.KoiFishes.ToListAsync();
        }

        public async Task<KoiFish> GetKoiFishByIdAsync(int id)
        {
            return await _dbContext.KoiFishes.FindAsync(id);
        }

        public async Task AddKoiFishAsync(KoiFish koiFish)
        {
            Console.WriteLine($"Adding KoiFish: {koiFish.KoiName}, Breed: {koiFish.Breed}");
            await _dbContext.KoiFishes.AddAsync(koiFish);
            var result = await _dbContext.SaveChangesAsync();
            Console.WriteLine($"AddKoiFishAsync result: {result}");
        }


        public async Task<bool> UpdateKoiFishAsync(KoiFish koiFish)
        {
            Console.WriteLine($"Updating KoiFish: {koiFish.KoiID}");
            var existingKoiFish = await _dbContext.KoiFishes.FindAsync(koiFish.KoiID);

            if (existingKoiFish == null)
            {
                Console.WriteLine($"KoiFish with ID {koiFish.KoiID} not found.");
                return false;
            }

            // Cập nhật tất cả các trường
            existingKoiFish.KoiName = koiFish.KoiName;
            existingKoiFish.Breed = koiFish.Breed;
            existingKoiFish.Age = koiFish.Age;
            existingKoiFish.KoiSex = koiFish.KoiSex;
            existingKoiFish.Length = koiFish.Length;
            existingKoiFish.Description = koiFish.Description;
            existingKoiFish.ImageUrl = koiFish.ImageUrl;
            existingKoiFish.OwnerID = koiFish.OwnerID;

            Console.WriteLine("Before SaveChangesAsync...");
            var result = await _dbContext.SaveChangesAsync();
            Console.WriteLine($"SaveChangesAsync result: {result}");

            return result > 0;
        }




        public async Task<bool> DeleteKoiFishAsync(int id)
        {
            var koiFish = await _dbContext.KoiFishes.FindAsync(id);
            if (koiFish != null)
            {
                _dbContext.KoiFishes.Remove(koiFish);
                return await _dbContext.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
