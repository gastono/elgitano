using ElGitano.DAL;
using ElGitano.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ElGitano.Apis
{
    public class NewProductApiController : ApiController
    {
        public Size defaultThumbnailSize = new Size { Height = 120, Width = 120 };

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
        public ElGitano.Models.Image ProcesarImagen()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var request = HttpContext.Current.Request;

            string root = HttpContext.Current.Server.MapPath("~/App_Temp/");

            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            string directorioImagenesFisico = HttpContext.Current.Server.MapPath("~/Images/");

            string rootDirectory = HttpContext.Current.Server.MapPath("~");

            string urlImagenes = this.Url.Content("~/Images/");

            try
            {
                //tomo parametros del ingreso del producto             

                var usuario = request.Params["UsuarioId"];

                var titulo = request.Params["Titulo"];

                string productDir = directorioImagenesFisico + "\\" + usuario + "\\" + titulo;

                string thumbnailDir = directorioImagenesFisico + "\\" + usuario + "\\Thumbnails" + "\\" + titulo;

                //creo los directorios si no existen

                if (!Directory.Exists(productDir))
                {
                    Directory.CreateDirectory(productDir);
                }

                if (!Directory.Exists(thumbnailDir))
                {
                    Directory.CreateDirectory(thumbnailDir);
                }

                int cantActualImagenes = 0;

                //creo los archivos en sus directorios especificos

                if (request.Files.Count > 0)
                {                   
                        cantActualImagenes = Directory.GetFiles(productDir).Length;

                        cantActualImagenes++;

                        var cantActualImagenesFormateada = String.Format("{0:00}", cantActualImagenes);

                        var imagenPath = productDir + "\\" + cantActualImagenesFormateada + ".jpg";

                        var thumbnailPath = thumbnailDir + "\\" + cantActualImagenesFormateada + ".jpg";

                        var postedFile = request.Files[0];

                        var filePath = HttpContext.Current.Server.MapPath(root + string.Format("{0}", postedFile.FileName));

                        resizeImagetoThumbnail(filePath).Save(thumbnailPath);

                        System.IO.File.Move(filePath, imagenPath);

                        return new ElGitano.Models.Image() { ThumnailUrl = thumbnailPath, Url = imagenPath };
                }
                else
                {
                    throw new Exception("No se ha subido ninguna imagen");
                }         

            }
            catch (Exception)
            {

                throw;
            }
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

        public System.Drawing.Image resizeImagetoThumbnail(string path)
        {
            System.Drawing.Image img = null;

            using (Stream bitmapStream = System.IO.File.Open(path, FileMode.Open))
            {
                img = System.Drawing.Image.FromStream(bitmapStream);
            }

            return (System.Drawing.Image)(new Bitmap(img, defaultThumbnailSize));
        }

    }
}
