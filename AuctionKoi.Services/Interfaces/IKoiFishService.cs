using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Services.Interfaces
{
    public interface IKoiFishService
    {
        Task<List<KoiFish>> GetAllKoiFishAsync();
        Task<KoiFish> GetKoiFishByIdAsync(int id);
        Task AddKoiFishAsync(KoiFish koiFish);
        Task<bool> UpdateKoiFishAsync(KoiFish koiFish);
        Task<bool> DeleteKoiFishAsync(int id);
    }
}
