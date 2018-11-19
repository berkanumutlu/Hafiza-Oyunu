using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Hafıza_Oyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            olustur();
            timer1.Start();
        }

        Random rnd = new Random();
        int[] kontrol = new int[40];
        PictureBox[,] pctrdizi = new PictureBox[5,8];
        int oyuncu = 0;
        int dogru1 = 0;
        int dogru2 = 0;

        public void olustur()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox pctr = new PictureBox();
                    pctr.Width = 100;
                    pctr.Top = 50 + (i * 100) ;
                    pctr.Left = 400 + (j * 125);
                    pctrdizi[i, j] = pctr;

                    pctr.SizeMode = PictureBoxSizeMode.StretchImage;
                    pctr.Click += new EventHandler(karşılaştırma);
                   
                    int rast = rnd.Next(0, 40);
                    
                    if (kontrol[rast] == 1)
                    {
                        j--;
                    }
                    else
                    {
                        pctrdizi[i, j].Image = imageList1.Images[rast];
                        kontrol[rast] += 1;
                        pctrdizi[i, j].Tag = rast;
                        imageList1.Images[rast].Tag = rast;
                        this.Controls.Add(pctr);
                    }
                }
            }

            oyuncu = 1;
            label2.Text = oyuncu + ". oyuncu";
        }

        int sure = 5;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sure == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        pctrdizi[i, j].Image = imageList1.Images[40];
                    }
                }

                timer1.Stop();
                sure = 5;
                label8.Text = sure.ToString();
            }
            else
            {
                sure -= 1;
                label8.Text = sure.ToString();
            }
        }

        int sayac = 0;
        int[] pc = new int[2];
        
        public void karşılaştırma(object sender, EventArgs e)
        {
            PictureBox picture = (PictureBox)sender;

            if (sayac < 2)
            {
                int indeks = (int)picture.Tag;
                picture.Image = imageList1.Images[indeks];
                pc[sayac] = indeks;
                sayac++;
                timer2.Start();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (sure == 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if(pctrdizi[i,j].Tag != "dogru")
                        {
                            pctrdizi[i,j].Image = imageList1.Images[40];
                        }
                    }
                }
                sure = 5;
                label8.Text = sure.ToString();
                sayac = 0;
                timer2.Stop();

                if (oyuncu == 1)
                {
                    oyuncu = 2;
                    label2.Text = oyuncu + ". oyuncu";
                }
                else
                {
                    oyuncu = 1;
                    label2.Text = oyuncu + ". oyuncu";
                }
            }
            else
            {
                sure--;
                label8.Text = sure.ToString();
            }

            if (pc[0] == pc[1] + 20 || pc[0] + 20 == pc[1])
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (pctrdizi[i, j].Tag.ToString() == pc[0].ToString() || pctrdizi[i, j].Tag.ToString() == pc[1].ToString())
                        {
                            pctrdizi[i, j].Image = imageList1.Images[41];
                            pctrdizi[i, j].Tag = "dogru";
                        }
                    }
                }
                sure = 5;
                label8.Text = sure.ToString();
                sayac = 0;
                timer2.Stop();
                if (oyuncu == 1)
                {
                    dogru1 += 1;
                    label5.Text =dogru1.ToString();
                }
                else
                {
                    dogru2 += 1;
                    label6.Text = dogru2.ToString();
                }
            }
            else
            {
                if (sayac == 2)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (pctrdizi[i, j].Tag != "dogru")
                            {
                                pctrdizi[i, j].Image = imageList1.Images[40];
                            }
                        }
                    }

                    sure = 5;
                    label8.Text = sure.ToString();
                    sayac = 0;
                    timer2.Stop();

                    if (oyuncu == 1)
                    {
                        oyuncu = 2;
                        label2.Text = oyuncu + ". oyuncu";

                    }
                    else
                    {
                        oyuncu = 1;
                        label2.Text = oyuncu + ". oyuncu";
                    }
                }
            }

            if (dogru1 == 11)
            {
                MessageBox.Show("1. Oyuncu Kazandı!");
            }

            if (dogru2 == 11)
            {
                MessageBox.Show("2. Oyuncu Kazandı!");
            }
            if (dogru1 == 10 && dogru2 == 10)
            {
                MessageBox.Show("Berabere!!!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}