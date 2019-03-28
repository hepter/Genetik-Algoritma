using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetikAlgoritma
{
    public class Gen
    {
        public double x1 { get; set; }
        public double x2 { get; set; }

        public Gen()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            x1 = rnd.NextDouble() * 20 - 10;
            x2 = rnd.NextDouble() * 20 - 10;
        }
        public Gen(double x1,double x2)
        {
            this.x1 = x1;
            this.x2 = x2;
        }

        public double PenaltySkor
        {
            get { return LimitFonksiyon(x1, x2); }
        }

        public double MatyasFormulSkor
        {
            get
            {
                double result = 0.26 * ((x1 * x1)+ (x2 * x2)) - 0.48 * x1 * x2;
                return result;
            }
        }
        double LimitFonksiyon(double x,double y)
        {
            return (x - y) < 0 ? -(x - y) : (x - y);
        }
    }
}
