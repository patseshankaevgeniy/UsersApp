using System.Collections.Generic;

namespace Users.Domain.Entities;

public class UserRole : BaseEntity
{
    public string Role { get; set; }

    public ICollection<User> Clients { get; set; } = default!;
}
