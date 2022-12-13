using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace bomboane
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int[] A, B;//bomboanele
        int n;//nr de bomboane
        PictureBox[] P;//imagini pt nr bomboane
        int max=0;//max de bomboane

        class mutare {
            public int i, j, b; 
            public mutare() { }
            public mutare(int a, int b, int c)
            {
                i = a; j = b; this.b = c;
            }
        };
        List<mutare> M;

        int poz = 0;

        private void deseneaza()
        {
            panel1.Controls.Clear();
            P = new PictureBox[n];//creez vector de picturebox
            for (int i = 0; i < n; i++)
            {
                P[i] = new PictureBox();//creez picture box
                P[i].BackColor = Color.Red;//colorat rosu
                P[i].Width = panel1.Width / n;//latime picturebox
                P[i].Height = A[i] * panel1.Height / max; //inaltimea
                P[i].Left = i * P[i].Width;//aliniere pe x
                P[i].Top = panel1.Height - P[i].Height;//y invers
                P[i].BorderStyle = BorderStyle.FixedSingle;
                panel1.Controls.Add(P[i]);//le adaug in panel1              
            }
            Graphics G = panel1.CreateGraphics();
            G.Clear(Color.White);
            Font f = new Font("Arial", 30);
            Brush B = new SolidBrush(Color.Black);
            for (int i = 0; i < n; i++)
            {
                //scriu pe picturebox
               

                G.DrawString(A[i].ToString(), f, B, i * P[i].Width + 5,
                    panel1.Height - A[i] * (panel1.Height - 50) / max - 90);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Filter = "IN|*.in";
            openFileDialog1.FileName = "";
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                StreamReader fin = new StreamReader(openFileDialog1.FileName);
                n = int.Parse(fin.ReadLine());
                A = new int[n];
                B = new int[n];
                string[] S = fin.ReadLine().Split();
                for (int i = 0; i < S.Length; i++)
                {
                    A[i] = int.Parse(S[i]);
                    B[i] = A[i];
                    max = Math.Max(max, A[i]);
                }
                fin.Close();
                deseneaza();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (n == 0)
            {
                MessageBox.Show("ïncarcati fisier");
            }
            else
            {

            
            int s = 0;
            for (int i = 0; i < n; i++)
            {
                s += A[i];
            }

            if (s % n != 0)
                MessageBox.Show("imposibnil" );

            else
            {
                    M = new List<mutare>();
                    int m = s / n;
                    int maxx = 0, min = 1000, imax, imin;
                    while (maxx != min)
                    {
                        maxx = B[0]; min = B[0]; imax = 0; imin = 0;
                        for (int i = 1; i < n; i++)
                        {
                            if (B[i] > maxx)
                            {
                                maxx = B[i];
                                imax = i;
                            }
                            if (B[i] < min)
                            {
                                min = B[i];
                                imin = i;
                            }
                        
                        }
                        if (maxx != min)
                        {
                            B[imax] = B[imax] - (m - min);
                            B[imin] = m;
                            M.Add(new mutare(imax, imin, m - min));
                        }

                    }
                    //MessageBox.Show(M.Count.ToString());
                    timer1.Start();
            
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2_Click(sender, e);
            if (n == 0)
                MessageBox.Show("incarcati fis");
            else
                timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (M is null)
                MessageBox.Show("A[asati rez");
            else
            {
                if (poz < M.Count)
                {
                    A[M[poz].i] = A[M[poz].i] - M[poz].b;
                    A[M[poz].j] = A[M[poz].j] + M[poz].b;
                    deseneaza();
                    poz++;
                }
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (poz < M.Count)
            {
                A[M[poz].i] = A[M[poz].i] - M[poz].b;
                A[M[poz].j] = A[M[poz].j] + M[poz].b;
                deseneaza();
                poz++;
            }
        }
    }
}
