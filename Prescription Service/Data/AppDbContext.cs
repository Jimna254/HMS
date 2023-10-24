using Microsoft.EntityFrameworkCore;
using Prescription_Service.Models;

namespace Prescription_Service.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}  

		public DbSet<Prescription> Prescriptions { get; set; }
	}
}
