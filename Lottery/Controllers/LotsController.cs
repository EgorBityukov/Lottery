﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lottery.Data;
using Lottery.Models;
using Lottery.ViewModels;

namespace Lottery.Controllers
{
    public class LotsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LotsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Lots
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Lots.Include(l => l.Photo);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Lots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Photo)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // GET: Lots/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Lots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LotId,Name,Price,TicketCount,TicketPrice,Image")] LotViewModel lotVM)
        {
            Lot lot = new Lot()
            {
                LotId =lotVM.LotId,
                Name =lotVM.Name,
                Price =lotVM.Price,
                TicketCount =lotVM.TicketCount,
                TicketPrice =lotVM.TicketPrice,
            };

            if (lotVM.Image != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(lotVM.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)lotVM.Image.Length);
                }
                // установка массива байтов
                lot.Photo = new Photo() { Image = imageData, Name = lotVM.Image.Name };
            }

            for (int i = 0; i < lot.TicketCount; i++)
            {
                _context.Tickets.Add(new Ticket()
                {
                    Lot = lot
                });
            }

            if (ModelState.IsValid)
            {
                _context.Add(lot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lot);
        }

        // GET: Lots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }
            return View(lot);
        }

        // POST: Lots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LotId,Name,Price,TicketCount,TicketPrice,PhotoId")] Lot lot)
        {
            if (id != lot.LotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.LotId))
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
            return View(lot);
        }

        // GET: Lots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Photo)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lots == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Lots'  is null.");
            }
            var lot = await _context.Lots.FindAsync(id);
            if (lot != null)
            {
                _context.Lots.Remove(lot);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
            return _context.Lots.Any(e => e.LotId == id);
        }
    }
}