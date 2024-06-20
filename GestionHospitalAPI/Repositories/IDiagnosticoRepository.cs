using GestionHospital.Models;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;

namespace GestionHospitalAPI.Repositories;

public interface IDiagnosticoRepository
{
    public Task<DiagnosticoResponse> CrearDiagnostico(DiagnosticoRequest diagnostico);
    public Task<DiagnosticoResponse> EditarDiagnostico(DiagnosticoRequest diagnostico, int id);
    public Task<IEnumerable<DiagnosticoFullResponse>> Diagnosticos();
    public Task<DiagnosticoFullResponse> Diagnostico(int idDiagnostico);
    public Task<bool> BorrarDiagnostico(int idDiagnostico);
}
