using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CG_Lab1
{
    public partial class MainWindow : Form
    {
        Bitmap bitmapColorWheel;

        private Color defaultColor = Color.White;
        
        private Colors.HSV hsv;
        private Color rgb;
        private Colors.CMYK cmyk;
        private Colors.LUV luv;
        private Colors.XYZ xyz;

        public MainWindow()
        {
            InitializeComponent();

            Bitmap tmpBitmap = new Bitmap("colorwheel.png");
            bitmapColorWheel = new Bitmap(tmpBitmap, colorWheel.Width, colorWheel.Height);

            rgb = defaultColor;
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);

            xNumUpDown.Maximum = (int)Colors.XWhite;
            yNumUpDown.Maximum = (int)Colors.YWhite;
            zNumUpDown.Maximum = (int)Colors.ZWhite;
            xTrackBar.Maximum = (int)Colors.XWhite;
            yTrackBar.Maximum = (int)Colors.YWhite;
            zTrackBar.Maximum = (int)Colors.ZWhite;

            UpdateTextBoxes();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        public void UpdateTextBoxes()
        {
            DisableEvents();

            CheckRounds();

            lblCurrentColor.BackColor = rgb;

            // RGB
            redNumUpDown.Value   = rgb.R;
            greenNumUpDown.Value = rgb.G;
            blueNumUpDown.Value  = rgb.B;

            redTrackBar.Value   = rgb.R;
            greenTrackBar.Value = rgb.G;
            blueTrackBar.Value  = rgb.B;

            // HSV

            hueNumUpDown.Value        = (int)hsv.H;
            saturationNumUpDown.Value = (int)hsv.S;
            valueNumUpDown.Value      = (int)hsv.V;

            hueTrackBar.Value        = (int)hsv.H;
            saturationTrackBar.Value = (int)hsv.S;
            valueTrackBar.Value      = (int)hsv.V;

            //CMYK

            cyanNumUpDown.Value    = (int)cmyk.C;
            magentaNumUpDown.Value = (int)cmyk.M;
            yellowNumUpDown.Value  = (int)cmyk.Y;
            keyCNumUpDown.Value    = (int)cmyk.K;

            cyanTrackBar.Value    = (int)cmyk.C;
            magentaTrackBar.Value = (int)cmyk.M;
            yellowTrackBar.Value  = (int)cmyk.Y;
            keyCTrackBar.Value    = (int)cmyk.K;

            //LUV

            lNumUpDown.Value = (int)luv.L;
            uNumUpDown.Value = (int)luv.U;
            vNumUpDown.Value = (int)luv.V;

            lTrackBar.Value = (int)luv.L;
            uTrackBar.Value = (int)luv.U;
            vTrackBar.Value = (int)luv.V;

            //XYZ

            xNumUpDown.Value = (int)xyz.X;
            yNumUpDown.Value = (int)xyz.Y;
            zNumUpDown.Value = (int)xyz.Z;

            xTrackBar.Value = (int)xyz.X;
            yTrackBar.Value = (int)xyz.Y;
            zTrackBar.Value = (int)xyz.Z;

            UpdateRedColorBox();
            UpdateGreenColorBox();
            UpdateBlueColorBox();

            UpdateHueColorBox();
            UpdateSatColorBox();
            UpdateValColorBox();

            UpdateCyanColorBox();
            UpdateMagentaColorBox();
            UpdateYellowColorBox();
            UpdateKeyCColorBox();

            UpdateLColorBox();
            UpdateUColorBox();
            UpdateVColorBox();

            UpdateXColorBox();
            UpdateYColorBox();
            UpdateZColorBox();

            EnableEvents();

            Colors.ClearRoundArr();
            this.Invalidate();
        }

        private void UpdateRedColorBox()
        {
            Graphics g = redColorBox.CreateGraphics();
            for (int i = 0; i < redColorBox.Width; ++i)
            {
                int red = (int)Math.Round((double)255 * i / redColorBox.Width);

                Pen pen = new Pen(Color.FromArgb(red, rgb.G, rgb.B));
                g.DrawLine(pen, i, 0, i, redColorBox.Height);
            }
            this.Invalidate();
        }

        private void UpdateGreenColorBox()
        {
            Graphics g = greenColorBox.CreateGraphics();
            for (int i = 0; i < greenColorBox.Width; ++i)
            {
                int green = (int)Math.Round((double)255 * i / greenColorBox.Width);

                Pen pen = new Pen(Color.FromArgb(rgb.R, green, rgb.B));
                g.DrawLine(pen, i, 0, i, greenColorBox.Height);
            }
        }

        private void UpdateBlueColorBox()
        {
            Graphics g = blueColorBox.CreateGraphics();
            for (int i = 0; i < blueColorBox.Width; ++i)
            {
                int blue = (int)Math.Round((double)255 * i / blueColorBox.Width);

                Pen pen = new Pen(Color.FromArgb(rgb.R, rgb.G, blue));
                g.DrawLine(pen, i, 0, i, blueColorBox.Height);
            }
        }

        private void UpdateHueColorBox()
        {
            Graphics g = hueColorBox.CreateGraphics();
            for (int i = 0; i < hueColorBox.Width; ++i)
            {
                double hue = Math.Round((double)360 * i / hueColorBox.Width);

                Pen pen = new Pen(Colors.HSVToRGB(new Colors.HSV(hue, hsv.S, hsv.V)));
                g.DrawLine(pen, i, 0, i, hueColorBox.Height);
            }
        }

        private void UpdateSatColorBox()
        {
            Graphics g = saturationColorBox.CreateGraphics();
            for (int i = 0; i < saturationColorBox.Width; ++i)
            {
                double saturation = Math.Round((double)100 * i / saturationColorBox.Width);

                Pen pen = new Pen(Colors.HSVToRGB(new Colors.HSV(hsv.H, saturation, hsv.V)));
                g.DrawLine(pen, i, 0, i, saturationColorBox.Height);
            }
        }

        private void UpdateValColorBox()
        {
            Graphics g = valueColorBox.CreateGraphics();
            for (int i = 0; i < valueColorBox.Width; ++i)
            {
                double value = Math.Round((double)100 * i / valueColorBox.Width);

                Pen pen = new Pen(Colors.HSVToRGB(new Colors.HSV(hsv.H, hsv.S, value)));
                g.DrawLine(pen, i, 0, i, valueColorBox.Height);
            }
        }

        private void UpdateCyanColorBox()
        {
            Graphics g = cyanColorBox.CreateGraphics();
            for (int i = 0; i < cyanColorBox.Width; ++i)
            {
                double cyan = Math.Round((double)100 * i / cyanColorBox.Width);

                Pen pen = new Pen(Colors.CMYKToRGB(new Colors.CMYK(cyan, cmyk.M, cmyk.Y, cmyk.K)));
                g.DrawLine(pen, i, 0, i, cyanColorBox.Height);
            }
        }

        private void UpdateMagentaColorBox()
        {
            Graphics g = magentaColorBox.CreateGraphics();
            for (int i = 0; i < magentaColorBox.Width; ++i)
            {
                double magenta = Math.Round((double)100 * i / magentaColorBox.Width);

                Pen pen = new Pen(Colors.CMYKToRGB(new Colors.CMYK(cmyk.C, magenta, cmyk.Y, cmyk.K)));
                g.DrawLine(pen, i, 0, i, magentaColorBox.Height);
            }
        }

        private void UpdateYellowColorBox()
        {
            Graphics g = yellowColorBox.CreateGraphics();
            for (int i = 0; i < yellowColorBox.Width; ++i)
            {
                double yellow = Math.Round((double)100 * i / yellowColorBox.Width);

                Pen pen = new Pen(Colors.CMYKToRGB(new Colors.CMYK(cmyk.C, cmyk.M, yellow, cmyk.K)));
                g.DrawLine(pen, i, 0, i, yellowColorBox.Height);
            }
        }

        private void UpdateKeyCColorBox()
        {
            Graphics g = keyCColorBox.CreateGraphics();
            for (int i = 0; i < keyCColorBox.Width; ++i)
            {
                double keyC = Math.Round((double)100 * i / keyCColorBox.Width);

                Pen pen = new Pen(Colors.CMYKToRGB(new Colors.CMYK(cmyk.C, cmyk.M, cmyk.Y, keyC)));
                g.DrawLine(pen, i, 0, i, keyCColorBox.Height);
            }
        }

        private void UpdateLColorBox()
        {
            Graphics g = lColorBox.CreateGraphics();
            for (int i = 0; i < lColorBox.Width; ++i)
            {
                
                double l = Math.Round((double)100 * i / lColorBox.Width);

                Pen pen = new Pen(Colors.LUVToRGB(new Colors.LUV(l, luv.U, luv.V)));
                g.DrawLine(pen, i, 0, i, lColorBox.Height);
            }
        }

        private void UpdateUColorBox()
        {
            Graphics g = uColorBox.CreateGraphics();
            for (int i = 0; i < uColorBox.Width; ++i)
            {
                double u = Math.Round((double)100 * i / uColorBox.Width);

                Pen pen = new Pen(Colors.LUVToRGB(new Colors.LUV(luv.L, u, luv.V)));
                g.DrawLine(pen, i, 0, i, uColorBox.Height);
            }
        }

        private void UpdateVColorBox()
        {
            Graphics g = vColorBox.CreateGraphics();
            for (int i = 0; i < vColorBox.Width; ++i)
            {
                double v = Math.Round((double)100 * i / vColorBox.Width);

                Pen pen = new Pen(Colors.LUVToRGB(new Colors.LUV(luv.L, luv.U, v)));
                g.DrawLine(pen, i, 0, i, vColorBox.Height);
            }
        }

        private void UpdateXColorBox()
        {
            Graphics g = xColorBox.CreateGraphics();
            for (int i = 0; i < xColorBox.Width; ++i)
            {
                double x = Math.Round((double)Colors.XWhite * i / xColorBox.Width);

                Pen pen = new Pen(Colors.XYZToRGB(new Colors.XYZ(x, xyz.Y, xyz.Z)));
                g.DrawLine(pen, i, 0, i, xColorBox.Height);
            }
        }

        private void UpdateYColorBox()
        {
            Graphics g = yColorBox.CreateGraphics();
            for (int i = 0; i < yColorBox.Width; ++i)
            {
                double y = Math.Round((double)Colors.YWhite * i / yColorBox.Width);

                Pen pen = new Pen(Colors.XYZToRGB(new Colors.XYZ(xyz.X, y, xyz.Z)));
                g.DrawLine(pen, i, 0, i, yColorBox.Height);
            }
        }


        private void UpdateZColorBox()
        {
            Graphics g = zColorBox.CreateGraphics();
            for (int i = 0; i < zColorBox.Width; ++i)
            {
                double z = Math.Round((double)Colors.ZWhite * i / zColorBox.Width);

                Pen pen = new Pen(Colors.XYZToRGB(new Colors.XYZ(xyz.X, xyz.Y, z)));
                g.DrawLine(pen, i, 0, i, zColorBox.Height);
            }
        }

        // RGB

        private void Red_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                rgb = Color.FromArgb((sender as TrackBar).Value, rgb.G, rgb.B);
            else if (sender is NumericUpDown)
                rgb = Color.FromArgb((int)(sender as NumericUpDown).Value, rgb.G, rgb.B);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Green_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                rgb = Color.FromArgb(rgb.R, (sender as TrackBar).Value, rgb.B);
            else if (sender is NumericUpDown)
                rgb = Color.FromArgb(rgb.R, (int)(sender as NumericUpDown).Value, rgb.B);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Blue_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                rgb = Color.FromArgb(rgb.R, rgb.G, (sender as TrackBar).Value);
            else if (sender is NumericUpDown)
                rgb = Color.FromArgb(rgb.R, rgb.G, (int)(sender as NumericUpDown).Value);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        // HSV

        private void Hue_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                hsv.H = (int)(sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                hsv.H = (int)(sender as NumericUpDown).Value;
            if (hsv.H == -1)
                hsv.H = 359;
            if (hsv.H == 360)
                hsv.H = 0;
            rgb = Colors.HSVToRGB(hsv);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Saturation_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                hsv.S = (int)(sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                hsv.S = (int)(sender as NumericUpDown).Value;
            rgb = Colors.HSVToRGB(hsv);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Value_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                hsv.V = (int)(sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                hsv.V = (int)(sender as NumericUpDown).Value;
            rgb = Colors.HSVToRGB(hsv);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        //CMYK

        private void Cyan_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                cmyk.C = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                cmyk.C = (int)(sender as NumericUpDown).Value;
            rgb = Colors.CMYKToRGB(cmyk);
            hsv = Colors.RGBToHSV(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Magenta_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                cmyk.M = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                cmyk.M = (int)(sender as NumericUpDown).Value;
            rgb = Colors.CMYKToRGB(cmyk);
            hsv = Colors.RGBToHSV(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void Yellow_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                cmyk.Y = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                cmyk.Y = (int)(sender as NumericUpDown).Value;
            rgb = Colors.CMYKToRGB(cmyk);
            hsv = Colors.RGBToHSV(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void KeyC_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                cmyk.K = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                cmyk.K = (int)(sender as NumericUpDown).Value;
            rgb = Colors.CMYKToRGB(cmyk);
            hsv = Colors.RGBToHSV(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        //LUV

        private void L_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                luv.L = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                luv.L = (int)(sender as NumericUpDown).Value;
            rgb = Colors.LUVToRGB(luv);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void U_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                luv.U = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                luv.U = (int)(sender as NumericUpDown).Value;
            rgb = Colors.LUVToRGB(luv);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        private void V_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                luv.V = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                luv.V = (int)(sender as NumericUpDown).Value;
            rgb = Colors.LUVToRGB(luv);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            UpdateTextBoxes();
        }

        //XYZ

        private void X_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                xyz.X = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                xyz.X = (int)(sender as NumericUpDown).Value;
            rgb = Colors.XYZToRGB(xyz);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);

            UpdateTextBoxes();
        }

        private void Y_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                xyz.Y = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                xyz.Y = (int)(sender as NumericUpDown).Value;
            rgb = Colors.XYZToRGB(xyz);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);

            UpdateTextBoxes();
        }

        private void Z_ValueChangedEv(object sender, EventArgs e)
        {
            if (sender is TrackBar)
                xyz.Z = (sender as TrackBar).Value;
            else if (sender is NumericUpDown)
                xyz.Z = (int)(sender as NumericUpDown).Value;
            rgb = Colors.XYZToRGB(xyz);
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);

            UpdateTextBoxes();
        }

        private void ColorWheel_MouseClick(object sender, MouseEventArgs e)
        {
            rgb = bitmapColorWheel.GetPixel(e.X, e.Y);
 
            hsv = Colors.RGBToHSV(rgb);
            cmyk = Colors.RGBToCMYK(rgb);
            luv = Colors.RGBToLUV(rgb);
            xyz = Colors.RGBToXYZ(rgb);
            
            UpdateTextBoxes();
        }

        private void CheckRounds()
        {
            if (Colors.isRoundArr[0])
                redLabel.BackColor = Color.Red;
            else
                redLabel.BackColor = SystemColors.Control;

            if (Colors.isRoundArr[1])
                greenLabel.BackColor = Color.Red;
            else
                greenLabel.BackColor = SystemColors.Control;

            if (Colors.isRoundArr[2])
                blueLabel.BackColor = Color.Red;
            else
                blueLabel.BackColor = SystemColors.Control;
        }

        private void DisableEvents()
        {
            redTrackBar.Scroll -= Red_ValueChangedEv;
            redNumUpDown.ValueChanged -= Red_ValueChangedEv;
            greenTrackBar.Scroll -= Green_ValueChangedEv;
            greenNumUpDown.ValueChanged -= Green_ValueChangedEv;
            blueTrackBar.Scroll -= Blue_ValueChangedEv;
            blueNumUpDown.ValueChanged -= Blue_ValueChangedEv;

            hueTrackBar.Scroll -= Hue_ValueChangedEv;
            hueNumUpDown.ValueChanged -= Hue_ValueChangedEv;
            saturationTrackBar.Scroll -= Saturation_ValueChangedEv;
            saturationNumUpDown.ValueChanged -= Saturation_ValueChangedEv;
            valueTrackBar.Scroll -= Value_ValueChangedEv;
            valueNumUpDown.ValueChanged -= Value_ValueChangedEv;

            cyanNumUpDown.ValueChanged -= Cyan_ValueChangedEv;
            cyanTrackBar.Scroll -= Cyan_ValueChangedEv;
            magentaNumUpDown.ValueChanged -= Magenta_ValueChangedEv;
            magentaTrackBar.Scroll -= Magenta_ValueChangedEv;
            yellowNumUpDown.ValueChanged -= Yellow_ValueChangedEv;
            yellowTrackBar.Scroll -= Yellow_ValueChangedEv;
            keyCNumUpDown.ValueChanged -= KeyC_ValueChangedEv;
            keyCTrackBar.Scroll -= KeyC_ValueChangedEv;

            lNumUpDown.ValueChanged -= L_ValueChangedEv;
            lTrackBar.Scroll -= L_ValueChangedEv;
            uNumUpDown.ValueChanged -= U_ValueChangedEv;
            uTrackBar.Scroll -= U_ValueChangedEv;
            vNumUpDown.ValueChanged -= V_ValueChangedEv;
            vTrackBar.Scroll -= V_ValueChangedEv;

            xNumUpDown.ValueChanged -= X_ValueChangedEv;
            xTrackBar.Scroll -= X_ValueChangedEv;
            yNumUpDown.ValueChanged -= Y_ValueChangedEv;
            yTrackBar.Scroll -= Y_ValueChangedEv;
            zNumUpDown.ValueChanged -= Z_ValueChangedEv;
            zTrackBar.Scroll -= Z_ValueChangedEv;
        }

        private void EnableEvents()
        {
            redTrackBar.Scroll += Red_ValueChangedEv;
            redNumUpDown.ValueChanged += Red_ValueChangedEv;
            greenTrackBar.Scroll += Green_ValueChangedEv;
            greenNumUpDown.ValueChanged += Green_ValueChangedEv;
            blueTrackBar.Scroll += Blue_ValueChangedEv;
            blueNumUpDown.ValueChanged += Blue_ValueChangedEv;

            hueTrackBar.Scroll += Hue_ValueChangedEv;
            hueNumUpDown.ValueChanged += Hue_ValueChangedEv;
            saturationTrackBar.Scroll += Saturation_ValueChangedEv;
            saturationNumUpDown.ValueChanged += Saturation_ValueChangedEv;
            valueTrackBar.Scroll += Value_ValueChangedEv;
            valueNumUpDown.ValueChanged += Value_ValueChangedEv;

            cyanNumUpDown.ValueChanged += Cyan_ValueChangedEv;
            cyanTrackBar.Scroll += Cyan_ValueChangedEv;
            magentaNumUpDown.ValueChanged += Magenta_ValueChangedEv;
            magentaTrackBar.Scroll += Magenta_ValueChangedEv;
            yellowNumUpDown.ValueChanged += Yellow_ValueChangedEv;
            yellowTrackBar.Scroll += Yellow_ValueChangedEv;
            keyCNumUpDown.ValueChanged += KeyC_ValueChangedEv;
            keyCTrackBar.Scroll += KeyC_ValueChangedEv;

            lNumUpDown.ValueChanged += L_ValueChangedEv;
            lTrackBar.Scroll += L_ValueChangedEv;
            uNumUpDown.ValueChanged += U_ValueChangedEv;
            uTrackBar.Scroll += U_ValueChangedEv;
            vNumUpDown.ValueChanged += V_ValueChangedEv;
            vTrackBar.Scroll += V_ValueChangedEv;

            xNumUpDown.ValueChanged += X_ValueChangedEv;
            xTrackBar.Scroll += X_ValueChangedEv;
            yNumUpDown.ValueChanged += Y_ValueChangedEv;
            yTrackBar.Scroll += Y_ValueChangedEv;
            zNumUpDown.ValueChanged += Z_ValueChangedEv;
            zTrackBar.Scroll += Z_ValueChangedEv;
        }
    }
}
