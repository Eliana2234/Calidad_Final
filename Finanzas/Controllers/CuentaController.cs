using System;
using System.Collections.Generic;
using System.Linq;
using Finanzas.Models;
using Finanzas.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Finanzas.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ICuentaRepositorio _context;
        public CuentaController(ICuentaRepositorio context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var cuentas = _context.GetCuentas();
            ViewBag.Categorias = _context.GetCategorias();
            ViewBag.Total = cuentas.Sum(o => o.Saldo);
            return View("Index", cuentas);
        }

        [HttpGet]
        public ActionResult Registrar()
        {
            ViewBag.Categorias = _context.GetCategorias();
            return View("Registrar", new Cuenta());
        }

        [HttpPost]
        public ActionResult Registrar(Cuenta cuenta)
        {
            if (ModelState.IsValid)
            {
                if(cuenta.IdCategoria == 2)
                {
                    cuenta.Limite = cuenta.Saldo;
                    cuenta.Saldo = 0;
                }
                if (cuenta.Saldo != 0 && cuenta.IdCategoria != 2)
                {
                    cuenta.Limite = 0;
                    cuenta.Transaccions = new List<Transaccion>
                    {
                        new Transaccion
                        {
                            Fecha = DateTime.Now,
                            IdTipo = 1,
                            Monto = cuenta.Saldo,
                            Descripcion = "Monto Inicial"
                        }
                    };
                }
                _context.SaveCuenta(cuenta);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Categorias = _context.GetCategorias();
                return View("Registrar", cuenta);
            }
        }
    }
}
