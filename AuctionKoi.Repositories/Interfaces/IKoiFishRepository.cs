using AuctionKoi.Repositories.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuctionKoi.Repositories.Interfaces
{
    public interface IKoiFishRepository
    {
        Task<List<KoiFish>> GetAllKoiFishAsync();
        Task<KoiFish> GetKoiFishByIdAsync(int id);
        Task AddKoiFishAsync(KoiFish koiFish);
        Task<bool> UpdateKoiFishAsync(KoiFish koiFish);
        Task<bool> DeleteKoiFishAsync(int id);
    }
}
