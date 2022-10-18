using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Projekt.DAL;
using Projekt.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Projekt.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<Korisnik> _userManager;
        private LigaDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, UserManager<Korisnik> userManager, LigaDbContext dbContext)
        {
            this._logger = logger;
            this._userManager = userManager;
            this._dbContext = dbContext;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult ONama()
        {
            return View();
        }

        [Authorize]
        public IActionResult OKorisniku()
        {
            string korisnikId = this._userManager.GetUserId(base.User);

            Korisnik korisnik = this._dbContext.Users.Include(k => k.KlubNajdrazi).FirstOrDefault(k => k.Id.Equals(korisnikId));

            return View(korisnik);
        }
    }
}
