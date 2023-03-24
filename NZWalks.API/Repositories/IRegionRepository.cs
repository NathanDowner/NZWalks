using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(CreateRegionDto regionDto);
        Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto regionDto);
        Task<Region> DeleteRegionAsync(Guid id);
        Task<List<Region>> GetAllRegionsAsync();
    }
}
