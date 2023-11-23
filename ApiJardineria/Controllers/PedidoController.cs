using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
namespace ApiJardineria.Controllers
{
public class PedidoController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public PedidoController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<PedidoDto>>> Get()
{
    var Pedido = await _unitOfWork.Pedidos.GetAllAsync();
    return _mapper.Map<List<PedidoDto>>(Pedido);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PedidoDto>> Get(int id)
{
    var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
    return _mapper.Map<PedidoDto>(Pedido);
}

[HttpGet("pedidosnoentregadosatiempo")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetPediddosNoEntregaTiempo()
{
    var Pedido = await _unitOfWork.Pedidos.GetPedidosNoEntregadosAtiempo();
    return Ok(Pedido);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Pedido>> Post(PedidoDto PedidoDto)
{
    var Pedido = _mapper.Map<Pedido>(PedidoDto);
    _unitOfWork.Pedidos.Add(Pedido);
    await _unitOfWork.SaveAsync();

    if (Pedido == null)
    {
        return BadRequest();
    }
    Pedido.CodigoPedido = Pedido.CodigoPedido;
    return CreatedAtAction(nameof(Post), new { id = Pedido.CodigoPedido }, Pedido);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PedidoDto>> Put(int id, [FromBody]PedidoDto PedidoDto)
{
    if (PedidoDto == null)
    {
        return NotFound();
    }
    var Pedido = _mapper.Map<Pedido>(PedidoDto);
    _unitOfWork.Pedidos.Update(Pedido);
    await _unitOfWork.SaveAsync();
    return PedidoDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PedidoDto>> Delete(int id)
{
    var Pedido = await _unitOfWork.Pedidos.GetByIdAsync(id);
    if (Pedido == null)
    {
        return NotFound();
    }
    _unitOfWork.Pedidos.Remove(Pedido);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}