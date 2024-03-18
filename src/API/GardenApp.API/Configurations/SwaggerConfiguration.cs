namespace GardenApp.API.Configurations;

public static class SwaggerConfiguration
{
    public static void GetSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "GardenApp Modular Monolith",
                Version = "v1",
                Description = "ASP.NET Core 6.0 Web API",
                Contact = new OpenApiContact
                {
                    Name = "Github",
                    Url = new Uri("https://github.com/DubieleckiBartosz")
                }
            });
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Enter JWT Bearer token",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };

            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[] { }}
            });
        });
    }
}