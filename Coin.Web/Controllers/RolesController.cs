using System.Threading.Tasks;
using Coin.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodWorqz.Web.Controllers
{
    // [Authorize(Roles = "admin")]
    // [Area("Administration")]
    public class RolesController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;

        public RolesController(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ApplicationRole role)
        {
            if (ModelState.IsValid)
            {
                await _roleManager.CreateAsync(role);;
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
    }
}