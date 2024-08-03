using Microsoft.Data.SqlClient;

namespace MKKDotNetCore.RestApi.Services;

public static class SqlConnectionBuilder
{
    public static readonly SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sasa@123",
        // TrustServerCertificate = true
    };
}