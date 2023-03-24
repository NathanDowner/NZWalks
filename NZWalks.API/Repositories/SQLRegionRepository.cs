using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateRegionAsync(CreateRegionDto regionDto)
        {
            var region = new Region
            {
                Code = regionDto.Code,
                Name = regionDto.Name,
                RegionImageUrl = regionDto.RegionImageUrl,
            };

            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();

            return region;

        }

        public Task<Region> DeleteRegionAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var regions = await dbContext.Regions.ToListAsync();

            return regions;
        }

        public Task<Region> GetRegionByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> UpdateRegionAsync(Guid id, UpdateRegionDto regionDto)
        {
            throw new NotImplementedException();
        }
    }
}

