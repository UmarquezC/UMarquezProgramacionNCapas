using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetAllByMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.ColoniaGetAllByIdMunicipio(IdMunicipio).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Colonia colonia = new ML.Colonia();

                            colonia.IdColonia = obj.IdColonia;
                            colonia.Nombre = obj.Nombre_Colonia;
                            result.Objects.Add(obj);
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
    }
}
