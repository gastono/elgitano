using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.Requests
{
    public class NuevaPublicacionRequest
    {
        public int CategoriaId { get; set; }
        public int SubCategoriaId { get; set; }
        public int UsuarioId { get; set; }
        public int ProductoId { get; set; }
        public string Comentario { get; set; }
        public string Titulo { get; set; }
        public List<Image> Imagenes { get; set; }
    }
}