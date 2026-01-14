using static ModelGraficaFull.LSystems;

namespace ModelGraficaFull
{
    public partial class CasaMeaDeLaTara : Form
    {
        Graphics mainGraphics;
        Bitmap mainBitmap;

        public CasaMeaDeLaTara()
        {
            InitializeComponent();
            mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            mainGraphics = Graphics.FromImage(mainBitmap);

            #region Sun
            Sun s = new Sun(mainGraphics, pictureBox1.Width/2-310, pictureBox1.Height/2-230, 80f, 30);
            s.Draw();
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
            #region Trees
            //Gosper gosper = new Gosper(mainGraphics, "A", MathF.PI / 3, new PointF(pictureBox1.Width / 2, pictureBox1.Height / 2), 8f, 4);
            //gosper.Draw();
            AcaciaTree tree = new AcaciaTree(mainGraphics, "X", MathF.PI/8, new(300f, 300f), 40f, 3);
            tree.Draw();
            Algae1 algae1 = new Algae1(mainGraphics, "A", MathF.PI / 7, new(100f, 400f), 10f, 5);
            algae1.Draw();
            #endregion
            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
