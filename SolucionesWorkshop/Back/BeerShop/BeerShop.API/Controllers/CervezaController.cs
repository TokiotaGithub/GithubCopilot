using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CervezaController : ControllerBase
{
    private readonly ICervezaService _cervezaService;

    public CervezaController(ICervezaService cervezaService)
    {
        _cervezaService = cervezaService;
    }

    [HttpGet]
    public IActionResult GetCervezas(int offset, int limit)
    {
        var cervezas = _cervezaService.GetCervezas(offset, limit);
        return Ok(cervezas);
    }

    [HttpGet("{id}")]
    public IActionResult GetCerveza(int id)
    {
        var cerveza = _cervezaService.GetCerveza(id);
        if (cerveza == null)
        {
            return NotFound();
        }
        return Ok(cerveza);
    }

    [HttpGet("search")]
    public IActionResult SearchCervezas(string? nombre, string? descripcion, string? eslogan, string? maridaje, decimal? precio)
    {
        var cervezas = _cervezaService.SearchCervezas(nombre, descripcion, eslogan, maridaje, precio);
        return Ok(cervezas);
    }

}
