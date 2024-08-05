using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace MKKDotNetCore.Shared;

public class DapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<T> QueryAll<T>(string query, object? parameter = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var list = db.Query<T>(query, parameter).ToList();

        return list;
    }

    public T? QueryOne<T>(string query, object? parameter = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var item = db.QueryFirstOrDefault<T>(query, parameter);

        return item;
    }

    public int Execute(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);

        var result = db.Execute(query, param);

        return result;
    }
}