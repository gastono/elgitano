using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.Models
{
    public class Producto
    {
        public int ID { get; set;}
        public int UsuarioID { get; set; }
        public string Descripcion { get; set; }
        public string Url { get; set; }
        public string ThumnailUrl { get; set; } 
    }
}