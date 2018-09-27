using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ShopApi.Models;

public class Seed
{
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    public Seed(RoleManager<Role> roleManager, UserManager<User> userManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;

    }


    public void SeedUsers()
    {
        if (!_roleManager.Roles.Any())
        {
            var roles = new List<Role>
            {
                new Role{Name ="User"},
                new Role {Name = "Admin"}
            };


            foreach (var role in roles)
            {
                _roleManager.CreateAsync(role).Wait();
            }

            var adminUser = new User
            {
                UserName = "Admin"
            };

            IdentityResult result = _userManager.CreateAsync(adminUser,"qwerty").Result;

            if(result.Succeeded)
            {
                var admin = _userManager.FindByNameAsync("Admin").Result;
                _userManager.AddToRolesAsync(admin,new[]{"Admin"}).Wait();
            }
        }
    }
}