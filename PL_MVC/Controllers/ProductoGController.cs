using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            producto.SubCategoria.Categoria.Categorias = CategoriaDDL.Objects/*.Cast<ML.Categoria>().ToList()*/;

            ML.Result SubCategoriaDDL = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = SubCategoriaDDL.Objects/*.Cast<ML.SubCategoria>().ToList()*/;
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


    }
}