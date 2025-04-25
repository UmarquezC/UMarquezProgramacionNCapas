using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Sucursal
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from sucursal in context.Sucursal
                                 select new
                                 {
                                     IdSucursal = sucursal.IdSucursal,
                                     Nombre = sucursal.Nombre,
                                     Latitud = sucursal.Latitud,
                                     Longitud = sucursal.Longitud
                                 }
                                 );

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach(var obj in query)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = obj.IdSucursal;
                            sucursal.Nombre = obj.Nombre;
                            sucursal.Latitud = obj.Latitud;
                            sucursal.Longitud = obj.Longitud;
                            result.Objects.Add( sucursal );
                        }
                        result.Success = true;

                    }
                    else
                    {
                        result.Success = false;
                    }
                }
                /*
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    var query = "SELECT * FROM Sucursal";

                    SqlCommand cmd = new SqlCommand(query, context);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 1)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            ML.Sucursal sucursal = new ML.Sucursal();
                            sucursal.IdSucursal = Convert.ToByte(dataRow[0]);
                            sucursal.Nombre = dataRow[1].ToString();
                            sucursal.Latitud = dataRow[2].ToString();
                            sucursal.Longitud = dataRow[3].ToString();

                            result.Objects.Add(sucursal);

                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "No hay sucursales";
                    }
                }*/
            }
            catch (Exception ex)
            {
                result.Success = true;
                result.Message = ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    var query = "INSERT INTO Sucursal (Nombre, Latitud, Longitud) VALUES (@Nombre, @Latitud, @Longitud)";
                    SqlCommand cmd = new SqlCommand(query, context);

                    cmd.Parameters.AddWithValue("@Nombre", sucursal.Nombre);
                    cmd.Parameters.AddWithValue("@Latitud", sucursal.Latitud);
                    cmd.Parameters.AddWithValue("@Longitud", sucursal.Longitud);

                    context.Open();
                    int rowAffects = cmd.ExecuteNonQuery();
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

        public static ML.Result GetById(int IdSucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT IdSucursal, Nombre, Latitud, Longitud FROM Sucursal WHERE IdSucursal = @IdSucursal";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("IdSucursal", IdSucursal);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow datarow = dataTable.Rows[0];
                        ML.Sucursal sucursal = new ML.Sucursal();

                        sucursal.IdSucursal = Convert.ToByte(datarow[0]);
                        sucursal.Nombre = datarow[1].ToString();
                        sucursal.Latitud = datarow[2].ToString();
                        sucursal.Longitud = datarow[3].ToString();

                        result.Object = sucursal;
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

        public static ML.Result Update(ML.Sucursal sucursal)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UPDATE Sucursal SET Nombre = @Nombre, Latitud = @Latitud, Longitud = @Longitud WHERE IdSucursal = @IdSucursal";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@IdSucursal", sucursal.IdSucursal);
                    cmd.Parameters.AddWithValue("@Nombre", sucursal.Nombre);
                    cmd.Parameters.AddWithValue("@Latitud", sucursal.Latitud);
                    cmd.Parameters.AddWithValue("@Longitud", sucursal.Longitud);
                    context.Open();
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        result.Success = true;
                        result.Message = "Sucursal actualizada correctamente.";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "No se pudo actualizar la sucursal.";
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
