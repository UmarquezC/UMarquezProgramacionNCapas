using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
            if (ModelState.IsValid) {
                HttpPostedFileBase file = Request.Files["inptFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }

                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = BL.Usuario.AddEF(usuario);
                    if (result.Success)
                    {
                        ViewBag.Message = "El usuario se agrego correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = result.Message;
                    }
                }
                else
                {
                    var result = BL.Usuario.UpdateEF(usuario);
                    if (result.Success)
                    {
                        ViewBag.Message = "Registro actualizado";
                    }
                    else
                    {
                        ViewBag.Message = result.Message;
                    }
                }
                return PartialView("_Mensajes");
            }
            else
            {
                ML.Result resultDDL = BL.Rol.GetAll();
                usuario.Rol.Roles = resultDDL.Objects;

                ML.Result resultEstados = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                ML.Result resultMunicipios = BL.Municipio.GetAllByEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

                ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;
                return View(usuario);
            }
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
                ViewBag.Message = "Registro eliminado correctamente ";                
            }
            else
            {
                ViewBag.Message = result.Message;      
            }
            return PartialView("_Mensajes");
        }

        [HttpPost]
        public JsonResult CambiarStatus(int IdUsuario, bool Estatus)
        {
            ML.Result JsResult = BL.Usuario.CambiarStatus(IdUsuario, Estatus);

            return Json(JsResult, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]       
        public JsonResult GetAllByEstado(int IdEstado)
        {

            ML.Result resultMunicipio = BL.Municipio.GetAllByEstado(IdEstado);

            return Json(resultMunicipio, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllByMunicipio(int IdMunicipio)
        {
            ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(IdMunicipio);
            return Json(resultColonia, JsonRequestBehavior.AllowGet);
        }

        //CARGA MASIVA
        [HttpPost]
        public ActionResult CargaMasiva()
        {

            if (Session["RutaExcel"] == null)
            {
                //Archivo
                HttpPostedFileBase excelUsuario = Request.Files["Excel"];

                string extensionPermitida = ".xlsx";

                if (excelUsuario.ContentLength > 0) //Si el usuario me envio algo
                {
                    string extensionObtenida = Path.GetExtension(excelUsuario.FileName);

                    if (extensionObtenida == extensionPermitida)
                    {
                        string ruta = Server.MapPath("~/CargaMasiva/") + Path.GetFileNameWithoutExtension(excelUsuario.FileName) + "-" + DateTime.Now.ToString("ddMMyyyyHmssff") + ".xlsx";

                        if (!System.IO.File.Exists(ruta))
                        {
                            excelUsuario.SaveAs(ruta);
                            string cadeConexion = ConfigurationManager.ConnectionStrings["OleDBConnection"] + ruta;
                            ML.Result resultExcel = BL.Usuario.LeerExcel(cadeConexion);

                            if (resultExcel.Objects.Count > 0)
                            {
                                ML.ResultExcel resultValidacion = BL.Usuario.ValidarExcel(resultExcel.Objects);
                                if (resultValidacion.Errores.Count > 0)
                                {
                                    //Hubo un error
                                    //Mostrar una vista una tabla                             
                                    ViewBag.ErroresExcel = resultValidacion.Errores;
                                    return PartialView("_Modal");
                                }
                                else
                                {
                                    Session["RutaExcel"] = ruta;
                                    ViewBag.ErroresExcel = "El archivo se valido con exito";
                                    return PartialView("_Modal");

                                }     
                            }                            
                        }
                        else
                        {
                            //Vista PARCIAL
                            ViewBag.ErroresExcel = "Vuelve a cargar el archivo, porque ya existe";
                            return PartialView("_Modal");
                            //VUELVE A CARGAR EL ARCHIVO, PORQUE YA EXISTE
                        }
                    }
                    else
                    {
                        //VISTA PARCIAL                        
                        ViewBag.ErroresExcel = "El archivo no es un excel";
                        return PartialView("_Modal");
                        //EL ARCHIVO NO ES UN EXCEL
                    }
                }
                else
                {
                    //VISTA PARCIAL
                    ViewBag.ErroresExcel = "No me diste ningun archivo";
                    return PartialView("_Modal");
                    //NO ME DISTE NINGUN ARCHIVO
                }
            }
            else
            {
                //YA LEI Y VALIDE UN EXCEL
                //INSERTA
                string cadeConexion = ConfigurationManager.ConnectionStrings["OleDBConnection"] + Session["RutaExcel"].ToString();
                ML.Result resultLeer = BL.Usuario.LeerExcel(cadeConexion);               

                if (resultLeer.Objects.Count > 0)
                {
                    //Todo lo que leyo bien

                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    int registrosCorrectos = 0;
                    int registrosIncorrectos = 0;

                    List<ML.Usuario>usuariosCorrectos = new List<ML.Usuario>();
                    List<ML.Usuario>usuariosIncorrectos = new List<ML.Usuario>();

                    foreach (ML.Usuario usuario in resultLeer.Objects)
                    {
                        ML.Result resultInsertar = BL.Usuario.AddEF(usuario);
                        if (!resultInsertar.Success)
                        {
                            registrosIncorrectos++;
                            usuariosIncorrectos.Add(usuario);
                        }
                        else
                        {
                            registrosCorrectos++;
                            usuariosCorrectos.Add(usuario);

                        }
                    }

                    if (registrosIncorrectos > 0)
                    {
                        ViewBag.ErroresExcel = "Se hizo el proceso de registro " + resultLeer.Objects.Count + ", usuarios de los cuales fallaron "+ registrosIncorrectos + ", y se insertaron " + registrosCorrectos;
                        Session["RutaExcel"] = null;
                        return PartialView("_Modal");
                    }
                    else
                    {
                        ViewBag.ErroresExcel = "La carga masiva fue realizada con exito";
                        Session["RutaExcel"] = null;
                        return PartialView("_Modal");
                    }
                }
                else
                {
                    //ERROR
                    ViewBag.ErroresExcel = "No se pudo insertar";                  
                    Session["RutaExcel"] = null;
                    return PartialView("_Modal");
                }

            }
            Session["RutaExcel"] = null;
            return RedirectToAction("GetAll");
        }




    }
}