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

            return Ok(result.OrderBy(x => x.LockerNo));
        }

        // Get Locker
        [HttpGet]
        [Route("{lockerNo}")]
        [ActionName("GetLocker")]
        public async Task<IActionResult> GetLocker([FromRoute] string lockerNo)
        {
            var result = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.LockerNo == lockerNo);

            if (result != null)
                return Ok(result);

            return NotFound("Locker not found");
        }

        // Add Locker
        [HttpPost]
        public async Task<IActionResult> AddLocker([FromBody] LockerInfo locker)
        {
            var empNumberExist = await lockersDbContext.Lockers
                                .Where(x => x.EmployeeNumber == locker.EmployeeNumber && !x.EmployeeNumber.Equals(string.Empty))
                                .Select(x => x.EmployeeNumber).ToListAsync();

            var lockerNumberExist = await lockersDbContext.Lockers
                                .Where(x => x.LockerNo == locker.LockerNo)
                                .Select(x => x.EmployeeNumber).ToListAsync();

            if (empNumberExist.Any())
            {
                var message = new Exception($"Employee number {locker.EmployeeNumber} already exists");

                return BadRequest(message);
            }
            else if (lockerNumberExist.Any())
            {
                var message = new Exception($"Locker number {locker.LockerNo} already exists");

                return BadRequest(message);
            }
            else
            {
                // if the input of employee number is not null, then the value isEmpty is false
                locker.IsEmpty = locker.EmployeeNumber != string.Empty ? false : true;

                await lockersDbContext.Lockers.AddAsync(locker);
                await lockersDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetLocker), new { locker.IsEmpty }, locker);
            }
            
        }

        // TODO: update logic UpdateLocker with noLocker no Id
        // Update a Locker
        [HttpPut]
        [Route("{lockerNo}")]
        public async Task<IActionResult> UpdateLocker([FromRoute] string lockerNo, [FromBody] LockerInfo locker)
        {
            var existingLocker = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.LockerNo == lockerNo);

            if (existingLocker != null)
            {
                existingLocker.EmployeeNumber = locker.EmployeeNumber;
                existingLocker.LockerNo = locker.LockerNo;
                existingLocker.Size = locker.Size;
                existingLocker.Location = locker.Location;
                existingLocker.IsEmpty = locker.EmployeeNumber != string.Empty ? false : true;

                await lockersDbContext.SaveChangesAsync();

                return Ok(existingLocker);
            }

            return NotFound("Locker not found");
        }

        // Delete a Locker
        [HttpDelete]
        [Route("{lockerNo}")]
        public async Task<IActionResult> DeleteLocker([FromRoute] string lockerNo)
        {
            var existingLocker = await lockersDbContext.Lockers.FirstOrDefaultAsync(x => x.LockerNo == lockerNo);

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
