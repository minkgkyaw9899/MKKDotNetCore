using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace MKKDotNetCore.Shared;

public class AdoDotNetService
{
    private readonly string _connectionString;

    public AdoDotNetService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T>? Query<T>(string query)
    {
        var connection = new SqlConnection(_connectionString);

        connection.Open();

        var command = new SqlCommand(query, connection);

        var adaptor = new SqlDataAdapter();

        var dt = new DataTable();

        adaptor.Fill(dt);

        connection.Close();

        var json = JsonConvert.SerializeObject(dt);

        var result = JsonConvert.DeserializeObject<List<T>>(json);

        return result;
    }

    public T? QueryOne<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        var connection = new SqlConnection(_connectionString);

        connection.Open();

        var command = new SqlCommand(query, connection);

        if (parameters is not null && parameters.Length > 0)
        {
            var paramArray = parameters.Select(i => new SqlParameter(i.Name, i.Value)).ToArray();

            command.Parameters.AddRange(paramArray);
        }

        var adaptor = new SqlDataAdapter(command);

        var dt = new DataTable();

        adaptor.Fill(dt);

        connection.Close();

        var json = JsonConvert.SerializeObject(dt);

        var result = JsonConvert.DeserializeObject<List<T>>(json);

        if (result is null || result.Count == 0)
        {
            return default;
        }

        return result[0];
    }

    public int Execute(string query, params AdoDotNetParameter[]? parameters)
    {
        var connection = new SqlConnection(_connectionString);
        
        connection.Open();
        
        var command = new SqlCommand(query, connection);

        if (parameters is not null && parameters.Length > 0)
        {
            var paramsArr = parameters.Select(item => new SqlParameter(item.Name, item.Value)).ToArray();
            
            command.Parameters.AddRange(paramsArr);
        }

        var result = command.ExecuteNonQuery();
            
        connection.Close();

        return result;
    }
}

public class AdoDotNetParameter
{
    public AdoDotNetParameter(string name, object value)
    {
        Name = name;
        Value = value;
    }

    public string Name { get; set; }
    public object Value { get; set; }
}