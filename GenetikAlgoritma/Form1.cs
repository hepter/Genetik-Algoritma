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

        private bool isRunning = false;
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        public void TabloRender(List<Canli> c,int cap=10,Color? color=null,Image img=null)
        {
            bool check = img == null;

            if (check)
                 img = Properties.Resources.matyas;
            
            foreach (Canli canli in c)
            {
                int x = (int)((double) ((canli.Gen.x1 + 10) / 20) * (img.Width - 50));
                int y = (int)((double) ((canli.Gen.x2 + 10) / 20) * (img.Height - 60));
                drawPoint(x+25,y+30,img,cap,color);
            }
            if (check)
                pictureBox1.Image = img;

        }
        Random rndColor = new Random(Guid.NewGuid().GetHashCode());
        public void drawPoint(int x, int y, Image img, int radius = 10, Color? color=null)
        {
            Graphics g = Graphics.FromImage(img);
            SolidBrush brush;
            if (color.HasValue)
            {
                 brush = new SolidBrush(color.Value);
            }
            else
            {
                brush = new SolidBrush(Color.FromArgb(rndColor.Next(0,255),rndColor.Next(0,255),rndColor.Next(0,255)));
            }
          
            Point dPoint = new Point(x, (img.Height - y));
            dPoint.X = dPoint.X - 2;
            dPoint.Y = dPoint.Y - 2;

           
            g.FillCircle(brush,dPoint.X, dPoint.Y, radius+4);
            g.FillCircle(new SolidBrush(Color.White),dPoint.X, dPoint.Y, radius);
            
            g.FillCircle(new SolidBrush(Color.Black),dPoint.X, dPoint.Y, 3);
            g.DrawCircle(new Pen(brush),dPoint.X, dPoint.Y, radius);
            g.Dispose();
        }

        private bool ToggleKontrol()
        {
            if (isRunning)
            {
                isRunning = false;
                button1.Text = "HESAPLA";
            }
            else
            {
                button1.Text = "Durdur";
                isRunning = true;
            }

            return isRunning;
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
            if(!ToggleKontrol()) return;
            
            Series series = GenSeries();

            int popSayi = (int)numericUpDown1.Value;
            int elitPop = (int)numericUpDown5.Value;
            int iterasyon = (int) numericUpDown4.Value;
            double carazlamaOran = (double) numericUpDown2.Value / 100;
            double mutasyonOran = (double) numericUpDown3.Value / 100;
            int ms = (int)numericUpDown6.Value;
            
            GenetikDriver GenDrv = new GenetikDriver(popSayi);
            GenDrv.elitPop = elitPop;

            
            
            chart1.SuspendLayout();
            for (int j = 0; j <iterasyon; j++)
            { 
                
                GenDrv.Elitizm();
                GenDrv.TurnuvaCiftiOlustur();
                GenDrv.Caprazla(carazlamaOran);
                GenDrv.Mutasyon(mutasyonOran);


                ElitizmFlowLayoutEkle(GenDrv.BestCanli());
                //TabloRender(GenDrv.populasyonList);
                Image img = Properties.Resources.matyas;
                TabloRender(GenDrv.canliList,10,Color.Black,img);
                TabloRender(GenDrv.elitList,10,Color.Red,img);
                pictureBox1.Image = img;
                

                series.Points.AddXY(j, GenDrv.BestCanli().Gen.MatyasFormulSkor * 1000);
                label8.Text = GenDrv.BestCanli().Gen.x1.ToString();
                label9.Text = GenDrv.BestCanli().Gen.x2.ToString();

                bekle(ms);

                if (!isRunning) break;
                if(j==iterasyon-1) ToggleKontrol();
            }
            chart1.ResumeLayout();
        }
        
       
        public bool ElitizmFlowLayoutEkle(Canli c)
        {
            foreach (var elitizm in flowLayoutPanel1.Controls.OfType<ElitizmComponent>())
                if (c.Gen.MatyasFormulSkor == elitizm.Canli.Gen.MatyasFormulSkor) 
                    return false;

            label11.Text = "Toplam:"+ (flowLayoutPanel1.Controls.Count + 1);
            var comp = new ElitizmComponent(c, flowLayoutPanel1.Controls.Count + 1);
            
            comp.pictureBox2.Click += (s, arg) =>
            {
                var canli = ((s as Control).Parent.Parent.Parent as ElitizmComponent).Canli ;
                var list = new List<Canli>();
                list.Add(canli);
                TabloRender(list,20);
            };
            flowLayoutPanel1.Controls.Add(comp);
            return true;
        }
        Stopwatch BekleWatch;
        public void bekle(int ms)
        {
            if (ms==0) return;
            BekleWatch= Stopwatch.StartNew();
            while (ms>BekleWatch.ElapsedMilliseconds)
                Application.DoEvents();
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
