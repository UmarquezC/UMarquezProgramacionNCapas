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
        public static ML.Result GetAll(int idCategoria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from subCategoria in context.SubCategoria
                                 where subCategoria.IdCategoria == idCategoria
                                 select subCategoria).ToList();
                    

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.SubCategoria subCategoria = new ML.SubCategoria()
                            {
                                IdSubCategoria = obj.IdSubCategoria,
                                Nombre = obj.Nombre
                            };
                            result.Objects.Add(subCategoria);
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
    }
}
