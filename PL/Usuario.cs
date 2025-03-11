using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        //AGREGAR USUARIO CON STORE PROCEDURE
        public static void Add()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dame su nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Dame su Email");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Dame su User Name");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Dame su Password");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Dame su Fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Dame su Sexo ( M/F )");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Dame su Telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Dame su Celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Dame su Estatus (true / false)");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Dame su Curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            } else {
                Console.WriteLine("No existe");
            }

            Console.WriteLine("Dame su IdRol");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            BL.Usuario.Add(usuario);
        }


        //Agregar usuario con sql normal
        public static void AddSqlClient()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dame el nombre del alumno");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Dame su apellido paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Dame su correo");
            usuario.Email = Console.ReadLine();

            //GUardar datos
            BL.Usuario.Add(usuario);
        }

        //BORRAR USUARIO CON STORE PROCEDURE
        public static void Delete()
        {
            Console.WriteLine("Dime el Id que quieres eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            BL.Usuario.Delete(IdUsuario);
        }
        
        
        //Borrar usuario con sql
        public static void DeleteSqlClient()
        {
            Console.WriteLine("Dime el id del usuario que quieres eliminar");
            int idUsuario = Convert.ToInt32(Console.ReadLine());
            BL.Usuario.DeleteSqlClient(idUsuario);
        }


        //ACTUALIZAR USUARIO CON STORE PROCEDURE
        public static void Update()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dime el id del usuario para actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Actualizar nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Actualizar apellido");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Actualizar correo");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Actualizar Username");
            usuario.UserName = Console.ReadLine();
            
            Console.WriteLine("Actualizar Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Actualizar contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Actualizar fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Actualizar telefono");
            usuario.Telefono = Console.ReadLine();
            
            Console.WriteLine("Actualizar celular");
            usuario.Celular = Console.ReadLine();
            
            Console.WriteLine("Actualizar Estatus");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Actualizar curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            BL.Usuario.Update(usuario);
        }


        //Actualizar usuario con sql
        public static void UpdateSqlClient()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dime el id del usuario para actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Actualizar nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Actualizar apellido");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Actualizar correo");
            usuario.Email = Console.ReadLine();

            BL.Usuario.UpdateSqlClient(usuario);
        }

        //Mostrar URIEL yO lo hice
        public static void Get()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("ID      Nombre      Apellido         Email");
            BL.Usuario.Get(usuario);
        }


        //MOSTRAR TODOS CON STORE PROCEDURE
        public static void GetAll()
        {
            ML.Result result = BL.Usuario.GetAll();

            if (result.Success)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.Write(usuario.Email);
                    
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.Estatus);
                    Console.WriteLine(usuario.Curp);
                    Console.WriteLine(usuario.Rol.IdRol);
                    
                }
            }
            else
            {
                Console.WriteLine("Error " + result.Message);
            }
        }


        //GetAll con sql normal
        public static void GetAllSqlCLient()
        {
            ML.Result result = BL.Usuario.GetAllSqlCLient();

            if (result.Success)
            {
                foreach(ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.WriteLine(usuario.ApellidoPaterno);
                }
            }
            else
            {
                Console.WriteLine("Error " + result.Message);
            }
        }


        //GETBYID CON STORE PROCEDURE
        public static void GetById()
        {
            Console.WriteLine("Que id deseas consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuario.GetById(IdUsuario);
            if (result.Success)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.Write(usuario.Email);
                
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.Estatus);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
            }
            else
            {
                Console.WriteLine("error " + result.Message);
            }
        }

        //Obtener por ID
        public static void GetByIdSqlClient()
        {
            Console.WriteLine("Que id deseas consultar");
            int idUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuario.GetByIdSqlClient(idUsuario);
            if (result.Success) {
               ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine(Convert.ToInt32(idUsuario.ToString()));
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.WriteLine(usuario.Email);
            }
            else
            {
                Console.WriteLine("error " + result.Message);
            }
        }




        //**ENTITY FRAMEWORK****************

        //AGREGAR
        public static void AddEF()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dame su nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Dame su Email");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Dame su User Name");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Dame su Password");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Dame su Fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Dame su Sexo ( M/F )");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Dame su Telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Dame su Celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Dame su Estatus (true / false)");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Dame su Curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            Console.WriteLine("Dame su IdRol");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            BL.Usuario.Add(usuario);
        }

        //UPDATE
        public static void UpdateEF()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dime el id del usuario para actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Actualizar nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Actualizar apellido");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Actualizar correo");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Actualizar Username");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Actualizar Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Actualizar contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Actualizar fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Actualizar sexo");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Actualizar telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Actualizar celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Actualizar Estatus");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Actualizar curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            BL.Usuario.UpdateEF(usuario);
        }

        //MOSTRAR TOOOS
        public static void GetAllEF(ML.Usuario usuarioObj)
        {
            ML.Result result = BL.Usuario.GetAllEF(usuarioObj);

            if (result.Success)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.Write(usuario.Email);
                    
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.Estatus);
                    Console.WriteLine(usuario.Curp);
                    Console.WriteLine(usuario.Rol.IdRol);

                }
            }
            else
            {
                Console.WriteLine("Error " + result.Message);
            }
        }

        //MOSTRAR POR ID 

       public static void GetByIdEF() {

            Console.WriteLine("Que id deseas consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Success)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.Write(usuario.Email);
                
                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.Estatus);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
            }
            else
            {
                Console.WriteLine("error " + result.Message);
            }
        }

        public static void DeleteEF()
        {
            Console.WriteLine("Dime el Id que quieres eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            BL.Usuario.Delete(IdUsuario);
        }



        //******** LINQ ****************
        //Agregar
        public static void AddLINQ()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dame su nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Paterno");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Dame su Email");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Dame su User Name");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Dame su Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Dame su Password");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Dame su Fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Dame su Sexo ( M/F )");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Dame su Telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Dame su Celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Dame su Estatus (true / false)");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Dame su Curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            Console.WriteLine("Dame su IdRol");
            usuario.Rol.IdRol = Convert.ToInt32(Console.ReadLine());

            BL.Usuario.AddLINQ(usuario);
        }

        public static void UpdateLINQ()
        {
            ML.Usuario usuario = new ML.Usuario();

            Console.WriteLine("Dime el id del usuario para actualizar");
            usuario.IdUsuario = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Actualizar nombre");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Actualizar apellido");
            usuario.ApellidoPaterno = Console.ReadLine();

            Console.WriteLine("Actualizar correo");
            usuario.Email = Console.ReadLine();

            Console.WriteLine("Actualizar Username");
            usuario.UserName = Console.ReadLine();

            Console.WriteLine("Actualizar Apellido Materno");
            usuario.ApellidoMaterno = Console.ReadLine();

            Console.WriteLine("Actualizar contraseña");
            usuario.Password = Console.ReadLine();

            Console.WriteLine("Actualizar fecha de nacimiento");
            usuario.FechaNacimiento = Console.ReadLine();

            Console.WriteLine("Actualizar sexo");
            usuario.Sexo = Console.ReadLine();

            Console.WriteLine("Actualizar telefono");
            usuario.Telefono = Console.ReadLine();

            Console.WriteLine("Actualizar celular");
            usuario.Celular = Console.ReadLine();

            Console.WriteLine("Actualizar Estatus");
            usuario.Estatus = Convert.ToBoolean(Console.ReadLine());

            Console.WriteLine("Actualizar curp");
            usuario.Curp = Console.ReadLine();

            Console.WriteLine("Dame su Imagen");
            string rutaImagen = Console.ReadLine();
            if (File.Exists(rutaImagen))
            {
                usuario.Imagen = File.ReadAllBytes(rutaImagen);
                Console.WriteLine("Cargado");
            }
            else
            {
                Console.WriteLine("No existe");
            }

            BL.Usuario.UpdateLINQ(usuario);
        }

        //MOSTRAR
        public static void GetAllLINQ()
        {
            ML.Result result = BL.Usuario.GetAllLINQ();

            if (result.Success)
            {
                foreach (ML.Usuario usuario in result.Objects)
                {
                    Console.WriteLine(usuario.IdUsuario);
                    Console.WriteLine(usuario.Nombre);
                    Console.WriteLine(usuario.ApellidoPaterno);
                    Console.Write(usuario.Email);
                    Console.WriteLine(usuario.UserName);
                    Console.WriteLine(usuario.ApellidoMaterno);
                    Console.WriteLine(usuario.Password);
                    Console.WriteLine(usuario.FechaNacimiento);
                    Console.WriteLine(usuario.Sexo);
                    Console.WriteLine(usuario.Telefono);
                    Console.WriteLine(usuario.Celular);
                    Console.WriteLine(usuario.Estatus);
                    Console.WriteLine(usuario.Curp);
                    Console.WriteLine(usuario.Rol.IdRol);
                }
            }
            else
            {
                Console.WriteLine("Error " + result.Message);
            }
        }


        //Obtener por Id
        public static void GetByIdLINQ()
        {

            Console.WriteLine("Que id deseas consultar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            ML.Result result = BL.Usuario.GetByIdLINQ(IdUsuario);
            if (result.Success)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;
                Console.WriteLine(usuario.IdUsuario);
                Console.WriteLine(usuario.Nombre);
                Console.WriteLine(usuario.ApellidoPaterno);
                Console.Write(usuario.Email);

                Console.WriteLine(usuario.UserName);
                Console.WriteLine(usuario.ApellidoMaterno);
                Console.WriteLine(usuario.Password);
                Console.WriteLine(usuario.FechaNacimiento);
                Console.WriteLine(usuario.Sexo);
                Console.WriteLine(usuario.Telefono);
                Console.WriteLine(usuario.Celular);
                Console.WriteLine(usuario.Estatus);
                Console.WriteLine(usuario.Curp);
                Console.WriteLine(usuario.Rol.IdRol);
                {
                    Console.WriteLine("error " + result.Message);
                }
            }
        }


        //Borrar
        public static void DeleteLINQ()
        {
            Console.WriteLine("Dime el Id que quieres eliminar");
            int IdUsuario = Convert.ToInt32(Console.ReadLine());
            BL.Usuario.DeleteLINQ(IdUsuario);
        }

        //******CARGAMASIVA
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            Console.WriteLine("Entrando a carga masiva");
            string ruta = @"C:\Users\digis\OneDrive\Documents\UrielMarquezCruz\CargaMasiva.txt";

            try
            {
                StreamReader streamReader = new StreamReader(ruta); //streamreader permite leer el archivo 
                string fila = "";
                streamReader.ReadLine();//Ignora la primera linea
                while ((fila = streamReader.ReadLine()) != null)
                {
                    string[] valores = fila.Split('|');

                    ML.Usuario usuario = new ML.Usuario();
                    usuario.Rol = new ML.Rol();
                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Colonia = new ML.Colonia();

                    usuario.Nombre = valores[0];
                    usuario.ApellidoPaterno = valores[1];
                    usuario.Email = valores[2];
                    usuario.UserName = valores[3];
                    usuario.ApellidoMaterno = valores[4];
                    usuario.Password = valores[5];
                    usuario.FechaNacimiento = valores[6];
                    usuario.Sexo = valores[7];
                    usuario.Telefono = valores[8];
                    usuario.Celular = valores[9];
                    usuario.Estatus = Convert.ToBoolean(valores[10]);
                    usuario.Curp = valores[11];

                    usuario.Rol.IdRol = Convert.ToInt32(valores[12]);
                    usuario.Direccion.Calle = valores[13];
                    usuario.Direccion.NumeroInteriror = valores[14];
                    usuario.Direccion.NumeroExterior = valores[15];
                    usuario.Direccion.Colonia.IdColonia = Convert.ToInt32(valores[16]);


                    BL.Usuario.AddEF(usuario);

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