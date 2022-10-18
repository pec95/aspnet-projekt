using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Igrac
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }
        [Required]
        public string Pozicija { get; set; }
        [Required]
        public DateTime DatumRodjenja { get; set; }
        [Required]
        public string Nacionalnost { get; set; }
        public char PreferiranaNoga { get; set; }
        [Range(0, 100, ErrorMessage = "Vrijednost ne smije biti viša od 100 milijuna eura")]
        public float VrijednostUMilijunima { get; set; }

        [ForeignKey("Klub")]
        public int? KlubId { get; set; }
        public Klub Klub { get; set; }

        public int Starost
        {
            get
            {
                var dani = DateTime.Now - DatumRodjenja;
                int godine = dani.Days / 365;
                return godine;
            }
        }
        

        public Igrac() { }
    }
}
