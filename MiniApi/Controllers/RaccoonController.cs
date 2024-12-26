using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniApi.Data;
using MiniApi.Entities;

namespace MiniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaccoonController : ControllerBase
    {
        private readonly DataContext _context;

        public RaccoonController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<Raccoon>> GetAllRaccons()
        {
            var raccons = await _context.Raccoons.ToListAsync();
            if(raccons.Count == 0)
            {
                return NotFound("There is no raccons in the database");
            }

            return Ok(raccons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Raccoon>> GetRaccoon(int id)
        {
            var raccoon = await _context.Raccoons.FindAsync(id);
            if (raccoon == null)
            {
                return NotFound("there is no raccoon with this id ");
            }

            return Ok(raccoon);
        }

        [HttpPost]
        public async Task<ActionResult<Raccoon>> AddRaccoon(Raccoon raccoon)
        {
            await _context.Raccoons.AddAsync(raccoon);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetRaccoon", new { id = raccoon.Id }, raccoon);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Raccoon>> UpdateRaccoon(int id, Raccoon raccoon)
        {
            var trueOwner = await _context.Raccoons.FindAsync(id);
            if (trueOwner == null)
            {
                return NotFound("There is no raccoon with this id");
            }
            trueOwner.Name = raccoon.Name;
            trueOwner.Age = raccoon.Age;
            await _context.SaveChangesAsync();
            return Ok(trueOwner);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Raccoon>> DeleteRaccoon(int id)
        {
            var raccoon = await _context.Raccoons.FindAsync(id);
            if (raccoon == null)
            {
                return NotFound("There is no raccoon with this id or already remove");
            }
            _context.Raccoons.Remove(raccoon);
            await _context.SaveChangesAsync();
            return Ok(raccoon);
        }

        [HttpPost]
        [Route("AddRaccoonToOwner")]
        public async Task<ActionResult<Raccoon>> AddRaccoonToOwner(int raccoonId, int ownerId)
        {
            var raccoon = await _context.Raccoons
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(r => r.Id == raccoonId);

            var owner = await _context.Owners.FindAsync(ownerId);

            if (raccoon == null)
            {
                return NotFound(new { message = "Raccoon not found", raccoonId });
            }

            if (owner == null)
            {
                return NotFound(new { message = "Owner not found", ownerId });
            }

            raccoon.OwnerId = owner.Id;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Raccoon successfully added to owner", raccoon });
        }

    }
}

