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

            Bezier.DrawBezierBird(new(Color.Black, 4), mainGraphics, new(202f,248f), new(80f, 30f));
            
            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
