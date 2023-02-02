using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SistemZaZakazuvanje.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UslugaId { get; set; }

        [DisplayName("Service")]
        public string Usluga { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        [DisplayName("Image")]
        public string ImageUrl { get; set; }
        public LinkedList<Comment> Comments { get; set; }
}
}