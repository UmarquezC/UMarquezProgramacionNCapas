using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class SubCategoria
    {
        public static ML.Result GetAll(int IdCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.SubCategoriaGetAllByCategoria(IdCategoria).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria();

                            subCategoria.IdSubCategoria = obj.IdSubCategoria;
                            subCategoria.Nombre = obj.NombreSubCategoria;
                            
                            result.Objects.Add(subCategoria);
                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "No hay subcategorías disponibles para la categoría seleccionada.";
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
