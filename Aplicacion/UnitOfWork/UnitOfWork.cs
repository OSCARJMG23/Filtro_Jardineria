using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia.Data;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly JardineriaContext _context;
       private ClienteRepository _clientes;
       private DetallePedidoRepository _detallePedidos;
       private EmpleadoRepository _empleados;
       private GamaProductoRepository _gamaProductos;
       private OficinaRepository _oficinas;
       private PagoRepository _pagos;
       private PedidoRepository _pedidos;
       private ProductoRepository _productos;


        public UnitOfWork(JardineriaContext context)
        {
            _context = context;
        }

        public IClienteRepository Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(_context);
                }
                return _clientes;
            }
        }

        public IDetallePedidoRepository DetallePedidos
        {
            get
            {
                if (_detallePedidos == null)
                {
                    _detallePedidos = new DetallePedidoRepository(_context);
                }
                return _detallePedidos;
            }
        }

        public IEmpleadoRepository Empleados
        {
            get
            {
                if (_empleados == null)
                {
                    _empleados = new EmpleadoRepository(_context);
                }
                return _empleados;
            }
        }

        public IGamaProductoRepository GamaProductos
        {
            get
            {
                if (_gamaProductos == null)
                {
                    _gamaProductos = new GamaProductoRepository(_context);
                }
                return _gamaProductos;
            }
        }

        public IOficinaRepository Oficinas
        {
            get
            {
                if (_oficinas == null)
                {
                    _oficinas = new OficinaRepository(_context);
                }
                return _oficinas;
            }
        }

        public IPagoRepository Pagos
        {
            get
            {
                if (_pagos == null)
                {
                    _pagos = new PagoRepository(_context);
                }
                return _pagos;
            }
        }


        public IPedidoRepository Pedidos
        {
            get
            {
                if (_pedidos == null)
                {
                    _pedidos = new PedidoRepository(_context);
                }
                return _pedidos;
            }
        }

        public IProductoRepository Productos
        {
            get
            {
                if (_productos == null)
                {
                    _productos = new ProductoRepository(_context);
                }
                return _productos;
            }
        }


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}