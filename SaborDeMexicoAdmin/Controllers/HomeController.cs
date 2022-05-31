using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SaborDeMexicoAdmin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SaborDeMexicoAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, u204501959_SaborDeMexicoContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                return Redirect(Url.ActionLink("Login", "Home"));
            }
            return View();
        }
        public IActionResult Login()
        {
            return View(new Adminitrador());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Adminitrador model)
        {
            var lis = _context.Adminitrador.Where(d => d.User.Equals(model.User)).ToList();
            if (lis.Count > 0)
            {
                if (lis[0].Pass.Equals(model.Pass))
                {
                    HttpContext.Session.SetString("id", lis[0].Id.ToString());
                    HttpContext.Session.SetString("nombre", lis[0].User.ToString());
                    return Redirect(Url.ActionLink("Index", "Home")); 
                }
                else
                {
                    model.Token  = "Error de datos";
                    return View(model);
                }    
            }
            model.Token = "Usuario no existente";
            return View(model);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
