using Microsoft.AspNetCore.Identity;

public static class SeedData
{
  public static async Task Initialize(IServiceProvider serviceProvider) {
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

    if(!await roleManager.RoleExistsAsync("Admin"))
    {
      await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = await userManager.FindByEmailAsync("b221210074@ogr.sakarya.edu.tr");
    if(adminUser == null) {
      adminUser = new IdentityUser
      {
        UserName = "admin",
        Email = "b221210074@ogr.sakarya.edu.tr",
        EmailConfirmed = true
      };
      await userManager.CreateAsync(adminUser, "sauA123*bb");
      await userManager.AddToRoleAsync(adminUser, "Admin");
    }


  }
}