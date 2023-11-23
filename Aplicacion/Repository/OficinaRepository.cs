using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository
{
    public class OficinaRepository : GenericRepository<Oficina>, IOficinaRepository
    {
        private readonly JardineriaContext _context;

        public OficinaRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Oficina>> GetOficinaNoTrabajanEmpleadoFrutales()
        {
            var oficinas = await _context.Oficinas
            .Where(e=>!e.Empleados
                .Any(e=>e.Clientes
                    .Any(t=>t.Pedidos
                        .Any(r=>r.DetallePedidos
                            .Any(c=>c.CodigoProductoNavigation.Gama.ToLower() =="frutales")))))
            .ToListAsync();

            return oficinas;
        }

    }
}