using GestionHospital.Models;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;

namespace GestionHospitalAPI.Repositories;

public interface IMedicoRepository
{
    public Task<MedicoResponse> CrearMedico(MedicoRequest medico);
    public Task<MedicoResponse> EditarMedico(MedicoRequest medico, int id);
    public Task<IEnumerable<MedicoResponse>> Medicos();
    public Task<MedicoResponse> Medico(int idMedico);
    public Task<bool> BorrarMedico(int idMedico);
}
