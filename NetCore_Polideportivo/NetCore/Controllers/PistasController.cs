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
    public class PistasController : ControllerBase
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Pistas
        [HttpGet]
        public IEnumerable<Pistas> GetPistas()
        {
            return _context.Pistas;
        }

        // GET: api/Pistas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPistas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pistas = await _context.Pistas.FindAsync(id);

            if (pistas == null)
            {
                return NotFound();
            }

            return Ok(pistas);
        }

        // PUT: api/Pistas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPistas([FromRoute] int id, [FromBody] Pistas pistas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pistas.Id)
            {
                return BadRequest();
            }

            _context.Entry(pistas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PistasExists(id))
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

        // POST: api/Pistas
        [HttpPost]
        public async Task<IActionResult> PostPistas([FromBody] Pistas pistas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Pistas.Add(pistas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPistas", new { id = pistas.Id }, pistas);
        }

        // DELETE: api/Pistas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePistas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pistas = await _context.Pistas.FindAsync(id);
            if (pistas == null)
            {
                return NotFound();
            }

            _context.Pistas.Remove(pistas);
            await _context.SaveChangesAsync();

            return Ok(pistas);
        }

        private bool PistasExists(int id)
        {
            return _context.Pistas.Any(e => e.Id == id);
        }


        [HttpPost("{p_date}")]
        public IActionResult BuscarPista(DateTime p_date, string p_sport, Socios socio)
        {

            if (p_date.Date.Date <= DateTime.Now.Date && p_date.Date.Hour <= DateTime.Now.Hour)
            {
                return NotFound("The introduced date is in the past");
            }

            //Validacion fecha
            if (p_date.Minute != 00 || p_date.Second != 00)
            {
                return NotFound("The date operations are only with hour " + p_date);
            }

            //Validacion rangos fechas
            if (p_date.Hour < 08 || p_date.Hour > 22)
            {
                return NotFound("The range of available hours is from 08:00 to 22:00");
            }

            //Validar si existe deporte
            if(p_sport == null)
            {
                return BadRequest("You must specify a valid sport");
            }
            var deportes = _context.Deportes.Where(x => x.Name.ToUpper() == p_sport.ToUpper()).ToList();

            if (deportes.Count == 0)
            {
                return NotFound("The specified sport not exist");
            }


            //Validar si el socio existe.
            var socios = _context.Socios.Where(x => x.Dni == socio.Dni).ToList();

            if (socios.Count == 0)
            {
                return NotFound("The specified DNI not exist");
            }

            //reservas de esa fecha
            var reservas = _context.Reservas.Where(x => x.Date.Date == p_date.Date).ToList();

            //Verificar si este usuario ha hecho 3 reservas para ese dia.
            var cuantas_reservas_socio = reservas.Where(x => x.Dni == socio.Dni).ToList();
            if (cuantas_reservas_socio.Count >= 3)
            {
                return Ok("Sorry but you can not make more reserves for this day, yout already got " + cuantas_reservas_socio.Count + " reserves in this day");
            }

            //obtener todas las pistas
            var pistas = _context.Pistas.Where(x => x.Sport.ToUpper() == p_sport.ToUpper()).ToList();

            if(pistas.Count == 0)
            {
                return NotFound("Cannot find the sport " + p_sport);
            }

            //Resultado inic
            var resultado = new List<Pistas>();
            var libre = true;

            //sacar pistas disponibles eliminando las reservadas para ese dia
            foreach (var pista in pistas)
            {
                libre = true;
                foreach (var reserva in reservas)
                {
                    if (reserva.Sport == pista.Sport && reserva.NField == pista.NField && reserva.Date == p_date)
                    {
                        //pista reservada
                        libre = false;                     
                    }
                }
                if (libre)
                {
                    //pista libre
                    resultado.Add(pista);
                }
            }

            //si no hay pistas disponibles
            if (resultado.Count == 0)
            {
                return Ok("There are not available fields for this date, try it with another date");
            }

            return Ok(resultado);
        }



    }
}