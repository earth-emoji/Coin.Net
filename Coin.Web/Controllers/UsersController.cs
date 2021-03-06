using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coin.Web.Models.AdminViewModels;
using Coin.Web.Data;
using Coin.Web.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GoodWorqz.Web.Areas.Administration.Controllers
{
    // [Authorize(Roles = "admin")]
    [Area("Administration")]
    public class UsersController : Controller
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Include(u => u.UserRoles).ThenInclude(ur => ur.Role).ToListAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            var user = new ApplicationUser();
            user.UserRoles = new List<ApplicationUserRole>();
            PopulateAssignedRoleData(user);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Email,Password")] UserInputModel model, string[] selectedRoles)
        {
            
            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email
            };

            if (selectedRoles != null)
            {
                user.UserRoles = new List<ApplicationUserRole>();
                foreach (var role in selectedRoles)
                {
                    var roleToAdd = new ApplicationUserRole { UserId = user.Id, RoleId = role };
                    user.UserRoles.Add(roleToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                await _userManager.CreateAsync(user, model.Password);
                if (await _userManager.IsInRoleAsync(user, "Administrator"))
                {
                    _context.Add(new Administrator
                    {
                        IdentityId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }

                if (await _userManager.IsInRoleAsync(user, "Customer"))
                {
                    _context.Add(new Customer
                    {
                        IdentityId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }

                if (await _userManager.IsInRoleAsync(user, "Vendor"))
                {
                    _context.Add(new Vendor
                    {
                        IdentityId = user.Id
                    });
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateAssignedRoleData(user);
            return View(user);
        }

        private void PopulateAssignedRoleData(ApplicationUser user)
        {
            var allRoles = _roleManager.Roles.ToList();
            var userRoles = new HashSet<string>(user.UserRoles.Select(t => t.RoleId));
            var viewModel = new List<AssignedRole>();

            foreach(var role in allRoles)
            {
                viewModel.Add(new AssignedRole
                {
                    RoleId = role.Id,
                    Name = role.Name,
                    Assigned = userRoles.Contains(role.Id)
                });
            }
            ViewData["Roles"] = viewModel;
        }
    }
}