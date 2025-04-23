using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace BL
{
    public class ProductoSucursal
    {
        /*
        public static ML.Result Get(ML.ProductoSucursal productoSucursalObj)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.ProductoSucursalGetBySucursal(productoSucursalObj.Sucursal.IdSucursal).ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.ProductoSucursal productoSucursal = new ML.ProductoSucursal();
                            productoSucursal.Sucursal = new ML.Sucursal();
                            productoSucursal.Producto = new ML.Producto();

                            productoSucursal.Producto.IdProducto = obj.IdProducto;
                            productoSucursal.Producto.Imagen = obj.Imagen;
                            productoSucursal.Producto.Nombre = obj.NombreProducto;
                            productoSucursal.Sucursal.IdSucursal = obj.IdSucursal;
                            productoSucursal.Sucursal.Nombre = obj.NombreSucursal;
                            productoSucursal.Stock = obj.Stock;
                            result.Objects.Add(productoSucursal);
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
                result.Success = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result UpdateStock(int IdProducto, int IdSucursal, int NuevoStock)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffets = context.ProductoSucursalUpdateStock(IdProducto, IdSucursal, NuevoStock);

                    if (rowAffets > 0)
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

        public static ML.Result Delete(int IdProducto, int IdSucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var rowAffects = context.ProductoSucursalDelete(IdProducto, IdSucursal);

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
        */
    }
}
