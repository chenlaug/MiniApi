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
            var owners = await _context.Owners.ToListAsync();

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
                .Include(o => o.Raccoons)  // Inclure les raccoons de l'owner
                .FirstOrDefaultAsync(o => o.Id == ownerId);

            if (raccoon == null || owner == null)
            {
                return NotFound("There is no raccoon or owner with this id");
            }

            raccoon.Owner = owner;
            raccoon.OwnerId = owner.Id;

            owner.Raccoons.Add(raccoon);

            await _context.SaveChangesAsync();

            return Ok(raccoon);
        }

    }
}
