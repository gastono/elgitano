using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.Models
{
    public class Publicacion
    {
        public int ID { get; set; }
        public int ProductoID { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
