using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemZaZakazuvanje.Models
{
    public class Usluga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Duration { get; set; }
        
        public double Price { get; set; }
        public string Tip { get; set; }
        public string imageUrl { get; set; }
    }
}