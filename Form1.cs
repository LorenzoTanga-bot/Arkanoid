using System;
using System.Drawing;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class Form1 : Form
    {
        private Microsoft.VisualBasic.PowerPacks.RectangleShape[] arrayMattoni = new Microsoft.VisualBasic.PowerPacks.RectangleShape[100];
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer contenitoreMattoni;


        private int j = 0;
        private double moviumentoX;
        private double movimentoY = 5;
        private Rectangle cursore;
        private Rectangle[] arrayRettangoli = new Rectangle[100];
        private int punteggio = 0;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            contenitoreMattoni = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();

            Cursor.Hide();

            FormBorderStyle = FormBorderStyle.None;
            TopMost = true;
            Bounds = Screen.PrimaryScreen.Bounds;

            panel1.Size = ClientSize;

            informazioni.Left = (panel1.Width / 2) - (informazioni.Width / 2);
            informazioni.Top = (panel1.Height / 2) - (informazioni.Height / 2);

            racchetta.Top = panel1.Bottom - (panel1.Bottom / 10);

            Ball.Top = panel1.Height / 2;
            Ball.Left = panel1.Width / 2;

            lblPunteggio.Location = new Point(15, 15);

            cursore = new Rectangle(Ball.Location.X, Ball.Location.Y, Ball.Width, Ball.Height);

            for(int riga = panel1.Height / 30; riga < panel1.Height / 3; riga += panel1.Height / 20)
            {
                for (double colonna = panel1.Width / 100; colonna < panel1.Width; colonna += panel1.Width / 9)
                {
                    int larghezzaMattone = Convert.ToInt32(panel1.Width / 10);
                    int fineRiga = Convert.ToInt32(colonna) + panel1.Width / 10;
                    if (fineRiga < panel1.Width)
                    {
                        arrayMattoni[j] = new Microsoft.VisualBasic.PowerPacks.RectangleShape(Convert.ToInt32(colonna), 70 + riga, larghezzaMattone, 30);
                        arrayMattoni[j].FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
                        arrayMattoni[j].FillColor = Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
                    }
                    j++;
                }
            }

            contenitoreMattoni.Location = new System.Drawing.Point(0, 0);
            contenitoreMattoni.Margin = new System.Windows.Forms.Padding(0);
            contenitoreMattoni.Name = "contenitoreMattoni";

            for (int k = j; k > 0; k--)
            {
                contenitoreMattoni.Shapes.Add(arrayMattoni[k - 1]);  
            }

            contenitoreMattoni.Size = new System.Drawing.Size(663, 329);
            contenitoreMattoni.TabIndex = 13;
            contenitoreMattoni.TabStop = false;

            panel1.Controls.Add(contenitoreMattoni);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.Space)
            {
                moviumentoX = _random.Next(1, 11);
                movimentoY = 5;
                Ball.Top = panel1.Height / 2;
                Ball.Left = panel1.Width / 2;
                punteggio = 0;
                lblPunteggio.Text = "0";
                timer1.Enabled = true;
                informazioni.Visible = false;
                lblPunteggio.Visible = true;
                for (int k = j; k > 0; k--)
                {
                    contenitoreMattoni.Shapes.Add(arrayMattoni[k - 1]);
                    arrayMattoni[k - 1].Visible = true;
                }
                panel1.Controls.Add(contenitoreMattoni);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int i = 0;

            for (int riga = panel1.Height / 30; riga < panel1.Height / 3; riga += panel1.Height / 20) 
            {

                for (double colonna = panel1.Width / 100; colonna < panel1.Width; colonna += panel1.Width / 9) 
                {

                    int larghezzaMattone = Convert.ToInt32(panel1.Width / 10);
                    int fineRiga = Convert.ToInt32(colonna) + panel1.Width / 10;

                    if (fineRiga < panel1.Width)
                    {
                        arrayRettangoli[i] = new Rectangle(Convert.ToInt32(colonna), 70 + riga, larghezzaMattone, 30);
                    }
                    i++;
                }
            }
            Graphics objGraph = e.Graphics;
            Pen p = new Pen(Brushes.Green);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            racchetta.Left = Cursor.Position.X - (racchetta.Width / 2);
            cursore.Location = Ball.Location;

            Ball.Left += Convert.ToInt32(moviumentoX);
            Ball.Top += Convert.ToInt32(movimentoY);

            if (Ball.Bottom >= racchetta.Top && Ball.Bottom <= racchetta.Bottom && Ball.Left >= racchetta.Left && Ball.Right <= racchetta.Right)
            {
                movimentoY += 0.5;
                moviumentoX += 0.5;
                movimentoY = -movimentoY;
                lblPunteggio.Text = punteggio.ToString();
            }

            if (Ball.Left <= panel1.Left)
            {
                moviumentoX = -moviumentoX;
            }

            if (Ball.Right >= panel1.Right)
            {
                moviumentoX = -moviumentoX;
            }

            if (Ball.Top <= panel1.Top)
            {
                movimentoY = -movimentoY;
            }

            if (Ball.Bottom >= panel1.Bottom)
            {
                timer1.Enabled = false;
                informazioni.Text += "\n Score " + punteggio;
                informazioni.Visible = true;
                lblPunteggio.Visible = false;
            }
            for (int l = j; l > 0; l--)
            {
                if (arrayMattoni[l - 1].Visible == true && arrayRettangoli[l - 1].IntersectsWith(cursore))
                {
                    arrayMattoni[l - 1].Visible = false;
                    punteggio += 1;
                    movimentoY = -movimentoY;
                    lblPunteggio.Text = punteggio.ToString();
                }
            }
            if(punteggio == 54)
            {
                timer1.Enabled = false;
                informazioni.Text += "\n Score " + punteggio;
                informazioni.Visible = true;
                lblPunteggio.Visible = false;
            }
        }
    }
}
