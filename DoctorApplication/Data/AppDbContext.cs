using DoctorApplication.Model;
using Microsoft.EntityFrameworkCore;

namespace DoctorApplication.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<DocApplication> DoctorApplications { get; set; }
	}
}
