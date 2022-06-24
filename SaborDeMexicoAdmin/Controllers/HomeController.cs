using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SaborDeMexicoAdmin.Models;
using SaborDeMexicoAdmin.Models.ModelViews;
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

        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("id")))
            {
                return Redirect(Url.ActionLink("Login", "Home"));
            }
            ModelMenu modelMenu = new ModelMenu();
            try
            {
                modelMenu.GetListOrden = new List<Orden>(await _context.Orden.Where(d => d.Estatus >= 0).Include(d=>d.Cliente).Include(d=>d.Repartidor).Include(d=>d.Ruta).ToListAsync());
                modelMenu.GetListProduc = new List<Producto>(await _context.Producto.Where(d => d.Activo ==1).ToListAsync());
                modelMenu.GetListRepart = new List<Repartidor>(await _context.Repartidor.Where(d => d.Activo == 1).ToListAsync());
                return View(modelMenu);
            }
            catch (Exception)
            {

                throw;
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


                try
                {
                var model =  await _context.Orden.FirstOrDefaultAsync(m => m.Id == id);
                model.Estatus = -1;
                _context.Update(model);
                    await _context.SaveChangesAsync();
                }
            catch
            {
                return NotFound();
            }
               
                return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Confirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


                try
                {
                var model =  await _context.Orden.FirstOrDefaultAsync(m => m.Id == id);
                model.Estatus = 1;
                _context.Update(model);
                    await _context.SaveChangesAsync();
                }
            catch
            {
                return NotFound();
            }
               
                return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Entrega(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


                try
                {
                var model =  await _context.Orden.FirstOrDefaultAsync(m => m.Id == id);
                model.Estatus = 2;
                _context.Update(model);
                    await _context.SaveChangesAsync();
                }
            catch
            {
                return NotFound();
            }
               
                return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
