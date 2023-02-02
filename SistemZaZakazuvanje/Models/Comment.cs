using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemZaZakazuvanje.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int IdReview { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public LinkedList<Comment> Replies { get; set; }
    }
}