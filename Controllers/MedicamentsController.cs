﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    [Authorize]
    public class MedicamentsController : Controller
    {
        private readonly GSB_Gestion_AppContext _context;

        public MedicamentsController(GSB_Gestion_AppContext context)
        {
            _context = context;
        }

        // GET: Medicaments
        public async Task<IActionResult> Index()
        {
            //var gSB_Gestion_AppContext = _context.Medicaments.Include(m => m.FamCodeNavigation);
            //return View(await gSB_Gestion_AppContext.ToListAsync());
            string rech = string.Empty;
            var gSB_CRContext = _context.Medicaments.Include(m => m.FamCodeNavigation);
            try
            {
                rech = Request.Form["rech"];
            }
            catch 
            { }
            if (rech.Equals(string.Empty))  // pas de recherche
            {
                return View(await gSB_CRContext.ToListAsync());
            }
            else //recherche avec valeur saisie
            {
                var list = gSB_CRContext.Where(m => m.MedNomcommercial.Contains(rech)).ToList();
                return View(list);
            }

        }

        // GET: Medicaments/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments
                .Include(m => m.FamCodeNavigation)
                .FirstOrDefaultAsync(m => m.MedDepotlegal == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // GET: Medicaments/Create
        public IActionResult Create()
        {
            ViewData["FamCode"] = new SelectList(_context.Familles, "FamCode", "FamLibelle");
            return View();
        }

        // POST: Medicaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MedDepotlegal,MedNomcommercial,FamCode,MedComposition,MedEffets,MedContreindic,MedPrixechantillon,SsmaTimeStamp")] Medicament medicament)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(medicament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["FamCode"] = new SelectList(_context.Familles, "FamCode", "FamCode", medicament.FamCode);
            //return View(medicament);
        }

        // GET: Medicaments/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return NotFound();
            }
            ViewData["FamCode"] = new SelectList(_context.Familles, "FamCode", "FamCode", medicament.FamCode);
            return View(medicament);
        }

        // POST: Medicaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MedDepotlegal,MedNomcommercial,FamCode,MedComposition,MedEffets,MedContreindic,MedPrixechantillon,SsmaTimeStamp")] Medicament medicament)
        {
            if (id != medicament.MedDepotlegal)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentExists(medicament.MedDepotlegal))
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
            ViewData["FamCode"] = new SelectList(_context.Familles, "FamCode", "FamCode", medicament.FamCode);
            return View(medicament);
        }

        // GET: Medicaments/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments
                .Include(m => m.FamCodeNavigation)
                .FirstOrDefaultAsync(m => m.MedDepotlegal == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // POST: Medicaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Medicaments == null)
            {
                return Problem("Entity set 'GSB_Gestion_AppContext.Medicaments'  is null.");
            }
            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament != null)
            {
                _context.Medicaments.Remove(medicament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentExists(string id)
        {
          return (_context.Medicaments?.Any(e => e.MedDepotlegal == id)).GetValueOrDefault();
        }
    }
}
