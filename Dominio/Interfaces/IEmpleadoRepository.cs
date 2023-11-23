using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;

namespace Dominio.Interfaces
{
    public interface IEmpleadoRepository : IGenericRepository<Empleado>
    {
        Task<IEnumerable<object>> GetEmpleadoSinCliente();
    }
}