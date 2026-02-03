using System.Drawing;
using System.Reflection;
using static ModelGraficaFull.LSystems;

namespace ModelGraficaFull
{
    public partial class CasaMeaDeLaTara : Form
    {
        readonly Graphics mainGraphics;
        readonly Bitmap mainBitmap;
        readonly Random random = new();
        readonly OklchColor SkyBlue = new(0.632f, 0.1352f, 215f);
        readonly OklchColor SunsetFuchsia = new(0.7053f, 0.1574f, 32.4f);
        readonly OklchColor GrassGreen = new(0.5635f, 0.145f, 135.11f);
        readonly Brush shadow = new SolidBrush(Color.FromArgb(80, Color.Black));
        public CasaMeaDeLaTara()
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
            #region Sun
            Sun s = new Sun(mainGraphics, pictureBox1.Width/2-310, pictureBox1.Height/2-230, 80f, 30);
            s.Draw();
            #endregion
            #region Mountain (Or Lake)
            PointFTransformation mountainTransform = (p) => new(p.X + pictureBox1.Width * 0.3f, pictureBox1.Height * 1.4f - p.Y);
            Triangle v1 = new(-300, 0, -300), v2 = new(0, 0, 300), v3 = new(300, 0, -300);
            Mountain.DrawMountain(mainGraphics, v1, v2, v3, mountainTransform, 6, 250f);
            #endregion
            #region Clouds
            const int totalClouds = 23;
            for (int i = 0; i < totalClouds; i++)
            { 
                float xOffset = (float)random.NextDouble() * 60;
                float yOffset = (float)random.NextDouble() * 40;
                
                if (random.NextSingle() < 0.67f) //randomly make clouds missing
                {
                    continue;
                }

                XYTransformation cloudTransform = (x, y) => (x + xOffset, y + yOffset);
                Gosper gosper = new Gosper(mainGraphics, "A", MathF.PI / 3, new PointF(i*(pictureBox1.Width/ totalClouds), pictureBox1.Height / 2 - 50f - (float)random.NextDouble() * 20f), 20f, 4);
                //gosper.Draw(new Pen(Color.FromArgb(20, Color.White), 10), cloudTransform);
            }
            #endregion
            #region Birds
            PointFTransformation birdTransform = (point) => new(point.X * 1.3f - 100f, point.Y - 100f),
                                 smallTranslation = (p) => new(p.X+50, p.Y+23);
            PointFTransformation currT = new(birdTransform);
            
            for (int i = 0; i < 4; i++, currT = new(Transforms.Compose(currT, smallTranslation))) {
                Bezier.DrawBezierBird(
                    new(Color.Black, 4),
                    mainGraphics,
                    new(202f, 248f),
                    new(80f + i, 30f),
                    currT,
                    20
                 );  
            }
            #endregion
            #region House
            using (Bitmap woodImage = new Bitmap("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\minecraft_planks.png"))
            {
                using (TextureBrush woodBrush = new TextureBrush(woodImage)) { 
                    woodBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                    // front rectangle dimensions
                    float x0 = pictureBox1.Width * 0.05f;
                    float y0 = pictureBox1.Height * 0.6f;
                    float w = 250f;
                    float h = 200f;
                    // depth for the right-side face (isometric offset)
                    float depth = 60f;

                    // side
                    mainGraphics.FillPolygon(woodBrush, new PointF[]
                    {
                        new PointF(x0, y0),                          // top-left front
                        new PointF(x0 + w, y0),                      // top-right front
                        new PointF(x0 + w + depth, y0 - depth * 0.4f), // top-right back (slanted)
                        new PointF(x0 + w + depth, y0 + h - depth * 0.4f), // bottom-right back
                        new PointF(x0 + w, y0 + h),                  // bottom-right front
                        new PointF(x0, y0 + h)                       // bottom-left front
                    });
                    mainGraphics.FillPolygon(shadow, new PointF[]
                    {
                        new PointF(x0, y0),                          // top-left front
                        new PointF(x0 + w, y0),                      // top-right front
                        new PointF(x0 + w + depth, y0 - depth * 0.4f), // top-right back (slanted)
                        new PointF(x0 + w + depth, y0 + h - depth * 0.4f), // bottom-right back
                        new PointF(x0 + w, y0 + h),                  // bottom-right front
                        new PointF(x0, y0 + h)                       // bottom-left front
                    });
                    // front face
                    mainGraphics.FillRectangle(woodBrush, x0, y0, w + 20f, h);
                }
            }
            using (Bitmap brickImage = new Bitmap("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\minecraft_bricks.png"))
            {
                using (TextureBrush brickBrush = new TextureBrush(brickImage))
                {
                    brickBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                    brickBrush.ScaleTransform(0.3f, 0.3f);
                    // roof geometry matching the house front for a simple 3D impression
                    float x0 = pictureBox1.Width * 0.05f;
                    float y0 = pictureBox1.Height * 0.6f;
                    float w = 250f;
                    // use the same peak Y as before
                    float roofPeakY = pictureBox1.Height * 0.45f;
                    float roofRise = y0 - roofPeakY;
                    float depth = 60f;
                    float xOff = 20f, xOff2 = 0f;

                    mainGraphics.FillPolygon(brickBrush, new PointF[]
                    {
                        new PointF(x0 - 20f - xOff2, y0),
                        new PointF(x0 + w + 20f, y0),
                        new PointF(x0 + w + depth, y0 - depth * 0.4f),
                        new PointF(x0 + w * 0.5f + depth - xOff, y0 - roofRise - depth * 0.4f),
                    });
                    mainGraphics.FillPolygon(shadow, new PointF[]
                    {
                        new PointF(x0 - 20f - xOff2, y0),
                        new PointF(x0 + w + 20f, y0),
                        new PointF(x0 + w + depth, y0 - depth * 0.4f),
                        new PointF(x0 + w * 0.5f + depth - xOff, y0 - roofRise - depth * 0.4f),
                    });
                    mainGraphics.FillPolygon(brickBrush, new PointF[]
                    {
                        new PointF(x0 - 20f - xOff2, y0),
                        new PointF(x0 + w + 20f, y0),
                        //new PointF(x0 + w + depth, y0 - depth * 0.4f),
                        new PointF(x0 + w * 0.5f + depth - xOff , y0 - roofRise - depth * 0.4f),
                    });
                }
            }

            #endregion
            #region Trees
            for (int i = 0; i < 7; i++)
            {
                float treeXOffset = i * (pictureBox1.Width / 7f) + (float)random.NextDouble() * 40f;
                float treeHeightVariation = 0.7f + (float)random.NextDouble() * 0.6f;

                PointFTransformation treeTransformation = (point) => new(
                    point.X * 0.5f + treeXOffset,
                    pictureBox1.Height * 0.99f - point.Y * treeHeightVariation
                );


                // Start angle at PI/2 (90 degrees) so it grows Upwards
                Trees.drawTree(mainGraphics, new PointF(pictureBox1.Width*0.7f, 0), 80f, treeTransformation, MathF.PI / 2, 4 + (int)(random.NextSingle() * 7));
            }
            #endregion
            #region Fence
            for (int j = 0; j < 5; j++) {
                for (int i = 0; i < 20; i++)
                {
                    float fenceXOffset = i * 65f + (float)random.NextDouble() * 30f;
                    float fenceExtraYScale = (float)random.NextDouble() * 0.3f;
                    float fenceLightnessVariation = ((float)random.NextDouble() - 0.5f) * 0.3f;
                    float fenceHeightChangePerLoop = j * 12f * (float) random.NextSingle();
                    PointFTransformation fenceTransformation = (point) => new(point.X * 0.4f + fenceXOffset, pictureBox1.Height * 0.99f - fenceHeightChangePerLoop + point.Y * (0.2f + fenceExtraYScale));
                    List<PointF> fencePoints = new([
                        new PointF(0f, 0f),
                        new PointF(120,0 + MathF.Pow(random.NextSingle(), 3) * 62f),
                        new PointF(120-60,-225 + MathF.Pow(random.NextSingle(), 3)),
                        new PointF(0,-200 + MathF.Pow(random.NextSingle(), 3))
                    ]);
                    Polygon p = new([.. fencePoints.Select(fenceTransformation)]);
                    mainGraphics.FillPolygon(new SolidBrush(new OklchColor(0.3371f + fenceLightnessVariation, 0.0902f, 51.46f)), p.Points);
                }
            }
            mainGraphics.FillRectangle(new SolidBrush(new OklchColor(0.3371f, 0.0902f, 51.46f)), 0, pictureBox1.Height * 0.99f - 70f, pictureBox1.Width, 20f);
            #endregion
            
            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
