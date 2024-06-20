using Dapper;
using GestionHospital.Models;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;
using MySql.Data.MySqlClient;

namespace GestionHospitalAPI.Repositories;

public class DiagnosticoRepository : IDiagnosticoRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public DiagnosticoRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task<bool> BorrarDiagnostico(int idDiagnostico)
    {
        string query = @"DELETE FROM diagnosticos WHERE Id = @Id";

        var result = await _mySqlConnection.ExecuteAsync(query, new { Id = idDiagnostico });

        return result > 0;
    }

    public async Task<DiagnosticoResponse> CrearDiagnostico(DiagnosticoRequest diagnostico)
    {
        string query = @"INSERT INTO diagnosticos(Fecha, Diagnostico, PacienteId, MedicoId)
                         VALUES(@Fecha, @Diagnostico, @PacienteId, @MedicoId);
                         SELECT LAST_INSERT_ID()";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new
        {
            diagnostico.Fecha,
            diagnostico.Diagnostico,
            diagnostico.PacienteId,
            diagnostico.MedicoId
        });

        DiagnosticoResponse diagnosticoResponse = new()
        {
            Id = result,
            Fecha = diagnostico.Fecha,
            Diagnostico = diagnostico.Diagnostico,
            PacienteId = diagnostico.PacienteId,
            MedicoId = diagnostico.MedicoId
        };

        return diagnosticoResponse;
    }

    public async Task<DiagnosticoFullResponse> Diagnostico(int idDiagnostico)
    {
        string query = @"SELECT d.Id, d.Fecha, d.Diagnostico, d.PacienteId, d.MedicoId, p.Apellido + ', ' + p.Nombre AS NombrePaciente
	                        , m.Apellido + ', ' + m.Nombre AS NombreMedico
                         FROM diagnosticos AS d
	                         INNER JOIN pacientes AS p ON d.PacienteId = p.ID
	                         INNER JOIN medicos AS m ON m.ID = d.MedicoId
                         WHERE d.Id = @Id";

        return await _mySqlConnection.QueryFirstOrDefaultAsync<DiagnosticoFullResponse>(query, new { Id = idDiagnostico });
    }

    public async Task<IEnumerable<DiagnosticoFullResponse>> Diagnosticos()
    {
        string query = @"SELECT d.Id, d.Fecha, d.Diagnostico, d.PacienteId, d.MedicoId, p.Apellido + ', ' + p.Nombre AS NombrePaciente
	                        , m.Apellido + ', ' + m.Nombre AS NombreMedico
                         FROM diagnosticos AS d
	                         INNER JOIN pacientes AS p ON d.PacienteId = p.ID
	                         INNER JOIN medicos AS m ON m.ID = d.MedicoId";

        return await _mySqlConnection.QueryAsync<DiagnosticoFullResponse>(query, new { });
    }

    public async Task<DiagnosticoResponse> EditarDiagnostico(DiagnosticoRequest diagnostico, int id)
    {
        string query = @"UPDATE diagnosticos
                            SET Fecha = @Fecha,
                                Diagnostico = @Diagnostico,
                                PacienteId = @PacienteId,
                                MedicoId = @MedicoId;
                         SELECT Id FROM diagnosticos ORDER BY Id DESC LIMIT 1";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new
        {
            diagnostico.Fecha,
            diagnostico.Diagnostico,
            diagnostico.PacienteId,
            diagnostico.MedicoId,
            Id = id,
        });

        DiagnosticoResponse diagnosticoResponse = new()
        {
            Id = result,
            Fecha = diagnostico.Fecha,
            Diagnostico = diagnostico.Diagnostico,
            PacienteId = diagnostico.PacienteId,
            MedicoId = diagnostico.MedicoId
        };

        return diagnosticoResponse;
    }
}
