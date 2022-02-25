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
    public class ConferenceController : Controller
    {
        private readonly seminarRegistration _context;

        public ConferenceController(seminarRegistration context)
        {
            _context = context;
        }

        // GET: Conference
        public async Task<IActionResult> Index()
        {
            var seminarRegistration = _context.Conference.Include(c => c.Organiser);
            return View(await seminarRegistration.ToListAsync());
        }

        // GET: Conference/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conference
                .Include(c => c.Organiser)
                .FirstOrDefaultAsync(m => m.conferenceID == id);
            if (conference == null)
            {
                return NotFound();
            }

            return View(conference);
        }

        // GET: Conference/Create
        public IActionResult Create()
        {
            ViewData["organiserID"] = new SelectList(_context.Set<Organiser>(), "organiserID", "organiserID");
            return View();
        }

        // POST: Conference/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("conferenceID,conferenceName,conferenceVenue,conferenceDate,conferencePrice,organiserID")] Conference conference)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conference);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["organiserID"] = new SelectList(_context.Set<Organiser>(), "organiserID", "organiserID", conference.organiserID);
            return View(conference);
        }

        // GET: Conference/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conference.FindAsync(id);
            if (conference == null)
            {
                return NotFound();
            }
            ViewData["organiserID"] = new SelectList(_context.Set<Organiser>(), "organiserID", "organiserID", conference.organiserID);
            return View(conference);
        }

        // POST: Conference/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("conferenceID,conferenceName,conferenceVenue,conferenceDate,conferencePrice,organiserID")] Conference conference)
        {
            if (id != conference.conferenceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conference);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConferenceExists(conference.conferenceID))
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
            ViewData["organiserID"] = new SelectList(_context.Set<Organiser>(), "organiserID", "organiserID", conference.organiserID);
            return View(conference);
        }

        // GET: Conference/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conference = await _context.Conference
                .Include(c => c.Organiser)
                .FirstOrDefaultAsync(m => m.conferenceID == id);
            if (conference == null)
            {
                return NotFound();
            }

            return View(conference);
        }

        // POST: Conference/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conference = await _context.Conference.FindAsync(id);
            _context.Conference.Remove(conference);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConferenceExists(int id)
        {
            return _context.Conference.Any(e => e.conferenceID == id);
        }
    }
}
