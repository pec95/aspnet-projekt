using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Klub
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ImeKluba { get; set; }
        [Required]
        public int BrojBodova { get; set; }
        [Required]
        public int GolRazlika { get; set; }
        [Required]
        public string Trener { get; set; }
        [Required]
        public string Stadion { get; set; }
        [Required]
        public int BrojTrofeja { get; set; }
        [Required]
        public int GodinaOsnutka { get; set; }

        [ForeignKey("Slika")]
        public int? idSlike { get; set; }
        public Slika Slika { get; set; }

        public virtual ICollection<Igrac> IgraciKluba { get; set; }

        public Klub() { }
    }
}
