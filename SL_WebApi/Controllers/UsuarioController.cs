using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;
using RoutePrefixAttribute = System.Web.Http.RoutePrefixAttribute;

namespace SL_WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UsuarioController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Suma(int numero1, int numero2)
        {
            int suma = numero1 + numero2;
            return Content(HttpStatusCode.OK, suma);
        }

        [HttpGet]
        [Route("Usuario/GetAll")]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();
            usuario.Nombre = "";
            usuario.ApellidoPaterno = "";
            usuario.ApellidoMaterno = "";
            usuario.Rol.IdRol = 0;
            ML.Result resultRol = BL.Rol.GetAll();

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Success)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("Usuario/GetAllPost")]
        public IHttpActionResult GetAllPost([FromBody]ML.Usuario usuario)
        {
            
            usuario.Rol = new ML.Rol();
            
            ML.Result resultRol = BL.Rol.GetAll();

            ML.Result result = BL.Usuario.GetAllEF(usuario);

            if (result.Success)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }


        [HttpPut]
        [Route("Usuario/Update")]
        public IHttpActionResult Update([FromBody]ML.Usuario usuario)
        {
            
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, result);
            }
        }

        [HttpPost]
        [Route("Usuario/Add")]
        public IHttpActionResult Add([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            if (result.Success)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }           
        }

        [HttpGet]
        [Route("Usuario/GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Success)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }




        [HttpDelete]
        [Route("Usuario/Delete/{IdUsuario}")]
        public IHttpActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(IdUsuario);

            if (result.Success)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.InternalServerError, result);
            }
        }
    }
}
