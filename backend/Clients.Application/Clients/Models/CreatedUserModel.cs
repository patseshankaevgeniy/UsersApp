using System.Collections.Generic;

namespace Users.Application.Clients.Models;

public class CreatedUserModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int Age { get; set; }
    public List<string> Roles { get; set; }
}
