using Feedback_Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Feedback_Service.Data
{
	public class AppDbContext :DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		public DbSet<Feedback> Feedbacks { get; set; }
	}
}
