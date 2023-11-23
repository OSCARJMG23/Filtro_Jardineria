using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository
{
    public class ProductoRepository : GenericRepository<Producto>, IProductoRepository
    {
        private readonly JardineriaContext _context;

        public ProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetProductosNoPedidos()
        {
            var productos = await _context.Productos
            .Where(e=>e.DetallePedidos.Count() == 0)
            .Select(e=>new
            {
                Nombre_Producto = e.Nombre,
                Descripcion_Producto = e.Descripcion,
                Imagen_Producto = e.GamaNavigation.Imagen
            }).ToListAsync();

            return productos;
        }

        public async Task<IEnumerable<object>> GetProductoFacturado3000()
        {
            var productos = await _context.Productos
            .Select(e=>new
            {
                Nombre_Producto = e.Nombre,
                UnidadesVendidas = e.DetallePedidos.Sum(t=>t.Cantidad),
                TotalFacturado = e.DetallePedidos.Sum(e=>e.Cantidad * e.PrecioUnidad),
                TotalFactura_IVA = e.DetallePedidos.Sum(e=> e.Cantidad * e.PrecioUnidad * 1.21m)
            }).Where(t=> t.TotalFacturado >= 3000)
            .ToListAsync();

            return productos;
        }

        public async Task<object> GetProductoVendidoMasUnidades()
        {
            var producto = await _context.DetallePedidos
            .GroupBy(e=>e.CodigoProducto)
            .OrderByDescending(e=>e.Sum(e=>e.Cantidad))
            .Select(e=>new
            {
                NombreProducto = _context.Productos
                    .Where(t=>t.CodigoProducto==e.Key)
                    .Select(e=>e.Nombre)
                    .FirstOrDefault(),
                CantidadVendida = e.Sum(e=>e.Cantidad)
            }).FirstOrDefaultAsync();

            
            return producto;
        }

        public async Task<IEnumerable<object>> GetProductosMasVendidosYTotalUnidades()
        {
            var productos = await _context.Productos
            .OrderByDescending(e=>e.DetallePedidos.Sum(t=>t.Cantidad))
            .Take(20)
            .Select(e=>new
            {
                Codigo_Producto = e.CodigoProducto,
                Nombre_Producto = e.Nombre,
                Total_UnidadesVendidas = e.DetallePedidos.Sum(e=>e.Cantidad)
            })
            .ToListAsync();

            return productos;
        }
    }
}