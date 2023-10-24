using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Prescription_Service.Extension
{
	public static class AddSwagger
	{
		public static WebApplicationBuilder AddSwaggenGenExtension(this WebApplicationBuilder builder)
		{
			builder.Services.AddSwaggerGen(options =>
			{
				options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "Provide a Valid Token",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference= new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id=JwtBearerDefaults.AuthenticationScheme
				}
			}, new string[]{}

		}
	});
			});

			return builder;
		}
	}
}
