using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult GetAll()
        {
            ML.Sucursal sucursal = new ML.Sucursal();
            ML.Result result = BL.Sucursal.GetAll();

            if (result.Success)
            {
                sucursal.Sucursales = result.Objects;
            }
            else
            {
                sucursal.Sucursales = new List<object>();
            }

            return View(sucursal);
        }

        [HttpGet]
        public ActionResult Form(int? IdSucursal)
        {
            ML.Sucursal sucursal = new ML.Sucursal();

            
            if (IdSucursal == null)
            {
                
                sucursal.Latitud = "0"; 
                sucursal.Longitud = "0";
            }
            else
            {
                ML.Result result = BL.Sucursal.GetById(IdSucursal.Value);
                if (result.Success)
                {
                    sucursal = (ML.Sucursal)result.Object;
                }
                else
                {
                    return RedirectToAction("Error");
                }
            }

            return View(sucursal);
        }



        [HttpPost]
        public ActionResult Form(ML.Sucursal sucursal)
        {
            if(sucursal.IdSucursal == 0)
            {
                ML.Result result = BL.Sucursal.Add(sucursal);
                ViewBag.Message = "El usuario se agrego correctamente";
            }
            else
            {
                var result = BL.Sucursal.Update(sucursal);
                ViewBag.Message = "El usuario se actualizo correctamente";

            }
            return PartialView("_Mensajes");
        }
    }
}