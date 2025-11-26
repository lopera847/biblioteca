using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaApp.Models
{
    public class Prestamo
    {
        public string docsuscriptor { get; set; }
        public int codigolibro { get; set; }
        public DateTime fechaprestamo { get; set; }
        public DateTime? fechadevolucion { get; set; }

        public string NombreSuscriptor { get; set; }
        public string TituloLibro { get; set; }
    }
}
