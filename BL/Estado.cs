using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Estado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from estado in context.Estado
                                 select new
                                 {
                                     IdEstado = estado.IdEstado,
                                     Nombre = estado.Nombre,
                                 }
                                 ).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Estado estado = new ML.Estado();

                            estado.IdEstado = obj.IdEstado;
                            estado.Nombre = obj.Nombre;

                            result.Objects.Add(estado);
                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success= false;
                        result.Message = "No hay registros";
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
    }
}