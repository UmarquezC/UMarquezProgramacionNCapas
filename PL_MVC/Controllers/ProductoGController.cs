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

            ML.Result CategoriaDDL = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = CategoriaDDL.Objects/*.Cast<ML.Categoria>().ToList()*/;

            ML.Result SubCategoriaDDL = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = SubCategoriaDDL.Objects/* != null ? SubCategoriaDDL.Objects.Cast<ML.SubCategoria>().ToList() : new List<ML.SubCategoria>()*/;


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

            ML.Result CategoriaDDL = BL.Categoria.GetAll();
            producto.SubCategoria.Categoria.Categorias = CategoriaDDL.Objects/*.Cast<ML.Categoria>().ToList()*/;

            ML.Result SubCategoriaDDL = BL.SubCategoria.GetAll(producto.SubCategoria.Categoria.IdCategoria);
            producto.SubCategoria.SubCategorias = SubCategoriaDDL.Objects/*.Cast<ML.SubCategoria>().ToList()*/;

            return View(producto);
        }


        //BORRAR

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
            return View();
        }

    }
}