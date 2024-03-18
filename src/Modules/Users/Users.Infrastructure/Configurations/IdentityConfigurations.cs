namespace Users.Infrastructure.Configurations;

internal static class IdentityConfigurations
{
    public static WebApplicationBuilder RegisterIdentity(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var options = builder.Configuration.GetSection("EfOptions").Get<EfOptions>()!;
        var schema = UsersContext.UsersSchema;
        var connectionString = options.ConnectionString + schema;

        builder.RegisterEntityFrameworkNpg<UsersContext>(connectionString, schema);
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 7;
            options.Password.RequireDigit = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<UsersContext>();

        services.Configure<DataProtectionTokenProviderOptions>(opt =>
                    opt.TokenLifespan = TimeSpan.FromHours(2));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            var settings = new JwtSettings();
            builder.Configuration.GetSection(nameof(JwtSettings)).Bind(settings);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret!));

            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidAudience = settings.Audience,
                ValidIssuer = settings.Issuer,
                IssuerSigningKey = key,
            };
        });

        return builder;
    }
}