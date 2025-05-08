using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoGController : Controller
    {
        // GET: ProductoG
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();


            ML.Result result = BL.ProductoG.GetAll(producto);

            if (result.Success)
            {
                
                producto.Productos = result.Objects;
            }

            ML.Result ddlCategoria = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = ddlCategoria.Objects;

            ML.Result ddlSubCategoria = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = ddlSubCategoria.Objects;

            return View(producto);
        }

        [HttpGet]
        public JsonResult GetAllByCategoria(int idCategoria)
        {
            ML.Result result = BL.SubCategoria.GetAll(idCategoria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAll(ML.Producto producto)
        {
            producto.SubCategoria.IdSubCategoria = producto.SubCategoria.IdSubCategoria == 0 ? 0 : producto.SubCategoria.IdSubCategoria;

            ML.Result result = BL.ProductoG.GetAll(producto);

            if (result.Success)
            {
                producto.Productos = result.Objects;
            }

            ML.Result ddlCategoria = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = ddlCategoria.Objects;

            ML.Result ddlSubCategoria = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = ddlSubCategoria.Objects;

            return View(producto);
        }


        //BORRAR
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.ProductoG.ProductoDelete(IdProducto);


            return RedirectToAction("GetAll");
        }


        //FORMULARIOS
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();

            if (IdProducto == null)
            {
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();
            }
            else
            {
                ML.Result result = BL.ProductoG.GetById(IdProducto.Value);
                producto = (ML.Producto)result.Object;
            }
            ML.Result CategoriaDDL = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = CategoriaDDL.Objects;

            ML.Result SubCategoriaDDL = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = SubCategoriaDDL.Objects;
            return View(producto);
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            if (file != null && file.ContentLength > 0)
            {
                producto.Imagen = ConvertirAArrayBytes(file);
            }

            if (producto.IdProducto == 0)
            {
                ML.Result result = BL.ProductoG.ProductoAdd(producto);
                if (result.Success)
                {
                    result.Message = "Agregado correctamente";
                }
                else
                {
                    result.Message = "Error";
                }
                return RedirectToAction("GetAll");
            }
            else
            {
                ML.Result result = BL.ProductoG.ProductoUpdate(producto);
                if (result.Success)
                {
                    result.Message = "Actualizado";
                }
                else
                {
                    result.Message = "Error";

                }

                return RedirectToAction("GetAll");

            }
        }
        public Byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

        
        
        public ActionResult Enviar()
        {
            try

            {
                string correo = WebConfigurationManager.AppSettings["Correo"];

                string password = WebConfigurationManager.AppSettings["Password"];

                
                string htmlPath = Server.MapPath("~/Content/Email/Template.html");


                string htmlBody;

                using (StreamReader reader = new StreamReader(htmlPath))

                {

                    htmlBody = reader.ReadToEnd();
                }

                htmlBody = htmlBody.Replace("{{Usuario}}", "Prueba");


                AlternateView avHtml = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

                string imagePath = Server.MapPath("~/Content/Email/TemImagen.webp");

                LinkedResource logo = new LinkedResource(imagePath, MediaTypeNames.Image.Jpeg);

                logo.ContentId = "imagenProducto";

                logo.TransferEncoding = TransferEncoding.Base64;

                avHtml.LinkedResources.Add(logo);



                MailMessage mensaje = new MailMessage

                {
                    From = new MailAddress(correo, "Uriel Mc"),

                    Subject = "Paquete en camino",

                    IsBodyHtml = true

                };
                mensaje.To.Add("urielmc28@hotmail.com");
                

                mensaje.AlternateViews.Add(avHtml);


                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,

                    Credentials = new NetworkCredential(correo, password),

                    EnableSsl = true

                };

                smtp.Send(mensaje);

            }

            catch (Exception ex)

            {
                ML.Result result = new ML.Result();
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error al enviar correo: " + ex.Message);

            }

            return View();

        }

    }
}