using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.DAL;
using Projekt.Model;
using Projekt.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt.Web.Controllers
{
    public class IgracController : Controller
    {
        private LigaDbContext _dbContext;

        public IgracController(LigaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [Authorize(Roles = "Admin")]
        [Route("popis-igraca/kreiraj-igraca")]
        public IActionResult KreirajIgraca()
        {
            ViewBag.klubovi = padajuciIzbornik();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("popis-igraca/kreiraj-igraca")]
        [ActionName("KreirajIgraca")]
        public IActionResult KreirajIgracaPost(Igrac igrac)
        {
            ViewBag.klubovi = padajuciIzbornik();
            ViewBag.igracUspjesno = false;

            if (ModelState.IsValid)
            {
                ViewBag.igracUspjesno = true;

                this._dbContext.Igraci.Add(igrac);
                this._dbContext.SaveChanges();
            }

            return View("KreirajIgraca", igrac);
        }

        [AllowAnonymous]
        [Route("popis-igraca")]
        public IActionResult PopisIgraca()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult PopisIgracaAjax(IgracFilter filterModel)
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).AsQueryable();

            string ime = filterModel.Ime;
            string klub = filterModel.Klub;
            string vrijednost = filterModel.Vrijednost;

            if (!string.IsNullOrEmpty(ime))
            {
                igraci = igraci.Where(i => i.Ime.ToLower().Contains(ime.ToLower()));
            }
            if (!string.IsNullOrEmpty(klub))
            {
                igraci = igraci.Where(i => i.Klub != null && i.Klub.ImeKluba.ToLower().Contains(klub.ToLower()));
            }
            if (!string.IsNullOrEmpty(vrijednost))
            {
                igraci = igraci.Where(i => i.VrijednostUMilijunima.ToString().Equals(vrijednost));
            }

            return PartialView("_IgraciTablica", igraci.ToList());

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult NoviIgrac()
        {
            return PartialView("_NoviIgrac");
        }

        [Authorize]
        [Route("detalji-igrac/{id}")]
        public IActionResult DetaljiIgrac(int id)
        {
            Igrac igrac = this._dbContext.Igraci.Include(i => i.Klub).FirstOrDefault(i => i.Id == id);

            return View(igrac);
        }

        [Authorize(Roles = "Admin")]
        [Route("detalji-igrac/uredi-igrac/{id}")]
        public IActionResult IzmjeniIgrac(int id)
        {
            Igrac igrac = this._dbContext.Igraci.Include(i => i.Klub).FirstOrDefault(i => i.Id == id);

            ViewBag.klubovi = padajuciIzbornik();

            return View(igrac);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("detalji-igrac/uredi-igrac/{id}")]
        [ActionName("IzmjeniIgrac")]
        public async Task<IActionResult> IzmjeniIgracPost(int id, Igrac igracModel)
        {
            Igrac igrac = this._dbContext.Igraci.Include(i => i.Klub).First(i => i.Id == id);

            Transfer transfer = new Transfer();
            transfer.IdMaticnogKluba = igrac.KlubId;
            transfer.IdDolaznogKluba = igracModel.KlubId;
            transfer.IdIgraca = id;

            ViewBag.klubovi = padajuciIzbornik();
            ViewBag.igracUredi = false;

            bool updejtano = await this.TryUpdateModelAsync(igrac);

            if (updejtano)
            {
                this._dbContext.Transferi.Add(transfer);
                this._dbContext.SaveChanges();
                ViewBag.igracUredi = true;
            }

            return View("IzmjeniIgrac", igrac);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult IzbrisiGumb(int id)
        {
            Igrac igrac = this._dbContext.Igraci.First(i => i.Id == id);
            string igracIme = igrac.Ime;

            return PartialView("_IzbrisiGumb", igracIme);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public IActionResult IzbrisiIgraca(int id)
        {
            Igrac igracZaBrisanje = this._dbContext.Igraci.First(i => i.Id == id);

            this._dbContext.Igraci.Attach(igracZaBrisanje);
            this._dbContext.Igraci.Remove(igracZaBrisanje);
            this._dbContext.SaveChanges();

            return RedirectToAction("PopisIgraca");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult IzmjeniGumb(int id)
        {
            return PartialView("_IzmjeniGumb", id);
        }

        [AllowAnonymous]
        [Route("transferi")]
        public IActionResult Transferi()
        {
            var transferi = this._dbContext.Transferi.Include(t => t.MaticniKlub).Include(t => t.DolazniKlub).Include(t => t.Igrac).AsQueryable();

            return View(transferi.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajImeAsc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderBy(i => i.Ime).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajImeDesc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderByDescending(i => i.Ime).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajStarostAsc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).AsQueryable();

            var igraciSort = igraci.AsEnumerable().OrderBy(i => i.Starost).ToList();

            return PartialView("_IgraciTablica", igraciSort);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajStarostDesc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).AsQueryable();

            var igraciSort = igraci.AsEnumerable().OrderByDescending(i => i.Starost).ToList();

            return PartialView("_IgraciTablica", igraciSort);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajKlubAsc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderBy(i => i.Klub.ImeKluba).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajKlubDesc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderByDescending(i => i.Klub.ImeKluba).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajVrijednostDesc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderByDescending(i => i.VrijednostUMilijunima).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SortirajVrijednostAsc()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).OrderBy(i => i.VrijednostUMilijunima).AsQueryable();

            return PartialView("_IgraciTablica", igraci.ToList());
        }

        private List<SelectListItem> padajuciIzbornik()
        {
            var selectItems = new List<SelectListItem>();

            var listItem = new SelectListItem();
            listItem.Text = "Odaberite klub";
            listItem.Value = "";
            selectItems.Add(listItem);

            foreach (var klub in _dbContext.Klubovi)
            {
                listItem = new SelectListItem();
                listItem.Text = klub.ImeKluba;
                listItem.Value = klub.Id.ToString();
                selectItems.Add(listItem);
            }

            return selectItems;
        }
    }
}
