using Microsoft.EntityFrameworkCore;
using RecordsManagement.Models;

namespace RecordsManagement.Data
{
	public class AppDbContext : DbContext
	{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        public DbSet<Record> Records { get; set; }
    }
}
