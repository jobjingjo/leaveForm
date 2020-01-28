using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveForm.Data;
using LeaveForm.Data.Entities;

namespace LeaveForm.Controllers
{
    public class LeavesController : Controller
    {
        private readonly LeaveDbContext _context;

        public LeavesController(LeaveDbContext context)
        {
            _context = context;
        }

        // GET: Leaves
        public async Task<IActionResult> Index()
        {
            return View(await _context.LeaveForms.Include(x=>x.Employee).ToListAsync());
        }

        // GET: Leaves/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.LeaveForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // GET: Leaves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leaves/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Status,CreatedDate,ApprovedDate,LeaveFrom,LeaveTo,NumberOfLeaveDays,Reason")] Leave leave)
        {
            if (ModelState.IsValid)
            {
                leave.Id = Guid.NewGuid();
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leave);
        }

        [HttpPost]
        public async Task<IActionResult> RequestSickLeave([Bind("LeaveFrom,LeaveTo,NumberOfLeaveDays,Reason")] Leave leave, string LineUserId)
        {
            var requester = await _context.Employees.FirstOrDefaultAsync(x => x.LineUserId == LineUserId);
            if (requester == null) return BadRequest();
            if (ModelState.IsValid)
            {
                leave.Employee = requester;
                leave.Id = Guid.NewGuid();
                leave.Status = LeaveStatuses.New;
                leave.Type = LeaveTypes.Sick;
                leave.CreatedDate = DateTime.UtcNow;
                _context.Add(leave);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }

        // GET: Leaves/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.LeaveForms.FindAsync(id);
            if (leave == null)
            {
                return NotFound();
            }
            return View(leave);
        }

        // POST: Leaves/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Type,Status,CreatedDate,ApprovedDate,LeaveFrom,LeaveTo,NumberOfLeaveDays,Reason")] Leave leave)
        {
            if (id != leave.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(leave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveExists(leave.Id))
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
            return View(leave);
        }

        // GET: Leaves/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leave = await _context.LeaveForms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leave == null)
            {
                return NotFound();
            }

            return View(leave);
        }

        // POST: Leaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var leave = await _context.LeaveForms.FindAsync(id);
            _context.LeaveForms.Remove(leave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveExists(Guid id)
        {
            return _context.LeaveForms.Any(e => e.Id == id);
        }
    }
}
