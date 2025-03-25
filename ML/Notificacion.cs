using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Notificacion
    {
        public int IdNotificacion { get; set; }
        public ML.Pedidos Pedidos { get; set; }
        public int IdTipoNotificacion { get; set; }
        public string Mensaje { get; set; }
        public string Fecha { get; set; }
    }
}
