using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        //NOMBRE
        [DisplayName ("Nombre") ]
        [Required (ErrorMessage = "Nombre requerido")]
        [RegularExpression ("^[a-zA-Z]*$", ErrorMessage = "No se aceptan numeros")]
        public string Nombre { get; set; }

        //Apellido Paterno
        [DisplayName("Apellido Paterno")]
        [Required(ErrorMessage = "Apellido paterno requerido")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "No se aceptan numeros")]
        public string ApellidoPaterno { get; set; }

        //Email
        [DisplayName("Email")]
        [Required(ErrorMessage = "Campo Obligatorio")]
        [RegularExpression(@"^[^\s@@]+@[^\s@@]+\.[^\s@@]+$", ErrorMessage = "Este campo solo acepta correos correctos")]
        public string Email { get; set; }

        //USERNAME
        [DisplayName("Nombre de usuario")]
        [Required(ErrorMessage = "Nombre de usuario requerido")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "No se aceptan numeros")]
        public string UserName { get; set; }

        //APELLIDO MATERNO
        [DisplayName("Apellido Materno")]
        [Required(ErrorMessage = "Apellido Materno requerido")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "No se aceptan numeros")]
        public string ApellidoMaterno { get; set; }

        //Contraseña
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "Contraseña requerido")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[\W_]).{8}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo una letra mayuscula y un número")]
        public string Password { get; set; }
        //Fecha Nacimiento
        [DisplayName("Fecha de Nacimiento")]
        [Required(ErrorMessage = "Fecha requerida")]
        public string FechaNacimiento { get; set; }

        //Sexo
        [DisplayName("Sexo")]
        [Required(ErrorMessage = "Genero requerido")]
        public string Sexo {  get; set; }

        //Telefono
        [DisplayName("Telefono")]
        [Required(ErrorMessage = "Telefono requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No se aceptan letras")]
        public string Telefono { get; set; }
        //Celular
        [DisplayName("Celular")]
        [Required(ErrorMessage = "Celular requerido")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "No se aceptan letras")]
        public string Celular {  get; set; }
        public bool Estatus {  get; set; }
        //CURP
        [DisplayName("CURP")]
        [Required(ErrorMessage = "CURP requerido")]
        [RegularExpression(@"^([A-Z][AEIOUX][A-Z]{2}\d{2}(?:0\d|1[0-2])(?:[0-2]\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\d])(\d)$", ErrorMessage = "CURP INVALIDA")]        
        public string Curp { get; set; }
        //VOY A COMENTAR LA IMAGEN
        public byte[] Imagen { get; set; }
        //public int IdRol {  get; set; }

        //Agregar lo de la imagen para poder usarlo con JS
        public string ImagenBase64 { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
        public List<Object> Usuarios { get; set; }

    }
}
