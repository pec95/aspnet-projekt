using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.DAL;
using Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Projekt.Model.API;

namespace Projekt.Web.Controllers
{
    [ApiController]
    [Route("api/igrac")]
    public class IgracAPIController : Controller
    {
        private LigaDbContext _dbContext;

        public IgracAPIController(LigaDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult GetSve()
        {
            var igraci = pretvorbaUListu();

            return Ok(igraci);
        }

        [Route("{id}")]
        public IActionResult GetPojedini(int id)
        {
            var igraci = pretvorbaUListu();

            var igrac = igraci.FirstOrDefault(i => i.Id == id);

            if(igrac == null)
            {
                return NotFound();
            }

            return Ok(igrac);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Igrac igrac)
        {
            if(ModelState.IsValid)
            {
                this._dbContext.Igraci.Add(igrac);
                this._dbContext.SaveChanges();
            }

            return GetPojedini(igrac.Id);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Igrac igracPost)
        {
            Igrac igrac = this._dbContext.Igraci.FirstOrDefault(i => i.Id == id);

            if(igrac == null)
            {
                return NotFound();
            }

            igrac.Id = id;
            igrac.Ime = igracPost.Ime;
            igrac.Pozicija = igracPost.Pozicija;
            igrac.DatumRodjenja = igracPost.DatumRodjenja;
            igrac.PreferiranaNoga = igracPost.PreferiranaNoga;
            igrac.VrijednostUMilijunima = igracPost.VrijednostUMilijunima;
            igrac.Nacionalnost = igracPost.Nacionalnost;
            igrac.KlubId = igracPost.KlubId;

            bool updejtano = await this.TryUpdateModelAsync(igrac);

            if (updejtano)
            {
                this._dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            var klijenti = pretvorbaUListu();

            Igrac igracZaBrisanje = this._dbContext.Igraci.FirstOrDefault(i => i.Id == id);
            
            if(igracZaBrisanje == null)
            {
                return NotFound();
            }

            this._dbContext.Igraci.Attach(igracZaBrisanje);
            this._dbContext.Igraci.Remove(igracZaBrisanje);
            this._dbContext.SaveChanges();

            return Ok();
        }

        private List<IgracAPI> pretvorbaUListu()
        {
            var igraci = this._dbContext.Igraci.Include(i => i.Klub).Select(i => new IgracAPI()
                                {
                                    Id = i.Id,
                                    Ime = i.Ime,
                                    Pozicija = i.Pozicija,
                                    DatumRodjenja = i.DatumRodjenja,
                                    Nacionalnost = i.Nacionalnost,
                                    PreferiranaNoga = i.PreferiranaNoga,
                                    VrijednostUMilijunima = i.VrijednostUMilijunima,
                                    KlubId = i.KlubId
                                }).AsQueryable();

            var igraciApi = igraci.ToList();

            var klubovi = this._dbContext.Klubovi.Select(k => new KlubAPI()
                                {
                                    Id = k.Id,
                                    ImeKluba = k.ImeKluba,
                                    BrojBodova = k.BrojBodova,
                                    GolRazlika = k.GolRazlika,
                                    Trener = k.Trener,
                                    Stadion = k.Stadion,
                                    BrojTrofeja = k.BrojTrofeja,
                                    GodinaOsnutka = k.GodinaOsnutka                                    
                                }).AsQueryable();

            var kluboviApi = klubovi.ToList();

            foreach(IgracAPI igracApi in igraciApi)
            {
                foreach(KlubAPI klubApi in kluboviApi)
                {
                    if(igracApi.KlubId == klubApi.Id)
                    {
                        igracApi.Klub = klubApi;
                    }
                }
            }

            return igraciApi;
        }
    }
}
