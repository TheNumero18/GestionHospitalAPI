using GestionHospital.Models;

namespace GestionHospitalAPI.Model.DTO.Response;

public class DiagnosticoResponse
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Diagnostico { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
}
