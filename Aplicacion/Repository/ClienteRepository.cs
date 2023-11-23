using Dominio.Entities;
using Dominio.Interfaces;
using Persistencia.Data;
using Aplicacion.Repository;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository
{
    public class ClienteRepository : GenericRepository<Cliente>, IClienteRepository
    {
        private readonly JardineriaContext _context;

        public ClienteRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetClientesYTotalPedidos()
        {
            var cliente = await _context.Clientes
            .Select(e=>new
            {
                Nombre_Cliente = e.NombreCliente,
                PedidosRealizados = e.Pedidos.Count()
            }).ToListAsync();

            return cliente;
        }

        public async Task<IEnumerable<object>> GetClientesEntregadoFueraDeTiempo()
        {
            var clientes = await _context.Clientes
            .Where(e=> e.Pedidos.Any(e=>e.FechaEntrega > e.FechaEsperada))
            .Select(e=>new
            {
                Nombre_Cliente = e.NombreCliente,
                Pedido = "Entregado Fuera de tiempo"
            }).ToListAsync();

            return clientes;
        }

        public async Task<IEnumerable<object>> GetClienteGamasHaComprado()
        {
            var clientes = await _context.Clientes
            .Where(e=>e.Pedidos.Any())
            .Select(e=>new
            {
                Nombre_Cliente = e.NombreCliente,
                Gamas_Producto = e.Pedidos
                                    .SelectMany(t=>t.DetallePedidos)
                                    .Select(n=>n.CodigoProductoNavigation.GamaNavigation.Gama)
                                    .Distinct()
            }).ToListAsync();

            return clientes;
        }

    }
}