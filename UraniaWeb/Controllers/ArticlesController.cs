using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UraniaWeb.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace UraniaWeb.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly UraniaWebDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticlesController(UraniaWebDbContext context)
        {
            _context = context;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Articles.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.IdAticle == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdAticle,TitleArticle,DescritionArticle,DateCreation,UrlImagen1,UrlImagen2,UrlSound1")] Article article, DateTime dateTime)
        {
            if (ModelState.IsValid)
            {
                string primaryPath = _hostingEnvironment.WebRootPath;
                var file = HttpContext.Request.Form.Files;

                if (article.article.IdAticle == 0)
                {
                    string nameFile = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(primaryPath, @"imagenes\articulos");
                    var extension = Path.GetExtension(file[0].FileName);
                    

                    using (var fileStream = new FileStream(Path.Combine(subidas, nameFile + extension), FileMode.Create))
                    {
                        file[0].CopyTo(fileStream); 
                    }
                    article.article.UrlImagen1 = @"\imagenes\articulos\" + primaryPath + extension;
                    

                    _context.Articles.Add(article.article);
                    _context.SaveChanges();
                }

                article.DateCreation = DateTime.Now;
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Edit(int id, [Bind("IdAticle,TitleArticle,DescritionArticle,DateCreation,UrlImagen1,UrlImagen2,UrlSound1")] Article article)
        {
            if (id != article.IdAticle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.IdAticle))
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
            return View(article);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Articles
                .FirstOrDefaultAsync(m => m.IdAticle == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await _context.Articles.FindAsync(id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e.IdAticle == id);
        }
    }
}
