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
    public class ReservasController : ControllerBase
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: api/Reservas
        [HttpGet]
        public IEnumerable<Reservas> GetReservas()
        {
            return _context.Reservas;
        }

        // GET: api/Reservas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservas = await _context.Reservas.FindAsync(id);

            if (reservas == null)
            {
                return NotFound();
            }

            return Ok(reservas);
        }

        // PUT: api/Reservas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservas([FromRoute] int id, [FromBody] Reservas reservas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservas.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservasExists(id))
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

        // POST: api/Reservas
        [HttpPost]
        public async Task<IActionResult> PostReservas([FromBody] Reservas reservas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(reservas.Date.Date <= DateTime.Now.Date && reservas.Date.Hour <= DateTime.Now.Hour)
            {
                    return NotFound("The introduced date is in the past");
            }

            //Validacion fecha
            if (reservas.Date.Minute != 00 || reservas.Date.Second != 00)
            {
                return NotFound("The date operations are only with hour " + reservas.Date);
            }

            //Validacion rangos fechas
            if (reservas.Date.Hour < 08 || reservas.Date.Hour > 22)
            {
                return NotFound("The range of available hours is from 08:00 to 22:00");
            }

            var deportes = _context.Deportes.Where(x => x.Name.ToUpper() == reservas.Sport.ToUpper()).ToList();

            if (deportes.Count == 0)
            {
                return NotFound("The specified sport not exist");
            }

            //validar si tiene alguna otra reserva a esa hora
            var reservas_soc = _context.Reservas.Where(x => x.Date == reservas.Date && x.Dni == reservas.Dni).ToList();
            if (reservas_soc.Count != 0)
            {
                return Ok("Sorry but you already got another reserve in this day at the same time");
            }

            //reservas de esa fecha
            var list_reservas = _context.Reservas.Where(x => x.Date.Date == reservas.Date.Date).ToList();

            //Verificar si este usuario ha hecho 3 reservas para ese dia.
            var cuantas_reservas_socio = list_reservas.Where(x => x.Dni == reservas.Dni).ToList();
            if (cuantas_reservas_socio.Count >= 3)
            {
                return Ok("Sorry but you can not make more reserves for this day, yout already got " + cuantas_reservas_socio.Count + " reserves in this day");
            }


            //Validar si el socio existe.
            var socios = _context.Socios.Where(x => x.Dni == reservas.Dni).ToList();

            if (socios.Count == 0)
            {
                return NotFound("The specified DNI not exist");
            }

            //Validar nº de pista
            var pistas = _context.Pistas.Where(x => x.Sport == reservas.Sport && x.NField == reservas.NField).ToList();

            if (pistas.Count == 0)
            {
                return NotFound("The specified Field not exist");
            }

            //validar si la pista eseta libre
            var libre = true;

            //sacar pistas disponibles eliminando las reservadas para ese dia
            foreach (var pista in pistas)
            {
                libre = true;
                foreach (var reserva in list_reservas)
                {
                    if (reserva.Sport == pista.Sport && reserva.NField == pista.NField && reserva.Date == reservas.Date)
                    {
                        //pista reservada
                        libre = false;
                    }
                }

            }
            if (libre)
            {
                //se hace la reserva
                _context.Reservas.Add(reservas);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetReservas", new { id = reservas.Id }, reservas);
            }
            else
            {
                return Ok("There are not available fields for this date, try it with another date");
            }
        }

        // DELETE: api/Reservas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservas([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reservas);
            await _context.SaveChangesAsync();

            return Ok(reservas);
        }

        private bool ReservasExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }

        [HttpPost("{p_date}")]
        public IActionResult DameReservas(DateTime p_date)
        {

            //Validacion fecha
            if (p_date.Minute != 00 || p_date.Second != 00)
            {
                return NotFound("The date operations are only with hour " + p_date);
            }

            //Validacion rangos fechas
            if(p_date.Hour < 08 || p_date.Hour > 22)
            {
                return NotFound("The range of available hours is from 08:00 to 22:00");
            }

            //obtener todas las reservas en funcion de fecha
            var reservas = _context.Reservas.Where(x => x.Date == p_date).ToList();

            if (reservas.Count == 0)
            {
                //http 404
                return NotFound("There are no reservations for this day");
            }

            return Ok(reservas);
        }
    }
}