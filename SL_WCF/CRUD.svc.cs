using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "CRUD" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select CRUD.svc or CRUD.svc.cs at the Solution Explorer and start debugging.
    public class CRUD : ICRUD
    {
        public void DoWork()
        {
        }

        public SL_WCF.Result usuarioGetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuario);
            return new SL_WCF.Result
            {
                Success = result.Success,
                Message = result.Message,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result usuarioUpdate(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UpdateEF(usuario);
            return new SL_WCF.Result
            {
                Success = result.Success,
                Message = result.Message,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result usuarioDelete(int idUsuario)
        {
            ML.Result result = BL.Usuario.DeleteEF(idUsuario);
            return new SL_WCF.Result
            {
                Success = result.Success,
                Message = result.Message,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result usuarioAdd(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);
            return new SL_WCF.Result
            {
                Success = result.Success,
                Message = result.Message,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects,
            };
        }

        public SL_WCF.Result usuarioGetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            return new SL_WCF.Result
            {
                Success = result.Success,
                Message = result.Message,
                Ex = result.Ex,
                Object = result.Object,
                Objects = result.Objects
            };
        }
    }
}
