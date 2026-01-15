using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection.Metadata;
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
        readonly Random random = new Random();
        public Bitmap ApplyBlur7x7(Bitmap src)
        {
            Bitmap dest = new Bitmap(src.Width, src.Height);

            // 1. Define 7x7 Kernel
            int[,] k = {
        { 0,  0,  1,   2,  1,  0,  0 },
        { 0,  3, 13,  22, 13,  3,  0 },
        { 1, 13, 59,  97, 59, 13,  1 },
        { 2, 22, 97, 159, 97, 22,  2 },
        { 1, 13, 59,  97, 59, 13,  1 },
        { 0,  3, 13,  22, 13,  3,  0 },
        { 0,  0,  1,   2,  1,  0,  0 }
    };

            int div = 1003; // Sum of all values in k
            int radius = 3; // (7 - 1) / 2

            for (int y = radius; y < src.Height - radius; y++)
            {
                for (int x = radius; x < src.Width - radius; x++)
                {
                    long r = 0, g = 0, b = 0; 

                    
                    for (int i = -radius; i <= radius; i++)
                    {
                        for (int j = -radius; j <= radius; j++)
                        {
                            Color c = src.GetPixel(x + j, y + i);
                            int w = k[i + radius, j + radius]; 

                            r += c.R * w;
                            g += c.G * w;
                            b += c.B * w;
                        }
                    }

                    
                    dest.SetPixel(x, y, Color.FromArgb((int)(r / div), (int)(g / div), (int)(b / div)));
                }
            }
            return dest;
        }

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
            #region BlurCoral
            Bitmap b = new Bitmap("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\coral.png");
            b = ApplyBlur7x7(b);
            mainGraphics.DrawImage(b, pictureBox1.Width * 0.2f, pictureBox1.Height * 0.75f, b.Width * 1.15f, b.Height * 1.15f);
            #endregion
            #region Flag
            BurkinaFasoFlag.DrawWithinRect(mainGraphics, 8f, pictureBox1.Height - 100f, 161f, 100f);
            #endregion
            #region Submarine
            PointFTransformation tr = (p) => new(p.X, p.Y - 120f);
            Submarine.Draw(mainGraphics, pictureBox1.Width * 0.7f, pictureBox1.Height * 0.89f, 200f, 70f, tr);
            #endregion
            #region BezierFish
            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 3; j++)
                {
                    float xOffset = i * 120f;
                    float yOffset = j * 140f + random.NextSingle() * (-0.5f) * 40f;
                    float radius = random.NextSingle() * 20f + 10f, angle = random.NextSingle() * MathF.Tau;
                    PointFTransformation fishScalingAndTranslationAndRotation = (p) => new(p.X * 0.15f + xOffset + radius * MathF.Sin(angle), p.Y * 0.65f + radius * MathF.Sin(angle) + yOffset);
                    Fish.Draw(mainGraphics, pictureBox1.Width * 0.5f, pictureBox1.Height * 0.8f, fishScalingAndTranslationAndRotation);
                }
            }
            #endregion
            #region FractalCoral
            for (int i = 0; i < 7; i++)
            {
                float treeXOffset = i * (pictureBox1.Width / 7f) + (float)random.NextDouble() * 40f;
                float treeHeightVariation = 0.7f + (float)random.NextDouble() * 0.6f;

                PointFTransformation treeTransformation = (point) => new(
                    point.X * 0.5f + treeXOffset,
                    pictureBox1.Height * 0.99f - point.Y * treeHeightVariation
                );


                // Start angle at PI/2 (90 degrees) so it grows Upwards
                Trees.drawTree(mainGraphics, new PointF(pictureBox1.Width * 0.7f, 0), 80f, treeTransformation, MathF.PI / 2, 4 + (int)(random.NextSingle() * 7));
            }
            #endregion
            #region FunnyBlur
            mainBitmap = ApplyBlur7x7(mainBitmap);
            #endregion
            pictureBox1.Image = mainBitmap;
            mainBitmap.Save("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\ExamenGrafica2026.png");
        }

        private void ExamenGrafica_Load(object sender, EventArgs e)
        {

        }
    }
}
