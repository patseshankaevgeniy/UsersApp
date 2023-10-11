using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Users.Domain.Entities;

namespace Users.Persistence;

public static class SeedData
{
    public static async Task AddDefaultClientRolesAsync(ApplicationDbContext db)
    {
        if (await db.UserRoles.AnyAsync())
        {
            return;
        }

        var userRoles = new List<UserRole>
        {
            new UserRole { Role = "User"},
            new UserRole { Role = "Admin"},
            new UserRole { Role = "Support"},
            new UserRole { Role = "SuperAdmin"}
        };

        db.UserRoles.AddRange(userRoles);
        await db.SaveChangesAsync();
    }

    public static async Task AddDefaultClientsAsync(ApplicationDbContext db)
    {
        if (await db.Users.AnyAsync())
        {
            return;
        }

        var users = new List<User>
        {
            new User
            {
                Email = "Patesh@mail.ru",
                Name = "Den",
                Password = "12345678",
                Age = 15,
            },
            new User
            {
                Email = "Prosto@mail.ru",
                Name = "Vlad",
                Password = "prostoparol",
                Age = 10,
            },
            new User
            {
                Email = "Lol@mail.ru",
                Name = "Jon",
                Password = "password456",
                Age = 5,
            },
        };

        db.Users.AddRange(users);
        await db.SaveChangesAsync();
    }
}
