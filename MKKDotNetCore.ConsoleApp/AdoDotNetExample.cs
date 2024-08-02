using System.Data;
using System.Data.SqlClient;

namespace MKKDotNetCore.ConsoleApp;

public class AdoDotNetExample
{
    private SqlConnectionStringBuilder _stringBuilder = new()
    {
        DataSource = "local",
        InitialCatalog = "DotNetTrainingBatch4",
        UserID = "sa",
        Password = "sasa@123"
    };

    public void Run()
    {
        ReadAll();
        return;
    }

    public void ReadAll()
    {
        var connection = new SqlConnection(_stringBuilder.ConnectionString);

        connection.Open();

        const string query = @"SELECT * From tbl_blog";

        var cmd = new SqlCommand(query, connection);

        var sqlDataAdaptor = new SqlDataAdapter(cmd);

        var dt = new DataTable();

        sqlDataAdaptor.Fill(dt);

        connection.Close();

        foreach (DataRow row in dt.Rows)
        {
            Console.WriteLine("id ==> " + row["BlogId"]);
            Console.WriteLine("title ==> " + row["BlogTitle"]);
            Console.WriteLine("author ==> " + row["BlogAuthor"]);
            Console.WriteLine("content ==> " + row["BlogContent"]);
            Console.WriteLine("-------------------------");
            Console.WriteLine("");
        }
    }

    public void ReadOne(int id)
    {
        var connection = new SqlConnection(_stringBuilder.ConnectionString);

        connection.Open();

        const string query = @"SELECT * FROM Tbl_Blog WHERE BlogId=@BlogId";

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogId", id);

        var sqlDataAdaptor = new SqlDataAdapter(cmd);

        var dt = new DataTable();

        sqlDataAdaptor.Fill(dt);

        connection.Close();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        var row = dt.Rows[0];

        Console.WriteLine("id ==> " + row["BlogId"]);
        Console.WriteLine("title ==> " + row["BlogTitle"]);
        Console.WriteLine("author ==> " + row["BlogAuthor"]);
        Console.WriteLine("content ==> " + row["BlogContent"]);
        Console.WriteLine("-------------------------");
        Console.WriteLine("");
    }

    public void CreateOne(string title, string author, string content)
    {
        var connection = new SqlConnection(_stringBuilder.ConnectionString);

        connection.Open();

        const string query = @"
        INSERT INTO [dbo].[Tbl_Blog] ([BlogTitle], [BlogAuthor], [BlogContent]) VALUES (@BlogTitle, @BlogAuthor, @BlogContent)
";

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);

        var affectedRows = cmd.ExecuteNonQuery();

        connection.Close();

        var message = affectedRows > 0 ? "Successfully created" : "creating Failed";

        Console.WriteLine(message);
    }

    public void DeleteOne(int id)
    {
        var connection = new SqlConnection(_stringBuilder.ConnectionString);

        connection.Open();

        const string query = @"
        DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId
";

        var cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogId", id);

        var affectedRows = cmd.ExecuteNonQuery();

        connection.Close();

        var message = affectedRows > 0 ? "Successfully Deleted" : "Delete Failed";

        Console.WriteLine(message);
    }

    public void UpdateOne(int id, string title, string author, string content)
    {
        var connection = new SqlConnection(_stringBuilder.ConnectionString);
        
        connection.Open();
        
        const string readQuery = @"
        SELECT * FROM [dbo].[Tbl_Blog] WHERE BlogId=@BlogId
";
        
        var cmd1 = new SqlCommand(readQuery, connection);
        
        cmd1.Parameters.AddWithValue("@BlogId", id);
        
        var sqlDataAdaptor = new SqlDataAdapter(cmd1);
        
        var dt = new DataTable();
        
        sqlDataAdaptor.Fill(dt);
        
        if (dt.Rows.Count == 0)
        {
            connection.Close();
            Console.WriteLine("No Data Found");
            return;
        }
        
        const string updateQuery = @"
        UPDATE [dbo].[Tbl_Blog] 
        SET 
            [BlogTitle]=@BlogTitle, 
            [BlogAuthor]=@BlogAuthor, 
            [BlogContent]=@BlogContent 
        WHERE 
            BlogId=@BlogId
";
        
        var cmd2 = new SqlCommand(updateQuery, connection);
        
        cmd2.Parameters.AddWithValue("@BlogId", id);
        cmd2.Parameters.AddWithValue("@BlogTitle", title);
        cmd2.Parameters.AddWithValue("@BlogAuthor", author);
        cmd2.Parameters.AddWithValue("@BlogContent", content);
        
        var affectedRows = cmd2.ExecuteNonQuery();
        
        connection.Close();
        
        var message = affectedRows > 0 ? "Successfully Updated" : "Update Failed";
        
        Console.WriteLine(message);
    }
}