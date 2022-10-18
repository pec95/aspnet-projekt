using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
    public class Slika
    {
        [Key]
        public int Id { get; set; }
        public string NazivSlike { get; set; }
        public byte[] PodaciSlike { get; set; }
    }
}
