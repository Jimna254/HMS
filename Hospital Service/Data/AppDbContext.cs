using Hospital_Service.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Hospital_Service.Data
{
		public class AppDbContext : DbContext
		{
			public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
			{
			}
			
		public DbSet<Hospital> Hospitals { get; set; }
			
		}
	
}
