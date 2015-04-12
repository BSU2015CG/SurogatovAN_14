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
        
        public Form1()
        {
            InitializeComponent();
        }

        private void GetInfo(Image image)
        {
            imageProps.Clear();

            richTextBox1.Text += "Width: " + image.PhysicalDimension.Width + "\n";
            richTextBox1.Text += "Height: " + image.PhysicalDimension.Height + "\n";
            richTextBox1.Text += "VerticalResolution: " + image.VerticalResolution + "\n";
            richTextBox1.Text += "HorizontalResolution: " + image.HorizontalResolution + "\n";

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
            ShowInfo(image);
        }

        public void ShowInfo(Image image)
        {

            foreach (KeyValuePair<PropertyTagId, KeyValuePair<PropertyTagType, Object>> property in imageProps)
            {
                richTextBox1.Text += property.Key.ToString() + ": " + property.Value.ToString()+ "\n";
            }

            if (imageProps.Keys.Contains(NumToEnum<PropertyTagId>(0x0103)))
                richTextBox1.Text += "Compression: " + imageProps[NumToEnum<PropertyTagId>(0x0103)].Value.ToString() + "\n";
            else
                richTextBox1.Text += "Compression: Undefined.\n";

            richTextBox1.Text += "ChrominanceTable:\n" + getChrominanceTable(image);
            richTextBox1.Text += "LuminanceTable:\n" + getLuminanceTable(image);
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
                        result += "\n";
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
                        result += "\n";
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
                
                GetInfo(image);
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
            richTextBox1.Text = "";
            foreach (string file in files)
            {
                richTextBox1.Text += file + "\n";
                Image image = Image.FromFile(file);
                GetInfo(image);
                richTextBox1.Text += "\n\n";
            }
            pbImage.Image = new Bitmap(new Bitmap(Image.FromFile(files[0])), pbImage.Width, pbImage.Height);
        }
    }
}
