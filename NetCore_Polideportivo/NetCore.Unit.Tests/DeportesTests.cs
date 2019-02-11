using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.Models;

namespace NetCore.Unit.Tests
{
    [TestClass]
    public class DeportesTests
    {
        [TestMethod]
        public void Deporte_Invalido()
        {
            var deporte = new Deportes() { Name = string.Empty };

            var resultado = deporte.EsValido(deporte);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Deporte_Valido()
        {
            var deporte = new Deportes() { Name = "Natacion" };

            var resultado = deporte.EsValido(deporte);

            Assert.IsTrue(resultado);
        }

    }
}
