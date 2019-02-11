using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.Models;
using System;

namespace NetCore.Unit.Tests
{
    [TestClass]
    public class ReservasTests
    {
        [TestMethod]
        public void Reserva_Invalido()
        {
            var reserva = new Reservas() { Dni = string.Empty, Sport = string.Empty, NField = 1, Date = new DateTime(2019, 1, 2, 08, 32, 15) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Reserva_Valido()
        {
            var reserva = new Reservas() { Dni = "11111111A", Sport = "Futbol", NField = 1, Date = new DateTime(2019, 1, 2, 08,00,00) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Reserva_DniInvalido()
        {
            var reserva = new Reservas() { Dni = "111111111", Sport = "Futbol", NField = 1, Date = new DateTime(2019, 1, 2, 08, 00, 00) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Reserva_DniInvalido2()
        {
            var reserva = new Reservas() { Dni = "111111A", Sport = "Futbol", NField = 1, Date = new DateTime(2019, 1, 2, 08, 00, 00) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Reserva_FechaInvalida()
        {
            var reserva = new Reservas() { Dni = "11111111A", Sport = "Futbol", NField = 1, Date = new DateTime(2019, 1, 2, 08, 30, 00) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Reserva_FechaInvalida2()
        {
            var reserva = new Reservas() { Dni = "11111111A", Sport = "Futbol", NField = 1, Date = new DateTime(2019, 1, 2, 08, 00, 30) };

            var resultado = reserva.EsValido(reserva);

            Assert.IsFalse(resultado);
        }

    }
}
