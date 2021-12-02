using Finanzas.Controllers;
using Finanzas.Models;
using Finanzas.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FinanzasTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void IndexTest()
        {
            var repo = new Mock<ICuentaRepositorio>();
            repo.Setup(o => o.GetCuentas()).Returns(new List<Cuenta>());
            repo.Setup(o => o.GetCategorias()).Returns(new List<Categoria>());

            var controller = new CuentaController(repo.Object);
            var view = controller.Index() as ViewResult;

            Assert.AreEqual("Index", view.ViewName);
        }

        [Test]
        public void RegistrarGet()
        {
            var repo = new Mock<ICuentaRepositorio>();
            repo.Setup(o => o.GetCategorias()).Returns(new List<Categoria>());

            var controller = new CuentaController(repo.Object);
            var view = controller.Registrar() as ViewResult;

            Assert.AreEqual("Registrar", view.ViewName);
        }

        [Test]
        public void RegistrarPostGood()
        {
            var repo = new Mock<ICuentaRepositorio>();
            repo.Setup(o => o.SaveCuenta(new Cuenta()));

            var controller = new CuentaController(repo.Object);
            var view = controller.Registrar(new Cuenta()) as RedirectToActionResult;

            Assert.AreEqual("Index", view.ActionName);
        }
    }
}