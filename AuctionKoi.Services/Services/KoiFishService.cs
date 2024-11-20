using AuctionKoi.Repositories.Entities;
using AuctionKoi.Repositories.Interfaces;
using AuctionKoi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Services
{
    public class KoiFishService : IKoiFishService
    {
        private readonly IKoiFishRepository _repository;

        public KoiFishService(IKoiFishRepository repository)
        {
            _repository = repository;
        }

        public Task<List<KoiFish>> GetAllKoiFishAsync()
        {
            return _repository.GetAllKoiFishAsync();
        }

        public Task<KoiFish> GetKoiFishByIdAsync(int id)
        {
            return _repository.GetKoiFishByIdAsync(id);
        }

        public async Task AddKoiFishAsync(KoiFish koiFish)
        {
            if (koiFish == null)
            {
                Console.WriteLine("KoiFish object is null.");
                throw new ArgumentNullException(nameof(koiFish));
            }

            if (koiFish.Age < 0)
            {
                Console.WriteLine("Invalid Age: Age cannot be negative.");
                throw new ArgumentException("Age cannot be negative.");
            }

            Console.WriteLine($"Adding KoiFish to repository: {koiFish.KoiName}");
            await _repository.AddKoiFishAsync(koiFish);
        }


        public async Task<bool> UpdateKoiFishAsync(KoiFish koiFish)
        {
            if (koiFish == null)
            {
                Console.WriteLine("KoiFish object is null.");
                return false;
            }

            if (koiFish.Age < 0)
            {
                Console.WriteLine("Invalid Age: Age cannot be negative.");
                return false;
            }

            Console.WriteLine($"Updating KoiFish in Repository with ID: {koiFish.KoiID}");
            return await _repository.UpdateKoiFishAsync(koiFish);
        }




        public Task<bool> DeleteKoiFishAsync(int id)
        {
            return _repository.DeleteKoiFishAsync(id);
        }
    }
}
