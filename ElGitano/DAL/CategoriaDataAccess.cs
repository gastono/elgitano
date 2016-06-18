using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElGitano.DAL
{
    public class CategoriaDataAccess
    {
        private ElGitanoContext db = new ElGitanoContext();

        public List<Categoria> GetCategorias()
        {
            try
            {
                var query = db.Categorias;

                return query.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public List<Subcategoria> GetSubCategoriasByCategoria(int categoriaID)
        {
            try
            {
                var query = db.Subcategorias.Where(_ => _.CategoriaID == categoriaID);

                return query.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}