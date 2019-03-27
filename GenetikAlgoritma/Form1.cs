using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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

        public void drawPoint(int x, int y)
        {
            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);

            SolidBrush brush = new SolidBrush(Color.FromArgb(rndColor.Next(0,255),rndColor.Next(0,255),rndColor.Next(0,255)));
           
            Point dPoint = new Point(x, (pictureBox1.Height - y));
            dPoint.X = dPoint.X - 2;
            dPoint.Y = dPoint.Y - 2;

            g.FillCircle(brush,dPoint.X, dPoint.Y, 3);
            g.DrawCircle(new Pen(brush),dPoint.X, dPoint.Y, 3);
            //Rectangle rect = new Rectangle(dPoint, new Size(4, 4));
            //g.FillRectangle(brush, rect);
            g.Dispose();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            label11.Text = "Toplam:0";
            pictureBox1.Image = Properties.Resources.matyas;

            List<Canli> liste = new Canli().Olustur((int)numericUpDown1.Value);
            List<Canli> elitizm=new List<Canli>();
           
            this.chart1.Series.Clear();
            Series series = this.chart1.Series.Add("Sonuclar");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.Color = Color.Black;
            chart1.SuspendLayout();
            for (int j = 0; j < (int)numericUpDown4.Value; j++)
            { 
                
                Turnuva t = new Turnuva(liste);
                liste = t.Olustur();
                liste = t.Caprazla(liste,(double)numericUpDown2.Value);
                liste = t.Mutasyon(liste,(double)numericUpDown3.Value);
                liste.AddRange(elitizm);
                Canli canli = t.BestCanli(liste);
                
                if (ElitizmListeyeEkle(canli))
                {
                   Render(liste);
                } 
                series.Points.AddXY(j, canli.Gen.MatyasFormulSkor * 10);

                elitizm=liste.OrderBy(a=>a.Gen.MatyasFormulSkor).Take((int)numericUpDown5.Value).ToList();
                liste=liste.OrderBy(a=>a.Gen.MatyasFormulSkor).Reverse().Take(liste.Count()-(int)numericUpDown5.Value).ToList();
                label8.Text = canli.Gen.x1.ToString();
                label9.Text = canli.Gen.x2.ToString();
                bekle((int)numericUpDown6.Value);
            }
            chart1.ResumeLayout();
        }
        Stopwatch w;
        public void Render(List<Canli> c)
        {
           
            pictureBox1.SuspendLayout();
            foreach (Canli canli in c)
            {
                int x = (int)((double) ((canli.Gen.x1 + 10) / 20) * (pictureBox1.Width - 50));
                int y = (int)((double) ((canli.Gen.x2 + 10) / 20) * (pictureBox1.Height - 60));
                drawPoint(x+25,y+30);
            }
            pictureBox1.ResumeLayout();

        }
        public bool ElitizmListeyeEkle(Canli c)
        {
            foreach (var elitizm in flowLayoutPanel1.Controls.OfType<ElitizmComponent>())
                if (c.Gen.MatyasFormulSkor == elitizm.Canli.Gen.MatyasFormulSkor) 
                    return false;
            label11.Text = "Toplam:"+ (flowLayoutPanel1.Controls.Count + 1);
            flowLayoutPanel1.Controls.Add(new ElitizmComponent(c,flowLayoutPanel1.Controls.Count+1));
            return true;
        }
        public void bekle(int ms)
        {
            if (ms==0)
                return;
            w= Stopwatch.StartNew();
            while (ms>w.ElapsedMilliseconds)
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            drawPoint(25,30);
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
