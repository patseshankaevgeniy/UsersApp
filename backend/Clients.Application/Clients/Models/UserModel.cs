using System.Collections.Generic;

namespace Users.Application.Users.Models;

public class UserModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
}
