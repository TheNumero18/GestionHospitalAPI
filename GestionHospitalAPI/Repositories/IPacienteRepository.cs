using GestionHospital.Models;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;

namespace GestionHospitalAPI.Repositories;

public interface IPacienteRepository
{     
    public Task<PacienteResponse> CrearPaciente(PacienteRequest paciente);
    public Task<PacienteResponse> EditarPaciente(PacienteRequest paciente, int id);
    public Task<IEnumerable<PacienteResponse>> Pacientes();
    public Task<PacienteResponse> Paciente(int idPaciente);
    public Task<bool> BorrarPaciente(int idPaciente);
}
