using DL_EF;
using ML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        //Agregar usuario con Store Procedure
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioAdd";
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = context;
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Sexo", usuario.Sexo);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);
                    cmd.Parameters.AddWithValue("@IdRol", usuario.Rol.IdRol);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "Hubo un error";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine(result.Message);
            }

            return result;
        }


        //Agregar Usuario con sqlClient
        public static ML.Result AddSqlClient(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                //Establecer conexion
                //Destrouir todo despues de usarlo
                //Importar librerias
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    //Agregar insercion
                    string query = "INSERT INTO Usuario (Nombre, ApellidoPaterno, Email) VALUES (@nombre, @apellidoPaterno, @email)";

                    //Crear command
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    // O

                    //SqlCommand cmd = new SqlCommand(query, context);


                    //Agregar parametros a SQL
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);

                    //Abrir conexion
                    context.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        Console.WriteLine("Agregado incorrectamente");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }


        //Borar Usuario con Store Procedure
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioDelete";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        Console.WriteLine("Error al eliminiar");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }


        //Borrar usuario con sql normal
        public static void DeleteSqlClient(int idUsuario)
        {
            try
            {
                // Establecer conexion
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";
                    SqlCommand cmd = new SqlCommand(query, context);

                    //Agregar el parametro
                    cmd.Parameters.AddWithValue("IdUsuario", idUsuario);
                    context.Open();
                    //Muestra las lineas afectadas
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    if (filasAfectadas > 0)
                    {
                        Console.WriteLine("Alumno borrado con exito");
                    }
                    else
                    {
                        Console.WriteLine("Alumno no borrado");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }


        //ACTUALIZAR USUARIO CON STORE PROCEDURE
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioUpdate";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@ApellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@Email", usuario.Email);
                    cmd.Parameters.AddWithValue("@UserName", usuario.UserName);
                    cmd.Parameters.AddWithValue("@ApellidoMaterno", usuario.ApellidoMaterno);
                    cmd.Parameters.AddWithValue("@Password", usuario.Password);
                    cmd.Parameters.AddWithValue("@FechaNacimiento", usuario.FechaNacimiento);
                    cmd.Parameters.AddWithValue("@Telefono", usuario.Telefono);
                    cmd.Parameters.AddWithValue("@Celular", usuario.Celular);
                    cmd.Parameters.AddWithValue("@Estatus", usuario.Estatus);
                    cmd.Parameters.AddWithValue("@CURP", usuario.Curp);
                    cmd.Parameters.AddWithValue("@Imagen", usuario.Imagen);


                    context.Open();
                    int filaAfectada = cmd.ExecuteNonQuery();
                    if (filaAfectada > 0)
                    {
                        Console.WriteLine("Actualizado correctamente");

                    }
                    else
                    {
                        Console.WriteLine("No actualizado");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }

        //Actualiar usuario
        public static void UpdateSqlClient(ML.Usuario usuario)
        {
            try
            {
                //usar using destruye e importa
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    // crear query
                    string query = "UPDATE Usuario SET Nombre = @nombre,ApellidoPaterno = @apellidoPaterno,Email = @email WHERE IdUsuario = @idUsuario; ";

                    SqlCommand cmd = new SqlCommand(query, context);

                    //Agregar parametros en Sql
                    cmd.Parameters.AddWithValue("@idUsuario", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@apellidoPaterno", usuario.ApellidoPaterno);
                    cmd.Parameters.AddWithValue("@email", usuario.Email);

                    context.Open();
                    int filaAfectada = cmd.ExecuteNonQuery();
                    if (filaAfectada > 0)
                    {
                        Console.WriteLine("Actualizado correctamente");

                    }
                    else
                    {
                        Console.WriteLine("No actualizado");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }


        //Obtener datos Uriel
        public static void Get(ML.Usuario usuario)
        {
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = " SELECT TOP 5 * FROM Usuario; ";
                    // string query = "SELECT TOP 3 Nombre, ApellidoPaterno, Email FROM Usuario ;";
                    SqlCommand command = new SqlCommand(query, context);

                    context.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}", reader.GetInt32(0),
                                reader.GetString(1), reader.GetString(2), reader.GetString(3));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No hay datos");
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
        }

        //OBTENER DATOS CON STORE PROCEDURE
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioGetAll";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());

                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.Email = row[3].ToString();

                            usuario.UserName = row[4].ToString();
                            usuario.ApellidoMaterno = row[5].ToString();
                            usuario.Password = row[6].ToString();
                            usuario.FechaNacimiento = row[7].ToString();
                            usuario.Sexo = row[8].ToString();
                            usuario.Telefono = row[9].ToString();
                            usuario.Celular = row[10].ToString();
                            usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                            usuario.Curp = row[12].ToString();
                            if (row[13].ToString() == "")
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = Convert.ToInt32(row[14]);

                            }

                            result.Objects.Add(usuario);

                        }

                        result.Success = true;

                    }
                    else
                    {
                        result.Success = false;

                        result.Message = "No hay registro";
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }



        //Obtener todos igual SQLCLIENT
        public static ML.Result GetAllSqlCLient()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT * FROM Usuario;";
                    SqlCommand cmd = new SqlCommand(query, context);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = Convert.ToInt32(row[0].ToString());

                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.Email = row[3].ToString();

                            result.Objects.Add(usuario);

                        }

                        result.Success = true;

                    }
                    else
                    {
                        result.Success = false;

                        result.Message = "No hay registro";
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




        //MOSTRAR POR ID STORE PROCEDURE
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "UsuarioGetById";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdUsuario", IdUsuario);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());

                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.Email = row[3].ToString();

                        usuario.UserName = row[4].ToString();
                        usuario.ApellidoMaterno = row[5].ToString();
                        usuario.Password = row[6].ToString();
                        usuario.FechaNacimiento = row[7].ToString();
                        usuario.Sexo = row[8].ToString();
                        usuario.Telefono = row[9].ToString();
                        usuario.Celular = row[10].ToString();
                        usuario.Estatus = Convert.ToBoolean(row[11].ToString());
                        usuario.Curp = row[12].ToString();
                        if (row[13].ToString() == "")
                        {
                            usuario.Rol.IdRol = 0;
                        }
                        else
                        {
                            usuario.Rol.IdRol = Convert.ToInt32(row[14]);

                        }
                        result.Success = true;
                        result.Object = usuario;
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


        //Mostrar por id SQL CLIENT

        public static ML.Result GetByIdSqlClient(int idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT * FROM Usuario WHERE IdUsuario = @idUsuario;";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);



                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow row = dataTable.Rows[0];
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = Convert.ToInt32(row[0].ToString());
                        usuario.Nombre = row[1].ToString();
                        usuario.ApellidoPaterno = row[2].ToString();
                        usuario.Email = row[3].ToString();

                        result.Object = usuario;
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








        //HACER LO DE ENTITY FRAME*****************************

        //AGREGAR CON ENTITY
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffect = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.Email, usuario.UserName, usuario.ApellidoMaterno, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInteriror, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (rowAffect >= 2)
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
                result.Ex = ex;
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        //UPDATE CON ENTITY
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffect = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.Email, usuario.UserName, usuario.ApellidoMaterno, usuario.Password, Convert.ToDateTime(usuario.FechaNacimiento), usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.Estatus, usuario.Curp, usuario.Imagen, usuario.Rol.IdRol, usuario.Direccion.Calle, usuario.Direccion.NumeroInteriror, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    if (rowAffect > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        Console.WriteLine("No actualizado");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }

        //MOSTRAR TODOS CON ENTITY
        public static ML.Result GetAllEF(ML.Usuario usuarioObj)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetAllVIewSqlSP(usuarioObj.Nombre, usuarioObj.ApellidoPaterno, usuarioObj.ApellidoMaterno, usuarioObj.Rol.IdRol).ToList();
                    //IGUAL SE PUEDE USAR LA VISTA
                    //var query = context.UsuarioGetAllVIewSqlSP(usuarioObj.Nombre, usuarioObj.ApellidoPaterno, usuarioObj.ApellidoMaterno, usuarioObj.Rol.IdRol).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                            usuario.Rol = new ML.Rol();

                            usuario.IdUsuario = Convert.ToInt32(obj.IdUsuario);

                            usuario.Nombre = obj.NombreUsuario;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.Email = obj.Email;

                            usuario.UserName = obj.UserName;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento;
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.Estatus = Convert.ToBoolean(obj.Estatus);
                            usuario.Curp = obj.CURP;
                            usuario.Imagen = obj.Imagen;
                            if (obj.IdRol == null)
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = Convert.ToInt32(obj.IdRol);
                            }

                            usuario.Rol.Nombre = obj.NombreRol;
                            usuario.Direccion.IdDireccion = Convert.ToInt32(obj.IdDireccion);
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInteriror = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;
                            usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(obj.IdColonia);
                            usuario.Direccion.Colonia.Nombre = obj.NombreColonia;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(obj.IdMunicipio);
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(obj.IdEstado);
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            result.Objects.Add(usuario);
                        }
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;

                        result.Message = "No hay registro";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }

        //MOSTRAR POR ID CON ENTITY
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = context.UsuarioGetById(IdUsuario).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.Rol = new ML.Rol();
                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();

                        usuario.IdUsuario = Convert.ToInt32(query.IdUsuario);
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.Email = query.Email;

                        usuario.UserName = query.UserName;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Estatus = Convert.ToBoolean(query.Estatus);
                        usuario.Curp = query.CURP;
                        usuario.Imagen = query.Imagen;


                        if (query.IdRol == null)
                        {
                            usuario.Rol.IdRol = 1;
                        }
                        else
                        {
                            usuario.Rol.IdRol = Convert.ToInt32(query.IdRol);
                        }

                        usuario.Direccion.IdDireccion = Convert.ToInt32(query.IdDireccion);
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInteriror = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExterior;
                        usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(query.IdColonia);
                        usuario.Direccion.Colonia.Nombre = query.NombreColonia;
                        usuario.Direccion.Colonia.CodigoPostal = query.CodigoPostal;
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = Convert.ToInt32(query.IdMunicipio);
                        usuario.Direccion.Colonia.Municipio.Nombre = query.NombreMunicipio;
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = Convert.ToInt32(query.IdEstado);
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = query.NombreEstado;

                        result.Success = true;
                        result.Object = usuario;
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
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }

        //BORRAR CON ENTITY
        public static ML.Result DeleteEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    int rowAffects = context.UsuarioDelete(IdUsuario);

                    if (rowAffects > 0)
                    {
                        result.Success = true;
                        Console.WriteLine("Borrado con exito");
                    }
                    else
                    {
                        result.Success = false;
                        Console.WriteLine("No eliminado");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }

        //MODIFICAR EL ESTATUS
        public static ML.Result CambiarStatus(int IdUsuario, bool Estatus)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new UMarquezProgramacionNCapasEntities())
                {
                    int rowAffets = context.CambiarStatus(IdUsuario, Estatus);
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
                result.Ex = ex;
            }

            return result;
        }


        //******** LINQ ****************

        //AGREGAR CON LINQ
        public static ML.Result AddLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    DL_EF.Usuario usuarioDB = new DL_EF.Usuario();
                    usuarioDB.Nombre = usuario.Nombre;
                    usuarioDB.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDB.Email = usuario.Email;
                    usuarioDB.UserName = usuario.UserName;
                    usuarioDB.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDB.Password = usuario.Password;
                    usuarioDB.FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
                    usuarioDB.Sexo = usuario.Sexo;
                    usuarioDB.Telefono = usuario.Telefono;
                    usuarioDB.Celular = usuario.Celular;
                    usuarioDB.Estatus = usuario.Estatus;
                    usuarioDB.CURP = usuario.Curp;
                    usuarioDB.Imagen = usuario.Imagen;
                    usuarioDB.IdRol = usuario.Rol.IdRol;
                    context.Usuario.Add(usuarioDB);
                    int rowAffects = context.SaveChanges();
                    if (rowAffects > 0)
                    {
                        result.Success = true;
                    }
                    else
                    {
                        result.Success = false;
                        Console.WriteLine("Error al agregar");
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }

        //UPDATE
        public static ML.Result UpdateLINQ(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from usuarioDB in context.Usuario
                                 where usuarioDB.IdUsuario == usuario.IdUsuario
                                 select usuarioDB).SingleOrDefault();
                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.Email = usuario.Email;
                        query.UserName = usuario.UserName;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.Password = usuario.Password;
                        query.FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
                        query.Sexo = usuario.Sexo;
                        query.Telefono = usuario.Telefono;
                        query.Celular = usuario.Celular;
                        query.Estatus = usuario.Estatus;
                        query.CURP = usuario.Curp;
                        query.Imagen = usuario.Imagen;
                        int rowAffects = context.SaveChanges();
                        if (rowAffects > 0)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Error";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }
            return result;
        }

        //MOSTRAR 
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new UMarquezProgramacionNCapasEntities())
                {
                    var query = (
                            from usuario in context.Usuario
                            select new
                            {
                                Id = usuario.IdUsuario,
                                Nombre = usuario.Nombre,
                                ApellidoPaterno = usuario.ApellidoPaterno,
                                Email = usuario.Email,
                                UserName = usuario.UserName,
                                ApellidoMaterno = usuario.ApellidoMaterno,
                                password = usuario.Password,
                                FechaNacimiento = usuario.FechaNacimiento,
                                Sexo = usuario.Sexo,
                                Telefono = usuario.Telefono,
                                Celular = usuario.Celular,
                                Estatus = usuario.Estatus,
                                IdRol = usuario.IdRol,
                            }
                        ).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();

                            usuario.IdUsuario = obj.Id;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.Email = obj.Email;
                            usuario.UserName = obj.UserName;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Password = obj.password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MMMM/yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.Estatus = obj.Estatus;
                            if (obj.IdRol == null)
                            {
                                usuario.Rol.IdRol = 0;
                            }
                            else
                            {
                                usuario.Rol.IdRol = Convert.ToInt32(obj.IdRol);
                            }

                            result.Objects.Add(usuario);
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
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
                result.Ex = ex;
                Console.WriteLine("Error" + ex.Message);
            }


            return result;
        }

        //MOSTRAR POR ID
        public static ML.Result GetByIdLINQ(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new UMarquezProgramacionNCapasEntities())
                {
                    var query = (
                        from usuario in context.Usuario
                        where usuario.IdUsuario == IdUsuario
                        select new
                        {
                            Id = usuario.IdUsuario,
                            Nombre = usuario.Nombre,
                            ApellidoPaterno = usuario.ApellidoPaterno,
                            Email = usuario.Email,

                            UserName = usuario.UserName,
                            ApellidoMaterno = usuario.ApellidoMaterno,
                            password = usuario.Password,
                            FechaNacimiento = usuario.FechaNacimiento,
                            Sexo = usuario.Sexo,
                            Telefono = usuario.Telefono,
                            Celular = usuario.Celular,
                            Estatus = usuario.Estatus,
                            IdRol = usuario.IdRol,
                        }
                        ).SingleOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.Id;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.Email = query.Email;

                        usuario.UserName = query.UserName;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.Estatus = query.Estatus;
                        if (query.IdRol == null)
                        {
                            usuario.Rol.IdRol = 0;
                        }
                        else
                        {
                            usuario.Rol.IdRol = Convert.ToInt32(query.IdRol);
                        }
                        result.Object = usuario;
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
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }

        //BORRAR
        public static ML.Result DeleteLINQ(int IdUsuario)
        {
            ML.Result result = new Result();

            try
            {
                using (DL_EF.UMarquezProgramacionNCapasEntities context = new DL_EF.UMarquezProgramacionNCapasEntities())
                {
                    var query = (from usuario in context.Usuario
                                 where usuario.IdUsuario == IdUsuario
                                 select usuario).SingleOrDefault();

                    if (query != null)
                    {
                        context.Usuario.Remove(query);
                        int rowAffects = context.SaveChanges();
                        if (rowAffects > 0)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "No se pudo eliminar";
                        }
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
                Console.WriteLine("Error" + ex.Message);
            }

            return result;
        }

    }
}
