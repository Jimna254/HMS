using Appointments.Models;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Data
{
	public class AppDbContext : DbContext {
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Appointment> Appointments { get; set; }
	}
}
