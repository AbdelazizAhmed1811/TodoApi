using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class AppDbContext : DbContext
    {
       public DbSet<TodoItem> TodoItems { get; set; }



        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
       
    }
}
