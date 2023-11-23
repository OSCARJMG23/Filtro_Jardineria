using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Entities;
using Dominio.Interfaces;

namespace Dominio.Interfaces
{
    public interface IProductoRepository : IGenericRepository<Producto>
    {
        Task<IEnumerable<object>> GetProductosNoPedidos();
        Task<IEnumerable<object>> GetProductoFacturado3000();
        Task<object> GetProductoVendidoMasUnidades();
        Task<IEnumerable<object>> GetProductosMasVendidosYTotalUnidades();
    }
}