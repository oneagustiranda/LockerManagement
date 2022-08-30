using Locker.API.Data;
using Locker.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LockersController : Controller
    {
        private readonly LockersDbContext lockersDbContext;
        public LockersController(LockersDbContext lockersDbContext)
        {
            this.lockersDbContext = lockersDbContext;
        }

        // Get All Lockers
        [HttpGet]
        public async Task<IActionResult> GetAllLockers()
        {
            var result = await lockersDbContext.Lockers.ToListAsync();

            return Ok(result);
        }

        // Get Locker
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetLocker")]
        public async Task<IActionResult> GetLocker([FromRoute] Guid id)
        {
            var result = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
                return Ok(result);

            return NotFound("Locker not found");
        }

        // Add Locker
        [HttpPost]
        public async Task<IActionResult> AddLocker([FromBody] LockerInfo locker)
        {
            locker.Id = Guid.NewGuid();

            await lockersDbContext.Lockers.AddAsync(locker);
            await lockersDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLocker), new { id = locker.Id }, locker);
        }

        // Update a Locker
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateLocker([FromRoute] Guid id, [FromBody] LockerInfo locker)
        {
            var existingLocker = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingLocker != null)
            {
                existingLocker.EmployeeNumber = locker.EmployeeNumber;
                existingLocker.LockerNo = locker.LockerNo;
                existingLocker.Size = locker.Size;
                existingLocker.Location = locker.Location;
                existingLocker.IsEmpty = locker.IsEmpty;

                await lockersDbContext.SaveChangesAsync();

                return Ok(existingLocker);
            }

            return NotFound("Locker not found");
        }

        // Delete a Locker
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteLocker([FromRoute] Guid id)
        {
            var existingLocker = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.Id == id);

            if (existingLocker != null)
            {
                lockersDbContext.Remove(existingLocker);
                await lockersDbContext.SaveChangesAsync();

                return Ok(existingLocker);
            }

            return NotFound("Locker not found");
        }
    }
}
