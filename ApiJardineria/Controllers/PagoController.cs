using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
namespace ApiJardineria.Controllers
{
public class PagoController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public PagoController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<PagoDto>>> Get()
{
    var Pago = await _unitOfWork.Pagos.GetAllAsync();
    return _mapper.Map<List<PagoDto>>(Pago);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PagoDto>> Get(int id)
{
    var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
    return _mapper.Map<PagoDto>(Pago);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Pago>> Post(PagoDto PagoDto)
{
    var Pago = _mapper.Map<Pago>(PagoDto);
    _unitOfWork.Pagos.Add(Pago);
    await _unitOfWork.SaveAsync();

    if (Pago == null)
    {
        return BadRequest();
    }
    Pago.IdTransaccion = Pago.IdTransaccion;
    return CreatedAtAction(nameof(Post), new { id = Pago.IdTransaccion }, Pago);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PagoDto>> Put(int id, [FromBody]PagoDto PagoDto)
{
    if (PagoDto == null)
    {
        return NotFound();
    }
    var Pago = _mapper.Map<Pago>(PagoDto);
    _unitOfWork.Pagos.Update(Pago);
    await _unitOfWork.SaveAsync();
    return PagoDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PagoDto>> Delete(int id)
{
    var Pago = await _unitOfWork.Pagos.GetByIdAsync(id);
    if (Pago == null)
    {
        return NotFound();
    }
    _unitOfWork.Pagos.Remove(Pago);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}