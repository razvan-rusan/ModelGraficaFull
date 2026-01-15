using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class Fish
    {
        static readonly OklchColor fishColor = new(0.6518f, 0.0526f, 65.22f), fishColor2 = new(0.6871f, 0.109f, 65.22f);
        public static void Draw(Graphics g, float x, float y, PointFTransformation tr)
        {
            Polygon p = new Polygon([
                new(280, 308),
                new(333, 306),
                new(371, 278),
                new(332, 247),
                new(325, 272),
                new(299, 288)
            ]);
            p.Points = [.. p.Points.Select(tr)];
            g.FillEllipse(new SolidBrush(fishColor2), p.Points[^3].X, p.Points[^3].Y, 20f, 20f);
            for (int i = 0; i < p.Points.Length - 3; i++) {
                Bezier.DrawDegreeNBezier(new Pen(fishColor), [p.Points[i], p.Points[i+1], p.Points[i+2]], g, 60);
            }
        }
    }
}
