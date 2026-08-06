using Microsoft.AspNetCore.Identity;
using Caso1.Models;

namespace Caso1.Data;

public static class SeedData
{
    public const string RolAdministrador = "Administrador";
    public const string RolCliente       = "Cliente";

    public static async Task InicializarAsync(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await CrearRolSiNoExisteAsync(roleManager, RolAdministrador);
        await CrearRolSiNoExisteAsync(roleManager, RolCliente);
    }

    private static async Task CrearRolSiNoExisteAsync(RoleManager<IdentityRole> roleManager, string roleName)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"No se pudo crear el rol '{roleName}': {errors}");
            }
        }
    }
}