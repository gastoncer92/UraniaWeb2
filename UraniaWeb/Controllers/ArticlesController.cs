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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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


                    string extension1;
                    string extension2;
                    string extension3;


                    ////////////////////////////
                    try
                    {
                        extension1 = Path.GetExtension(archivos[0].FileName);
                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo1 + extension1), FileMode.Create))
                        {
                            archivos[0].CopyTo(fileStreams);

                        }
                        articulo.UrlImagen1 = @"\archivos\articulos" + nombreArchivo1 + extension1;
                    }
                    catch (Exception)
                    {
                        extension1 = "";
                    }
                    ////////////////////////
                    try
                    {
                        extension2 = Path.GetExtension(archivos[1].FileName);
                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo2 + extension2), FileMode.Create))
                        {
                            archivos[1].CopyTo(fileStreams);
                        }
                        articulo.UrlImagen2 = @"\archivos\articulos" + nombreArchivo2 + extension2;
                    }
                    catch (Exception)
                    {
                        extension2 = "";
                    }
                    ////////////////////////
                    try
                    {
                        extension3 = Path.GetExtension(archivos[2].FileName);
                        using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo3 + extension3), FileMode.Create))
                        {
                            archivos[2].CopyTo(fileStreams);
                        }
                        articulo.UrlSound1= @"\archivos\articulos" + nombreArchivo3 + extension3;
                    }
                    catch (Exception)
                    {
                        extension3 = "";
                    }
                    ////////////////////////

                    articulo.DateCreation = DateTime.Now;
                    _context.Articles.Add(articulo);
                    _context.SaveChanges();
                    return RedirectToAction(nameof(Index));


                    //_contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    //_contenedorTrabajo.Save();

                    //return RedirectToAction(nameof(Index));



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
        
        public async Task<IActionResult> Edit(int id, [Bind("IdAticle,TitleArticle,DescritionArticle,DateCreation,UrlImagen1,UrlImagen2,UrlSound1")] Article article1)
        {
            if (id != article1.IdAticle)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(article1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article1.IdAticle))
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
            return View(article1);
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
