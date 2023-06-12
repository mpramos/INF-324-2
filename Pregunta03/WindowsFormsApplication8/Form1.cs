using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication8
{
    public partial class Form1 : Form
    {
        Bitmap bmp;
        int x, y;
        int mR = 0, mG = 0, mB = 0;
        int cR, cG, cB;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Todos|*.*|Archivos JPEG|*.jpg|Archivos GIF|*.gif";
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            bmp = new Bitmap(openFileDialog1.FileName);
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Color c = new Color();
            c = bmp.GetPixel(10, 10);
            textBox1.Text = c.R.ToString();
            textBox2.Text = c.G.ToString();
            textBox3.Text = c.B.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    bmp2.SetPixel(i, j, Color.FromArgb(c.R, 0, 0));
                }
            pictureBox1.Image = bmp2;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    bmp2.SetPixel(i, j, Color.FromArgb(0, c.G, 0));
                }
            pictureBox1.Image = bmp2;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    bmp2.SetPixel(i, j, Color.FromArgb(0, 0, c.B));
                }
            pictureBox1.Image = bmp2;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            x = e.X;
            y = e.Y;
            textBox4.Text = x.ToString();
            textBox5.Text = y.ToString();
            Color c = new Color();
            mR = 0; mG = 0; mB = 0;
            for (int i = x; i < x + 10; i++)
                for (int j = y; j < y + 10; j++)
                {
                    c = bmp.GetPixel(i, j);
                    mR = mR + c.R;
                    mG = mG + c.G;
                    mB = mB + c.B;
                }
            mR = mR / 100;
            mG = mG / 100;
            mB = mB / 100;
            textBox1.Text = mR.ToString();
            textBox2.Text = mG.ToString();
            textBox3.Text = mB.ToString();
            cR = mR;
            cG = mG;
            cB = mB;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();

            con.ConnectionString = "server=(local);user=usuario1;pwd=123456;database=academico";
            cmd.Connection = con;
            cmd.CommandText = "insert into colores values('" +
                textBox4.Text + "'," + mR.ToString() + "," + mG.ToString()
                + "," + mB.ToString() + ",'" + textBox5.Text + "')";
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Color cleido = new Color();
            cleido = bmp.GetPixel(x, y);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    c = bmp.GetPixel(i, j);
                    if (((cleido.R - 10 <= c.R) && (c.R <= cleido.R + 10)) &&
                        ((cleido.G - 10 <= c.G) && (c.G <= cleido.G + 10)) &&
                        ((cleido.B - 10 <= c.B) && (c.B <= cleido.B + 10)))
                    {
                        bmp2.SetPixel(i, j, Color.Fuchsia);
                    }
                    else {
                        bmp2.SetPixel(i, j, Color.FromArgb(c.R, c.G, c.B));
                    }
            
                }
            pictureBox1.Image = bmp2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            Bitmap bmp = new Bitmap(pictureBox1.Image);
            Bitmap bmp2 = new Bitmap(bmp.Width, bmp.Height);
            Color c = new Color();
            int clR, clG, clB;
            con.ConnectionString = "server=(local);user=usuario1;pwd=123456;database=academico";
            cmd.Connection = con;
            cmd.CommandText = "select * from colores";
            con.Open();
            dr = cmd.ExecuteReader();

            int mRn= 0, mGn = 0, mBn = 0;

            while (dr.Read())
            {
                //cR = dr.GetInt32(1);
                //cG = dr.GetInt32(2);
                //cB = dr.GetInt32(3);

                mR = dr.GetInt32(1);
                mG = dr.GetInt32(2);
                mB = dr.GetInt32(3);

                bmp2 = new Bitmap(bmp.Width, bmp.Height);

                for (int i = 0; i < bmp.Width - 10; i = i + 10)
                    for (int j = 0; j < bmp.Height - 10; j = j + 10)
                    {
                        //sacando el 10x10
                        for (int i2 = i; i2 < i + 10; i2++)
                            for (int j2 = j; j2 < j + 10; j2++)
                            {
                                c = bmp.GetPixel(i2, j2);
                                mRn = mRn + c.R;
                                mGn = mGn + c.G;
                                mBn = mBn + c.B;
                            }
                        mRn = mRn / 100;
                        mGn = mGn / 100;
                        mBn = mBn / 100;

                        if (((mR - 10 <= mRn) && (mRn <= mR + 10)) &&
                            ((mG - 10 <= mGn) && (mGn <= mG + 10)) &&
                            ((mB - 10 <= mBn) && (mBn <= mB + 10)) &&
                            ( (8-5 <= mR && mR <= 8+5) &&
                              (42-5 <= mG && mR <= 42+ 5) &&
                              (62-5 <= mB && mB <= 62+ 5)))
                        {
                            for (int i2 = i; i2 < i + 10; i2++)
                                for (int j2 = j; j2 < j + 10; j2++)
                                {
                                    bmp2.SetPixel(i2, j2, Color.Blue);
                                }
                        }
                        else
                        {
                            if (((mR - 10 <= mRn) && (mRn <= mR + 10)) &&
                                    ((mG - 10 <= mGn) && (mGn <= mG + 10)) &&
                                    ((mB - 10 <= mBn) && (mBn <= mB + 10)) &&
                                    ((51 - 8 <= mR && mR <= 51 + 8) &&
                                      (60 - 8 <= mG && mR <= 60 + 8) &&
                                      (47 - 8 <= mB && mB <= 47 + 8)))
                            {
                                for (int i2 = i; i2 < i + 10; i2++)
                                    for (int j2 = j; j2 < j + 10; j2++)
                                    {
                                        bmp2.SetPixel(i2, j2, Color.Green);
                                    }
                            }
                            else {
                                if (((mR - 10 <= mRn) && (mRn <= mR + 10)) &&
                                   ((mG - 10 <= mGn) && (mGn <= mG + 10)) &&
                                ((mB - 10 <= mBn) && (mBn <= mB + 10)))
                                {
                                    for (int i2 = i; i2 < i + 10; i2++)
                                        for (int j2 = j; j2 < j + 10; j2++)
                                        {
                                            bmp2.SetPixel(i2, j2, Color.Brown);
                                        }
                                }
                                else
                                {
                                    for (int i2 = i; i2 < i + 10; i2++)
                                        for (int j2 = j; j2 < j + 10; j2++)
                                        {
                                            c = bmp.GetPixel(i2, j2);
                                            bmp2.SetPixel(i2, j2, Color.FromArgb(c.R, c.G, c.B));
                                        }
                                }
                            }
                        }
                    }
                bmp = bmp2;
                
            }
            pictureBox2.Image = bmp2;
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
