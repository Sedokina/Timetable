using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Timetable.DAL;
using Timetable.DomainCore.Domain;

namespace Timetable.WebApp.Controllers
{
    public class CriteriaController : Controller
    {
        private ScheduleKSTUContext _context;

        public CriteriaController()
        {
            _context = new ScheduleKSTUContext();
        }

        // GET: Criteria
        public async Task<IActionResult> Index()
        {
            return View(await _context.Criteria.ToListAsync());
        }

        // GET: Criteria/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria
                .SingleOrDefaultAsync(m => m.Id == id);
            if (criteria == null)
            {
                return NotFound();
            }

            return View(criteria);
        }

        // GET: Criteria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Criteria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Rate")] Criteria criteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(criteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(criteria);
        }

        // GET: Criteria/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria.SingleOrDefaultAsync(m => m.Id == id);
            if (criteria == null)
            {
                return NotFound();
            }
            return View(criteria);
        }

        // POST: Criteria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,Description,Rate")] Criteria criteria)
        {
            if (id != criteria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(criteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CriteriaExists(criteria.Id))
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
            return View(criteria);
        }

        // GET: Criteria/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var criteria = await _context.Criteria
                .SingleOrDefaultAsync(m => m.Id == id);
            if (criteria == null)
            {
                return NotFound();
            }

            return View(criteria);
        }

        // POST: Criteria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            var criteria = await _context.Criteria.SingleOrDefaultAsync(m => m.Id == id);
            _context.Criteria.Remove(criteria);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CriteriaExists(byte id)
        {
            return _context.Criteria.Any(e => e.Id == id);
        }
    }
}
