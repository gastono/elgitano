using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.DAL
{
    public class HomeDataAccess
    {
        private ElGitanoContext db = new ElGitanoContext();
        public List<Publicacion> GetPublicaciones()
        {
            try
            {
                var query = db.Publicaciones.Include("Producto");

                return query.ToList();
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}