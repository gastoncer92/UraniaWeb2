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
        private const string Format = "dd MMM yyy";
        private readonly UraniaWebDbContext _context;

        private readonly IWebHostEnvironment _hostingEnvironment;


        public ArticlesController(UraniaWebDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostEnvironment;
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
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Article articulo)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (articulo.IdAticle == 0)
                {
                    // Creamos imagen
                    string nombreArchivo1 = Guid.NewGuid().ToString(); //imagen 1
                    string nombreArchivo2 = Guid.NewGuid().ToString(); //imagen 2
                    string nombreArchivo3 = Guid.NewGuid().ToString(); //audio 1

                    var subidas = Path.Combine(rutaPrincipal, @"archivos\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo1 + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo2 + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo3 + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    articulo.UrlImagen1 = @"\archivos\articulos" + nombreArchivo1 + extension;
                    articulo.UrlImagen2 = @"\archivos\articulos" + nombreArchivo2 + extension;
                    articulo.UrlSound1 = @"\archivos\articulos" + nombreArchivo3 + extension;

                    articulo.DateCreation = DateTime.Now('dd MMM yyy');
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }


        //        [HttpPost]
        //      [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Create(int id, [Bind("IdAticle,TitleArticle,DescritionArticle,DateCreation,UrlImagen1,UrlImagen2,UrlSound1")]  Article articulo, DateTime dateTime)
        //       {
        //         if (ModelState.IsValid)
        //       {
        //         string primaryPath = _hostingEnvironment.WebRootPath;
        //       var file = HttpContext.Request.Form.Files;
        //     if (articulo.article.IdAticle != 0)
        //   {
        //     string namefile = guid.newguid().tostring();
        //                    var subidas = path.combine(primarypath, @"imagenes\articulos");
        //                    var extension = Path.GetExtension(file[0].FileName);
        //                  
        //
        //                using (var fileStream = new FileStream(Path.Combine(subidas, nameFile + extension), FileMode.Create))
        //              {
        //                file[0].CopyTo(fileStream); 
        //          }
        //        articulo.article.UrlImagen1 = @"\imagenes\articulos\" + primaryPath + extension;
        //      
        //
        //                  _context.Articles.Add(articulo.article);
        //                _context.SaveChanges();
        //          }
        //
        //      articulo.DateCreation = DateTime.Now;
        //        _context.Add(articulo);
        //  await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //  }
        //    return View(articulo);
        //  }

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
