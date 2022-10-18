using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.DAL
{
    public class LigaDbContext : IdentityDbContext<Korisnik>
    {
        protected LigaDbContext() { }

        public LigaDbContext(DbContextOptions<LigaDbContext> options)
            : base(options)
        { }

        public DbSet<Igrac> Igraci { get; set; }

        public DbSet<Klub> Klubovi { get; set; }

        public DbSet<Slika> Slike { get; set; }

        public DbSet<Transfer> Transferi { get; set; }

        /*protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Klub>().HasData(/*new Klub() { ImeKluba = "GNK Dinamo Zagreb", BrojBodova = 85, GolRazlika= 56, Trener = "Damir Krznar", Stadion = "Maksimir", BrojTrofeja = 38, GodinaOsnutka = 1911 },
                                      new Klub() { ImeKluba = "NK Osijek", BrojBodova = 77, GolRazlika = 34, Trener = "Nenad Bjelica", Stadion = "Gradski Vrt", BrojTrofeja = 1, GodinaOsnutka =  1947 },
                                      new Klub() { ImeKluba = "HNK Rijeka", BrojBodova = 61, GolRazlika = 5, Trener = "Goran Tomić", Stadion = "Rujevica", BrojTrofeja = 7, GodinaOsnutka = 1946 },
                                      new Klub() { ImeKluba = "HNK Hajduk Split", BrojBodova = 60, GolRazlika = 11, Trener = "Jens Gustafsson", Stadion = "Poljud", BrojTrofeja = 14, GodinaOsnutka = 1911 },
                                      new Klub() { ImeKluba = "HNK Gorica", BrojBodova = 59, GolRazlika = 13, Trener = "Krunoslav Rendulić", Stadion = "SRC Velika Gorica", BrojTrofeja = 0, GodinaOsnutka = 2009 },
                                      new Klub() { ImeKluba = "HNK Šibenik", BrojBodova = 35, GolRazlika = -15, Trener = "Ivan Močinić", Stadion = "Šubićevac", BrojTrofeja = 0, GodinaOsnutka = 1932 },
                                      new Klub() { ImeKluba = "NK Slaven Belupo Koprivnica", BrojBodova = 34, GolRazlika = -17, Trener = "Tomislav Stipić", Stadion = "Gradski stadion Ivan Kušek-Apaš", BrojTrofeja = 0, GodinaOsnutka = 1907 },
                                      new Klub() { ImeKluba = "NK Lokomotiva Zagreb", BrojBodova = 30, GolRazlika = -31, Trener = "", Stadion = "Kranjčevićeva", BrojTrofeja = 0, GodinaOsnutka = 1914 },
                                      new Klub() { ImeKluba = "NK Istra 1961", BrojBodova = 29, GolRazlika = -25, Trener = "Danijel Jumić", Stadion = "Aldo Drosina", BrojTrofeja = 0, GodinaOsnutka = 1948 },
                                      new Klub() { ImeKluba = "NK Varaždin", BrojBodova = 28, GolRazlika = -31, Trener = "", Stadion = "Stadion Varteks", BrojTrofeja = 0, GodinaOsnutka = 2012 });
        }*/
    }
}
