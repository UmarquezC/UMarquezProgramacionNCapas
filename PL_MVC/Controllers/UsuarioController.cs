using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Success)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;
            return View(usuario);
        }

        //BUSCAR POR EL SEARCH SP CON LIKE
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            
            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;


            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Success)
            {
                usuario.Usuarios = result.Objects;
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            return View(usuario);
        }


        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            if (IdUsuario == null)
            {
                //ADD
                //VACIO
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else
            {
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                usuario = (ML.Usuario)result.Object;                
            }
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects; //le pasa todos los valores a roles, para que pueda pintar el ddl

            ML.Result resultEstados = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

            ML.Result resultMunicipios = BL.Municipio.GetAllByEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

            ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            return View(usuario);
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            HttpPostedFileBase file = Request.Files["inptFileImagen"];
            if(file != null && file.ContentLength > 0)
            {
                usuario.Imagen = ConvertirAArrayBytes(file);
            }

            if (usuario.IdUsuario == 0)
            {
                BL.Usuario.AddEF(usuario);
            }
            else
            {
                BL.Usuario.UpdateEF(usuario);
            }
            
            return RedirectToAction("GetAll");
        }

        public Byte[] ConvertirAArrayBytes(HttpPostedFileBase Foto)
        {
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Foto.InputStream);
            byte[] data = reader.ReadBytes((int)Foto.ContentLength);
            return data;
        }


        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);

            if (result.Success)
            {
                return RedirectToAction("GetAll");
            }
            else
            {
                return View("GetAll");
            }
        }

        [HttpPost]
        public JsonResult CambiarStatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsResult = BL.Usuario.CambiarStatus(IdUsuario, Estatus);

            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetAllByEstado(int IdEstado) {
            
            ML.Result resultMunicipio = BL.Municipio.GetAllByEstado(IdEstado);

            return Json(resultMunicipio, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllByMunicipio(int IdMunicipio)
        {
            ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(IdMunicipio);
            return Json(resultColonia, JsonRequestBehavior.AllowGet);
        }
    }
}