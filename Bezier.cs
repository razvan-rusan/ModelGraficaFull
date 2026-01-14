using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class Bezier
    {
        public static void DrawDegree1Bezier(Pen p, PointF p1, PointF p2, Graphics g, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ <= resolution; _++)
            {
                points.Add(Utility2.lerp(p1, p2, Utility2.clamp(0f,1f,t)));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i], points[i + 1]);
            }
        }

        public static void DrawDegree2Bezier(Pen p, PointF p1, PointF p2, PointF p3, Graphics g, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ <= resolution; _++)
            {
                PointF a = Utility2.lerp(p1, p2, Utility2.clamp(0f, 1f, t));
                PointF b = Utility2.lerp(p2, p3, Utility2.clamp(0f, 1f, t));
                points.Add(Utility2.lerp(a, b, Utility2.clamp(0f,1f,t)));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i], points[i + 1]);
            }
        }

        public static void DrawDegree3Bezier(Pen p, PointF p1, PointF p2, PointF p3, PointF p4, Graphics g, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ <= resolution; _++)
            {
                PointF l_p1p2 = Utility2.lerp(p1, p2, Utility2.clamp(0f, 1f, t));
                PointF l_p2p3 = Utility2.lerp(p2, p3, Utility2.clamp(0f, 1f, t));
                PointF l_p3p4 = Utility2.lerp(p3, p4, Utility2.clamp(0f, 1f, t));
                PointF l_p1p2_p2p3 = Utility2.lerp(l_p1p2, l_p2p3, Utility2.clamp(0f, 1f, t));
                PointF l_p2p3_p3p4 = Utility2.lerp(l_p2p3, l_p3p4, Utility2.clamp(0f, 1f, t));
                //PointF l_final = Utility2.lerp(l_p1p2_p2p3, l_p2p3_p3p4, t);
                points.Add(Utility2.lerp(l_p1p2_p2p3, l_p2p3_p3p4, Utility2.clamp(0f,1f,t)));
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(Pens.Black, points[i], points[i + 1]);
            }
        }

        public static List<PointF> __underlying_bezier_lerp(List<PointF> points, float t)
        {
            if (points.Count == 2) return [Utility2.lerp(points[0], points[1], Utility2.clamp(0f, 1f, t))];
            else
            {
                List<PointF> res = [];
                for (int i = 0; i < points.Count - 1; i++)
                {
                    PointF p_i = Utility2.lerp(points[i], points[i + 1], Utility2.clamp(0f, 1f, t));
                    res.Add(p_i);
                }
                return __underlying_bezier_lerp(res, t);
            }
        }

        public static void DrawDegreeNBezier(Pen p, List<PointF> controlPoints, Graphics g, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ <= resolution; _++)
            {
                points.Add(__underlying_bezier_lerp(controlPoints, Utility2.clamp(0f, 1f, t))[0]);
                t += dt;
            }
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i], points[i + 1]);
            }
        }

        public static void DrawDegreeNBezier(Pen p, List<PointF> controlPoints, Graphics g, PointFTransformation tr, int resolution = 100)
        {
            List<PointF> points = [];
            float t = 0, dt = 1.0f / resolution;
            for (int _ = 0; _ <= resolution; _++)
            {
                points.Add(__underlying_bezier_lerp(controlPoints, Utility2.clamp(0f, 1f, t))[0]);
                t += dt;
            }
            points = [.. points.Select(tr)];
            for (int i = 0; i < points.Count - 1; i++)
            {
                g.DrawLine(p, points[i], points[i + 1]);
            }
        }

        /*
         * 
         * Pasarea ideala ar fi asa (cand jumatate de wingSpan e egal cu 160)
         *
        List<PointF> points = [new(a, b), new(a+100,b-50), new(a+130,b-50), new(a+160,b)];
           Bezier.DrawDegreeNBezier(Pens.Red, points, mainGraphics);
           List<PointF> points2 = [new(c, d), new(c + 30, d - 50), new(c + 60, d - 50), new(c + 160, d)];
           Bezier.DrawDegreeNBezier(Pens.Red, points2, mainGraphics);
        *
        * In functie de ea, o calculam proportional, cu regula de trei simpla pe urmatoarea:
        * (Ca sa fie scalata frumos)
        */

        public static void DrawBezierBird(Pen p, Graphics g, PointF middlePos, AspectRatio ar, int resolution = 100) {
            float[][] magicLeftOffsets = [[0f,0f],[100f,-50f],[130f,-50f],[160f,0f]],
                      magicRightOffsets = [[0f,0f],[30f,-50f],[60f,-50f],[160f,0f]];
            for (int i = 0; i < 4; i++) { 
                magicLeftOffsets[i][0] *= ar.X / 320f;
                magicRightOffsets[i][0] *= ar.X / 320f;
                magicLeftOffsets[i][1] *= ar.Y / 50f;
                magicRightOffsets[i][1] *= ar.Y / 50f;
            }
            float a = middlePos.X, b = middlePos.Y, c = middlePos.X + magicLeftOffsets[^1][0], d = middlePos.Y;
            List<PointF> pointsLeft=[],pointsRight=[];
            for (int i=0; i<4; i++)
            {
                pointsLeft.Add(new PointF(a + magicLeftOffsets[i][0], b + magicLeftOffsets[i][1]));
                pointsRight.Add(new PointF(c + magicRightOffsets[i][0], d + magicRightOffsets[i][1]));
            }
            DrawDegreeNBezier(p, pointsLeft, g, resolution);
            DrawDegreeNBezier(p, pointsRight, g, resolution);
        }

        public static void DrawBezierBird(Pen p, Graphics g, PointF middlePos, AspectRatio ar, PointFTransformation tr, int resolution = 100)
        {
            float[][] magicLeftOffsets = [[0f, 0f], [100f, -50f], [130f, -50f], [160f, 0f]],
                      magicRightOffsets = [[0f, 0f], [30f, -50f], [60f, -50f], [160f, 0f]];
            for (int i = 0; i < 4; i++)
            {
                magicLeftOffsets[i][0] *= ar.X / 320f;
                magicRightOffsets[i][0] *= ar.X / 320f;
                magicLeftOffsets[i][1] *= ar.Y / 50f;
                magicRightOffsets[i][1] *= ar.Y / 50f;
            }
            float a = middlePos.X, b = middlePos.Y, c = middlePos.X + magicLeftOffsets[^1][0], d = middlePos.Y;
            List<PointF> pointsLeft = [], pointsRight = [];
            for (int i = 0; i < 4; i++)
            {
                pointsLeft.Add(new PointF(a + magicLeftOffsets[i][0], b + magicLeftOffsets[i][1]));
                pointsRight.Add(new PointF(c + magicRightOffsets[i][0], d + magicRightOffsets[i][1]));
            }
            DrawDegreeNBezier(p, [.. pointsLeft.Select(tr)], g, tr, resolution);
            DrawDegreeNBezier(p, [.. pointsRight.Select(tr)], g, tr, resolution);
        }
    }
}
