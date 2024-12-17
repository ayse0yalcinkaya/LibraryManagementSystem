using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    // admin sayfası giriri
    public IActionResult Index()
    {
        return View();
    }

    // kullanıcıları listeleme
    public async Task<IActionResult> ManageUsers()
    {
        var users = _userManager.Users.ToList();
        return View(users);
    }
}
