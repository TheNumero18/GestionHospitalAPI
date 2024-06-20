namespace GestionHospitalAPI.Model;

public class MySqlConfiguration
{
    public string ConnectionString { get; set; }

    public MySqlConfiguration(string conecctionString)
    {
        ConnectionString = conecctionString;
    }
}
