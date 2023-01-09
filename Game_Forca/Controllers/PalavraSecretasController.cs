using GameForca.Data;
using GameForca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameForca.Controllers
{
    public class PalavraSecretasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PalavraSecretasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PalavraSecretas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PalavrasSecretas.Include(p => p.Categoria);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PalavraSecretas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PalavrasSecretas == null)
            {
                return NotFound();
            }

            var palavraSecreta = await _context.PalavrasSecretas
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palavraSecreta == null)
            {
                return NotFound();
            }

            return View(palavraSecreta);
        }

        // GET: PalavraSecretas/Create
        public IActionResult Create()
        {
            PalavraSecreta palavraSecreta = new();
            IEnumerable<SelectListItem> CategoriasList = _context.Categorias.ToList().Select(
                c => new SelectListItem
                {
                    Text = c.Descricao,
                    Value = c.Id.ToString()
                });
            ViewBag.CategoriasList = CategoriasList;
            return View(palavraSecreta);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PalavraSecreta obj)
        {
            if (ModelState.IsValid)
            {
                _context.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: PalavraSecretas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PalavrasSecretas == null)
            {
                return NotFound();
            }

            var palavraSecreta = await _context.PalavrasSecretas.FindAsync(id);
            if (palavraSecreta == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", palavraSecreta.CategoriaId);
            return View(palavraSecreta);
        }

        // POST: PalavraSecretas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Palavras,CategoriaId")] PalavraSecreta palavraSecreta)
        {
            if (id != palavraSecreta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palavraSecreta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalavraSecretaExists(palavraSecreta.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", palavraSecreta.CategoriaId);
            return View(palavraSecreta);
        }

        // GET: PalavraSecretas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PalavrasSecretas == null)
            {
                return NotFound();
            }

            var palavraSecreta = await _context.PalavrasSecretas
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (palavraSecreta == null)
            {
                return NotFound();
            }

            return View(palavraSecreta);
        }

        // POST: PalavraSecretas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PalavrasSecretas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PalavrasSecretas'  is null.");
            }
            var palavraSecreta = await _context.PalavrasSecretas.FindAsync(id);
            if (palavraSecreta != null)
            {
                _context.PalavrasSecretas.Remove(palavraSecreta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalavraSecretaExists(int id)
        {
            return _context.PalavrasSecretas.Any(e => e.Id == id);
        }
    }
}
