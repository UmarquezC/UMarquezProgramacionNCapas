using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Rol
    {
        //USAR LINQ para hacer el DropDownList
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from rol in context.Rol
                                 select new
                                 {
                                     IdRol = rol.IdRol,
                                     Nombre = rol.Nombre,
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Rol rol = new ML.Rol();

                            rol.IdRol = obj.IdRol;
                            rol.Nombre = obj.Nombre;

                            result.Objects.Add(rol);
                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "No hay registros";
                    }
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
                result.Ex = e;
            }

            return result;
        }
    }
}
