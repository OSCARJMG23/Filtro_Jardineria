using ApiJardineria.Dtos;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;
namespace ApiJardineria.Controllers
{
public class ProductoController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public ProductoController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<ProductoDto>>> Get()
{
    var Producto = await _unitOfWork.Productos.GetAllAsync();
    return _mapper.Map<List<ProductoDto>>(Producto);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ProductoDto>> Get(int id)
{
    var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
    return _mapper.Map<ProductoDto>(Producto);
}

[HttpGet("productosnuncapedidos")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetProductosNoPedidos()
{
    var Producto = await _unitOfWork.Productos.GetProductosNoPedidos();
    return Ok(Producto);
}

[HttpGet("productosfacturado3000")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetProductosFacturado3000()
{
    var Producto = await _unitOfWork.Productos.GetProductoFacturado3000();
    return Ok(Producto);
}

[HttpGet("productovendidoMasUnidades")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetProductoVendidoMasUnidades()
{
    var Producto = await _unitOfWork.Productos.GetProductoVendidoMasUnidades();
    return Ok(Producto);
}

[HttpGet("productomasvendidoytotalunidades")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<object>>> GetProductoMasVendidoytotalUnidades()
{
    var Producto = await _unitOfWork.Productos.GetProductosMasVendidosYTotalUnidades();
    return Ok(Producto);
}


[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Producto>> Post(ProductoDto ProductoDto)
{
    var Producto = _mapper.Map<Producto>(ProductoDto);
    _unitOfWork.Productos.Add(Producto);
    await _unitOfWork.SaveAsync();

    if (Producto == null)
    {
        return BadRequest();
    }
    Producto.CodigoProducto = Producto.CodigoProducto;
    return CreatedAtAction(nameof(Post), new { id = Producto.CodigoProducto }, Producto);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ProductoDto>> Put(int id, [FromBody]ProductoDto ProductoDto)
{
    if (ProductoDto == null)
    {
        return NotFound();
    }
    var Producto = _mapper.Map<Producto>(ProductoDto);
    _unitOfWork.Productos.Update(Producto);
    await _unitOfWork.SaveAsync();
    return ProductoDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<ProductoDto>> Delete(int id)
{
    var Producto = await _unitOfWork.Productos.GetByIdAsync(id);
    if (Producto == null)
    {
        return NotFound();
    }
    _unitOfWork.Productos.Remove(Producto);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}