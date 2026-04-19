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
    public class ProjectsController : Controller
    {
        private readonly ProjectContext _context;

        public ProjectsController(ProjectContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index()
        {
            var projectContext = _context.Projects.Include(p => p.Manager);
            return View(await projectContext.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.ProjectMembers)     
                .ThenInclude(pm => pm.User)      
                .Include(p => p.Tasks)               
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            //ViewBag.ProjectId = projectId;
            //ViewBag.ProjectName = _context.Projects.Where(c => c.ProjectId == projectId).FirstOrDefault().ProjectName;
            var users = _context.Users.ToList();
            ViewBag.AllUsers = users;
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjectName,Description,DateStart,DateEnd,ManagerId,Id")] Project project, int[] selectedUserIds)
        {
            var manager = _context.Users.FirstOrDefault(u => u.UserId == project.ManagerId);
            project.Manager = manager;
            ModelState.Clear();
            TryValidateModel(project);
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                if (selectedUserIds != null && selectedUserIds.Length > 0)
                {
                    foreach (var userId in selectedUserIds)
                    {
                        _context.ProjectMembers.Add(new ProjectMember
                        {
                            ProjectId = project.ProjectId,
                            UserId = userId
                        });
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
       
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Email", project.ManagerId);

            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.ProjectMembers)
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }
            var selectedUserIds = project.ProjectMembers.Select(pm => pm.UserId).ToList();
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Email", project.ManagerId);
            ViewBag.AllUsers = new MultiSelectList(_context.Users, "UserId", "Email", selectedUserIds);
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectId,ProjectName,Description,DateStart,DateEnd,ManagerId,Id")] Project project, int[] selectedUserIds)
        {
            if (id != project.ProjectId)
            {
                return NotFound();
            }
            var manager = _context.Users.FirstOrDefault(u => u.UserId == project.ManagerId);
            project.Manager = manager;
            ModelState.Clear();
            TryValidateModel(project);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    var existingMembers = _context.ProjectMembers.Where(m => m.ProjectId == id);
                    _context.ProjectMembers.RemoveRange(existingMembers);
                    if (selectedUserIds != null)
                    {
                        foreach (var userId in selectedUserIds)
                        {
                            _context.ProjectMembers.Add(new ProjectMember
                            {
                                ProjectId = id,
                                UserId = userId
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.ProjectId))
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
            ViewData["ManagerId"] = new SelectList(_context.Users, "UserId", "Email", project.ManagerId);
            ViewBag.AllUsers = new MultiSelectList(_context.Users, "UserId", "Email", selectedUserIds);
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects
                .Include(p => p.Manager)
                .Include(p => p.ProjectMembers)     
                .ThenInclude(pm => pm.User)      
                .Include(p => p.Tasks)              
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //var project = await _context.Projects.FindAsync(id);
            var project = await _context.Projects
                .Include(p => p.ProjectMembers)
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.ProjectId == id);
            if (project != null)
            {
                _context.ProjectMembers.RemoveRange(project.ProjectMembers);
                _context.Tasks.RemoveRange(project.Tasks);
                _context.Projects.Remove(project);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Any(e => e.ProjectId == id);
        }
    }
}
