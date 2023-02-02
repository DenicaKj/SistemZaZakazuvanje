using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemZaZakazuvanje.Models
{
    public class ListApp
    {
        public List<Appointment> mine { get; set; }
        public List<DeletedAppointment> cancelled { get; set; }
    }
}