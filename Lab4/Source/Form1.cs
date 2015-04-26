using System;
using System.Drawing;
using System.Windows.Forms;

namespace CG_Lab4
{
    public partial class Form1 : Form
    {
        private Bitmap originalImageBitmap;
        private StructuringElement structElement = new Square(0);
        private Morphology morphology = new Erosion();
        private ImageFilter filter = new Median();

        public Form1()
        {
            InitializeComponent();
        }

        public Bitmap GetBitmapFromMatrix(byte[,] matrix)
        {
            Bitmap ans = new Bitmap(matrix.GetLength(0), matrix.GetLength(1));
            for (int i = 0; i < matrix.GetLength(0); ++i)
                for (int j = 0; j < matrix.GetLength(1); ++j)
                    if (matrix[i, j] == 1)
                        ans.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    else
                        ans.SetPixel(i, j, Color.FromArgb(0, 0, 0));
            return ans;
        }

        public byte[,] GetBinary(Bitmap bmp)
        {
            Color c;
            byte[,] matrix = new byte[bmp.Width, bmp.Height];
            for (int i = 0; i < bmp.Width; ++i)
                for (int j = 0; j < bmp.Height; ++j)
                {
                    c = bmp.GetPixel(i, j);
                    if (.299 * c.R + .587 * c.G + .114 * c.B > 128)
                        matrix[i, j] = 1;
                    else 
                        matrix[i, j] = 0;
                }
            return matrix;
        }

        private void openButton_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(ofd.FileName);

                Bitmap tmpBitmap = new Bitmap(image);
                originalImageBitmap = new Bitmap(tmpBitmap, pbOriginalImage.Width, pbOriginalImage.Height);

                pbOriginalImage.Image = originalImageBitmap;
                processingBtn.Enabled = true;
            }
        }

        private void processingBtn_MouseClick(object sender, MouseEventArgs e)
        {
            if (rbMorphology.Checked)
            {
                var matrix = GetBinary(originalImageBitmap);
                var t = morphology.Process(matrix, structElement);
                pbNewImage.Image = GetBitmapFromMatrix(t);
            }
            else
            {
                pbNewImage.Image = filter.Process(originalImageBitmap);
            }
        }

        private void rbMorphology_CheckedChanged(object sender, EventArgs e)
        {
            if (rbErosion.Checked)
                morphology = new Erosion();
            if (rbDilation.Checked)
                morphology = new Dilation();
            if (rbOpening.Checked)
                morphology = new Opening();
            if (rbClosing.Checked)
                morphology = new Closing();
        }

        private void rbStructurElement_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSquare.Checked)
                structElement = new Square((int)nUDStructElSize.Value);
            if (rbCross.Checked)
                structElement = new Cross((int)nUDStructElSize.Value);
        }

        private void nUDStructElSize_ValueChanged(object sender, EventArgs e)
        {
            rbStructurElement_CheckedChanged(null, new EventArgs());
        }

        private void rbFilter_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMorphology.Checked)
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
            }

            if (rbMedian.Checked)
                filter = new Median();
            if (rbMax.Checked)
                filter = new Max();
            if (rbMin.Checked)
                filter = new Min();
        }
    }
}
