using ElGitano.DAL;
using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElGitano.Apis
{
    public class HomeApiController : ApiController
    {
        [HttpGet]
        public List<Publicacion> GetPublicaciones()
        {
            //List<Publicacion> Publicaciones = new List<Publicacion>();
            //Producto producto = new Producto(){ ID = 1, Descripcion = "caña de pescar", ThumnailUrl = "path1", Url = "path2", UsuarioID = 1} ;
            //Producto producto2 = new Producto() { ID = 1, Descripcion = "caña de pescar", ThumnailUrl = "path1", Url = "path2", UsuarioID = 1 };
           
            //Publicaciones.Add(new Publicacion { ID = 1, Producto = producto, ProductoID = 1 });
            //Publicaciones.Add(new Publicacion { ID = 1, Producto = producto2, ProductoID = 1 });

            //return Publicaciones;
            var HDataAccess = new HomeDataAccess();
            return HDataAccess.GetPublicaciones();            
        }
    }
}
