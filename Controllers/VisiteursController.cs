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
    public class VisiteursController : Controller
    {
        private readonly GSB_Gestion_AppContext _context;

        public VisiteursController(GSB_Gestion_AppContext context)
        {
            _context = context;
        }

        // GET: Visiteurs
        public async Task<IActionResult> Index()
        {
            var gSB_Gestion_AppContext = _context.Visiteurs.Include(v => v.LabCodeNavigation).Include(v => v.SecCodeNavigation);
            return View(await gSB_Gestion_AppContext.ToListAsync());
        }

        // GET: Visiteurs/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Visiteurs == null)
            {
                return NotFound();
            }

            var visiteur = await _context.Visiteurs
                .Include(v => v.LabCodeNavigation)
                .Include(v => v.SecCodeNavigation)
                .FirstOrDefaultAsync(m => m.VisMatricule == id);
            if (visiteur == null)
            {
                return NotFound();
            }

            return View(visiteur);
        }

        // GET: Visiteurs/Create
        public IActionResult Create()
        {
            ViewData["LabCode"] = new SelectList(_context.Labos, "LabCode", "LabCode");
            ViewData["SecCode"] = new SelectList(_context.Secteurs, "SecCode", "SecCode");
            return View();
        }

        // POST: Visiteurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VisMatricule,VisNom,VisPrenom,VisAdresse,VisCp,VisVille,VisDateembauche,SecCode,LabCode")] Visiteur visiteur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visiteur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabCode"] = new SelectList(_context.Labos, "LabCode", "LabCode", visiteur.LabCode);
            ViewData["SecCode"] = new SelectList(_context.Secteurs, "SecCode", "SecCode", visiteur.SecCode);
            return View(visiteur);
        }

        // GET: Visiteurs/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Visiteurs == null)
            {
                return NotFound();
            }

            var visiteur = await _context.Visiteurs.FindAsync(id);
            if (visiteur == null)
            {
                return NotFound();
            }
            ViewData["LabCode"] = new SelectList(_context.Labos, "LabCode", "LabCode", visiteur.LabCode);
            ViewData["SecCode"] = new SelectList(_context.Secteurs, "SecCode", "SecCode", visiteur.SecCode);
            return View(visiteur);
        }

        // POST: Visiteurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("VisMatricule,VisNom,VisPrenom,VisAdresse,VisCp,VisVille,VisDateembauche,SecCode,LabCode")] Visiteur visiteur)
        {
            if (id != visiteur.VisMatricule)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visiteur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisiteurExists(visiteur.VisMatricule))
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
            ViewData["LabCode"] = new SelectList(_context.Labos, "LabCode", "LabCode", visiteur.LabCode);
            ViewData["SecCode"] = new SelectList(_context.Secteurs, "SecCode", "SecCode", visiteur.SecCode);
            return View(visiteur);
        }

        // GET: Visiteurs/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Visiteurs == null)
            {
                return NotFound();
            }

            var visiteur = await _context.Visiteurs
                .Include(v => v.LabCodeNavigation)
                .Include(v => v.SecCodeNavigation)
                .FirstOrDefaultAsync(m => m.VisMatricule == id);
            if (visiteur == null)
            {
                return NotFound();
            }

            return View(visiteur);
        }

        // POST: Visiteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Visiteurs == null)
            {
                return Problem("Entity set 'GSB_Gestion_AppContext.Visiteurs'  is null.");
            }
            var visiteur = await _context.Visiteurs.FindAsync(id);
            if (visiteur != null)
            {
                _context.Visiteurs.Remove(visiteur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisiteurExists(string id)
        {
          return (_context.Visiteurs?.Any(e => e.VisMatricule == id)).GetValueOrDefault();
        }
    }
}
