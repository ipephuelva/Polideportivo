using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.Models;

namespace NetCore.Unit.Tests
{
    [TestClass]
    public class SociosTests
    {
        [TestMethod]
        public void Socio_Invalido()
        {
            var socio = new Socios() {Name = string.Empty, Surname = string.Empty, Telephone = null, Dni = string.Empty, User = string.Empty, Password = string.Empty };

            var resultado = socio.EsValido(socio);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Socio_Valido()
        {
            var socio = new Socios() { Name = "socio9" , Surname = "Ape9 Ape9", Telephone = null, Dni = "99999999X", User ="socio9", Password = "socio9" };

            var resultado = socio.EsValido(socio);

            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void Socio_DniInValidoConLetra()
        {
            var socio = new Socios() { Name = "socio9", Surname = "Ape9 Ape9", Telephone = null, Dni = "99999999999X", User = "socio9", Password = "socio9" };

            var resultado = socio.EsValido(socio);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Socio_DniInValidoSinLetra()
        {
            var socio = new Socios() { Name = "socio9", Surname = "Ape9 Ape9", Telephone = null, Dni = "999999999", User = "socio9", Password = "socio9" };

            var resultado = socio.EsValido(socio);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Socio_TelefonoInValido()
        {
            var socio = new Socios() { Name = "socio9", Surname = "Ape9 Ape9", Telephone = 1, Dni = "99999999X", User = "socio9", Password = "socio9" };

            var resultado = socio.EsValido(socio);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Socio_TelefonoInValido2()
        {
            var socio = new Socios() { Name = "socio9", Surname = "Ape9 Ape9", Telephone = 1234567899, Dni = "99999999X", User = "socio9", Password = "socio9" };

            var resultado = socio.EsValido(socio);

            Assert.IsFalse(resultado);
        }

    }
}
