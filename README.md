# DotNet Training Betch 4

```bash
Scaffold-DbContext "Server=.; Database=DotNetTrainingBatch4; User Id=sa; Password=sasa@123; TrustServerCertificate=True; Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer --OutputDir Models -Context AppDbContext -Force

# terminal cmd
dotnet ef dbcontext scaffold "Server=.;Database=DotNetTrainingBatch4;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f
```