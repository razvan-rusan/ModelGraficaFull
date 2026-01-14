namespace ModelGraficaFull
{
    public partial class Form1 : Form
    {
        Graphics mainGraphics;
        Bitmap mainBitmap;

        public Form1()
        {
            InitializeComponent();
            mainBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            mainGraphics = Graphics.FromImage(mainBitmap);

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

            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
