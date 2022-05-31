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
    public class RepartidorsController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public RepartidorsController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: Repartidors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Repartidor.ToListAsync());
        }

        // GET: Repartidors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repartidor == null)
            {
                return NotFound();
            }

            return View(repartidor);
        }

        // GET: Repartidors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Repartidors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Activo,Modificado,Pin,Token")] Repartidor repartidor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repartidor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repartidor);
        }

        // GET: Repartidors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidor.FindAsync(id);
            if (repartidor == null)
            {
                return NotFound();
            }
            return View(repartidor);
        }

        // POST: Repartidors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Activo,Modificado,Pin,Token")] Repartidor repartidor)
        {
            if (id != repartidor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repartidor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepartidorExists(repartidor.Id))
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
            return View(repartidor);
        }

        // GET: Repartidors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repartidor = await _context.Repartidor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repartidor == null)
            {
                return NotFound();
            }

            return View(repartidor);
        }

        // POST: Repartidors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repartidor = await _context.Repartidor.FindAsync(id);
            _context.Repartidor.Remove(repartidor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepartidorExists(int id)
        {
            return _context.Repartidor.Any(e => e.Id == id);
        }
    }
}
