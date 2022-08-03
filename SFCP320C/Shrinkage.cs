using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SFCP320C
{
    public class Shrinkage
    {
        public string Estado { get; set; }
        public decimal NumOrd { get; set; }
        public decimal IdCtroWrk { get; set; }
        public string NbCtroWrk { get; set; }
        public string IdProd { get; set; }
        public string NbProd { get; set; }
        public decimal Consumido { get; set; }
        public decimal Producido { get; set; }
        public decimal Rechazado { get; set; }
        public decimal Diferencia { get; set; }
        public decimal TotalShrinkage { get; set; }
    }
}