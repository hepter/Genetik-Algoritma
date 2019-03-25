using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetikAlgoritma
{
    public class Canli
    {
        public Canli TurnuvaCifti{ get; set; }
        public Gen Gen { get; set; }


        public double CezaPuani
        {
            get
            {
              return  Gen.PenaltySkor;
            }
        }


        //public bool MatyasAralik(long z) // -10 ≤ z,y ≤ 10 
        //{
        //    int r1 = -10;
        //    int r2 = 10;
        //    return (r1 <= z && z <= r2);
        //}
       

        public List<Canli> Olustur(int populasyon)
        {
            Random rnd = new Random();
            List<Canli> pop= new List<Canli>();
            for (int i = 0; i < populasyon; i++)
            {
                pop.Add(new Canli() { Gen = new Gen() });
            }
            return pop;
        }
    }
    


}
