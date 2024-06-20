using GestionHospital.Models;
using GestionHospitalAPI.Repositories;
using Microsoft.AspNetCore.Http;
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
    public async Task<IActionResult> CrearPaciente([FromBody] Paciente paciente)
    {
        if (paciente is null)
            return BadRequest();
        
        if(!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _pacienteRepository.CrearPaciente(paciente);
        return Created("created", created);
    }

    [HttpPut]
    public async Task<IActionResult> EditarPaciente([FromBody] Paciente paciente)
    {
        if (paciente is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _pacienteRepository.EditarPaciente(paciente);
        return Ok(created);
    }
}
