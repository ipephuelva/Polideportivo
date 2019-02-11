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
    public class SociosController : ControllerBase
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Socios
        [HttpGet]
        public IEnumerable<Socios> GetSocios()
        {
            return _context.Socios;
        }

        // GET: api/Socios/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSocios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socios = await _context.Socios.FindAsync(id);

            if (socios == null)
            {
                return NotFound();
            }

            return Ok(socios);
        }

        // PUT: api/Socios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocios([FromRoute] int id, [FromBody] Socios socios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != socios.Id)
            {
                return BadRequest();
            }

            _context.Entry(socios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SociosExists(id))
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

        // POST: api/Socios
        [HttpPost]
        public async Task<IActionResult> PostSocios([FromBody] Socios socios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Socios.Add(socios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocios", new { id = socios.Id }, socios);
        }

        // DELETE: api/Socios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocios([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var socios = await _context.Socios.FindAsync(id);
            if (socios == null)
            {
                return NotFound();
            }

            _context.Socios.Remove(socios);
            await _context.SaveChangesAsync();

            return Ok(socios);
        }

        private bool SociosExists(int id)
        {
            return _context.Socios.Any(e => e.Id == id);
        }
    }
}