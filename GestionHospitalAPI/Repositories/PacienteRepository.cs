using Dapper;
using GestionHospital.Models;
using GestionHospitalAPI.Model;
using MySql.Data.MySqlClient;

namespace GestionHospitalAPI.Repositories;

public class PacienteRepository : IPacienteRepository
{
    //private readonly Model.MySqlConfiguration _mySqlConfiguration;
    private readonly MySqlConnection _mySqlConnection;

    public PacienteRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    //protected MySqlConnection DbConnection()
    //{
    //    return new MySqlConnection(_mySqlConfiguration.ConnectionString);
    //}
    public async Task<bool> BorrarPaciente(int idPaciente)
    {
        string query = @"DELETE FROM pacientes WHERE Id = @Id";

        var result = await _mySqlConnection.ExecuteAsync(query, new{Id = idPaciente});

        return result > 0;
    }

    public async Task<Paciente> CrearPaciente(Paciente paciente)
    {
        string query = @"INSERT INTO pacientes(DNI, Nombre, Apellido, FechaNacimiento, Sexo, Telefono, Email)
                         VALUES(@DNI, @Nombre, @Apellido, @FechaNacimiento, @Sexo, @Telefono, @Email)";

        var result = await _mySqlConnection.ExecuteAsync(query, new { paciente.DNI, paciente.Nombre, paciente.Apellido,
            paciente.FechaNacimiento, paciente.Sexo, paciente.Telefono, paciente.Email});

        paciente.Id = result;
        return paciente;
    }

    public async Task<Paciente> EditarPaciente(Paciente paciente)
    {
        string query = @"UPDATE pacientes
                            SET DNI = @DNI,
                                Nombre = @Nombre, 
                                Apellido = @Apellido,
                                FechaNacimiento = @FechaNacimiento,
                                Sexo = @Sexo,
                                Telefono = @Telefono,
                                Email = @Email
                         WHERE Id = @Id";

        var result = await _mySqlConnection.ExecuteAsync(query, new
        {
            paciente.DNI,
            paciente.Nombre,
            paciente.Apellido,
            paciente.FechaNacimiento,
            paciente.Sexo,
            paciente.Telefono,
            paciente.Email,
            paciente.Id
        });

        return paciente;
    }

    public async Task<Paciente> Paciente(int idPaciente)
    {
        string query = @"SELECT * FROM pacientes WHERE Id = @Id";

        return await _mySqlConnection.QueryFirstOrDefaultAsync<Paciente>(query, new { Id = idPaciente});
    }

    public async Task<IEnumerable<Paciente>> Pacientes()
    {
        string query = @"SELECT * FROM pacientes";

        return await _mySqlConnection.QueryAsync<Paciente>(query, new { });
    }
}
