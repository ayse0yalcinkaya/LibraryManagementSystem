using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

public class AssignRoleViewModel
{
    public string UserId { get; set; }
    public List<IdentityRole> Roles { get; set; }
    public IList<string> UserRoles { get; set; }
}
