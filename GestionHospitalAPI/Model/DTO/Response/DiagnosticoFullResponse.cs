namespace GestionHospitalAPI.Model.DTO.Response;

public class DiagnosticoFullResponse
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Diagnostico { get; set; }
    public int PacienteId { get; set; }
    public string NombrePaciente { get; set; }
    public int MedicoId { get; set; }
    public string NombreMedico { get; set; }
}
