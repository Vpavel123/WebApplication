using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.ViewModels;

namespace WebApplication.Areas.Account.controllers
{
    [Area("Account")]
    [Authorize(Roles = "user")]
    public class PageController : Controller
    {
        private readonly UserManager<User> _userManager;

        public PageController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            ChangeDataAccount model = new ChangeDataAccount { Id = user.Id, Name = user.Name, Surname = user.Surname, Date = user.Date, Address = user.Address };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ChangeDataAccount model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Date = model.Date;
                    user.Address = model.Address;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Edit");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }
    }
}
