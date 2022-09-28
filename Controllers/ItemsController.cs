using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MAQUINAVENDAS.Models;

namespace MaquinaVendas.Controllers
{
    public class ItemsController : Controller
    {
        private readonly MaquinaVendasContext _context;

        public ItemsController(MaquinaVendasContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            List<String> operations = new List<String>(){"terça", "quarta", "quinta", "sexta"};
            ViewBag.Operations = operations;
            var maquinaVendasContext = _context.Items.Include(i => i.Category);
            return View(await maquinaVendasContext.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["FkCategory"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }
        // GET: Items/Create
        public IActionResult CreateWithCategory()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Price,ExpirationDate,Description,FkCategory")] Item item)
        {
            if (ModelState.IsValid)
            {
               _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCategory"] = new SelectList(_context.Categories, "Id", "Name", item.FkCategory);
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithCategory([Bind("Code,Name,Price,ExpirationDate,Description,FkCategory")] Item item, string category)
        {
            if (ModelState.IsValid)
            {
                Category category1 = new Category();
                category1.Name = category;
                _context.Add(category1);
                await _context.SaveChangesAsync();
                item.FkCategory = category1.Id;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FkCategory"] = new SelectList(_context.Categories, "Id", "Name", item.FkCategory);
            return View(item);
        }


        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["FkCategory"] = new SelectList(_context.Categories, "Id", "Name", item.FkCategory);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Name,Price,ExpirationDate,Description,FkCategory")] Item item)
        {
            if (id != item.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Code))
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
            ViewData["FkCategory"] = new SelectList(_context.Categories, "Id", "Name", item.FkCategory);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .FirstOrDefaultAsync(m => m.Code == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'MaquinaVendasContext.Items'  is null.");
            }
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
          return (_context.Items?.Any(e => e.Code == id)).GetValueOrDefault();
        }
    }
}
