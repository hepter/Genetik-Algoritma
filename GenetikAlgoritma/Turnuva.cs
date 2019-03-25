using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenetikAlgoritma
{
    class Turnuva
    {
        private List<Canli> canliList;
        public Turnuva(List<Canli> canliList)
        {
            this.canliList = canliList;
        }

        private Canli Kiyasla(Canli c1,Canli c2)
        {
            Canli c= new Canli();

            if (c1.Gen.PenaltySkor > c2.Gen.PenaltySkor)
                c = c2;
            else if (c1.Gen.PenaltySkor < c2.Gen.PenaltySkor)
                c = c1;
            else
                c = c1.Gen.MatyasFormulSkor > c2.Gen.MatyasFormulSkor ? c2 : c1;
            return c;
        }
        public List<Canli> Olustur()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> TurnuvaList=new List<Canli>();
            for (int i = 0; i < canliList.Count; i++)
            {
                int rndIndis1 ,rndIndis2;
                rndIndis1 = rnd.Next(0,canliList.Count);
                rndIndis2 = rnd.Next(0,canliList.Count);
                var v1 = canliList[rndIndis1];
                var v2 = canliList[rndIndis2];
                TurnuvaList.Add(Kiyasla(v1,v2));


                rndIndis1 = rnd.Next(0,canliList.Count);
                rndIndis2 = rnd.Next(0,canliList.Count);
                v1 = canliList[rndIndis1];
                v2 = canliList[rndIndis2];
                TurnuvaList[i].TurnuvaCifti = Kiyasla(v1,v2);
            }

            return TurnuvaList;
        }


        public List<Canli> Caprazla(List<Canli> canliList,double ihtimal)
        {
            
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            Random rnd2 = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> caprazlanmisList = new List<Canli>();

            foreach (var canli in canliList)
            {
                if (rnd2.NextDouble() > ihtimal)
                {
                    caprazlanmisList.Add(canli);
                    continue;
                }

                double rndDouble = rnd.NextDouble();
                double offspring1a = rndDouble * canli.Gen.x1 + (1-rndDouble) * canli.TurnuvaCifti.Gen.x1;
                double offspring1b = rndDouble * canli.Gen.x2 + (1-rndDouble)* canli.TurnuvaCifti.Gen.x2;

                
                double offspring2a = (1-rndDouble) * canli.Gen.x1 + rndDouble * canli.TurnuvaCifti.Gen.x1;
                double offspring2b = (1-rndDouble) * canli.Gen.x2 + rndDouble * canli.TurnuvaCifti.Gen.x2;

                caprazlanmisList.Add(new Canli()
                {
                    Gen = new Gen()
                    {
                        x1=offspring1a,
                        x2=offspring1b
                    },
                    TurnuvaCifti = new Canli()
                    {
                        Gen = new Gen()
                        {
                            x1=offspring2a,
                            x2=offspring2b
                        }
                    }
                });
            }

            return caprazlanmisList;
        }

        public List<Canli> Mutasyon(List<Canli> canliList,double ihtimal)
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            List<Canli> mutasyonList = new List<Canli>();

            foreach (var canli in canliList)
            {
                if (rnd.NextDouble() > ihtimal)
                {
                    mutasyonList.Add(canli);
                    continue;
                }

                mutasyonList.Add(new Canli()
                {
                    Gen = new Gen(),
                    TurnuvaCifti = new Canli(){ Gen = new Gen() }
                });
            }

            return mutasyonList;
        }



        public double Formul(List<Canli> canliList)
        {
            double best=0;
            foreach (Canli canli in canliList)
            {

                if (best == 0)
                {
                    best = canli.Gen.MatyasFormulSkor;
                    continue;
                }

                if (canli.Gen.MatyasFormulSkor<best)
                {
                    best = canli.Gen.MatyasFormulSkor;
                }

            }
            return best;
        }
    }
}
