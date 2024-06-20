namespace GestionHospitalAPI.Model.DTO.Request;

public class DiagnosticoRequest
{
    public DateTime Fecha { get; set; }
    public string Diagnostico { get; set; }
    public int PacienteId { get; set; }
    public int MedicoId { get; set; }
}
