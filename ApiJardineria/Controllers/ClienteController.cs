using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;

namespace ApiJardineria.Controllers
{
public class ClienteController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public ClienteController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
{
    var Cliente = await _unitOfWork.Clientes.GetAllAsync();
    return _mapper.Map<List<ClienteDto>>(Cliente);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ClienteDto>> Get(int id)
{
    var Cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
    return _mapper.Map<ClienteDto>(Cliente);
}

[HttpGet("clienteytotalpedidos")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetClientesYTpedidos()
{
    var Cliente = await _unitOfWork.Clientes.GetClientesYTotalPedidos();
    return Ok(Cliente);
}

[HttpGet("clienteentregadofueratiempo")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetClientesentregadoFueratiempo()
{
    var Cliente = await _unitOfWork.Clientes.GetClientesEntregadoFueraDeTiempo();
    return Ok(Cliente);
}

[HttpGet("clientesygamascomprado")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetClientesGamaComprado()
{
    var Cliente = await _unitOfWork.Clientes.GetClienteGamasHaComprado();
    return Ok(Cliente);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Cliente>> Post(ClienteDto ClienteDto)
{
    var Cliente = _mapper.Map<Cliente>(ClienteDto);
    _unitOfWork.Clientes.Add(Cliente);
    await _unitOfWork.SaveAsync();

    if (Cliente == null)
    {
        return BadRequest();
    }
    Cliente.CodigoCliente = Cliente.CodigoCliente;
    return CreatedAtAction(nameof(Post), new { id = Cliente.CodigoCliente }, Cliente);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody]ClienteDto ClienteDto)
{
    if (ClienteDto == null)
    {
        return NotFound();
    }
    var Cliente = _mapper.Map<Cliente>(ClienteDto);
    _unitOfWork.Clientes.Update(Cliente);
    await _unitOfWork.SaveAsync();
    return ClienteDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ClienteDto>> Delete(int id)
{
    var Cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
    if (Cliente == null)
    {
        return NotFound();
    }
    _unitOfWork.Clientes.Remove(Cliente);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}