using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml.Schema;

namespace GenetikAlgoritma
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.chart1.Series.Clear();
 
            this.chart1.Titles.Add("Total Income");
 
            Series series = this.chart1.Series.Add("Total Income");
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY(1, 100);
            series.Points.AddXY(2, 300);
            series.Points.AddXY(3, 800);
            series.Points.AddXY("December", 200);
            series.Points.AddXY("January", 600);
            series.Points.AddXY("February", 400);
        }

       

        public List<int> TurnuvaSecimi(List<int> list)
        {
            Random rnd= new Random();
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            List<Canli> liste = new Canli().Olustur((int)numericUpDown1.Value);

            

            this.chart1.Series.Clear();
            Series series = this.chart1.Series.Add("Sonuclar");
            series.ChartType = SeriesChartType.Spline;

           
            for (int j = 0; j < (int)numericUpDown4.Value; j++)
            { 
                Turnuva t = new Turnuva(liste);
                liste = t.Olustur();
                liste = t.Caprazla(liste,(double)numericUpDown2.Value);
                liste = t.Mutasyon(liste,(double)numericUpDown3.Value);
                series.Points.AddXY(j,t.Formul(liste)*1000);
            }
        }


        //double hesapla(long x1, long x2)
        //{
        //    double pay = Math.Cos(Math.Sin(Math.Abs((x1 * x1)- (x2 * x2)))) - 0.5;
        //    double payda = Math.Pow((1 + 0.001 * ((x1 * x1) + (x2 * x2))), 2);
        //    return 0.5 + pay / payda;
        //}


    }
}
