using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class JsCRUDController : Controller
    {
        // GET: JsCRUD
        public ActionResult GetAll()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AddUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.AddEF(usuario);

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult UpdateUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.UpdateEF(usuario); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        [HttpGet]
        public JsonResult UsuarioGetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            result = BL.Usuario.GetByIdEF(IdUsuario); ;

            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }


        public JsonResult GetAllRol()
        {
            ML.Result resultDDL = new ML.Result();
            resultDDL = BL.Rol.GetAll();
            return Json(resultDDL, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEstado()
        {
            
            ML.Result resultDDL = new ML.Result();
            resultDDL = BL.Estado.GetAll();
            return Json(resultDDL, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllByEstado(int IdEstado)
        {
            ML.Result resultDDL = new ML.Result();
            resultDDL = BL.Municipio.GetAllByEstado(IdEstado);

            return Json(resultDDL, JsonRequestBehavior.AllowGet);

        }

        

        [HttpGet]
        public JsonResult GetAllByMunicipio(int IdMunicipio)
        {
            ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(IdMunicipio);
            return Json(resultColonia, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Form(int? IdUsuario)
        //{
        //    ML.Usuario usuario = new ML.Usuario();
        //    return Json();
        //}

        [HttpGet]
        public JsonResult GetAllUsuarios()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            JsonResult jsonResult = Json(result, JsonRequestBehavior.AllowGet);

            jsonResult.MaxJsonLength = int.MaxValue;

            return jsonResult;
        }

        //AGREGAR
        [HttpPost]

        public JsonResult FormJs(ML.Usuario usuario)
        {
            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            if (file != null && file.ContentLength > 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);

            }

            ML.Result result;

            if (usuario.IdUsuario == 0)
            {
                result = BL.Usuario.AddEF(usuario);
            }
            else
            {
                result = BL.Usuario.UpdateEF(usuario);
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }


        public Byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }



        [HttpPost]
        public JsonResult Delete(int idUsuario)
        {
            ML.Result resultEliminar = BL.Usuario.DeleteEF(idUsuario);
            return Json(resultEliminar, JsonRequestBehavior.AllowGet);
        }
    }
}