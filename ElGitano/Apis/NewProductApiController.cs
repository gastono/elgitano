using ElGitano.DAL;
using ElGitano.Models;
using ElGitano.Requests;
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

            if (!Directory.Exists(root))//directorio temporal
            {
                Directory.CreateDirectory(root);
            }

            string directorioImagenesFisico = HttpContext.Current.Server.MapPath("~/Images/"); //directorio de imagenes

            string rootDirectory = HttpContext.Current.Server.MapPath("~");

            string urlImagenes = this.Url.Content("~/Images/");//url imagenes

            try
            {
                //tomo parametros del ingreso del producto             

                var usuario = request.Params["UsuarioId"];

                var productId = "0";//request.Params["Titulo"];

                 
                // Estructura: 
                //  Images
                //    |
                // UsuarioId
                //    |_ _ _ ProductoID
                //             |_ _ _ _ _ Img_01
                //                          |_ _ _ Original
                //                          |_ _ _ Thumnail

                //url directorio productos
                string RFDirectorioProducto = directorioImagenesFisico + "\\" + usuario + "\\" + productId; // directorio producto images-usuario-producto (correcto)
                string URLDirectorioProducto = "\\Images\\" + usuario + "\\" + productId; // url producto, images-usuario-producto (correcto)

                //ruta fisica directorio imagen original producto
                //url directorio imagen original producto
                string RFDirectorioImgOriginal = directorioImagenesFisico + "\\" + usuario + "\\" + productId + "\\" + "img_{0}" + "\\Original" + "\\";
                string URLDirectorioImgOriginal = "\\Images\\" + usuario + "\\" + productId + "\\" + "img_{0}" + "\\Original" + "\\";

                //ruta fisica directorio thumbnail producto
                //url directorio thumbnail producto
                string RFDirectorioThumbnail = directorioImagenesFisico + "\\" + usuario + "\\" + productId +"\\"+"img_{0}"+ "\\Thumbnail" + "\\";
                string URLDirectorioThumbnauk = "\\Images\\" + usuario + "\\" + productId + "\\" + "img_{0}" + "\\Thumbnail" + "\\";

                //-- Se crea el directorio del producto

                if (!Directory.Exists(RFDirectorioProducto))
                {
                    Directory.CreateDirectory(RFDirectorioProducto);
                }
                
                //Inicializacion de cantidad de directorios
                int cantDirectorios = 0;

                //creo los archivos en sus directorios especificos

                if (request.Files.Count > 0)
                {       
                        //cuento la cantidad de directorios para asignar el numero correspondiente
                        cantDirectorios = Directory.GetDirectories(RFDirectorioProducto).Length;
                       
                        //incremento el contador
                        cantDirectorios++;

                        //string de nombre de directorio nuevo
                        var cantActualDirectoriosFormateada = String.Format("{0:00}", cantDirectorios);

                        //rutas imagen original de producto
                        var RFImagenOriginal = string.Format(RFDirectorioImgOriginal, cantActualDirectoriosFormateada);
                        RFImagenOriginal += string.Format("img_{0:00}.jpg", cantDirectorios);

                        var URLImagenOriginal = string.Format(URLDirectorioImgOriginal, cantActualDirectoriosFormateada);
                        URLImagenOriginal += string.Format("img_{0:00}.jpg", cantDirectorios);
                        
                        //rutas thumbnail
                        var RFThumbnail = string.Format(RFDirectorioThumbnail, cantActualDirectoriosFormateada);
                        RFThumbnail += string.Format("img_{0:00}.jpg", cantDirectorios);

                        var URLThumbnail = string.Format(URLDirectorioThumbnauk, cantActualDirectoriosFormateada);
                        URLThumbnail += string.Format("img_{0:00}.jpg", cantDirectorios);

                        var postedFile = request.Files[0];

                        var filePath = root + string.Format("{0}", postedFile.FileName);

                        postedFile.SaveAs(filePath);
                      
                        resizeImagetoThumbnail(filePath).Save(RFThumbnail);

                        System.IO.File.Move(filePath, RFImagenOriginal);

                        return new ElGitano.Models.Image() { ThumnailUrl = URLThumbnail, Url = URLImagenOriginal };
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

        private void DeleteFilesAndDirectories(DirectoryInfo di)
        {
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

        public IHttpActionResult CancelarPublicacion(int idProducto, int idUsuario)
        {
            string directorioImagenesFisico = HttpContext.Current.Server.MapPath("~/Images/");

            string rootDirectory = HttpContext.Current.Server.MapPath("~");

            try
            {
                string productDir = directorioImagenesFisico + "\\" + idUsuario + "\\" + idProducto;

                string thumbnailDir = directorioImagenesFisico + "\\" + idUsuario + "\\Thumbnails" + "\\" + idProducto;

                DirectoryInfo prodDirInfo = new DirectoryInfo(productDir);

                DirectoryInfo thumnailDirInfo = new DirectoryInfo(thumbnailDir);

                DeleteFilesAndDirectories(prodDirInfo);

                DeleteFilesAndDirectories(thumnailDirInfo);

                return Ok(true);
            }
            catch (Exception)
            {
                
                throw;
            }

        }

        [HttpPost]
        public IHttpActionResult ConfirmarPublicacion(NuevaPublicacionRequest request)
        {
            var NPDataAccess = new NewProductDataAccess();

            Producto producto = new Producto();

            producto.Descripcion = request.Comentario;

            producto.Titulo = request.Titulo;

            producto.CategoriaID = request.CategoriaId;

            producto.SubcategoriaID = request.SubCategoriaId;

            producto.Imagenes = request.Imagenes;

            try
            {
                NPDataAccess.ConfirmarPublicacion(producto);

                return Ok(true);
            }
            catch (Exception)
            {
                
                throw;
            }        
 
        }

    }
}
