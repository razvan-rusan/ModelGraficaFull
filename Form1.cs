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

            //Bezier.DrawDegree3Bezier(
            //    Pens.Black,
            //    new(50, 400),
            //    new(150, 300),
            //    new(250, 500),
            //    new(350, 300),
            //    //[new(50, 400), new(150, 300), new(250, 500), new(350, 300)],
            //    mainGraphics,
            //    20
            //);

            mainGraphics.DrawEllipse(Pens.Black, pictureBox1.Width/2, pictureBox1.Height/2, 100, 100);

            
            Bezier.DrawBezierBird(Pens.Green, mainGraphics, new(202f,248f), 320);
            
            pictureBox1.Image = mainBitmap;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
