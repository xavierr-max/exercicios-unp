using CalMedAcad.Api.Models;
using CalMedAcad.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalMedAcad.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
{
    private readonly CalculadoraMediaService _service;

    public MediaController(
        CalculadoraMediaService service)
    {
        _service = service;
    }

    [HttpPost("calcular")]
    public ActionResult<CalculoMediaResponse> Calcular(
        CalculoMediaRequest request)
    {
        try
        {
            CalculoMediaResponse resultado =
                _service.Calcular(request);

            return Ok(resultado);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            return BadRequest(new
            {
                erro = ex.Message
            });
        }
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok(new
        {
            status = "ok",
            mensagem = "API funcionando."
        });
    }
}