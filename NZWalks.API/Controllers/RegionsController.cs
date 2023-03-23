using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext _context;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this._context = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _context.Regions.ToListAsync();

            var regionsDto = new List<RegionDTO>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDTO()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _context.Regions.FindAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDTO()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var regionModel = new Region
            {
                Code = createRegionDto.Code,
                Name = createRegionDto.Name,
                RegionImageUrl = createRegionDto.RegionImageUrl,
            };

            await _context.Regions.AddAsync(regionModel);
            await _context.SaveChangesAsync();

            var regionDto = new RegionDTO
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] CreateRegionDto updateRegionDto)
        {
            var regionModel = await _context.Regions.FindAsync(id);

            if (regionModel == null)
            {
                return NotFound();
            }

            regionModel.Code = updateRegionDto.Code;
            regionModel.Name = updateRegionDto.Name;
            regionModel.RegionImageUrl = updateRegionDto.RegionImageUrl;

            await _context.SaveChangesAsync();

            var regionDto = new RegionDTO
            {
                Id = regionModel.Id,
                Code = regionModel.Code,
                Name = regionModel.Name,
                RegionImageUrl = regionModel.RegionImageUrl
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await _context.Regions.FindAsync(id);
            if (regionModel == null)
            {
                return NotFound();
            }
            _context.Regions.Remove(regionModel);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
