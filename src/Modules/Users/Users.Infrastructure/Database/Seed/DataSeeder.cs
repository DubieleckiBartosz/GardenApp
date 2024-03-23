namespace Users.Infrastructure.Database.Seed;

internal class DataSeeder
{
    private readonly UsersContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DataSeeder(UsersContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
    }

    internal async Task SeedTemplatesAsync()
    {
        UserTemplateType[] types = { UserTemplateType.Confirmation, UserTemplateType.ResetPassword };

        foreach (var templateType in types)
        {
            var type = (int)templateType;
            if (!_context.Templates.Any(_ => _.TemplateType == type))
            {
                var template = Templates.Get(templateType);
                var newTemplate = new Template(type, template.Subject, template.Value);

                await _context.Templates.AddAsync(newTemplate);
            }
        }

        await _context.SaveChangesAsync();
    }

    internal async Task SeedRolesAsync()
    {
        UserRole[] roles = { UserRole.Admin, UserRole.User, UserRole.Business };

        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role.ToString()))
            {
                await _roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }

        await _context.SaveChangesAsync();
    }

    internal async Task SeedAdminAsync()
    {
        var userAdmin = User.NewUser("admin", "dev", "123-123-123", "admin.gardenapp@dev.com");
        userAdmin.Confirm();

        await SaveUser(userAdmin, "PasswordAdmin$123", UserRole.Admin);

        var user = User.NewUser("user", "dev", "111-222-333", "user.gardenapp@dev.com");
        user.Confirm();

        await SaveUser(user, "PasswordUser$123", UserRole.User);
    }

    private async Task SaveUser(User user, string password, UserRole role)
    {
        var result = await _userManager.FindByEmailAsync(user.Email);
        if (result == null)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, role.ToString());

            await _context.SaveChangesAsync();
        }
    }
}