using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CG_Lab1
{
    public class Colors
    {
        // 2° observer  Illuminant D65;
        public const double XWhite = 95.047;
        public const double YWhite = 100.00;
        public const double ZWhite = 108.883;
        public const double UN = 0.2009;
        public const double VN = 0.4610;

        public static bool[] isRoundArr = {false, false, false};

        public static void ClearRoundArr()
        {
            isRoundArr[0] = false;
            isRoundArr[1] = false;
            isRoundArr[2] = false;
        }

        public class HSV
        {
            double h;
            double s;
            double v;

            public HSV()
            {
                h = 0;
                s = 0;
                v = 0;
            }

            public HSV(double h, double s, double v)
            {
                this.h = h;
                this.s = s;
                this.v = v;
            }

            public double H
            {
                get{return h;} 
				set 
				{ 
					h = value; 
					h = h > 360 ? 360 : h < -1 ? -1 : h; 
				} 
            }
            public double S
            {
                get { return s; }
                set
                {
                    s = value;
                    s = s > 100 ? 100 : s < 0 ? 0 : s;
                }
            }


            public double V
            {
                get { return v; }
                set
                {
                    v = value;
                    v = v > 100 ? 100 : v < 0 ? 0 : v;
                }
            } 
        }

        public class CMYK
        {
            double c;
            double m;
            double y;
            double k;

            public CMYK()
            {
                c = 0;
                m = 0;
                y = 0;
                k = 0;
            }

            public CMYK(double c, double m, double y, double k)
            {
                this.c = c;
                this.m = m;
                this.y = y;
                this.k = k;
            }

            public double C
            {
                get { return c; }
                set
                {
                    c = value;
                    c = c > 100 ? 100 : c < 0 ? 0 : c;
                }
            }

            public double M
            {
                get { return m; }
                set
                {
                    m = value;
                    m = m > 100 ? 100 : m < 0 ? 0 : m;
                }
            }

            public double Y
            {
                get { return y; }
                set
                {
                    y = value;
                    y = y > 100 ? 100 : y < 0 ? 0 : y;
                }
            }

            public double K
            {
                get { return k; }
                set
                {
                    k = value;
                    k = k > 100 ? 100 : k < 0 ? 0 : k;
                }
            }
        }

        public class LUV
        {
            double l;
            double u;
            double v;

            public LUV()
            {
                l = 0;
                u = 0;
                v = 0;
            }

            public LUV(double l, double u, double v)
            {
                this.l = l;
                this.u = u;
                this.v = v;
            }

            public double L
            {
                get { return l; }
                set
                {
                    l = value;
                    l = l > 100 ? 100 : l < 0 ? 0 : l;
                }
            }

            public double U
            {
                get { return u; }
                set
                {
                    u = value;
                    u = u > 100 ? 100 : u < 0 ? 0 : u;
                }
            }

            public double V
            {
                get { return v; }
                set
                {
                    v = value;
                    v = v > 100 ? 100 : v < 0 ? 0 : v;
                }
            }
        }

        public class XYZ
        {
            double x;
            double y;
            double z;

            public XYZ()
            {
                x = 0;
                y = 0;
                z = 0;
            }

            public XYZ(double x, double y, double z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }

            
            public double X
            {
                get { return x; }
                set
                {
                    x = value;
                    x = x > XWhite ? XWhite : x < 0 ? 0 : x;
                }
            }

            public double Y
            {
                get { return y; }
                set
                {
                    y = value;
                    y = y > YWhite ? YWhite : y < 0 ? 0 : y;
                }
            }

            public double Z
            {
                get { return z; }
                set
                {
                    z = value;
                    z = z > ZWhite ? ZWhite : z < 0 ? 0 : z;
                }
            }
        }

        public static HSV RGBToHSV(Color rgb)
        {
            HSV hsv = new HSV();

            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;

            double max = Math.Max(Math.Max(r, g), b);
            double min = Math.Min(Math.Min(r, g), b);

            double delta = max - min;

            if (delta == 0)
                hsv.H = 0;
            else if (max == r && g >= b)
                hsv.H = 60 * (g - b) / delta;
            else if (max == r)
                hsv.H = 60 * (g - b) / delta + 360;
            else if (max == g)
                hsv.H = 60 * (b - r) / delta + 120;
            else if (max == b)
                hsv.H = 60 * (r - g) / delta + 240;

            if (max == 0)
                hsv.S = 0;
            else
                hsv.S = (1 - min / max) * 100;

            hsv.V = max * 100;

            return hsv;
        }

        public static Color HSVToRGB(HSV hsv)
        {
            Color rgb = new Color();
            int h = (int)Math.Floor(hsv.H / 60);

            double vmin = (100 - hsv.S) * hsv.V / 100;
            double a = (hsv.V - vmin) * ((((int)hsv.H) % 60) / 60.0);
            double vinc = vmin + a;
            double vdec = hsv.V - a;

            vmin = vmin * 255 / 100;
            vinc = vinc * 255 / 100;
            vdec = vdec * 255 / 100;
            double v = hsv.V * 255 / 100;


            switch (h)
            {
                case 0:
                    rgb = Color.FromArgb((int)v, (int)vinc, (int)vmin);
                    break;
                case 1:
                    rgb = Color.FromArgb((int)vdec, (int)v, (int)vmin);
                    break;
                case 2:
                    rgb = Color.FromArgb((int)vmin, (int)v, (int)vinc);
                    break;
                case 3:
                    rgb = Color.FromArgb((int)vmin, (int)vdec, (int)v);
                    break;
                case 4:
                    rgb = Color.FromArgb((int)vinc, (int)vmin, (int)v);
                    break;
                case 5:
                    rgb = Color.FromArgb((int)v, (int)vmin, (int)vdec);
                    break;
            }

            return rgb;
        }

        public static CMYK RGBToCMYK(Color rgb)
        {
            CMYK cmyk = new CMYK();

            double r = rgb.R / 255.0;
            double g = rgb.G / 255.0;
            double b = rgb.B / 255.0;

            double k = 1 - Math.Max(Math.Max(r, g), b);

            if (k == 1)
            {
                return cmyk;
            }
            cmyk.C = (1 - r - k) / (1 - k) * 100;
            cmyk.M = (1 - g - k) / (1 - k) * 100;
            cmyk.Y = (1 - b - k) / (1 - k) * 100;
            cmyk.K = k * 100;
            return cmyk;
        }

        public static Color CMYKToRGB(CMYK cmyk)
        {
            Color rgb = new Color();

            byte r = (byte)(255 * (1 - cmyk.C / 100) * (1 - cmyk.K / 100));
            byte g = (byte)(255 * (1 - cmyk.M / 100) * (1 - cmyk.K / 100));
            byte b = (byte)(255 * (1 - cmyk.Y / 100) * (1 - cmyk.K / 100));

            rgb = Color.FromArgb(r, g, b);
            return rgb;
        }

        public static XYZ RGBToXYZ(Color rgb)
        {
            XYZ xyz = new XYZ();

            double var_R = rgb.R / 255.0;        //R from 0 to 255
            double var_G = rgb.G / 255.0;        //G from 0 to 255
            double var_B = rgb.B / 255.0;        //B from 0 to 255

            if (var_R > 0.04045) 
                var_R = Math.Pow((var_R + 0.055) / 1.055, 2.4);
            else 
                var_R = var_R / 12.92;
            if (var_G > 0.04045) 
                var_G = Math.Pow((var_G + 0.055) / 1.055, 2.4);
            else 
                var_G = var_G / 12.92;
            if (var_B > 0.04045) 
                var_B = Math.Pow((var_B + 0.055) / 1.055, 2.4);
            else 
                var_B = var_B / 12.92;
                
            var_R = var_R * 100;
            var_G = var_G * 100;
            var_B = var_B * 100;

            //Observer. = 2°, Illuminant = D65
            xyz.X = var_R * 0.4124 + var_G * 0.3576 + var_B * 0.1805;
            xyz.Y = var_R * 0.2126 + var_G * 0.7152 + var_B * 0.0722;
            xyz.Z = var_R * 0.0193 + var_G * 0.1192 + var_B * 0.9505;

            return xyz;
        }

        public static Color XYZToRGB(XYZ xyz)
        {
            Color rgb = new Color();

            double var_X = xyz.X / 100.0;  //X from 0 to  95.047    
            double var_Y = xyz.Y / 100.0;  //Y from 0 to 100.000
            double var_Z = xyz.Z / 100.0;  //Z from 0 to 108.883      

            double var_R = var_X * 3.2406 + var_Y * -1.5372 + var_Z * -0.4986;
            double var_G = var_X * -0.9689 + var_Y * 1.8758 + var_Z * 0.0415;
            double var_B = var_X * 0.0557 + var_Y * -0.2040 + var_Z * 1.0570;

            if (var_R > 0.0031308)
                var_R = 1.055 * Math.Pow(var_R, 1.0 / 2.4) - 0.055;
            else
                var_R = 12.92 * var_R;
            if (var_G > 0.0031308)
                var_G = 1.055 * Math.Pow(var_G, 1.0 / 2.4) - 0.055;
            else 
                var_G = 12.92 * var_G;
            if (var_B > 0.0031308) 
                var_B = 1.055 * Math.Pow(var_B, 1.0 / 2.4) - 0.055;
            else 
                var_B = 12.92 * var_B;

            ClearRoundArr();
            var_R = var_R * 255;
            var_G = var_G * 255;
            var_B = var_B * 255;
            if (var_R < 0)
            {
                isRoundArr[0] = true;
                var_R = 0;
            }
            if (var_R > 255)
            {
                isRoundArr[0] = true;
                var_R = 255;
            }
            if (var_G < 0)
            {
                isRoundArr[1] = true;
                var_G = 0;
            }
            if (var_G > 255)
            {
                isRoundArr[1] = true;
                var_G = 255;
            }
            if (var_B < 0)
            {
                isRoundArr[2] = true;
                var_B = 0;
            }
            if (var_B > 255)
            {
                isRoundArr[2] = true;
                var_B = 255;
            }

            rgb = Color.FromArgb((int)var_R, (int)var_G , (int)var_B);

            return rgb;
        }

        public static LUV RGBToLUV(Color rgb)
        {
            XYZ xyz = RGBToXYZ(rgb);
            LUV luv = new LUV();

            if (xyz.X == 0 && xyz.Y == 0 && xyz.Z == 0)
            {
                return luv;
            }

            luv.U = 4 * xyz.X / (xyz.X + 15 * xyz.Y + 3 * xyz.Z) * 100;
            luv.V = 9 * xyz.Y / (xyz.X + 15 * xyz.Y + 3 * xyz.Z) * 100;

            double y = xyz.Y / YWhite;
            if (y <= Math.Pow(6.0 / 29.0, 3))
                luv.L = Math.Pow(29.0 / 3.0, 3) * y;
            else
                luv.L = 116 * Math.Pow(y, 1.0 / 3.0) - 16;

            return luv;
        }

        public static Color LUVToRGB(LUV luv)
        {
            XYZ xyz = new XYZ();
    
            double u = luv.U / 100.0; 
            double v = luv.V / 100.0;
            /*
            if (v == 0)
            {
                errorMsg = "LUVToRGB. v = 0.";
                return XYZToRGB(xyz);
            }
            xyz.X = 9.0 * luv.L * luv.U / (4.0 * luv.V);
            xyz.Y = luv.L;
            xyz.Z = (-3.0 * luv.L * luv.U - 20.0 * luv.L * luv.V + 1200.0 * luv.L) / (4.0 * luv.V);


            return XYZToRGB(xyz);*/
            
            double var_Y = (luv.L + 16) / 116.0;
            if (Math.Pow(var_Y, 3) > 0.008856)
                var_Y = Math.Pow(var_Y, 3);
            else
                var_Y = (var_Y - 16 / 116.0) / 7.787;

            if (v == 0)
            {
                return XYZToRGB(xyz);
            }
            xyz.Y = var_Y * YWhite;
            xyz.X = -((9 * xyz.Y * u) / ((u - 4) * v - v * u));
            xyz.Z = (9 * xyz.Y - 15 * v * xyz.Y - v * xyz.X) / (3 * v);
            return XYZToRGB(xyz);
        }
    }
}
