using Dapper;
using GestionHospital.Models;
using GestionHospitalAPI.Model.DTO.Request;
using GestionHospitalAPI.Model.DTO.Response;
using MySql.Data.MySqlClient;

namespace GestionHospitalAPI.Repositories;

public class MedicoRepository : IMedicoRepository
{
    private readonly MySqlConnection _mySqlConnection;

    public MedicoRepository(MySqlConnection mySqlConnection)
    {
        _mySqlConnection = mySqlConnection;
    }

    public async Task<bool> BorrarMedico(int idMedico)
    {
        string query = @"DELETE FROM medicos WHERE Id = @Id";

        var result = await _mySqlConnection.ExecuteAsync(query, new { Id = idMedico });

        return result > 0;
    }

    public async Task<MedicoResponse> CrearMedico(MedicoRequest medico)
    {
        string query = @"INSERT INTO medicos(DNI, Nombre, Apellido, FechaNacimiento, Sexo, Telefono, Email, Especialidad, Domicilio)
                         VALUES(@DNI, @Nombre, @Apellido, @FechaNacimiento, @Sexo, @Telefono, @Email, @Especialidad, @Domicilio);
                         SELECT LAST_INSERT_ID()";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new
        {
            medico.DNI,
            medico.Nombre,
            medico.Apellido,
            medico.FechaNacimiento,
            medico.Sexo,
            medico.Telefono,
            medico.Email,
            medico.Especialidad,
            medico.Domicilio
        });

        MedicoResponse medicoResponse = new()
        {
            Id = result,
            DNI = medico.DNI,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            FechaNacimiento = medico.FechaNacimiento,
            Sexo = medico.Sexo,
            Telefono = medico.Telefono,
            Email = medico.Email,
            Especialidad = medico.Especialidad,
            Domicilio = medico.Domicilio
        };

        return medicoResponse;
    }

    public async Task<MedicoResponse> EditarMedico(MedicoRequest medico, int id)
    {
        string query = @"UPDATE medicos
                            SET DNI = @DNI,
                                Nombre = @Nombre, 
                                Apellido = @Apellido,
                                FechaNacimiento = @FechaNacimiento,
                                Sexo = @Sexo,
                                Telefono = @Telefono,
                                Email = @Email,
                                Especialidad = @Especialidad,
                                Domicilio = @Domicilio
                         WHERE Id = @Id;
                         SELECT Id FROM medicos ORDER BY Id DESC LIMIT 1";

        var result = await _mySqlConnection.QuerySingleAsync<int>(query, new
        {
            medico.DNI,
            medico.Nombre,
            medico.Apellido,
            medico.FechaNacimiento,
            medico.Sexo,
            medico.Telefono,
            medico.Email,
            medico.Especialidad,
            medico.Domicilio,
            Id = id,
        });

        MedicoResponse medicoResponse = new()
        {
            Id = result,
            DNI = medico.DNI,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            FechaNacimiento = medico.FechaNacimiento,
            Sexo = medico.Sexo,
            Telefono = medico.Telefono,
            Email = medico.Email,
            Especialidad = medico.Especialidad,
            Domicilio = medico.Domicilio
        };

        return medicoResponse;
    }

    public async Task<MedicoResponse> Medico(int idMedico)
    {
        string query = @"SELECT * FROM medicos WHERE Id = @Id";

        return await _mySqlConnection.QueryFirstOrDefaultAsync<MedicoResponse>(query, new { Id = idMedico });
    }

    public async Task<IEnumerable<MedicoResponse>> Medicos()
    {
        string query = @"SELECT * FROM medicos";

        return await _mySqlConnection.QueryAsync<MedicoResponse>(query, new { });
    }
}
