using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CodeSnippets.Data;
using CodeSnippets.Models;
using Microsoft.AspNetCore.Authorization;

namespace CodeSnippets.Controllers
{
    public class SnippetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SnippetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Snippets
        public async Task<IActionResult> Index()
        {
              return _context.Snippet != null ? 
                          View(await _context.Snippet.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Snippet'  is null.");
        }

        // GET: Snippets/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // POST: Snippets/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Snippet.Where( s => s.Name.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Snippets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Snippet == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // GET: Snippets/Create
        [Authorize]
        public IActionResult Create()
        {
            var languages = new List<string> { "C++", "C#", "Java", "Python", "C" };

            ViewBag.Language = new SelectList(languages, "Language", "Language");

            return View();
        }

        // POST: Snippets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Language,Code,Author")] Snippet snippet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(snippet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(snippet);
        }

        // GET: Snippets/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Snippet == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet.FindAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }
            return View(snippet);
        }

        // POST: Snippets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Language,Code,Author")] Snippet snippet)
        {
            if (id != snippet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //snippet.Code = snippet.Code.Replace(System.Environment.NewLine, "<br/>");
                try
                {
                    _context.Update(snippet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SnippetExists(snippet.Id))
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
            return View(snippet);
        }

        // GET: Snippets/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Snippet == null)
            {
                return NotFound();
            }

            var snippet = await _context.Snippet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snippet == null)
            {
                return NotFound();
            }

            return View(snippet);
        }

        // POST: Snippets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Snippet == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Snippet'  is null.");
            }
            var snippet = await _context.Snippet.FindAsync(id);
            if (snippet != null)
            {
                _context.Snippet.Remove(snippet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnippetExists(int id)
        {
          return (_context.Snippet?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
