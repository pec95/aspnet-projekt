using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.DAL;
using Projekt.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Web.Controllers
{
    public class MomcadController : Controller
    {
        private LigaDbContext _dbContext;

        public MomcadController(LigaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [Route("detalji-momcadi/postavi-sliku/{id}")]
        public IActionResult PostaviSliku(int id)
        {
            Klub klub = this._dbContext.Klubovi.FirstOrDefault(k => k.Id == id);

            return View("PostaviSliku", klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("detalji-momcadi/postavi-sliku/{id}")]
        [ActionName("PostaviSliku")]
        public async Task<IActionResult> PostaviSlikuPost(int id)
        {
            ViewBag.uspjesnoSlika = false;
            var klub = this._dbContext.Klubovi.FirstOrDefault(k => k.Id == id);

            foreach (var fajl in Request.Form.Files)
            {
                Slika slika = new Slika();
                slika.NazivSlike = fajl.FileName;

                MemoryStream ms = new MemoryStream();
                fajl.CopyTo(ms);
                slika.PodaciSlike = ms.ToArray();

                ms.Close();
                ms.Dispose();

                this._dbContext.Slike.Add(slika);
                this._dbContext.SaveChanges();

                klub.idSlike = slika.Id;

                bool updejtano = await this.TryUpdateModelAsync(klub);

                if (updejtano)
                {
                    ViewBag.uspjesnoSlika = true;
                    this._dbContext.SaveChanges();
                }
            }

            return View("PostaviSliku", klub);
        }

        [AllowAnonymous]
        [Route("tablica-momcadi")]
        public IActionResult Tablica()
        {
            var klubovi = this._dbContext.Klubovi.Include(k => k.Slika).AsQueryable();

            return View(klubovi.ToList());
        }

        [AllowAnonymous]
        [Route("popis-momcadi")]
        public IActionResult PopisMomcadi()
        {
            var klubovi = this._dbContext.Klubovi.Include(k => k.Slika).AsQueryable();

            foreach(var klub in klubovi)
            {
                var idSlike = klub.idSlike;
                Slika slika = this._dbContext.Slike.FirstOrDefault(s => s.Id == idSlike);
                string imageBase64Data = Convert.ToBase64String(slika.PodaciSlike);
                string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

                klub.Slika.NazivSlike = imageDataURL;
            } 

            return View(klubovi.ToList());

        }

        [Authorize]
        [Route("detalji-momcadi/{idMomcadi}")]
        public IActionResult DohvatMomcad(int idMomcadi)
        {
            var klub = this._dbContext.Klubovi.Include(k => k.Slika).Include(k => k.IgraciKluba).FirstOrDefault(k => k.Id == idMomcadi);
            var idSlike = klub.idSlike;

            Slika slika = this._dbContext.Slike.FirstOrDefault(s => s.Id == idSlike);
            string imageBase64Data = Convert.ToBase64String(slika.PodaciSlike);
            string imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);

            ViewBag.Image = imageDataURL;

            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        [Route("detalji-momcadi/uredi-momcad/{id}")]
        public IActionResult UrediMomcad(int id)
        {
            Klub klub = this._dbContext.Klubovi.FirstOrDefault(k => k.Id == id);

            return View(klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("detalji-momcadi/uredi-momcad/{id}")]
        [ActionName("UrediMomcad")]
        public async Task<IActionResult> UrediMomcadPost(int id)
        {
            Klub klub = this._dbContext.Klubovi.First(k => k.Id == id);
            ViewBag.momcadUredi = false;

            bool updejtano = await this.TryUpdateModelAsync(klub);

            if(updejtano)
            {
                this._dbContext.SaveChanges();
                ViewBag.momcadUredi = true;
            }

            return View("UrediMomcad", klub);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult IzmjeniKlub(int id)
        {
            return PartialView("_IzmjeniKlub", id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult IzmjeniSliku(int id)
        {
            return PartialView("_IzmjeniSliku", id);
        }
    }
}
