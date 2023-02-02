using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemZaZakazuvanje.Models
{
    public class EditParams
    {
        public string key { get; set; }
        public string action { get; set; }
        public List<Appointment> added { get; set; }
        public List<Appointment> changed { get; set; }
        public List<Appointment> deleted { get; set; }
        public Appointment value { get; set; }
    }
}