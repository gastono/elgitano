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
    public class NewProductController : ApiController
    {
        [HttpGet]
        public List<Categoria> GetCategorias()
        {
            var CDataAccess = new CategoriaDataAccess();
            return CDataAccess.GetCategorias();
        }

        [HttpGet]
        public List<Subcategoria> GetSubCategorias(int categoriaID)
        {
            var CDataAccess = new CategoriaDataAccess();
            return CDataAccess.GetSubCategoriasByCategoria(categoriaID);
        }

    }
}
