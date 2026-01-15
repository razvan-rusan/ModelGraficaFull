using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class Trees
    {
        private static readonly OklchColor darkBrown = new OklchColor(0.3018f, 0.0244f, 87.46f), leafGreen = new OklchColor(0.5106f, 0.1184f, 48.28f);
        
        public static void drawTree(Graphics g, PointF pos, float length, PointFTransformation tr, float angle, int depth)
        {
            if (depth == 0) { return; } 
            

            float xEnd = pos.X + length * MathF.Cos(angle);
            float yEnd = pos.Y + length * MathF.Sin(angle);
            PointF endPos = new PointF(xEnd, yEnd);

            
            PointF screenStart = tr(pos);
            PointF screenEnd = tr(endPos);

            using (Pen p = new Pen(ColorUtility.Mix((float)depth, 4f, 8f, leafGreen, darkBrown), length/20))
            {
                g.DrawLine(p, screenStart, screenEnd);
            }

            float splitAngle = MathF.PI / 6;


            drawTree(g, endPos, length * 0.75f, tr, angle + splitAngle, depth - 1);

            drawTree(g, endPos, length * 0.75f, tr, angle - splitAngle, depth - 1);
        }
    }
}
