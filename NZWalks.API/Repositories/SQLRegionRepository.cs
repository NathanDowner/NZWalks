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

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var region = await this.GetRegionByIdAsync(id);

            if (region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var regions = await dbContext.Regions.ToListAsync();

            return regions;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            var region = await dbContext.Regions.FindAsync(id);
            
            return region;
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, UpdateRegionDto updateRegionDto)
        {
            var regionModel = await this.GetRegionByIdAsync(id);

            if (regionModel == null)
            {
                return null;
            }

            regionModel.Code = updateRegionDto.Code;
            regionModel.Name = updateRegionDto.Name;
            regionModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            await dbContext.SaveChangesAsync();

            return regionModel;

        }
    }
}

