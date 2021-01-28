using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lect_3.Models;
using tut3.Models;

namespace Lect_3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : Controller
    {

        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Enroll(int id, [Bind("IdSemester,StartDate,studies")] Enrollment enrollment)
        {
            var student = await _context.Students.FindAsync(id);
            student.enrollment = enrollment;
            return View(await Edit(id, student));
        }

        [HttpPost]
        public async Task<IActionResult> Promote(int id)
        {
            var student = await _context.Students.FindAsync(id);
            student.enrollment.IdSemester++;
            return View(await Edit(id, student));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("IndexNumber,FirstName,LastName,BirthDate,enrollment")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("IndexNumber,FirstName,LastName,BirthDate,enrollment")] Student student)
        {
            if (id != student.IndexNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.IndexNumber))
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
            return View(student);
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.IndexNumber == id);
        }

    }
}