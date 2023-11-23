using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominio.Interfaces;

namespace Dominio.Interfaces
{
    public interface IUnitOfWork
    {
        IClienteRepository Clientes { get; }
        IDetallePedidoRepository DetallePedidos { get; }
        IEmpleadoRepository Empleados { get; }
        IGamaProductoRepository GamaProductos { get; }
        IOficinaRepository Oficinas { get; }
        IPagoRepository Pagos { get; }
        IPedidoRepository Pedidos { get; }
        IProductoRepository Productos { get; }

        Task<int> SaveAsync();
    }
}