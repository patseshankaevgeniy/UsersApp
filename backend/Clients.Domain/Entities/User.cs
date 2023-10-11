using System.Collections.Generic;

namespace Users.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public ICollection<UserRole> Roles { get; set; } = default!;
}
