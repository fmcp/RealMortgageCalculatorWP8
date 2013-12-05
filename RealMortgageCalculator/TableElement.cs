using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealMortgageCalculator
{

    /**
     * Clase usada para representar cada elemento de la tabla de amortización
     **/
    class TableElement
    {
        public String mes { get; set; }
        public String deuda { get; set; }
        public String amortizacion { get; set; }
        public String interes { get; set; }
    }
}
