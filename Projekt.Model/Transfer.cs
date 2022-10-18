using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Transfer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("MaticniKlub")]
        public int? IdMaticnogKluba { get; set; }
        public Klub MaticniKlub { get; set; }

        [ForeignKey("DolazniKlub")]
        public int? IdDolaznogKluba { get; set; }
        public Klub DolazniKlub { get; set; }

        [ForeignKey("Igrac")]
        public int? IdIgraca { get; set; }
        public Igrac Igrac { get; set; }
    }
}
