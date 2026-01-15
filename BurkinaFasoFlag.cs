using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class BurkinaFasoFlag
    {
        private static Color 
            red = Color.FromArgb(190, 0, 39), 
            green = Color.FromArgb(35, 158, 70),
            yellow = Color.FromArgb(247, 244, 9);
        public static void DrawWithinRect(Graphics g, float x, float y, float width, float height)
        {
            g.FillRectangle(new SolidBrush(red), x, y, width, height/2);
            g.FillRectangle(new SolidBrush(green), x, y+height/2, width, height/2);
            float angle = 0f;
            float smallRadius = 7f, bigRadius = 15f;
            List<Point> starPoints = new List<Point>();
            float offset = MathF.PI * 0.5f;
            for (int i=0; i<=10; i++, angle += MathF.Tau / 10f) {
                starPoints.Add(new Point(
                    (int)(x + width / 2 + MathF.Cos(angle + offset) * ((i%2==0) ? smallRadius : bigRadius)),
                    (int)(y + height / 2 + MathF.Sin(angle + offset) * ((i % 2 == 0) ? smallRadius : bigRadius))
                 ));   
            }
            g.FillPolygon(new SolidBrush(yellow), starPoints.ToArray());
        }
       
    }
}
