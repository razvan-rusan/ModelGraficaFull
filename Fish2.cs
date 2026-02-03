namespace ModelGraficaFull
{
    public class Fish2
    {
        public static void Draw(Graphics mainGraphics, Func<float, float, (float x, float y)> tr, float scl)
        {
            using Pen pen = new(Color.Black, 3);
            using Brush brush = new SolidBrush(Color.PeachPuff);
            List<PointF> tail = [];
            Bezier.DrawDegreeNBezier(pen, new List<PointF>([new(360,90), new(320,140), new(380,120), new(380,90)]).Select((PointF p) => {
                (float xx, float yy) = tr(p.X, p.Y);
                return new PointF(xx, yy);
            }).ToList(), mainGraphics, resolution: 30);
            tail.AddRange(Bezier.cachedPoints!);
            mainGraphics.FillPolygon(brush, [..tail]);
            Bezier.DrawDegreeNBezier(pen, new List<PointF>([new(360,90), new(320,30), new(380,60), new(380,90)]).Select((PointF p) => {
                (float xx, float yy) = tr(p.X, p.Y);
                return new PointF(xx, yy); 
            }).ToList(), mainGraphics, resolution: 30);
            tail.AddRange(Bezier.cachedPoints!);
            mainGraphics.FillPolygon(brush, [.. tail]);
            (float elx, float ely) = tr(450f, 100f);
            mainGraphics.FillEllipse(brush, elx - (70f * scl), ely - (50f * scl)/2, 70f * scl, 50f * scl);
            mainGraphics.DrawEllipse(pen, elx - (70f * scl), ely - (50f * scl) / 2, 70f * scl, 50f * scl);
        }
    }
}