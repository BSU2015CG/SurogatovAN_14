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

namespace CG_Lab2
{
    public partial class Form1 : Form
    {
        Bitmap pictureBitmap;
        int[] redHistogram;
        int[] greenHistogram;
        int[] blueHistogram;

        public Form1()
        {
            redHistogram   = new int[256];
            greenHistogram = new int[256];
            blueHistogram  = new int[256];

            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Stream myStream = null;
                try
                {
                    if ((myStream = ofd.OpenFile()) != null)
                    {
                        Bitmap tmpBitmap = new Bitmap(myStream);
                        pictureBitmap = new Bitmap(tmpBitmap, picture.Width, picture.Height);

                        picture.Image = pictureBitmap;

                        PictureAnalyze();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        void PictureAnalyze()
        {
            for (int i = 0; i < 256; ++i)
            {
                redHistogram[i] = 0;
                greenHistogram[i] = 0;
                blueHistogram[i] = 0;
            }

            pbRed.CreateGraphics().Clear(SystemColors.Control);
            pbGreen.CreateGraphics().Clear(SystemColors.Control);
            pbBlue.CreateGraphics().Clear(SystemColors.Control);

            Int64 red = 0;
            Int64 green = 0;
            Int64 blue = 0;

            for (int i = 0; i < pictureBitmap.Width; ++i)
                for (int j = 0; j < pictureBitmap.Height; ++j)
                {
                    Color rgb = pictureBitmap.GetPixel(i, j);
                    ++redHistogram[rgb.R];
                    ++greenHistogram[rgb.G];
                    ++blueHistogram[rgb.B];
                    red += rgb.R;
                    green += rgb.G;
                    blue += rgb.B;
                }

            averageRed.Text = "Average value: " + (double)red / (pictureBitmap.Width * pictureBitmap.Height);
            averageGreen.Text = "Average value: " + (double)green / (pictureBitmap.Width * pictureBitmap.Height);
            averageBlue.Text = "Average value: " + (double)blue / (pictureBitmap.Width * pictureBitmap.Height);
        }

        void DrawHistogram(int[] array, PictureBox pb, Color color)
        {
            int max = Int32.MinValue;
            for (int i = 0; i < array.Length; ++i)
                if (array[i] > max)
                    max = array[i];

            Graphics g = pb.CreateGraphics();
            Pen pen = new Pen(color);

            if (max != 0)
            for (int i = 0; i < pb.Width; ++i)
                g.DrawLine(pen, new Point(i, pb.Height),
                                new Point(i, (int)(pb.Height - (double)pb.Height * array[(int)((double)i * 256 / pb.Width)] / max)));
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawHistogram(redHistogram, pbRed, Color.Red);
            DrawHistogram(greenHistogram, pbGreen, Color.Green);
            DrawHistogram(blueHistogram, pbBlue, Color.Blue);
        }
    }
}
