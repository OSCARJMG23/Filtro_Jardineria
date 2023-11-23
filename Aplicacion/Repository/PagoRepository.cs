using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class PagoRepository : GenericRepository<Pago>, IPagoRepository
    {
        private readonly JardineriaContext _context;

        public PagoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
    }
}