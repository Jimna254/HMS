using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Hospital_Service.Extensions
{
	public static class AuthenticationExtension
	{
		public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
		{
			try
			{
				builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateIssuerSigningKey = true,
						//what is valid
						ValidAudience = builder.Configuration["JwtOptions:Audience"],
						ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
					};
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
				
				// You can also re-throw the exception if needed
			}

			return builder;
		}
	}

}
