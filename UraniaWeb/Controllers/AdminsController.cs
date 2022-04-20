using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UraniaWeb.Models;

namespace UraniaWeb.Controllers
{
    public class AdminsController : Controller
    {
        private readonly UraniaWebDbContext _context;

        public AdminsController(UraniaWebDbContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Admins.ToListAsync());
        }

        // GET: Admins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdAdmin == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: Admins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAdmin,NameAdmin,PassAdmin")] Admin admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(admin);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "No se ha posiso crear el nuevo admin");
            }
            return View(admin);
        }

        // GET: Admins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            return View(admin);
        }

        // POST: Admins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.IdAdmin == id);

            if (await TryUpdateModelAsync<Admin>(
                admin,
                "",
                a => a.NameAdmin, a => a.PassAdmin))
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "No se ha podido editar el Admin. " + "Si el problema persiste, contacte a soporte.");
                }
            return View(admin);
        }

        // GET: Admins/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? guardarCambiosError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admins
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.IdAdmin == id);
            if (admin == null)
            {
                return NotFound();
            }
            if (guardarCambiosError.GetValueOrDefault())
            {
                ViewData["MessageError"] = "Error en la eliminacion del registro";
            }

            return View(admin);
        }

        // POST: Admins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(Delete), new(id = id, guardarCambiosError = true));
            }
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.IdAdmin == id);
        }
    }
}
