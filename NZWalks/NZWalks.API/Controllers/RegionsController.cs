using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

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
        public IActionResult GetAll()
        {
            var regions = _context.Regions.ToList();

            return Ok(regions);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var region = _context.Regions.Find(id);

            if (region == null)
            {
                return NotFound();
            }

            return Ok(region);
        }
    }
}
