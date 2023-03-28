using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionRepository.GetAllRegionsAsync();

            var regionsDto = mapper.Map<List<RegionDTO>>(regions);

            return Ok(regionsDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionRepository.GetRegionByIdAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDTO>(region);

            return Ok(regionDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var regionModel = await regionRepository.CreateRegionAsync(createRegionDto);

            var regionDto = mapper.Map<RegionDTO>(regionModel);

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var regionModel = await regionRepository.UpdateRegionAsync(id, updateRegionDto);

            if (regionModel == null)
            {
                return NotFound();
            }

            var regionDto = mapper.Map<RegionDTO>(regionModel);

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionModel = await regionRepository.DeleteRegionAsync(id);
            if (regionModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
