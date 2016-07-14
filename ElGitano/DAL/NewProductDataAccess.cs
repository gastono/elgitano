using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.DAL
{
    public class NewProductDataAccess
    {
        private ElGitanoContext db = new ElGitanoContext();

        public int ConfirmarPublicacion(Producto producto)
        {
            try
            {
                db.Productos.Add(producto);

                db.SaveChanges();

                return producto.ID;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
 
    }
}