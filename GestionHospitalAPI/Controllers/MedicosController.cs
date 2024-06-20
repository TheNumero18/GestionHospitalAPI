using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GestionHospitalAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicosController : ControllerBase
{
    private readonly IMedicoRepository _medicoRepository;

    public MedicosController(IMedicoRepository medicoRepository)
    {
        _medicoRepository = medicoRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMedicos()
    {
        return Ok(await _medicoRepository.Medicos());
    }

    [HttpGet("{Id}")]
    public async Task<IActionResult> GetMedico(int Id)
    {
        return Ok(await _medicoRepository.Medico(Id));
    }

    [HttpPost]
    public async Task<IActionResult> CrearMedico([FromBody] MedicoRequest medico)
    {
        if (medico is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _medicoRepository.CrearMedico(medico);
        return Created("created", created);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> EditarMedico(int Id, [FromBody] MedicoRequest medico)
    {
        if (medico is null)
            return BadRequest();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var created = await _medicoRepository.EditarMedico(medico, Id);
        return Ok(created);
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> BorrarMedico(int Id)
    {
        return Ok(await _medicoRepository.BorrarMedico(Id));
    }
}
