using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;

namespace Dominio.Interfaces
{
    public interface IClienteRepository : IGenericRepository<Cliente>
    {
        Task<IEnumerable<object>> GetClientesYTotalPedidos();
        Task<IEnumerable<object>> GetClientesEntregadoFueraDeTiempo();
        Task<IEnumerable<object>> GetClienteGamasHaComprado();
    }
}