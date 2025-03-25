using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class ActualizacionPedidos
    {
        public int IdActualizacionPedidos { get; set; }
        public ML.Pedidos Pedidos { get; set; }
        public ML.EstadoPedido EstadoPedido { get; set; }       
        public string Descripcion { get; set; }
        public string FechaActualizacion { get; set; }
    }
}
