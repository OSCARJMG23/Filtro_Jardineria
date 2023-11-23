using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;

namespace Aplicacion.Repository
{
    public class DetallePedidoRepository : GenericRepository<DetallePedido>, IDetallePedidoRepository
    {
        private readonly JardineriaContext _context;

        public DetallePedidoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }
    }
}