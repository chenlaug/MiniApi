using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniApi.Data;
using MiniApi.Entities;

namespace MiniApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OwnerController : ControllerBase
    {
        private readonly DataContext _context;

        public OwnerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Owner>>> GetAllOwners()
        {
            var owners = await _context.Owners.Select(o => new
            {
                o.Id,
                o.Name,
            }).ToListAsync();
            if (owners.Count == 0)
            {
                return NotFound("There is no owners in the database");
            }

            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _context.Owners
                .Include(o => o.Raccoons)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (owner == null)
            {
                return NotFound("there is no owner with this id ");
            }

            return Ok(owner);
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> AddOwner(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Owner>> UpdateOwner(int id, Owner owner)
        {
            var trueOwner = await _context.Owners.FindAsync(id);
            if (trueOwner == null)
            {
                return NotFound("There is no owner with this id");
            }

            trueOwner.Name = owner.Name;

            await _context.SaveChangesAsync();

            return Ok(trueOwner);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Owner>> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound("there is no owner with this id ");
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();
            return Ok(owner);
        }

        [HttpPost]
        [Route("AddRaccoonToOwner")]
        public async Task<ActionResult<Raccoon>> AddRaccoonToOwner(int raccoonId, int ownerId)
        {
            var raccoon = await _context.Raccoons
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(r => r.Id == raccoonId);

            var owner = await _context.Owners
                .Include(o => o.Raccoons)
                .FirstOrDefaultAsync(o => o.Id == ownerId);

            if (raccoon == null || owner == null)
            {
                return NotFound("There is no raccoon or owner with this id");
            }

            raccoon.Owner = owner;
            raccoon.OwnerId = owner.Id;

            owner.Raccoons.Add(raccoon);

            await _context.SaveChangesAsync();

            return Ok(new { Raccoon = raccoon, Owner = owner });
        }

        [HttpPost]
        [Route("RemoveRaccoonFromOwner")]
        public async Task<ActionResult<Raccoon>> RemoveRaccoonFromOwner(int raccoonId, int ownerId)
        {
            var raccoon = await _context.Raccoons
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(r => r.Id == raccoonId);
            var owner = await _context.Owners
                .Include(o => o.Raccoons)
                .FirstOrDefaultAsync(o => o.Id == ownerId);
            if (raccoon == null || owner == null)
            {
                return NotFound("There is no raccoon or owner with this id");
            }

            raccoon.Owner = null;
            raccoon.OwnerId = null;
            owner.Raccoons.Remove(raccoon);
            await _context.SaveChangesAsync();
            return Ok(new { Raccoon = raccoon, Owner = owner });
        }

        [HttpGet]
        [Route("GetRaccoonsByOwner")]
        public async Task<ActionResult<List<Raccoon>>> GetOwnerByRaccoonId(int raccoonId)
        {
            var raccoon = await _context.Raccoons
                .Include(r => r.Owner)
                .FirstOrDefaultAsync(r => r.Id == raccoonId);
            if (raccoon == null)
            {
                return NotFound("There is no owner with this id");
            }

            return Ok(raccoon.Owner);
        }

        [HttpPost]
        [Route("CreateRaccoonForOwner")]
        public async Task<ActionResult<Raccoon>> CreateRaccoonForOwner(int ownerId, Raccoon raccoon)
        {
            var owner = await _context.Owners
                .Include(o => o.Raccoons)
                .FirstOrDefaultAsync(o => o.Id == ownerId);
            if (owner == null)
            {
                return NotFound("There is no owner with this id");
            }
            raccoon.Owner = owner;
            raccoon.OwnerId = owner.Id;
            await _context.Raccoons.AddAsync(raccoon);
            await _context.SaveChangesAsync();
            return CreatedAtAction(
                "GetRaccoon",
                "Raccoon",
                new { id = raccoon.Id },
                raccoon
            );
        }
    }
}
