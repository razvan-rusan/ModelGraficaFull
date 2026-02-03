using System;
using System.Collections.Generic;
using System.Drawing.Imaging.Effects;
using System.Text;

namespace ModelGraficaFull
{
    public class RoadSign
    {
        public static void Draw(Graphics mainGraphics, XYTransformation tr)
        {
            using Pen red = new Pen(Color.FromArgb(255, 218, 37, 28), 12);
            using Brush white = new SolidBrush(Color.White);
            using Brush black = new SolidBrush(Color.FromArgb(31, 26, 23));
            PointF[] trianglePoints = [new(300f, 200f), new(500f, 200f), new(400f, 26.794f)];
            trianglePoints = trianglePoints.Select(p => tr(p.X, p.Y)).Select(tp => new PointF(tp.x, tp.y)).ToArray();
            mainGraphics.FillPolygon(white, trianglePoints);
            mainGraphics.DrawPolygon(red, trianglePoints);
            float l1 = (trianglePoints[1].X - trianglePoints[0].X) / 2;
            for (int i = 0; i < 5; i++) {
                XYTransformation tr2 = (x,y) => tr(x*0.5f + 18f * i + 210f,y*0.9f + 90f);
                DrawStakeShape(mainGraphics, tr2);    
            }
            mainGraphics.FillRectangle(black, trianglePoints[0].X + l1 / 2, trianglePoints[0].Y - 28f, l1 * 1f, 5f);
            mainGraphics.FillRectangle(black, trianglePoints[0].X + l1/2, trianglePoints[0].Y - 20f, l1 * 1f, 5f);
        }

        public static void DrawStakeShape(Graphics mainGraphics, XYTransformation tr)
        {
            using Brush black = new SolidBrush(Color.FromArgb(31, 26, 23));
            PointF[] stakePoints = [new(320, 100), new(320,50), new(310,40), new(300,50), new(300,100)];
            stakePoints = stakePoints.Select(p => tr(p.X, p.Y)).Select(tp => new PointF(tp.x, tp.y)).ToArray();
            mainGraphics.FillPolygon(black, stakePoints);
        }
    }
}
