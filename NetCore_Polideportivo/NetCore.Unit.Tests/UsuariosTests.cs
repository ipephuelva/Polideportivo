using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCore.Models;

namespace NetCore.Unit.Tests
{
    [TestClass]
    public class UsuariosTests
    {
        [TestMethod]
        public void Usuario_Invalido()
        {
            var usuario = new Usuarios() { User = string.Empty, Password =string.Empty };

            var resultado = usuario.EsValido(usuario);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void Usuario_Valido()
        {
            var usuario = new Usuarios() { User = "root", Password = "root123" };

            var resultado = usuario.EsValido(usuario);

            Assert.IsTrue(resultado);
        }

    }
}
