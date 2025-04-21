using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "ICRUD" in both code and config file together.
    [ServiceContract]
    public interface ICRUD
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Usuario))]
        SL_WCF.Result usuarioGetAll(ML.Usuario usuario);


        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Usuario))]
        SL_WCF.Result usuarioUpdate(ML.Usuario usuario);

        [OperationContract]        
        SL_WCF.Result usuarioDelete(int idUsuario);

        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Usuario))]
        SL_WCF.Result usuarioAdd(ML.Usuario usuario);

        [OperationContract]
        [ServiceKnownTypeAttribute(typeof(ML.Usuario))]
        SL_WCF.Result usuarioGetById(int IdUsuario);

    }
}
