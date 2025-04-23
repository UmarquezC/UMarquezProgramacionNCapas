using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Producto
    {
        /*
        public static ML.Result GetAll(ML.Producto productoObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {

                    var query = context.ProductoGetAll(productoObj.SubCategoria.Categoria.IdCategoria, productoObj.SubCategoria.IdSubCategoria).ToList();

                    if (query.Count > 0)
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
                            producto.Imagen = obj.Imagen;
                            producto.SubCategoria.IdSubCategoria = obj.IdSubCategoria;
                            producto.SubCategoria.Nombre = obj.NombreSubCategoria;
                            producto.SubCategoria.Categoria.IdCategoria = obj.IdCategoria;
                            producto.SubCategoria.Categoria.Nombre = obj.NombreCategoria;
                            producto.Precio = obj.Precio;

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


        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var rowAffets = context.ProductoAdd(producto.Nombre, producto.Descripcion, producto.Precio, producto.Imagen, producto.SubCategoria.IdSubCategoria).SingleOrDefault();

                    if (rowAffets >= 2)
                    {
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
            }

            return result;
        }

        public static ML.Result Update(ML.Producto producto)
        {
            ML.Result result = new ML.Result();


            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffects = context.ProductoUpdate(producto.IdProducto, producto.Nombre, producto.Descripcion, producto.Precio, producto.Imagen, producto.SubCategoria.IdSubCategoria);

                    if (rowAffects > 0)
                    {
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
            }


            return result;

        }

        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.ProductoGetById(IdProducto).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();
                        producto.SubCategoria = new ML.SubCategoria();
                        producto.SubCategoria.Categoria = new ML.Categoria();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.NombreCategoria;
                        producto.Descripcion = query.Descripcion;
                        producto.Precio = query.Precio;
                        producto.Imagen = query.Imagen;
                        producto.SubCategoria.IdSubCategoria = query.IdSubCategoria;
                        producto.SubCategoria.Nombre = query.NombreSubCategoria;
                        producto.SubCategoria.Categoria.IdCategoria = query.IdSubCategoria;
                        producto.SubCategoria.Categoria.Nombre = query.NombreCategoria;

                        result.Success = true;
                        result.Object = producto;
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
            }

            return result;
        }

        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {

                using(DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffect = context.ProductoDelete(IdProducto);
                    if (rowAffect > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                    }
                }

            }catch(Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }
        */

    }
        
}
