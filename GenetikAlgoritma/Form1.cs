using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
            series.BorderWidth = 10;
            
            series.ChartType = SeriesChartType.Spline;
            series.Points.AddXY(1, 100);
            series.Points.AddXY(2, 300);
            series.Points.AddXY(3, 800);
            series.Points.AddXY(4, 800);
            series.Points.AddXY(5, 800);
            series.Points.AddXY(9, 800);

        }

       

        public List<int> TurnuvaSecimi(List<int> list)
        {
            Random rnd= new Random(Guid.NewGuid().GetHashCode());
            return null;
        }
        Random rndColor = new Random(Guid.NewGuid().GetHashCode());

        public void drawPoint(int x, int y,Image img)
        {
            Graphics g = Graphics.FromImage(img);
            SolidBrush brush = new SolidBrush(Color.FromArgb(rndColor.Next(0,255),rndColor.Next(0,255),rndColor.Next(0,255)));
            Point dPoint = new Point(x, (img.Height - y));
            dPoint.X = dPoint.X - 2;
            dPoint.Y = dPoint.Y - 2;

            g.FillCircle(brush,dPoint.X, dPoint.Y, 8);
            g.DrawCircle(new Pen(brush),dPoint.X, dPoint.Y, 8);
            g.Dispose();
        }

        private Series GenSeries()
        {
            flowLayoutPanel1.Controls.Clear();
            label11.Text = "Toplam:0";
            pictureBox1.Image = Properties.Resources.matyas;
            this.chart1.Series.Clear();
            Series series = this.chart1.Series.Add("Sonuclar");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.Color = Color.Black;
            return series;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Series series = GenSeries();
            
            List<Canli> elitizm=new List<Canli>();
            GenetikDriver GenDrv = new GenetikDriver((int) numericUpDown1.Value);
            GenDrv.elitPop = (int) numericUpDown5.Value;

            chart1.SuspendLayout();
            for (int j = 0; j < (int)numericUpDown4.Value; j++)
            { 
                
                GenDrv.Elitizm();
                GenDrv.TurnuvaCiftiOlustur();
                GenDrv.Caprazla((double)numericUpDown2.Value);
                GenDrv.Mutasyon((double)numericUpDown3.Value);
                

                //GenDrv.AddRange(elitizm);

                ElitizmFlowLayoutEkle(GenDrv.BestCanli());
                TabloRender(GenDrv.populasyonList);
                series.Points.AddXY(j, GenDrv.BestCanli().Gen.MatyasFormulSkor * 1000);


               // elitizm=GenDrv.Elitizm((int)numericUpDown5.Value);

                label8.Text = GenDrv.BestCanli().Gen.x1.ToString();
                label9.Text = GenDrv.BestCanli().Gen.x2.ToString();
                bekle((int)numericUpDown6.Value);
            }
            chart1.ResumeLayout();
        }
        
        public void TabloRender(List<Canli> c)
        {
            Image img = Properties.Resources.matyas;
           
            foreach (Canli canli in c)
            {
                int x = (int)((double) ((canli.Gen.x1 + 10) / 20) * (img.Width - 50));
                int y = (int)((double) ((canli.Gen.x2 + 10) / 20) * (img.Height - 60));
                drawPoint(x+25,y+30,img);
            }
            pictureBox1.Image = img;

        }
        public bool ElitizmFlowLayoutEkle(Canli c)
        {
            foreach (var elitizm in flowLayoutPanel1.Controls.OfType<ElitizmComponent>())
                if (c.Gen.MatyasFormulSkor == elitizm.Canli.Gen.MatyasFormulSkor) 
                    return false;
            label11.Text = "Toplam:"+ (flowLayoutPanel1.Controls.Count + 1);
            flowLayoutPanel1.Controls.Add(new ElitizmComponent(c,flowLayoutPanel1.Controls.Count+1));
            return true;
        }
        Stopwatch BekleWatch;
        public void bekle(int ms)
        {
            if (ms==0)
                return;
            BekleWatch= Stopwatch.StartNew();
            while (ms>BekleWatch.ElapsedMilliseconds)
            {
                Application.DoEvents();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            
            
            List<Canli> liste = new Canli().Olustur((int)numericUpDown1.Value);
            GenetikDriver t= new GenetikDriver(liste);
            liste = t.TurnuvaCiftiOlustur();
            liste = t.Caprazla((double)numericUpDown2.Value);
            liste = t.Mutasyon((double)numericUpDown3.Value);
            TabloRender(liste);
          
           
        }
    }
    public static class GraphicsExtensions
    {
        public static void DrawCircle(this Graphics g, Pen pen,
            float centerX, float centerY, float radius)
        {
            g.DrawEllipse(pen, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }

        public static void FillCircle(this Graphics g, Brush brush,
            float centerX, float centerY, float radius)
        {
            g.FillEllipse(brush, centerX - radius, centerY - radius,
                radius + radius, radius + radius);
        }
    }
   
}
