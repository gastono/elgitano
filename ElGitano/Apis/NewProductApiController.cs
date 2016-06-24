using ElGitano.DAL;
using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ElGitano.Apis
{
    public class NewProductApiController : ApiController
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

        [HttpPost]
        public IHttpActionResult CrearPublicacion()
        {            

            var request = HttpContext.Current.Request;

            var categoria = request.Params["Categoria"];

            var subcategoria = request.Params["SubCategoria"];

            var descripcion = request.Params["Descripcion"];

            if (request.Files.Count > 0)
            {
                foreach (string file in request.Files)
                {                    
                    var postedFile = request.Files[file];
                    var filePath = HttpContext.Current.Server.MapPath(string.Format("~/Uploads/{0}", postedFile.FileName));
                    postedFile.SaveAs(filePath);
                }
                return Ok(true);
            }
            else
                return BadRequest();
        }

    }
}
