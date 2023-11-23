using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
namespace ApiJardineria.Controllers
{
public class EmpleadoController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public EmpleadoController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
{
    var Empleado = await _unitOfWork.Empleados.GetAllAsync();
    return _mapper.Map<List<EmpleadoDto>>(Empleado);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<EmpleadoDto>> Get(int id)
{
    var Empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
    return _mapper.Map<EmpleadoDto>(Empleado);
}

[HttpGet("empleadoSinClientes")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetEmpleadoSinClientes()
{
    var Empleado = await _unitOfWork.Empleados.GetEmpleadoSinCliente();
    return Ok(Empleado);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Empleado>> Post(EmpleadoDto EmpleadoDto)
{
    var Empleado = _mapper.Map<Empleado>(EmpleadoDto);
    _unitOfWork.Empleados.Add(Empleado);
    await _unitOfWork.SaveAsync();

    if (Empleado == null)
    {
        return BadRequest();
    }
    Empleado.CodigoEmpleado = Empleado.CodigoEmpleado;
    return CreatedAtAction(nameof(Post), new { id = Empleado.CodigoEmpleado }, Empleado);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody]EmpleadoDto EmpleadoDto)
{
    if (EmpleadoDto == null)
    {
        return NotFound();
    }
    var Empleado = _mapper.Map<Empleado>(EmpleadoDto);
    _unitOfWork.Empleados.Update(Empleado);
    await _unitOfWork.SaveAsync();
    return EmpleadoDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<EmpleadoDto>> Delete(int id)
{
    var Empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
    if (Empleado == null)
    {
        return NotFound();
    }
    _unitOfWork.Empleados.Remove(Empleado);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}