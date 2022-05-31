using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SaborDeMexicoAdmin.Models;

namespace SaborDeMexicoAdmin.Controllers
{
    public class PresentacionsController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public PresentacionsController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: Presentacions
        public async Task<IActionResult> Index()
        {
            var u204501959_SaborDeMexicoContext = _context.Presentacion.Include(p => p.IdProductoNavigation);
            return View(await u204501959_SaborDeMexicoContext.ToListAsync());
        }

        // GET: Presentacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentacion
                .Include(p => p.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presentacion == null)
            {
                return NotFound();
            }

            return View(presentacion);
        }

        // GET: Presentacions/Create
        public IActionResult Create()
        {
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id");
            return View();
        }

        // POST: Presentacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Precentacion,Medida,Precio,IdProducto")] Presentacion presentacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(presentacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", presentacion.IdProducto);
            return View(presentacion);
        }

        // GET: Presentacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentacion.FindAsync(id);
            if (presentacion == null)
            {
                return NotFound();
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", presentacion.IdProducto);
            return View(presentacion);
        }

        // POST: Presentacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Precentacion,Medida,Precio,IdProducto")] Presentacion presentacion)
        {
            if (id != presentacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(presentacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresentacionExists(presentacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdProducto"] = new SelectList(_context.Producto, "Id", "Id", presentacion.IdProducto);
            return View(presentacion);
        }

        // GET: Presentacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var presentacion = await _context.Presentacion
                .Include(p => p.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (presentacion == null)
            {
                return NotFound();
            }

            return View(presentacion);
        }

        // POST: Presentacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var presentacion = await _context.Presentacion.FindAsync(id);
            _context.Presentacion.Remove(presentacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresentacionExists(int id)
        {
            return _context.Presentacion.Any(e => e.Id == id);
        }
    }
}
