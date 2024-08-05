namespace MKKDotNetCore.ApiWithNLayer.Services;

public static class ConnectionStringBuilder
{
    private static readonly SqlConnectionStringBuilder SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sasa@123",
        TrustServerCertificate = true
    };

    public static readonly string SqlConnectionString = SqlConnectionStringBuilder.ConnectionString;
}