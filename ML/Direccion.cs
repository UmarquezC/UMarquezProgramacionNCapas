using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Direccion
    {
        public int IdDireccion {  get; set; }
        //CALLE
        [DisplayName("Calle")]
        [Required(ErrorMessage = "Calle requerido")]  
        public string Calle { get; set; }
        // Numero interior
        [DisplayName("Numero Interior")]
        public string NumeroInteriror { get; set; }

        //Numero Exterior
        [DisplayName("Numero Exterior")]
        [Required(ErrorMessage = "Numero Exterior requerido")]
        public string NumeroExterior { get; set; }
        public ML.Colonia Colonia { get; set; }
        public ML.Usuario Usuario { get; set; }
        public List<Object> DIrecciones { get; set; }
    }
}
