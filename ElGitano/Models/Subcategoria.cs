using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.Models
{
    public class Subcategoria
    {
        public int ID { get; set; }
        public string Descripcion { get; set; }
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }
    }
}