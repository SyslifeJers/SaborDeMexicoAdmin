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
    public class AdminitradorsController : Controller
    {
        private readonly u204501959_SaborDeMexicoContext _context;

        public AdminitradorsController(u204501959_SaborDeMexicoContext context)
        {
            _context = context;
        }

        // GET: Adminitradors
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adminitrador.ToListAsync());
        }

        // GET: Adminitradors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminitrador = await _context.Adminitrador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminitrador == null)
            {
                return NotFound();
            }

            return View(adminitrador);
        }

        // GET: Adminitradors/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Adminitradors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User,Pass,Token,Registro")] Adminitrador adminitrador)
        {
            if (ModelState.IsValid)
            {
                adminitrador.Registro = DateTime.Now;
                adminitrador.Token = "";
                _context.Add(adminitrador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adminitrador);
        }

        // GET: Adminitradors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminitrador = await _context.Adminitrador.FindAsync(id);
            if (adminitrador == null)
            {
                return NotFound();
            }
            return View(adminitrador);
        }

        // POST: Adminitradors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User,Pass,Token,Registro")] Adminitrador adminitrador)
        {
            if (id != adminitrador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminitrador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminitradorExists(adminitrador.Id))
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
            return View(adminitrador);
        }

        // GET: Adminitradors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminitrador = await _context.Adminitrador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminitrador == null)
            {
                return NotFound();
            }

            return View(adminitrador);
        }

        // POST: Adminitradors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adminitrador = await _context.Adminitrador.FindAsync(id);
            _context.Adminitrador.Remove(adminitrador);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminitradorExists(int id)
        {
            return _context.Adminitrador.Any(e => e.Id == id);
        }
    }
}
