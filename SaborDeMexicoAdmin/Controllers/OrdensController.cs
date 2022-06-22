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
    public class OrdensController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public OrdensController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            var u204501959_SaborDeMexicoContext = _context.Orden.Include(o => o.Cliente).Include(o => o.Repartidor).Include(o => o.Ruta);
            return View(await u204501959_SaborDeMexicoContext.ToListAsync());
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .Include(o => o.Cliente)
                .Include(o => o.Repartidor)
                .Include(o => o.Ruta)
                .Include(o => o.DetalleOrden).ThenInclude(d=>d.IdPresentacionNavigation).ThenInclude(d=>d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // GET: Ordens/Create
      

        // GET: Ordens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", orden.ClienteId);
            ViewData["RepartidorId"] = new SelectList(_context.Repartidor, "Id", "Pin", orden.RepartidorId);
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Id", orden.RutaId);
            return View(orden);
        }

        // POST: Ordens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Total,Ordencol,Notas,Fecha,Cantidad,Activo,Modifcado,CostoEnvio,Estatus,TipoPago,RutaId,ClienteId,RepartidorId")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "Id", orden.ClienteId);
            ViewData["RepartidorId"] = new SelectList(_context.Repartidor, "Id", "Pin", orden.RepartidorId);
            ViewData["RutaId"] = new SelectList(_context.Ruta, "Id", "Id", orden.RutaId);
            return View(orden);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .Include(o => o.Cliente)
                .Include(o => o.Repartidor)
                .Include(o => o.Ruta)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return _context.Orden.Any(e => e.Id == id);
        }
    }
}
