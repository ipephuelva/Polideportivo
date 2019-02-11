using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetCore.Models;

namespace NetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Deportes
        [HttpGet]
        public IEnumerable<Deportes> GetDeportes()
        {
            return _context.Deportes;
        }

        // GET: api/Deportes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeportes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deportes = await _context.Deportes.FindAsync(id);

            if (deportes == null)
            {
                return NotFound();
            }

            return Ok(deportes);
        }

        // PUT: api/Deportes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeportes([FromRoute] int id, [FromBody] Deportes deportes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deportes.Id)
            {
                return BadRequest();
            }

            _context.Entry(deportes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeportesExists(id))
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

        // POST: api/Deportes
        [HttpPost]
        public async Task<IActionResult> PostDeportes([FromBody] Deportes deportes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Deportes.Add(deportes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeportes", new { id = deportes.Id }, deportes);
        }

        // DELETE: api/Deportes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeportes([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deportes = await _context.Deportes.FindAsync(id);
            if (deportes == null)
            {
                return NotFound();
            }

            _context.Deportes.Remove(deportes);
            await _context.SaveChangesAsync();

            return Ok(deportes);
        }

        private bool DeportesExists(int id)
        {
            return _context.Deportes.Any(e => e.Id == id);
        }
    }
}