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
    public class DetalleOrdensController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public DetalleOrdensController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: DetalleOrdens
        public async Task<IActionResult> Index()
        {
            var u204501959_SaborDeMexicoContext = _context.DetalleOrden.Include(d => d.IdPresentacionNavigation).Include(d => d.Orden).Include(d => d.Producto);
            return View(await u204501959_SaborDeMexicoContext.ToListAsync());
        }

        // GET: DetalleOrdens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrden
                .Include(d => d.IdPresentacionNavigation)
                .Include(d => d.Orden)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.IdDetalleOrden == id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Create
        public IActionResult Create()
        {
            ViewData["IdPresentacion"] = new SelectList(_context.Presentacion, "Id", "Medida");
            ViewData["OrdenId"] = new SelectList(_context.Orden, "Id", "Id");
            ViewData["ProductoId"] = new SelectList(_context.Presentacion, "Id", "Medida");
            return View();
        }

        // POST: DetalleOrdens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleOrden,Cantidad,Nota,Subtotal,OrdenId,ProductoId,IdPresentacion")] DetalleOrden detalleOrden)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleOrden);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPresentacion"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.IdPresentacion);
            ViewData["OrdenId"] = new SelectList(_context.Orden, "Id", "Id", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrden.FindAsync(id);
            if (detalleOrden == null)
            {
                return NotFound();
            }
            ViewData["IdPresentacion"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.IdPresentacion);
            ViewData["OrdenId"] = new SelectList(_context.Orden, "Id", "Id", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleOrden,Cantidad,Nota,Subtotal,OrdenId,ProductoId,IdPresentacion")] DetalleOrden detalleOrden)
        {
            if (id != detalleOrden.IdDetalleOrden)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleOrden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleOrdenExists(detalleOrden.IdDetalleOrden))
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
            ViewData["IdPresentacion"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.IdPresentacion);
            ViewData["OrdenId"] = new SelectList(_context.Orden, "Id", "Id", detalleOrden.OrdenId);
            ViewData["ProductoId"] = new SelectList(_context.Presentacion, "Id", "Medida", detalleOrden.ProductoId);
            return View(detalleOrden);
        }

        // GET: DetalleOrdens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleOrden = await _context.DetalleOrden
                .Include(d => d.IdPresentacionNavigation)
                .Include(d => d.Orden)
                .Include(d => d.Producto)
                .FirstOrDefaultAsync(m => m.IdDetalleOrden == id);
            if (detalleOrden == null)
            {
                return NotFound();
            }

            return View(detalleOrden);
        }

        // POST: DetalleOrdens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleOrden = await _context.DetalleOrden.FindAsync(id);
            _context.DetalleOrden.Remove(detalleOrden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleOrdenExists(int id)
        {
            return _context.DetalleOrden.Any(e => e.IdDetalleOrden == id);
        }
    }
}
