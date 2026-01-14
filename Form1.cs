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

            PointFTransformation scale = (point) => new PointF(point.X * 1.3f - 100f, point.Y - 100f);

            Bezier.DrawBezierBird(
                new(Color.Black, 4), 
                mainGraphics, 
                new(202f,248f), 
                new(80f, 30f), 
                scale,
                20);
            
            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
