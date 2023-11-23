using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
namespace ApiJardineria.Controllers
{
public class DetallePedidoController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public DetallePedidoController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<DetallePedidoDto>>> Get()
{
    var DetallePedido = await _unitOfWork.DetallePedidos.GetAllAsync();
    return _mapper.Map<List<DetallePedidoDto>>(DetallePedido);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DetallePedidoDto>> Get(int id)
{
    var DetallePedido = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
    return _mapper.Map<DetallePedidoDto>(DetallePedido);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DetallePedido>> Post(DetallePedidoDto DetallePedidoDto)
{
    var DetallePedido = _mapper.Map<DetallePedido>(DetallePedidoDto);
    _unitOfWork.DetallePedidos.Add(DetallePedido);
    await _unitOfWork.SaveAsync();

    if (DetallePedido == null)
    {
        return BadRequest();
    }
    DetallePedido.CodigoPedido = DetallePedido.CodigoPedido;
    return CreatedAtAction(nameof(Post), new { id = DetallePedido.CodigoPedido }, DetallePedido);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DetallePedidoDto>> Put(int id, [FromBody]DetallePedidoDto DetallePedidoDto)
{
    if (DetallePedidoDto == null)
    {
        return NotFound();
    }
    var DetallePedido = _mapper.Map<DetallePedido>(DetallePedidoDto);
    _unitOfWork.DetallePedidos.Update(DetallePedido);
    await _unitOfWork.SaveAsync();
    return DetallePedidoDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DetallePedidoDto>> Delete(int id)
{
    var DetallePedido = await _unitOfWork.DetallePedidos.GetByIdAsync(id);
    if (DetallePedido == null)
    {
        return NotFound();
    }
    _unitOfWork.DetallePedidos.Remove(DetallePedido);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}