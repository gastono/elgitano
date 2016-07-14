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
        public int CategoriaID { get; set; }
        public int SubcategoriaID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }        
        public ICollection<Image> Imagenes { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
    }
}