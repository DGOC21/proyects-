using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlumPromBD.Models;
using Rotativa;
using Rotativa.AspNetCore;

namespace AlumPromBD.Controllers
{
    public class AlumnoesController : Controller
    {
        private readonly AlumnoContext _context;

        public AlumnoesController(AlumnoContext context)
        {
            _context = context;
        }

        // GET: Alumnoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Alumnos.ToListAsync());
        }

        // GET: Alumnoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // GET: Alumnoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Alumnoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Nota1,Nota2,Nota3,Promedio,Estado")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                var prom = (alumno.Nota1 + alumno.Nota2 + alumno.Nota3) / 3;
                prom = Math.Round(prom, 1);
                var estado = prom > 6 ? true : false;
                alumno.Promedio = prom;
                alumno.Estado = estado;
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        // GET: Alumnoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //else
            //{
            //    var prom = (alumno.Nota1 + alumno.Nota2 + alumno.Nota3) / 3;
            //    prom = Math.Round(prom, 1);
            //    var estado = prom > 6 ? true : false;
            //    alumno.Promedio = prom;
            //    alumno.Estado = estado;
            //    _context.Add(alumno);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}

            var alumno = await _context.Alumnos.FindAsync(id);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        // POST: Alumnoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Nota1,Nota2,Nota3,Promedio,Estado")] Alumno alumno)
        {
            if (id != alumno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var prom = (alumno.Nota1 + alumno.Nota2 + alumno.Nota3) / 3;
                    prom = Math.Round(prom, 1);
                    var estado = prom > 6 ? true : false;
                    alumno.Promedio = prom;
                    alumno.Estado = estado;
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Id))
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
            return View(alumno);
        }

        // GET: Alumnoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // POST: Alumnoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alumno = await _context.Alumnos.FindAsync(id);
            _context.Alumnos.Remove(alumno);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlumnoExists(int id)
        {
            return _context.Alumnos.Any(e => e.Id == id);
        }
        public IActionResult PrintPDF()
        {
            return new ViewAsPdf("PrintPDF", _context.Alumnos)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                FileName = "Alumnos.pdf",
                PageMargins = new Rotativa.AspNetCore.Options.Margins(20, 20, 20, 20)
            };
        }
    }
}
