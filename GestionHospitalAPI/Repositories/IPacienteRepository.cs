using GestionHospital.Models;

namespace GestionHospitalAPI.Repositories;

public interface IPacienteRepository
{     
    public Task<Paciente> CrearPaciente(Paciente paciente);
    public Task<Paciente> EditarPaciente(Paciente paciente);
    public Task<IEnumerable<Paciente>> Pacientes();
    public Task<Paciente> Paciente(int idPaciente);
    public Task<bool> BorrarPaciente(int idPaciente);
}
