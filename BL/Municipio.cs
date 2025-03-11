using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetAllByEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.MunicipioGetAllByIdEstado(IdEstado).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Municipio municipio = new ML.Municipio();
                                

                            municipio.IdMunicipio = obj.IdMunicipio;
                            municipio.Nombre = obj.Nombre_Municipio;              

                            result.Objects.Add(obj);
                        }
                        
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
                result.Ex = ex;
            }
            return result;
        }
    }
}
