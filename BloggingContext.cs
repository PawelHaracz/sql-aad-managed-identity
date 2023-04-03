using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
    public BloggingContext()
    {
        
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    { 
        //ref: https://learn.microsoft.com/en-us/azure/app-service/tutorial-connect-msi-azure-database?tabs=sqldatabase%2Csystemassigned%2Cdotnet%2Cwindowsclient
        var serverName = Environment.GetEnvironmentVariable("SERVERNAME");
        var database = Environment.GetEnvironmentVariable("DATABASE");
        //var clientId = Environment.GetEnvironmentVariable("CLIENTID");
        var connection = new SqlConnection($"Server=tcp:{serverName}.database.windows.net;Database={database};Authentication=Active Directory Default;TrustServerCertificate=True");
        //var connection = new SqlConnection($"Server=tcp:{serverName}.database.windows.net;Database={database};Authentication=Active Directory Default;User Id={clientId};TrustServerCertificate=True"); // user-assigned identity
        
        // connect to sql server with connection string from app settings
        options.UseSqlServer(connection);


    }
}

public class Blog
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(Order = 1)]
    public string Name { get; set; }

    public virtual ICollection<Post> Posts { get; set; }
}

public class Post
{
    [Key]
    [Column(Order = 0)]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Column(Order = 1)]
    public string Title { get; set; }
    [Column(Order = 2)]
    public string Content { get; set; }

  
    public int BlogId { get; set; }
    [ForeignKey(nameof(BlogId))]
    public virtual Blog Blog { get; set; }
}