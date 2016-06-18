using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Subcategoria> SubCategorias { get; set; }
    }
}