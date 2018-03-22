using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    public delegate void GlobalMouseClickEventHander(object sender, MouseEventArgs e);

    public partial class Form1 : Form
    {
        [Category("Action")]
        [Description("Fires when any control on the form is clicked.")]
        public event GlobalMouseClickEventHander GlobalMouseClick;
        //System.Windows.Forms.PaintEventArgs pe;
        Point point = new Point(0, 0);
        //Point[] bpoint = new Point[361];
        //Point[] wpoint = new Point[361];
        int[,] mid = new int[24, 24];
        int game = 1;
        string[] winner = new string[] { "black win", "white win"};



        int wbflag = 1;
        int black = 0;
        int white = 0;

        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    mid[i, j] = 0;
                }
            }

            BindControlMouseClicks(this);


        }

        private void BindControlMouseClicks(Control con)
        {
            con.MouseClick += delegate (object sender, MouseEventArgs e)
            {
                TriggerMouseClicked(sender, e);
                
            };
            // bind to controls already added
            foreach (Control i in con.Controls)
            {
                BindControlMouseClicks(i);
                
            }
            // bind to controls added in the future
            con.ControlAdded += delegate (object sender, ControlEventArgs e)
            {
                BindControlMouseClicks(e.Control);
                
            };
        }

        private void TriggerMouseClicked(object sender, MouseEventArgs e)
        {
            if (GlobalMouseClick != null)
            {
                GlobalMouseClick(sender, e);
            }
            
            MouseEventArgs me = (MouseEventArgs)e;
            Point coordinates = me.Location;
            label1.Text = ("Coordinates are: " + coordinates);
            point = coordinates;

            if (point.X >= 50 && point.X <= 590 && point.Y >= 50 && point.Y <= 590 && game == 1)
            {
                if ((point.X - 50) % 30 < 15)
                {
                    point.X = point.X - (point.X - 50) % 30;
                }
                else
                {
                    point.X = point.X - (point.X - 50) % 30 + 30;
                }

                if ((point.Y - 50) % 30 < 15)
                {
                    point.Y = point.Y - (point.Y - 50) % 30;
                }
                else
                {
                    point.Y = point.Y - (point.Y - 50) % 30 + 30;
                }
                this.Refresh();
            }

        }


       

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g;
            g = e.Graphics;
            Pen bpen = new Pen(Color.Black);
            bpen.Width = 1;
            Pen wpen = new Pen(Color.White);
            wpen.Width = 1;
           
            

            if ((point.X >= 50) && (point.X <= 590) && (point.Y >= 50) && (point.Y <= 590) && (mid[(point.X - 50) / 30, (point.Y - 50) / 30] != 1)) {
                //g.DrawArc(bpen, x, y, width, height, startAngle, sweepAngle);
                //mid[(point.X-50)/30, (point.Y-50)/30] = 1;
                //point.X = point.X - 10;
                //point.Y = point.Y - 10;
                label1.Text = ("Coordinates are: " + point);
                if (wbflag % 2 == 1 && mid[(point.X - 50) / 30, (point.Y - 50) / 30] == 0 )
                {
                    //g.FillEllipse(new SolidBrush(Color.Black), x, y, width, height);
                    mid[(point.X - 50) / 30, (point.Y - 50) / 30] = 1;
                }
                else if(wbflag % 2 == 0 && mid[(point.X - 50) / 30, (point.Y - 50) / 30] == 0)
                {
                    //g.FillEllipse(new SolidBrush(Color.White), x, y, width, height);
                    mid[(point.X - 50) / 30, (point.Y - 50) / 30] = 2;
                }
                wbflag++;

                int win = 0;
                int bwin = 0;
                int wwin = 0;


                //黑白X+
                for (int i = 1; i < 3; i++)
                {
                    for (int j = (point.X - 50) / 30; j < (point.X - 50) / 30 + 5; j++)
                    {
                        if (mid[j, (point.Y - 50) / 30] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i-1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //黑白X-
                for (int i = 1; i < 3; i++)
                {
                    for (int j = (point.X - 50) / 30 ; j >= (((point.X - 50) / 30 - 5) > 0 ? ((point.X - 50) / 30 - 5) : 0); j--)
                    {
                        if (mid[j, (point.Y - 50) / 30] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //黑白Y+
                for (int i = 1; i < 3; i++)
                {
                    for (int j = (point.Y - 50) / 30; j < (point.Y - 50) / 30 + 5; j++)
                    {
                        if (mid[(point.X - 50) / 30, j] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }
                
                //黑白Y-
                for (int i = 1; i < 3; i++)
                {
                    for (int j = (point.Y - 50) / 30; j >= (((point.Y - 50) / 30 - 5) > 0 ? ((point.Y - 50) / 30 - 5) : 0); j--)
                    {
                        if (mid[(point.X - 50) / 30, j] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //X+Y+
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 0 ; j < 5; j++)
                    {
                        if (mid[(point.X - 50) / 30 + j, (point.Y - 50) / 30 + j] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //X-Y-
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (mid[((point.X - 50) / 30 - j) > 0 ? ((point.X - 50) / 30 - j) : 0, ((point.Y - 50) / 30 - j) > 0 ? ((point.Y - 50) / 30 - j) : 0] == i )
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }
                            if (((point.X - 50) / 30 - j) <= 0  && ((point.Y - 50) / 30 - j) <= 0 ) win =0;

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //X+Y-
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (mid[(point.X - 50) / 30 + j, ((point.Y - 50) / 30 - j) > 0 ? ((point.Y - 50) / 30 - j) : 0] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }

                //X-Y+
                for (int i = 1; i < 3; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (mid[((point.X - 50) / 30 - j) > 0 ? ((point.X - 50) / 30 - j) : 0, (point.Y - 50) / 30 + j] == i)
                        {
                            win++;
                            if (win == 5) { label1.Text = winner[i - 1]; game = 0; }

                        }
                        else
                        {
                            win = 0;

                        }

                    }
                    win = 0;
                }



            }

            
            int width = 20;
            int height = 20;
            int startAngle = 0;
            int sweepAngle = 360;

            if(wbflag % 2 == 1)
            {
                g.FillEllipse(new SolidBrush(Color.Black), 310, 5, width, height);
            }
            else
            {
                g.DrawArc(bpen, 310, 5, width, height, startAngle, sweepAngle);
                g.FillEllipse(new SolidBrush(Color.White), 310, 5, width, height);
            }

            for (int i = 0; i < 19; i++)
            {

                g.DrawLine(bpen, 50, i * 30 + 50, 590, i * 30 + 50);
                g.DrawLine(bpen, i * 30 + 50, 50, i * 30 + 50, 590);
                
                    
                

            }


           

            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if(mid[i,j] == 1)
                    {
                        g.FillEllipse(new SolidBrush(Color.Black), 30*i+40, 30*j+40, width, height);
                    }else if(mid[i,j] == 2)
                    {
                        g.DrawArc(bpen, 30*i+40, 30*j+40, width, height, startAngle, sweepAngle);
                        g.FillEllipse(new SolidBrush(Color.White), 30*i+40, 30*j+40, width, height);
                    }
                    
                    
                }
            }

            





        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
