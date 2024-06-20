using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionHospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiagnosticosController : ControllerBase
{
    private readonly IDiagnosticoRepository _diagnosticoRepository;

    public DiagnosticosController(IDiagnosticoRepository diagnosticoRepository)
    {
        _diagnosticoRepository = diagnosticoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetDiagnosticos()
    {
        return Ok(await _diagnosticoRepository.Diagnosticos());
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetDiagnostico(int Id)
    {
        return Ok(await _diagnosticoRepository.Diagnostico(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CrearDiagnostico([FromBody] DiagnosticoRequest diagnostico)
    {
        if (diagnostico is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _diagnosticoRepository.CrearDiagnostico(diagnostico);
        return Created("created", created);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> EditarDiagnostico(int Id, [FromBody] DiagnosticoRequest diagnostico)
    {
        if (diagnostico is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _diagnosticoRepository.EditarDiagnostico(diagnostico, Id);
        return Ok(created);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> BorrarDiagnostico(int Id)
    {
        return Ok(await _diagnosticoRepository.BorrarDiagnostico(Id));
    }
}
