using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class ProductoSucursalController : Controller
    {
        // GET: ProductoSucursal
        public ActionResult GetAll()
        {
            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
            productoSucursal.Sucursal = new ML.Sucursal();
            productoSucursal.Producto = new ML.Producto();
          
            ML.Result resultSucursales = BL.Sucursal.GetAll();

            if (resultSucursales.Success)
            {
                productoSucursal.Sucursal.Sucursales = resultSucursales.Objects;
            }
            else
            {
                productoSucursal.Sucursal.Sucursales = new List<object>();
            }
            
            productoSucursal.Sucursal.IdSucursal = 0;
            ML.Result result = BL.ProductoSucursal.Get(productoSucursal);

            if (result.Success)
            {
                productoSucursal.ProductoSucursales = result.Objects;
            }
            else
            {
                productoSucursal.ProductoSucursales = new List<object>();
            }

            return View(productoSucursal);
        }



        [HttpPost]
        public ActionResult GetAll(ML.ProductoSucursal productoSucursal)
        {
            productoSucursal.Sucursal.IdSucursal = Convert.ToByte(productoSucursal.Sucursal.IdSucursal == 0 ? 0 : productoSucursal.Sucursal.IdSucursal);
            
            ML.Result result = BL.ProductoSucursal.Get(productoSucursal);            

            if (result.Success)
            {
                productoSucursal.ProductoSucursales = result.Objects;
            }
            else
            {
                productoSucursal.ProductoSucursales = new List<object>();
            }
            
            ML.Result resultDDL = BL.Sucursal.GetAll();

            if (resultDDL.Success)
            {
                productoSucursal.Sucursal.Sucursales = resultDDL.Objects;
            }
            else
            {
                productoSucursal.Sucursal.Sucursales = new List<object>();
            }

            return View(productoSucursal);
        }



        [HttpPost]
        public JsonResult ActualizarStock(int IdProducto, int IdSucursal, int NuevoStock)
        {
            ML.Result result = BL.ProductoSucursal.UpdateStock(IdProducto, IdSucursal, NuevoStock);

            if (result.Success)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "No se pudo actualizar el stock" });
            }
        }

        [HttpPost]
        public JsonResult Delete(int IdProducto, int IdSucursal)
        {
            ML.Result result = BL.ProductoSucursal.Delete(IdProducto, IdSucursal);
            return Json(new
            {
                success = result.Success,
                message = result.Success ? "Producto eliminado correctamente de la sucursal." : "Error al eliminar el producto: " + result.Message
            });
        }




    }
}