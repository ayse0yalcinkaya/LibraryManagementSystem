using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

public class RoleController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    // rolleri getir
    [HttpGet]
    public IActionResult Index()
    {
        var roles = _roleManager.Roles.ToList();
        return View(roles);
    }

    // yeni rol ekle
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        if (!string.IsNullOrEmpty(roleName))
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View();
    }

    // rolü güncelle
    [HttpGet]
    public async Task<IActionResult> Edit(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role == null)
        {
            return NotFound();
        }
        return View(role);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(IdentityRole role)
    {
        if (ModelState.IsValid)
        {
            var existingRole = await _roleManager.FindByIdAsync(role.Id);
            if (existingRole == null)
            {
                return NotFound();
            }

            existingRole.Name = role.Name;
            var result = await _roleManager.UpdateAsync(existingRole);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(role);
    }

    //rol sil
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction(nameof(Index));
    }

    // rol atama
    [HttpGet]
    public async Task<IActionResult> AssignRole(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var roles = _roleManager.Roles.ToList();
        var userRoles = await _userManager.GetRolesAsync(user);

        var model = new AssignRoleViewModel
        {
            UserId = user.Id,
            Roles = roles,
            UserRoles = userRoles
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null && !string.IsNullOrEmpty(roleName))
        {
            await _userManager.AddToRoleAsync(user, roleName);
        }
        return RedirectToAction("Index", "User");
    }

    // rolü geri alma silme
    [HttpPost]
    public async Task<IActionResult> RemoveRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null && !string.IsNullOrEmpty(roleName))
        {
            await _userManager.RemoveFromRoleAsync(user, roleName);
        }
        return RedirectToAction("Index", "User");
    }
}
