using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class GamaProductoRepository : GenericRepository<GamaProducto>, IGamaProductoRepository
    {
        private readonly JardineriaContext _context;

        public GamaProductoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
    }
}