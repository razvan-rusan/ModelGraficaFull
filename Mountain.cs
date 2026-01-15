using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    using System;
    using System.Drawing;
    using System.Reflection;

    public static class Mountain
    {
        private static Random rng = new Random();

        public static void DrawMountain(Graphics g, Triangle p1, Triangle p2, Triangle p3, PointFTransformation tr, int depth, float roughness)
        {
            
            if (depth == 0)
            {
                
                PointF s1 = ProjectIso(p1);
                PointF s2 = ProjectIso(p2);
                PointF s3 = ProjectIso(p3);

                
                using (Pen p = new Pen(Color.FromArgb(200, Color.Black), 1))
                {
                    s1 = tr(s1);
                    s2 = tr(s2);
                    s3 = tr(s3);
                    g.DrawLine(p, s1, s2);
                    g.DrawLine(p, s2, s3);
                    g.DrawLine(p, s3, s1);
                }


                int gray = (int) Utility2.clamp(p1.Y * 2 + 100, 0, 255); 
                using (Brush b = new SolidBrush(Color.FromArgb(50, (int)Utility2.clamp(0,255,gray), (int)Utility2.clamp(0, 255, gray), (int)Utility2.clamp(0, 255, gray))))
                    g.FillPolygon(b, [s1, s2, s3]);

                return;
            }

            
            Triangle m1 = Triangle.Average(p1, p2);
            Triangle m2 = Triangle.Average(p2, p3);
            Triangle m3 = Triangle.Average(p3, p1);

            
            float offset = (float)(rng.NextDouble() - 0.5) * roughness;

            m1.Y += offset;
            m2.Y += offset;
            m3.Y += offset;

            
            float nextRoughness = roughness * 0.5f;

            
            DrawMountain(g, p1, m1, m3, tr, depth - 1,  nextRoughness); 
            DrawMountain(g, m1, p2, m2, tr, depth - 1,  nextRoughness); 
            DrawMountain(g, m3, m2, p3, tr, depth - 1,   nextRoughness); 
            DrawMountain(g, m1, m2, m3, tr, depth - 1,  nextRoughness); 
        }

        // Helper: Project 3D (X,Y,Z) to 2D Screen (X,Y)
        // This is a simple Isometric-style projection
        private static PointF ProjectIso(Triangle p)
        {
            float screenX = p.X - p.Z;        
            float screenY = p.Y + (p.X + p.Z) * 0.5f; 

            
            return new PointF(screenX + 300, 400 - screenY); 
        }
    }
}
