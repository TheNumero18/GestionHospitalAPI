using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionHospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly IPacienteRepository _pacienteRepository;

    public PacientesController(IPacienteRepository pacienteRepository)
    {
        _pacienteRepository = pacienteRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetPacientes()
    {
        return Ok(await _pacienteRepository.Pacientes());
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetPaciente(int Id)
    {
        return Ok(await _pacienteRepository.Paciente(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CrearPaciente([FromBody] PacienteRequest paciente)
    {
        if (paciente is null)
            return BadRequest();
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _pacienteRepository.CrearPaciente(paciente);
        return Created("created", created);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> EditarPaciente(int Id, [FromBody] PacienteRequest paciente)
    {
        if (paciente is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _pacienteRepository.EditarPaciente(paciente, Id);
        return Ok(created);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> BorrarPaciente(int Id)
    {
        return Ok(await _pacienteRepository.BorrarPaciente(Id));
    }
}
