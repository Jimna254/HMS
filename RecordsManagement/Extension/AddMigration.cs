using Microsoft.EntityFrameworkCore;
using RecordsManagement.Data;

namespace RecordsManagement.Extension
{
	public static class AddMigration
	{
		public static IApplicationBuilder UseMigration(this IApplicationBuilder app)
		{

			using (var scope = app.ApplicationServices.CreateScope())
			{
				var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}

			return app;
		}
	}
}
