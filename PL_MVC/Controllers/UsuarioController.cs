using ML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario

        /*
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result resultRol = BL.Rol.GetAll();

            UsuarioReference.CRUDClient obj = new UsuarioReference.CRUDClient();
            var usuarioService = obj.usuarioGetAll(usuario);
            //SE LO QUITE
            //ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (usuarioService.Success)
            {
                //Se le agrega igual el tolist
                usuario.Usuarios = usuarioService.Objects.ToList();
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }

            //ML.Result resultDDL = BL.Rol.GetAll();
            //Se le agrega el .tolist();
            usuario.Rol.Roles = resultRol.Objects.ToList();
            
            return View(usuario);
        }
        */

        //****************************
        //CONSUMIR SOAP SIN REFERENCE|
        //****************************
        //SE HACE LA CABECERA
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result result = GetAllRest();
            /*
            string action = "http://tempuri.org/ICRUD/usuarioGetAll";
            string url = "http://localhost:56102/CRUD.svc";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";

            string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?><soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:usuarioGetAll>
<tem:usuario>
            <!--Optional:-->
            <ml:ApellidoMaterno></ml:ApellidoMaterno>
            <!--Optional:-->
            <ml:ApellidoPaterno></ml:ApellidoPaterno>
           
           
            <ml:Nombre></ml:Nombre>

            <ml:Rol>
               <!--Optional:-->
               <ml:IdRol>0</ml:IdRol>
              
            </ml:Rol>
            
         </tem:usuario>

      </tem:usuarioGetAll>
   </soapenv:Body>
</soapenv:Envelope>
            ";

            //ENVIAR LA SOLICITUD
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        //DESERIALIZAR EL XML
                        var usuarios = GetAllUsuarios(result); //CAPTURA EL OBJETO COMPLETO
                        return View(usuarios);
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();

        }

        public ML.Usuario GetAllUsuarios(string xml)
        {
            var usuario1 = new ML.Usuario();

            ML.Result result = new ML.Result();

            result.Objects = new List<object>();

            var xdoc = XDocument.Parse(xml);

            //ACCEDER A GETALLUSUARIORESULT

            var objects = xdoc.Descendants("{http://schemas.microsoft.com/2003/10/Serialization/Arrays}anyType");

            foreach (var elem in objects)
            {
                var usuario = new ML.Usuario();
                //MANEJO DE IDUSUARIO NULL
                int idUsuario;

                int.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}IdUsuario")?.Value, out idUsuario); //0

                usuario.IdUsuario = idUsuario;
                usuario.Nombre = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.ApellidoMaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoMaterno")?.Value ?? string.Empty);
                usuario.ApellidoPaterno = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}ApellidoPaterno")?.Value ?? string.Empty);

                usuario.Telefono = (string)(elem.Element(XName.Get("Telefono", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty);
                usuario.Celular = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Celular")?.Value ?? string.Empty);
                usuario.Curp = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Curp")?.Value ?? string.Empty);

                usuario.Email = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Email")?.Value ?? string.Empty);

                usuario.FechaNacimiento = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}FechaNacimiento")?.Value ?? string.Empty);

                usuario.Password = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Password")?.Value ?? string.Empty);
                bool Estatus;
                bool.TryParse(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Estatus")?.Value, out Estatus);
                usuario.Estatus = Estatus;

                usuario.Sexo = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}Sexo")?.Value ?? string.Empty);
                usuario.UserName = (string)(elem.Element("{http://schemas.datacontract.org/2004/07/ML}UserName")?.Value ?? string.Empty);

                //ROLES

                //Extraer Rol(IdRol, Nombre)
                usuario.Rol = new ML.Rol();
                var rolElement = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Rol");
                usuario.Rol.Nombre = (string)(rolElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                //Direccciones
                usuario.Direccion = new ML.Direccion();

                var direccionElement = elem.Element("{http://schemas.datacontract.org/2004/07/ML}Direccion");
                usuario.Direccion.Calle = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Calle")?.Value ?? string.Empty);
                usuario.Direccion.NumeroExterior = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroExterior")?.Value ?? string.Empty);
                usuario.Direccion.NumeroInteriror = (string)(direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}NumeroInteriror")?.Value ?? string.Empty);


                usuario.Direccion.Colonia = new ML.Colonia();
                var coloniaElement = direccionElement.Element("{http://schemas.datacontract.org/2004/07/ML}Colonia");
                usuario.Direccion.Colonia.CodigoPostal = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}CodigoPostal")?.Value ?? string.Empty);
                usuario.Direccion.Colonia.Nombre = (string)(coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                var municipioElement = coloniaElement.Element("{http://schemas.datacontract.org/2004/07/ML}Municipio");
                usuario.Direccion.Colonia.Municipio.Nombre = (string)(municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);

                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                var estadoElement = municipioElement.Element("{http://schemas.datacontract.org/2004/07/ML}Estado");
                usuario.Direccion.Colonia.Municipio.Estado.Nombre = (string)(estadoElement.Element("{http://schemas.datacontract.org/2004/07/ML}Nombre")?.Value ?? string.Empty);


                //Agregar el objeto usuario a la lista
                result.Objects.Add(usuario);
            }

            usuario1.Usuarios = result.Objects;
            usuario1.Rol = new ML.Rol();
            ML.Result resultDDLL = BL.Rol.GetAll();
            usuario1.Rol.Roles = resultDDLL.Objects;

            return usuario1;

            */

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRol.Objects.ToList();


            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Reporte.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", result.Objects));
            ViewBag.ReportViewer = reportViewer;


            if (result.Objects.Count > 0)
            {
                usuario.Usuarios = result.Objects.ToList();
            }

            return View(usuario);
        }

        

        //SERVICIO DE LA MANERA FACIL EL GET ALL BUSQUEDA ABIERTA
        
        //SE ESTA OCUPANDO EL SERVICO DE LA MANERA FACIL
        //BUSCAR POR EL SEARCH SP CON LIKE
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            usuario.Nombre = usuario.Nombre == null ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = usuario.ApellidoPaterno == null ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = usuario.ApellidoMaterno == null ? "" : usuario.ApellidoMaterno;
            usuario.Rol.IdRol = usuario.Rol.IdRol == 0 ? 0 : usuario.Rol.IdRol;

            ML.Result result = GetAllRest(usuario);


            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(900);
            reportViewer.Height = Unit.Percentage(900);
            reportViewer.LocalReport.ReportPath = Request.MapPath(Request.ApplicationPath) + @"Reports\Reporte.rdlc";
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", result.Objects));
            ViewBag.ReportViewer = reportViewer;


            //ML.Result result = GetAllRest(usuario);


            if (result.Success)
            {
                usuario.Usuarios = result.Objects;
            }
            //se lo quite para agregar el servicio
            /*
            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Success)
            {
                usuario.Usuarios = result.Objects;
            }
            */

            //AGREGAMOS EL SERVICIO
            /*
            UsuarioReference.CRUDClient obj = new UsuarioReference.CRUDClient();
            var usuarioReference = obj.usuarioGetAll(usuario);
          
            //aqui iba el result
            if (usuarioReference.Success)
            {
                //Pongo el tolist
                usuario.Usuarios = usuarioReference.Objects.ToList();
            }
            else
            {
                usuario.Usuarios = new List<object>();
            }
            */
            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects;

            return View(usuario);
        }

        




        //SERIVIO DE LA MANERA FACIL EL INSERT Y EL UPDATE GET
        /*
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

                //AQUI USARIAMOS EL SERVICIO
                UsuarioReference.CRUDClient obj = new UsuarioReference.CRUDClient();
                var usuarioReference = obj.usuarioGetById(IdUsuario.Value);
                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                //vOY A QUITAR EL RESULT Y PONERLE EL USUARIOREFERENCE
                usuario = (ML.Usuario)usuarioReference.Object;
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
        */



        //SERVICIO DE LA MANERA DIFICIL EL INSERT Y EL UPDATE GET

        [HttpGet]
        public ActionResult FormBorrarAqui(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

            ML.Result resultDDL = BL.Rol.GetAll();
            usuario.Rol.Roles = resultDDL.Objects; //le pasa todos los valores a roles, para que pueda pintar el ddl

            ML.Result resultEstados = BL.Estado.GetAll();
            usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

            ML.Result resultMunicipios = BL.Municipio.GetAllByEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
            usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

            ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
            usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

            if (IdUsuario.HasValue)
            {
                //OBTENER EL USUARIO ID
                string action = "http://tempuri.org/ICRUD/usuarioGetById";
                string url = "http://localhost:56102/CRUD.svc";

                //CREAR EL SOBRE SOAP PARA OBTENER UN USUARIO POR SU ID
                string soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:usuarioGetById>     
         <tem:IdUsuario>{IdUsuario}</tem:IdUsuario>
      </tem:usuarioGetById>
   </soapenv:Body>
</soapenv:Envelope>
";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Headers.Add("SOAPAction", action);
                request.ContentType = "text/xml; charset= \"utf-8\"";
                request.Accept = "text/xml";
                request.Method = "POST";

                //Enviarla solicitud
                using (Stream stream = request.GetRequestStream())
                {
                    byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                    stream.Write(content, 0, content.Length);
                }
                //OBTENER LA RESPUESTA
                try
                {
                    using (WebResponse response = (WebResponse)request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            string result = reader.ReadToEnd();
                            Console.WriteLine(result);
                            //Aqui un breakpont
                            usuario = GetUsuarioById(result);
                        }
                    }
                }
                catch (WebException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return View(usuario);
            //DEVUELVE LA VISTA CON EL USUARIO SI EXISTE
        }

        //AQUI ES PARA EL API REST
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            if (IdUsuario == null)
            {
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            }
            else
            {
                ML.Result result = GetById(IdUsuario.Value);
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



        public ML.Usuario GetUsuarioById(string xml)
        {
            var xdoc = XDocument.Parse(xml);
            //Acceder a GetUsuarioByIdResult usando el namespace correcto 
            var usuarioElement = xdoc.Descendants().FirstOrDefault(e => e.Name.LocalName == "Object" && e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
            if (usuarioElement != null)
            {
                var usuario = new ML.Usuario()
                {
                    IdUsuario = int.TryParse(usuarioElement.Element(XName.Get("IdUsuario", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idUsuario) ? idUsuario : 0,
                    Nombre = usuarioElement.Element(XName.Get("Nombre", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    ApellidoMaterno = usuarioElement.Element(XName.Get("ApellidoMaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    ApellidoPaterno = usuarioElement.Element(XName.Get("ApellidoPaterno", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Celular = usuarioElement.Element(XName.Get("Celular", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Telefono = usuarioElement.Element(XName.Get("Telefono", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Curp = usuarioElement.Element(XName.Get("Curp", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Email = usuarioElement.Element(XName.Get("Email", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    FechaNacimiento = usuarioElement.Element(XName.Get("FechaNacimiento", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Password = usuarioElement.Element(XName.Get("Password", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Estatus = bool.TryParse(usuarioElement.Element(XName.Get("Estatus", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out bool estatus) && estatus,
                    Sexo = usuarioElement.Element(XName.Get("Sexo", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    UserName = usuarioElement.Element(XName.Get("UserName", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                    Rol = new ML.Rol
                    {
                        IdRol = int.TryParse(usuarioElement.Element(XName.Get("Rol", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdRol", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idRol) ? idRol : 0
                    },
                    Direccion = new ML.Direccion
                    {
                        IdDireccion = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdDireccíon", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idDireccion) ? idDireccion : 0,
                        Calle = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Calle", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        NumeroExterior = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("NumeroExterior", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        NumeroInteriror = usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("NumeroInteriror", "http://schemas.datacontract.org/2004/07/ML"))?.Value ?? string.Empty,
                        Colonia = new ML.Colonia
                        {
                            IdColonia = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdColonia", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idColonia) ? idColonia : 0,

                            Municipio = new ML.Municipio
                            {
                                IdMunicipio = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdMunicipio", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idMunicipio) ? idMunicipio : 0,

                                Estado = new ML.Estado
                                {
                                    IdEstado = int.TryParse(usuarioElement.Element(XName.Get("Direccion", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Colonia", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Municipio", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("Estado", "http://schemas.datacontract.org/2004/07/ML"))?.Element(XName.Get("IdEstado", "http://schemas.datacontract.org/2004/07/ML"))?.Value, out int idEstado) ? idEstado : 0
                                }
                            }

                        }
                    }

                };
                // Obtener lista de roles desde la capa de negocio
                ML.Result resultDDLL = BL.Rol.GetAll();
                usuario.Rol.Roles = resultDDLL.Objects;

                ML.Result resultEstados = BL.Estado.GetAll();
                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;

                ML.Result resultMunicipios = BL.Municipio.GetAllByEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;

                ML.Result resultColonia = BL.Colonia.GetAllByMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                return usuario;
            }
            return null;

        }
        [NonAction]
        public string EscapeXml(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&apos;");
        }



        //SERVICIO DE LA MANERA FACIL POST INSERT UPDATE POST
        /*
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["inptFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }

                UsuarioReference.CRUDClient obj = new UsuarioReference.CRUDClient();

                if (usuario.IdUsuario == 0)
                {
                    //AQUI QUITO EL RESULT PORQUE VOY A USAR EL SERVICIO
                    //ML.Result result = BL.Usuario.AddEF(usuario);

                    var usuarioReference = obj.usuarioAdd(usuario);
                    //AQUI QUITO EL RESULT Y PONGO E REFERENCE
                    if (usuarioReference.Success)
                    {
                        ViewBag.Message = "El usuario se agrego correctamente";
                    }
                    else
                    {
                        ViewBag.Add = false;
                        ViewBag.Message = usuarioReference.Message;
                    }
                }
                else
                {
                    var usuarioReference = obj.usuarioUpdate(usuario);
                    //var result = BL.Usuario.UpdateEF(usuario);
                    //aqui quite el result
                    if (usuarioReference.Success)
                    {
                        ViewBag.Message = "Registro actualizado";
                    }
                    else
                    {
                        ViewBag.Message = usuarioReference.Message;
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

        */



        //SERVICIO DE LA MANERA DIFICIL POST INSERT UPDATE
        /*
        //SE VA HACER DE LA MANERA DIFICIL EL FORMULARIO PARA OBTENER
        ENTONCES HAY QUE HACER LA CABECERA PARA HACER EL UPDATE Y EL INSERT
        */



        [HttpPost]
        public ActionResult FormBorrarAqui(ML.Usuario usuario)
        {
            string url = "http://localhost:56102/CRUD.svc";
            string soapEnvelope;
            string action;


            if (usuario.IdUsuario == 0)
            {
                //CREAR EL SOBRE SOAP PARA AGREGAR UN NUEVO USUARIO
                action = "http://tempuri.org/ICRUD/usuarioAdd";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:usuarioAdd>
        
         <tem:usuario>
            <ml:ApellidoMaterno>{usuario.ApellidoMaterno}</ml:ApellidoMaterno>
            <ml:ApellidoPaterno>{usuario.ApellidoPaterno}</ml:ApellidoPaterno>
            <ml:Celular>{usuario.Celular}</ml:Celular>
            <ml:Curp>{usuario.Curp}</ml:Curp>
            <ml:Direccion>
               <ml:Calle>{usuario.Direccion.Calle}</ml:Calle>
               <ml:Colonia>
                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                  <ml:Municipio>
                     <ml:Estado>
                        <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                     </ml:Estado>
                     <ml:IdMunicipio>{usuario.Direccion.Colonia.IdColonia}</ml:IdMunicipio>
                  </ml:Municipio>
               </ml:Colonia>
               <ml:NumeroExterior>{usuario.Direccion.NumeroExterior}</ml:NumeroExterior>
               <ml:NumeroInteriror>{usuario.Direccion.NumeroInteriror}</ml:NumeroInteriror>
               <ml:Usuario/>
            </ml:Direccion>
            <ml:Email>{usuario.Email}</ml:Email>
            <ml:FechaNacimiento>{usuario.FechaNacimiento}</ml:FechaNacimiento>            
            <ml:Nombre>{usuario.Nombre}</ml:Nombre>
            <ml:Password>{usuario.Password}</ml:Password>
            <ml:Rol>
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
            </ml:Rol>
            <ml:Sexo>{usuario.Sexo}</ml:Sexo>
            <ml:Telefono>{usuario.Telefono}</ml:Telefono>
            <ml:UserName>{usuario.UserName}</ml:UserName>
         </tem:usuario>
      </tem:usuarioAdd>
   </soapenv:Body>
</soapenv:Envelope>";
            }
            else
            {
                //CREAR EL SOAP PARA ACTUALIZAR UN USUARIO
                action = "http://tempuri.org/ICRUD/usuarioUpdate";
                soapEnvelope = $@"<?xml version=""1.0"" encoding=""utf-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"" xmlns:ml=""http://schemas.datacontract.org/2004/07/ML"" xmlns:arr=""http://schemas.microsoft.com/2003/10/Serialization/Arrays"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:usuarioUpdate>
         
         <tem:usuario>
            <ml:ApellidoMaterno>{EscapeXml(usuario.ApellidoMaterno)}</ml:ApellidoMaterno>
            <ml:ApellidoPaterno>{EscapeXml(usuario.ApellidoPaterno)}</ml:ApellidoPaterno>
            <ml:Celular>{EscapeXml(usuario.Celular)}</ml:Celular>
            <ml:Curp>{EscapeXml(usuario.Curp)}</ml:Curp>
            <ml:Direccion>
               <ml:Calle>{EscapeXml(usuario.Direccion.Calle)}</ml:Calle>
               <ml:Colonia>
                  <ml:IdColonia>{usuario.Direccion.Colonia.IdColonia}</ml:IdColonia>
                  <ml:Municipio>
                     <ml:Estado>
                        <ml:IdEstado>{usuario.Direccion.Colonia.Municipio.Estado.IdEstado}</ml:IdEstado>
                     </ml:Estado>
                     <ml:IdMunicipio>{usuario.Direccion.Colonia.Municipio.IdMunicipio}</ml:IdMunicipio>
                  </ml:Municipio>
               </ml:Colonia>               
               <ml:NumeroExterior>{EscapeXml(usuario.Direccion.NumeroExterior)}</ml:NumeroExterior>
               <ml:NumeroInteriror>{EscapeXml(usuario.Direccion.NumeroInteriror)}</ml:NumeroInteriror>
               <ml:Usuario/>
            </ml:Direccion>
            <ml:Email>{EscapeXml(usuario.Email)}</ml:Email>
            
            <ml:FechaNacimiento>{EscapeXml(usuario.FechaNacimiento)}</ml:FechaNacimiento>
            <ml:IdUsuario>{usuario.IdUsuario}</ml:IdUsuario>
            <ml:Imagen>{usuario.Imagen}</ml:Imagen>
            <ml:Nombre>{EscapeXml(usuario.Nombre)}</ml:Nombre>
            <ml:Password>{EscapeXml(usuario.Password)}</ml:Password>
            <ml:Rol>
               <ml:IdRol>{usuario.Rol.IdRol}</ml:IdRol>
            </ml:Rol>
            <ml:Sexo>{EscapeXml(usuario.Sexo)}</ml:Sexo>
            <ml:Telefono>{EscapeXml(usuario.Telefono)}</ml:Telefono>
            <ml:UserName>{EscapeXml(usuario.UserName)}</ml:UserName>
         </tem:usuario>
      </tem:usuarioUpdate>
   </soapenv:Body>
</soapenv:Envelope>
";
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST";
            //Enviar la solicitud
            using (Stream stream = request.GetRequestStream())
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);
                stream.Write(content, 0, content.Length);
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["inptFileImagen"];
                if (file != null && file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertirAArrayBytes(file);
                }

                if (usuario.IdUsuario == 0)
                {
                    ML.Result result = AddRest(usuario);
                    if (result.Success)
                    {
                        ViewBag.Message = "El usuario se agrego correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Hubo un error";
                    }
                }
                else
                {
                    ML.Result result = UpdateRest(usuario);
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

        //CONSUMIR EL SERVICE DE LA MANERA FACIL
        /*
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            UsuarioReference.CRUDClient obj = new UsuarioReference.CRUDClient();
            var usuarioReference = obj.usuarioDelete(IdUsuario);
            //ML.Result result = BL.Usuario.DeleteEF(IdUsuario);

            if (usuarioReference.Success)
            {
                ViewBag.Message = "Registro eliminado correctamente ";
            }
            else
            {
                ViewBag.Message = usuarioReference.Message;
            }
            return PartialView("_Mensajes");
        }
        */
        //CONSUMIR EL SERVICIO DE LA MANERA DICIL
        public ActionResult Delete(int IdUsuario)
        {
            var result = DeleteRest(IdUsuario);

            if (result.Success)
            {
                ViewBag.Message = "Borrado Correctamente";
                return PartialView("_Mensajes");
            }
            else
            {
                ViewBag.Message = "Error al eliminar";
                return PartialView("_Mensajes");
            }
            /*
            string action = "http://tempuri.org/ICRUD/usuarioDelete";
            string url = "http://localhost:56102/CRUD.svc"; // Cambia a la URL del servicio
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("SOAPAction", action);
            request.ContentType = "text/xml;charset=\"utf-8\"";
            request.Accept = "text/xml";
            request.Method = "POST"; // Cambia a POST ya que estás usando un servicio SOAP
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            string soapEnvelope =
                $@"<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org/"">
   <soapenv:Header/>
   <soapenv:Body>
      <tem:usuarioDelete>
         <tem:idUsuario>{IdUsuario}</tem:idUsuario>
      </tem:usuarioDelete>
   </soapenv:Body>
</soapenv:Envelope>";
            // Enviar la solicitud
            using (Stream stream = request.GetRequestStream())//cacha el soap
            {
                byte[] content = Encoding.UTF8.GetBytes(soapEnvelope);//almacena el xml
                stream.Write(content, 0, content.Length);//se envía 
            }

            // Obtener la respuesta
            try
            {
                using (WebResponse response = request.GetResponse()) //obtiene la respuesta
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))//lee la respuesta
                    {
                        
                        string xml = reader.ReadToEnd();// se convierte a string
                        var xdoc = XDocument.Parse(xml);
                        // Acceder a GetUsuarioByIdResult usando el namespace correcto
                        var usuarioElement = xdoc.Descendants().FirstOrDefault(e =>
                            e.Name.LocalName == "Success" &&
                            e.GetDefaultNamespace().NamespaceName == "http://tempuri.org/");
                             result.Success = bool.Parse(usuarioElement.Value);
                        if (result.Success)
                        {
                            ViewBag.Message = "El registro se eliminó correctamente";
                            return PartialView("_Mensajes");
                        }
                        else
                        {
                            ViewBag.error = "Hubo un error al eliminar el registro";
                            return View("_Mensjes");
                        }

                        // Asegúrate de que tu vista esté lista para recibir este objeto
                    }
                }
            }
            catch (WebException ex)
            {
                ViewBag.Error = ex.Message; // Para mostrar en la vista si es necesario
            }

            return View(); // Devuelve la vista
            */
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

                    List<ML.Usuario> usuariosCorrectos = new List<ML.Usuario>();
                    List<ML.Usuario> usuariosIncorrectos = new List<ML.Usuario>();

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
                        ViewBag.ErroresExcel = "Se hizo el proceso de registro " + resultLeer.Objects.Count + ", usuarios de los cuales fallaron " + registrosIncorrectos + ", y se insertaron " + registrosCorrectos;
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

        //***************************************
        //*********API REST**********************
        //***************************************


        [NonAction]
        public static ML.Result GetAllRest()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    //Investigar que es Uri
                    cliente.BaseAddress = new Uri(endPoint);
                    var responseTask = cliente.GetAsync("GetAll");
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();

                        foreach (var item in readTask.Result.Objects)
                        {
                            ML.Usuario usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());

                            result.Objects.Add(usuario);
                        }

                        result.Success = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [NonAction]
        public static ML.Result GetAllRest(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var responseTask = cliente.PostAsJsonAsync<ML.Usuario>("GetAllPost", usuario);
                    responseTask.Wait();

                    var resultServicio = responseTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        readTask.Wait();

                        result.Objects = new List<object>();

                        foreach (var item in readTask.Result.Objects)
                        {
                            usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());

                            result.Objects.Add(usuario);
                        }
                        result.Success = true;
                    }
                    
                    
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
            }

            return result;
        }

        [NonAction]
        public ML.Result AddRest(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var postTask = cliente.PostAsJsonAsync<ML.Usuario>("Add", usuario);
                    postTask.Wait();

                    var resultServicio = postTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                    }

                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [NonAction]
        public ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    var endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var getByIdTask = cliente.GetAsync($"GetById/{IdUsuario}");
                    getByIdTask.Wait();

                    var resultServicio = getByIdTask.Result;

                    if (resultServicio.IsSuccessStatusCode)
                    {
                        var readTask = resultServicio.Content.ReadAsAsync<ML.Result>();
                        ML.Usuario usuario = new ML.Usuario();
                        usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                        result.Object = usuario;
                        result.Success = true;

                    }
                    else
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        [NonAction]
        public ML.Result UpdateRest(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);
                    var putTask = cliente.PutAsJsonAsync<ML.Usuario>("Update", usuario);
                    putTask.Wait();

                    var resultServicio = putTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }


            return result;
        }


        [NonAction]
        public ML.Result DeleteRest(int IdUsuario)
        {

            ML.Result result = new ML.Result();
            try
            {
                using (var cliente = new HttpClient())
                {
                    string endPoint = ConfigurationManager.AppSettings["UsuarioEndPoint"].ToString();
                    cliente.BaseAddress = new Uri(endPoint);

                    var deleteTask = cliente.DeleteAsync($"Delete/{IdUsuario}");

                    deleteTask.Wait();

                    var resultServicio = deleteTask.Result;
                    if (resultServicio.IsSuccessStatusCode)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

    }
}