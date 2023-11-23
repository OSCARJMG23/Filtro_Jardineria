using Dominio.Entities;
using Dominio.Interfaces;
using Aplicacion.Repository;
using Persistencia.Data;
using Microsoft.EntityFrameworkCore;

namespace Aplicacion.Repository
{
    public class EmpleadoRepository : GenericRepository<Empleado>, IEmpleadoRepository
    {
        private readonly JardineriaContext _context;

        public EmpleadoRepository(JardineriaContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<object>> GetEmpleadoSinCliente()
        {
            var empleados = await _context.Empleados
            .Where(e=>e.Clientes.Count() == 0)
            .Select(e=>new
            {
                Nombre_Empleado = e.Nombre,
                PrimerApellido = e.Apellido1,
                SegundoApellido = e.Apellido2,
                PuestoEmpleado = e.Puesto,
                Telefono_Oficina = e.CodigoOficinaNavigation.Telefono
            }).ToListAsync();

            return empleados;
        }
    }
}