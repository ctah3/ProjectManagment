using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ManagmentInfracstruction;
using ProjectManagment_class.Models;

namespace ManagmentInfracstruction.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ProjectContext _context;

        public ReportsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Reports
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Reports.Include(r => r.Assignment);
            return View(await projectContext.ToListAsync());
        }

        // GET: Reports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Assignment)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Reports/Create
        public IActionResult Create()
        {
            ViewData["AssignmentId"] = new SelectList(_context.TaskAssignments, "AssignmentId", "AssignmentId");
            return View();
        }

        // POST: Reports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportId,AssignmentId,CreatedAt,Content")] Report report)
        {
            TaskAssignment assignment = _context.TaskAssignments
                .Include(a => a.Task)
                .ThenInclude(t => t.Project)
                .Include(a => a.User)
                .FirstOrDefault(a => a.AssignmentId == report.AssignmentId);
                report.Assignment = assignment;
                ModelState.Clear();
                TryValidateModel(report);

            if (ModelState.IsValid)
            {
                if (report.CreatedAt == default) report.CreatedAt = DateTime.Now;
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var projectList = _context.TaskAssignments
                .Include(a => a.Task).ThenInclude(t => t.Project)
                .Select(a => new {
                Id = a.AssignmentId,
                Name = a.Task.Project.Description
                }).ToList();
            //ViewData["AssignmentId"] = new SelectList(_context.TaskAssignments, "AssignmentId", "AssignmentId", report.AssignmentId);
            return View(report);
        }

        // GET: Reports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            ViewData["AssignmentId"] = new SelectList(_context.TaskAssignments, "AssignmentId", "AssignmentId", report.AssignmentId);
            return View(report);
        }

        // POST: Reports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportId,AssignmentId,CreatedAt,Content")] Report report)
        {
            var assignment = _context.TaskAssignments.FirstOrDefault(a => a.AssignmentId == report.AssignmentId);
            report.Assignment = assignment;
            ModelState.Clear();
            TryValidateModel(report);

            if (id != report.ReportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReportExists(report.ReportId))
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
            ViewData["AssignmentId"] = new SelectList(_context.TaskAssignments, "AssignmentId", "AssignmentId", report.AssignmentId);
            return View(report);
        }

        // GET: Reports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports
                .Include(r => r.Assignment)
                .FirstOrDefaultAsync(m => m.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // POST: Reports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report != null)
            {
                _context.Reports.Remove(report);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.ReportId == id);
        }
    }
}
