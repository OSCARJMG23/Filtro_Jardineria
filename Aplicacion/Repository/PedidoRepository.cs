using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository
{
    public class PedidoRepository : GenericRepository<Pedido>, IPedidoRepository
    {
        private readonly JardineriaContext _context;

        public PedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetPedidosNoEntregadosAtiempo()
        {
            var pedidos = await _context.Pedidos
            .Where(e=> e.FechaEntrega > e.FechaEsperada)
            .Select(e => new
            {
                Codigo_Pedido = e.CodigoPedido,
                Codigo_Cliente = e.CodigoCliente,
                Fecha_Esperada = e.FechaEsperada,
                Fecha_Entrega = e.FechaEntrega
            }).ToListAsync();

            return pedidos;
        }
    }
}