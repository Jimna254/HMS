using Email.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<EmailLoggers> EmailLoggers { get; set; }
	}
}
