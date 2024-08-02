using System.Data.SqlClient;

namespace MKKDotNetCore.ConsoleApp;

internal static class ConnectionString
{
    public static SqlConnectionStringBuilder StringBuilder = new()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sasa@123"
    };
}