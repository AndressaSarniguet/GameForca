using GameForca.Data;
using GameForca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GameForca.Controllers.Models
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        internal DbSet<PalavraSecreta> dbSet;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            this.dbSet = _db.Set<PalavraSecreta>();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jogar()
        {
            int lastId = _db.PalavrasSecretas.Max(_ => _.Id);
            Random r = new();
            int pRandom = r.Next(1, lastId);
            var pSecreta = _db.PalavrasSecretas.Include(c => c.Categoria).FirstOrDefault(_ => _.Id == pRandom);

            if (pSecreta == null)
                return NotFound();

            return View(pSecreta);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}