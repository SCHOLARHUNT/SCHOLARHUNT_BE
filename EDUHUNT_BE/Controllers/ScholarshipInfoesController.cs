using EDUHUNT_BE.Data;
using EDUHUNT_BE.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EDUHUNT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScholarshipInfoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScholarshipInfoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ScholarshipInfoes
        [HttpGet]
        public async Task<IActionResult> GetScholarshipInfo()
        {

            return Ok(await _context.ScholarshipInfo.ToListAsync());

        }

        // GET: api/ScholarshipInfoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ScholarshipInfo>> GetScholarshipInfo(int id)
        {
            var scholarshipInfo = await _context.ScholarshipInfo.FindAsync(id);

            if (scholarshipInfo == null)
            {
                return NotFound();
            }

            return scholarshipInfo;
        }

        // PUT: api/ScholarshipInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScholarshipInfo(int id, ScholarshipInfo scholarshipInfo)
        {
            if (id != scholarshipInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(scholarshipInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ScholarshipInfoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ScholarshipInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ScholarshipInfo>> PostScholarshipInfo(ScholarshipInfo scholarshipInfo)
        {
            _context.ScholarshipInfo.Add(scholarshipInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScholarshipInfo", new { id = scholarshipInfo.Id }, scholarshipInfo);
        }

        // DELETE: api/ScholarshipInfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScholarshipInfo(int id)
        {
            var scholarshipInfo = await _context.ScholarshipInfo.FindAsync(id);
            if (scholarshipInfo == null)
            {
                return NotFound();
            }

            _context.ScholarshipInfo.Remove(scholarshipInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ScholarshipInfoExists(int id)
        {
            return _context.ScholarshipInfo.Any(e => e.Id == id);
        }
    }
}
