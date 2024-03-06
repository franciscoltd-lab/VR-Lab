using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    internal class AnalitoInfo
    {
        public int concentracion_analito { get; set; }
        public double concentracion_titulante { get; set; }
        public string estructura_analito { get; set; }
        public string estructura_titulante { get; set; }
        public string nombre_analito { get; set; }
        public string nombre_indicador { get; set; }
        public string nombre_titulante { get; set; }
        public int volumen_analito { get; set; }
        public int volumen_titulante { get; set; }
    }
}
