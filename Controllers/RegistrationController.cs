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
    public class RegistrationController : Controller
    {
        private readonly seminarRegistration _context;

        public RegistrationController(seminarRegistration context)
        {
            _context = context;
        }

        // GET: Registration
        public async Task<IActionResult> Index()
        {
            var seminarRegistration = _context.Registration.Include(r => r.Attendee).Include(r => r.Conference);
            return View(await seminarRegistration.ToListAsync());
        }

        // GET: Registration/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .Include(r => r.Attendee)
                .Include(r => r.Conference)
                .FirstOrDefaultAsync(m => m.registrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // GET: Registration/Create
        public IActionResult Create()
        {
            ViewData["attendeeID"] = new SelectList(_context.Attendee, "attendeeID", "attendeeID");
            ViewData["conferenceID"] = new SelectList(_context.Conference, "conferenceID", "conferenceID");
            return View();
        }

        // POST: Registration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("registrationID,attendeeID,conferenceID")] Registration registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["attendeeID"] = new SelectList(_context.Attendee, "attendeeID", "attendeeID", registration.attendeeID);
            ViewData["conferenceID"] = new SelectList(_context.Conference, "conferenceID", "conferenceID", registration.conferenceID);
            return View(registration);
        }

        // GET: Registration/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            ViewData["attendeeID"] = new SelectList(_context.Attendee, "attendeeID", "attendeeID", registration.attendeeID);
            ViewData["conferenceID"] = new SelectList(_context.Conference, "conferenceID", "conferenceID", registration.conferenceID);
            return View(registration);
        }

        // POST: Registration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("registrationID,attendeeID,conferenceID")] Registration registration)
        {
            if (id != registration.registrationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrationExists(registration.registrationID))
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
            ViewData["attendeeID"] = new SelectList(_context.Attendee, "attendeeID", "attendeeID", registration.attendeeID);
            ViewData["conferenceID"] = new SelectList(_context.Conference, "conferenceID", "conferenceID", registration.conferenceID);
            return View(registration);
        }

        // GET: Registration/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registration = await _context.Registration
                .Include(r => r.Attendee)
                .Include(r => r.Conference)
                .FirstOrDefaultAsync(m => m.registrationID == id);
            if (registration == null)
            {
                return NotFound();
            }

            return View(registration);
        }

        // POST: Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var registration = await _context.Registration.FindAsync(id);
            _context.Registration.Remove(registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrationExists(int id)
        {
            return _context.Registration.Any(e => e.registrationID == id);
        }
    }
}
