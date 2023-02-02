using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace SistemZaZakazuvanje.Models
{
    public class DeletedAppointment
    {
        [DisplayName("Почеток на термин"),
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}"), Required]
        public DateTime StartTime { get; set; }
        [DisplayName("Kraj на термин")]
        public DateTime EndTime { get; set; }
        [DisplayName("Избери услуга"), Required]

        public int UslugaId { get; set; }
        [DisplayName("Тип на услуга")]
        public string Usluga { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }
}