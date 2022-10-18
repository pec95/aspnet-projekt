using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt.Model
{
   public class Korisnik : IdentityUser
    {
        [ForeignKey("KlubNajdrazi")]
        public int? KlubIdNajdrazi { get; set; }
        public Klub KlubNajdrazi { get; set; }
    }
}
