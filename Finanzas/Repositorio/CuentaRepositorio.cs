using Finanzas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finanzas.Repositorio
{
    public interface ICuentaRepositorio
    {
        List<Cuenta> GetCuentas();
        List<Categoria> GetCategorias();
        void SaveCuenta(Cuenta cuenta);
    }

    public class CuentaRepositorio : ICuentaRepositorio
    {
        private readonly ContextoFinanzas contexto;

        public CuentaRepositorio(ContextoFinanzas contexto)
        {
            this.contexto = contexto;
        }

        public List<Categoria> GetCategorias()
        {
            return contexto.Categorias
                .ToList();
        }

        public List<Cuenta> GetCuentas()
        {
            return contexto.Cuentas
                .Include(o => o.Categoria)
                .ToList();
        }

        public void SaveCuenta(Cuenta cuenta)
        {
            contexto.Cuentas.Add(cuenta);
            contexto.SaveChanges();
        }
    }
}
