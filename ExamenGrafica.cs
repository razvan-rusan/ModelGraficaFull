using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ModelGraficaFull
{
    public partial class ExamenGrafica : Form
    {
        //randu 7 (x)
        //coloana 15 (y)
        // lasloeugen@yahoo.com
        // Subiect: Grafica Examen 2026 
        // screesnhot + word
        readonly Graphics mainGraphics;
        readonly Bitmap mainBitmap;
        readonly OklchColor SkyBlue = new(0.7929f, 0.3113f, 179.58f);
        readonly OklchColor SunsetFuchsia = new(0.7587f, 0.2594f, 167.93f);
        readonly OklchColor GrassGreen = new(0.312f, 0.1341f, 268.52f);
        public ExamenGrafica()
        {
            InitializeComponent();
            mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            mainGraphics = Graphics.FromImage(mainBitmap);
            
            #region Gradient
            float
                endFuchsia = pictureBox1.Height * 0.40f,
                endBlue = pictureBox1.Height * 0.80f,
                fuchsiaSep = 0.4f,
                blueSep = 0.6f;
            for (int x = 0; x < pictureBox1.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Height; y++)
                {
                    OklchColor fillC;
                    if (y >= 0 && y <= endFuchsia * fuchsiaSep)
                    {
                        fillC = SunsetFuchsia;
                    }
                    else if (y > endFuchsia * fuchsiaSep && y <= endFuchsia)
                    {
                        fillC = ColorUtility.Mix(y, endFuchsia * fuchsiaSep + 1, endFuchsia, SunsetFuchsia, SkyBlue);

                    }
                    else if (y > endFuchsia && y <= endBlue * blueSep)
                    {
                        fillC = SkyBlue;
                    }
                    else if (y > blueSep)
                    {
                        fillC = ColorUtility.Mix(y, endBlue * blueSep + 1, endBlue, SkyBlue, GrassGreen);
                    }
                    else
                    {
                        fillC = GrassGreen;
                    }
                    mainBitmap.SetPixel(x, y, fillC);
                }
            }
            #endregion
            #region Flag
            BurkinaFasoFlag.DrawWithinRect(mainGraphics, 8f, pictureBox1.Height - 100f, 161f, 100f);
            #endregion
            #region Submarine
            #endregion
            pictureBox1.Image = mainBitmap;
        }

        private void ExamenGrafica_Load(object sender, EventArgs e)
        {

        }
    }
}
