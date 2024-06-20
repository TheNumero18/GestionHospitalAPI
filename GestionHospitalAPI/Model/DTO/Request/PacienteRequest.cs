namespace GestionHospitalAPI.Model.DTO.Request;

public class PacienteRequest
{    public string DNI { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string Sexo { get; set; }
    public string Telefono { get; set; }
    public string Email { get; set; }
}
