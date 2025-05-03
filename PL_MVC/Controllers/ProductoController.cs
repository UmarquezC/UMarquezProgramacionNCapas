using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        /*
        // GET: Producto
        [HttpGet]
        public ActionResult GetAllProducto()
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();
            producto.SubCategoria.IdSubCategoria = 0;
            producto.SubCategoria.Categoria.IdCategoria = 0;

            // Obtener productos
            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Success && result.Objects != null)
            {
                producto.Productos = result.Objects.Cast<ML.Producto>().ToList();
            }
            else
            {
                producto.Productos = new List<ML.Producto>();
            }

            
            ML.Result categoriaDDL = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = categoriaDDL.Objects;

            ML.Result subCategoria = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = subCategoria.Objects;


            return View(producto);
        }

        [HttpGet]
        public JsonResult GetAllByCategoria(int IdCategoria)
        {
            ML.Result result = BL.SubCategoria.GetAll(IdCategoria);

            return Json(result, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetAllProducto(ML.Producto producto)
        {
                                    
            producto.SubCategoria.IdSubCategoria = producto.SubCategoria.IdSubCategoria == 0 ? 0 : producto.SubCategoria.IdSubCategoria;
            producto.SubCategoria.Categoria.IdCategoria = producto.SubCategoria.Categoria.IdCategoria == 0 ? 0 : producto.SubCategoria.Categoria.IdCategoria;

            ML.Result result = BL.Producto.GetAll(producto);

            if (result.Success && result.Objects != null)
            {
                producto.Productos = result.Objects.Cast<ML.Producto>().ToList();
            }
            else
            {
                producto.Productos = new List<ML.Producto>();
            }
            ML.Result categoriaDDL = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = categoriaDDL.Objects;

            ML.Result subCategoria = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = subCategoria.Objects;

            return View(producto);
        }


        //FORMULARIO
        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Producto producto = new ML.Producto();

            if (IdProducto == null)
            {
                // Si es null, se está creando un nuevo producto
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();
            }
            else
            {
                // Si tiene un IdProducto, es un Edit, así que recuperamos el producto
                ML.Result result = BL.Producto.GetById(IdProducto.Value);
                producto = (ML.Producto)result.Object;
            }

            // Cargar las categorías
            ML.Result resultCategorias = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = resultCategorias.Objects;

            ML.Result subCategoria = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = subCategoria.Objects;

            return View(producto);
        }

        public ActionResult GetAllSubCategoria(int IdCategoria)
        {
            ML.Result result = BL.SubCategoria.GetAll(IdCategoria);
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            if (file != null && file.ContentLength > 0)
            {
                producto.Imagen = ConvertirAArrayBytes(file);
            }

            if (producto.IdProducto == 0) // Si IdProducto es 0, significa que es una inserción (Nuevo producto)
            {
                ML.Result result = BL.Producto.Add(producto);
                if (result.Success)
                {
                    ViewBag.Message = "El producto se ha agregado correctamente.";
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al agregar el producto: " + result.Message;
                }
            }
            else // Si tiene un IdProducto válido, significa que es una actualización
            {
                ML.Result result = BL.Producto.Update(producto);
                if (result.Success)
                {
                    ViewBag.Message = "El producto se ha actualizado correctamente.";
                }
                else
                {
                    ViewBag.Message = "Ocurrió un error al actualizar el producto: " + result.Message;
                }
            }           

            return PartialView("_Mensajes");
        }

        public Byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);

            if (result.Success)
            {
                ViewBag.Message = "Se elimino correctamente";
            }
            else
            {
                ViewBag.Message = "Hubo un error";
            }

            return PartialView("_Mensajes");
        }
        */
    }
}