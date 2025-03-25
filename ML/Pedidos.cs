using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Pedidos
    {
        public int IdPedido { get; set; }        
        public int IdUsuario { get; set; }

        public ML.Sucursal Sucursal { get; set; }
        
        public byte? IdTipoPago { get; set; }
        public int IdDireccion { get; set; }
        public ML.EstadoPedido Estado { get; set; }
        public string FechaCreacion { get; set; }
        public int? IdRepartidor { get; set; }
    }
}
