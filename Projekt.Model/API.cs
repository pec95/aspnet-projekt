using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class API
    {
        public class IgracAPI
        {
            public int Id { get; set; }
            public string Ime { get; set; }
            public string Pozicija { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string Nacionalnost { get; set; }
            public char PreferiranaNoga { get; set; }
            public float VrijednostUMilijunima { get; set; }
            public int? KlubId { get; set; }
            public KlubAPI Klub { get; set; }

            public int Starost
            {
                get
                {
                    var dani = DateTime.Now - DatumRodjenja;
                    int godine = dani.Days / 365;
                    return godine;
                }
            }

            public IgracAPI() { }
        }

        public class KlubAPI
        {
            public int Id { get; set; }
            public string ImeKluba { get; set; }
            public int BrojBodova { get; set; }
            public int GolRazlika { get; set; }
            public string Trener { get; set; }
            public string Stadion { get; set; }
            public int BrojTrofeja { get; set; }
            public int GodinaOsnutka { get; set; }

            public KlubAPI() { }
        }
    }
}
