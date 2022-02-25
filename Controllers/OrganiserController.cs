using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seminar.Data;
using Seminar.Models;

namespace Seminar.Controllers
{
    public class OrganiserController : Controller
    {
        private readonly seminarRegistration _context;

        public OrganiserController(seminarRegistration context)
        {
            _context = context;
        }

        // GET: Organiser
        public async Task<IActionResult> Index()
        {
            return View(await _context.Organiser.ToListAsync());
        }

        // GET: Organiser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organiser
                .FirstOrDefaultAsync(m => m.organiserID == id);
            if (organiser == null)
            {
                return NotFound();
            }

            return View(organiser);
        }

        // GET: Organiser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Organiser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("organiserID,organiserName,organiserLocation,organiserPhone,organiserEmail")] Organiser organiser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(organiser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(organiser);
        }

        // GET: Organiser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organiser.FindAsync(id);
            if (organiser == null)
            {
                return NotFound();
            }
            return View(organiser);
        }

        // POST: Organiser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("organiserID,organiserName,organiserLocation,organiserPhone,organiserEmail")] Organiser organiser)
        {
            if (id != organiser.organiserID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(organiser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrganiserExists(organiser.organiserID))
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
            return View(organiser);
        }

        // GET: Organiser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organiser = await _context.Organiser
                .FirstOrDefaultAsync(m => m.organiserID == id);
            if (organiser == null)
            {
                return NotFound();
            }

            return View(organiser);
        }

        // POST: Organiser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organiser = await _context.Organiser.FindAsync(id);
            _context.Organiser.Remove(organiser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrganiserExists(int id)
        {
            return _context.Organiser.Any(e => e.organiserID == id);
        }
    }
}
