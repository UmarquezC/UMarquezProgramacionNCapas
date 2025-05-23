﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Categoria
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context =  new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from categoria in context.Categoria
                                 select new
                                 {
                                     IdCategoria = categoria.IdCategoria,
                                     NombreCategoria = categoria.Nombre
                                 }).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Categoria categoria = new ML.Categoria
                            {
                                IdCategoria = obj.IdCategoria,
                                Nombre = obj.NombreCategoria
                            };
                            result.Objects.Add(categoria);
                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "No hay categorías disponibles.";
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
