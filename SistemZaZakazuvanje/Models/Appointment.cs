using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace SistemZaZakazuvanje.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Stauts")]
        public string Subject { get; set; }
        [DisplayName("Start Date"),
            DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss tt}"),Required]
        public DateTime StartTime { get; set; }
        [DisplayName("End Date")]
        public DateTime EndTime { get; set; }
        [DisplayName("Choose Service"),Required]

        public int UslugaId{ get; set; }
        [DisplayName("Type of service")]
        public string Usluga { get; set; }
        public string Status {get;set;}
        public string UserId { get; set; }
        public string color { get; set; }
    }
}