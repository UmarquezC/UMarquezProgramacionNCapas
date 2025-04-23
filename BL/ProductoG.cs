using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class ProductoG
    {
        public static ML.Result GetAll(ML.Producto productoObj)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.ProductoGGetAll(productoObj.SubCategoria.IdSubCategoria);

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.SubCategoria = new ML.SubCategoria();
                            producto.SubCategoria.Categoria = new ML.Categoria();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.NombreProducto;
                            producto.Descripcion = obj.Descripcion;
                            producto.Precio = obj.Precio;
                            producto.Imagen = obj.Imagen;
                            producto.SubCategoria.IdSubCategoria = obj.IdSubCategoria;
                            producto.SubCategoria.Nombre = obj.NombreSubCategoria;
                            producto.SubCategoria.Categoria.IdCategoria = obj.IdSubCategoria;
                            producto.SubCategoria.Categoria.Nombre = obj.NombreCategoria;

                            result.Objects.Add(producto);
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
