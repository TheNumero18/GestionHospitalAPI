using Dapper;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;
using MySql.Data.MySqlClient;

namespace GestionHospitalAPI.Repositories;

public class PacienteRepository : IPacienteRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public PacienteRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task<bool> BorrarPaciente(int idPaciente)
    {
        string query = @"DELETE FROM pacientes WHERE Id = @Id";

        var result = await _mySqlConnection.ExecuteAsync(query, new{Id = idPaciente});

        return result > 0;
    }

    public async Task<PacienteResponse> CrearPaciente(PacienteRequest paciente)
    {
        string query = @"INSERT INTO pacientes(DNI, Nombre, Apellido, FechaNacimiento, Sexo, Telefono, Email)
                         VALUES(@DNI, @Nombre, @Apellido, @FechaNacimiento, @Sexo, @Telefono, @Email);
                         SELECT LAST_INSERT_ID()";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new { paciente.DNI, paciente.Nombre, paciente.Apellido,
            paciente.FechaNacimiento, paciente.Sexo, paciente.Telefono, paciente.Email});

        PacienteResponse pacienteResponse = new ()
        {
            Id = result,
            DNI = paciente.DNI,
            Nombre = paciente.Nombre,
            Apellido = paciente.Apellido,
            FechaNacimiento = paciente.FechaNacimiento,
            Sexo = paciente.Sexo,
            Telefono = paciente.Telefono,
            Email = paciente.Email
        };

        return pacienteResponse;
    }

    public async Task<PacienteResponse> EditarPaciente(PacienteRequest paciente, int id)
    {
        string query = @"UPDATE pacientes
                            SET DNI = @DNI,
                                Nombre = @Nombre, 
                                Apellido = @Apellido,
                                FechaNacimiento = @FechaNacimiento,
                                Sexo = @Sexo,
                                Telefono = @Telefono,
                                Email = @Email
                         WHERE Id = @Id;
                         SELECT Id FROM pacientes ORDER BY Id DESC LIMIT 1";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new
        {
            paciente.DNI,
            paciente.Nombre,
            paciente.Apellido,
            paciente.FechaNacimiento,
            paciente.Sexo,
            paciente.Telefono,
            paciente.Email,
            Id = id,
        });

        PacienteResponse pacienteResponse = new ()
        {
            Id = result,
            DNI = paciente.DNI,
            Nombre = paciente.Nombre,
            Apellido = paciente.Apellido,
            FechaNacimiento = paciente.FechaNacimiento,
            Sexo = paciente.Sexo,
            Telefono = paciente.Telefono,
            Email = paciente.Email
        };

        return pacienteResponse;
    }

    public async Task<PacienteResponse> Paciente(int idPaciente)
    {
        string query = @"SELECT * FROM pacientes WHERE Id = @Id";

        return await _mySqlConnection.QueryFirstOrDefaultAsync<PacienteResponse>(query, new { Id = idPaciente});
    }

    public async Task<IEnumerable<PacienteResponse>> Pacientes()
    {
        string query = @"SELECT * FROM pacientes";

        return await _mySqlConnection.QueryAsync<PacienteResponse>(query, new { });
    }
}
