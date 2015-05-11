using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PFP.Imaging;

namespace CG_Lab2
{
    public partial class Form1 : Form
    {
        public Dictionary<PropertyTagId, KeyValuePair<PropertyTagType, Object>> imageProps 
            = new Dictionary<PropertyTagId,KeyValuePair<PropertyTagType,object>>();

        public String str = "";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void GetInfo(Image image)
        {
            imageProps.Clear();
            
            str += "Width: " + image.PhysicalDimension.Width + "\r\n";
            str += "Height: " + image.PhysicalDimension.Height + "\r\n";
            str += "VerticalResolution: " + image.VerticalResolution + "\r\n";
            str += "HorizontalResolution: " + image.HorizontalResolution + "\r\n";
            
            foreach (PropertyItem property in image.PropertyItems)
            {
                Object propValue =  new Object();
                switch ((PropertyTagType)property.Type)
                {
                    case PropertyTagType.ASCII:
                        ASCIIEncoding encoding = new ASCIIEncoding();
                        propValue = encoding.GetString(property.Value, 0, property.Len - 1);
                        break;
                    case PropertyTagType.Int16:
                        propValue = BitConverter.ToInt16(property.Value, 0);
                        break;
                    case PropertyTagType.SLONG:
                    case PropertyTagType.Int32:
                        propValue = BitConverter.ToInt32(property.Value, 0);
                        break;
                    case PropertyTagType.SRational:
                    case PropertyTagType.Rational:
                        UInt32 numberator =
                            BitConverter.ToUInt32(property.Value, 0);
                        UInt32 denominator =
                            BitConverter.ToUInt32(property.Value, 4);
                        try
                        {
                            propValue = ((double)numberator / (double)denominator).ToString();

                            if (propValue.ToString() == "NaN")
                                propValue = "0";
                        }
                        catch (DivideByZeroException)
                        {
                            propValue = "0";
                        }
                        break;
                    case PropertyTagType.Undefined:
                        propValue = "Undefined Data";
                        break;
                }
                imageProps.Add(NumToEnum<PropertyTagId>(property.Id), 
                    new KeyValuePair<PropertyTagType,object>(NumToEnum<PropertyTagType>(property.Type), propValue));
            }

            if (imageProps.Keys.Contains(NumToEnum<PropertyTagId>(0x0103)))
                str += "Compression: " + imageProps[NumToEnum<PropertyTagId>(0x0103)].Value.ToString() + "\r\n";
            else
                str += "Compression: Undefined.\r\n";
            
            ShowInfo(image);
        }

        public void ShowInfo(Image image)
        {
            foreach (KeyValuePair<PropertyTagId, KeyValuePair<PropertyTagType, Object>> property in imageProps)
            {
                str += property.Key.ToString() + ": " + property.Value.ToString()+ "\r\n";
            }

            str += "ChrominanceTable:\r\n" + getChrominanceTable(image);
            str += "LuminanceTable:\r\n" + getLuminanceTable(image);
        }


        private String getChrominanceTable(Image image)
        {
            String result = "";
            foreach (PropertyItem property in image.PropertyItems)
            {
                if (property.Id == 0x5091)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                            result += String.Format("{0,6:X} ", property.Value[i * 16 + j]);
                        result += "\r\n";
                    }
                }
            }
            return result;
        }


        private String getLuminanceTable(Image image)
        {
            String result = "";
            foreach (PropertyItem property in image.PropertyItems)
            {
                if (property.Id == 0x5090)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                            result += String.Format("{0,6:X} ", property.Value[i * 16 + j]);
                        result += "\r\n";
                    }
                }
            }
            return result;
        }

        public T NumToEnum<T>(int number)
        {
            return (T)Enum.ToObject(typeof(T), number);
        }

        private void openButton_MouseClick(object sender, MouseEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = Environment.CurrentDirectory;
            ofd.Filter = "All files (*.*)|*.*";
            ofd.RestoreDirectory = true;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Clear();
                Image image = Image.FromFile(ofd.FileName);

                Bitmap tmpBitmap = new Bitmap(image);
                Bitmap pictureBitmap = new Bitmap(tmpBitmap, pbImage.Width, pbImage.Height);

                pbImage.Image = pictureBitmap;

                string[] file = {ofd.FileName};
                GetFilesInfo(file);
            }
        }

        private void openFolderBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                string[] files = Directory.GetFiles(fbd.SelectedPath);
                GetFilesInfo(files);
            }
        }

        private void GetFilesInfo(string[] files)
        {
            str = "";
            Image image;
            foreach (string file in files)
            {
                try
                {
                    image = Image.FromFile(file);
                    str += file + "\r\n";
                    GetInfo(image);
                    str += "\r\n\r\n";
                }
                catch (Exception ex)
                {
                }
            }
            richTextBox1.Text = str;
            pbImage.Image = new Bitmap(new Bitmap(Image.FromFile(files[0])), pbImage.Width, pbImage.Height);
        }
    }
}
