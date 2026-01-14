using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public class Sun
    {
        float radius, x, y;
        Graphics graphics;
        List<PointF> points = [];
        int rayWidth;
        public Sun(Graphics g, float x, float y, float radius, int rayWidth)
        {
            this.graphics = g;
            this.x = x;
            this.y = y;
            this.radius = radius;
            this.rayWidth = rayWidth;
        }
        private enum TriangleVertex
        {
            Uninitialized,
            Left,
            Top,
            Right
        }
        int rayTriangleCounter = 0;
        static private readonly OklchColor _myYellow = new(0.9341f, 0.225f, 100.16f), _myWhite = new(0.9341f, 0.037f, 104.4f);
        static private Color GetAppropriateYellow(float percent)
        {
            return ColorUtility.Mix(percent, 0f, 1f, _myYellow, _myWhite);
        }
        private struct Triangle(Graphics g)
        {
            public PointF left, top, right;
            private readonly Graphics _g = g;

            public readonly void Draw(float percent)
            {
                _g.FillPolygon(new SolidBrush(GetAppropriateYellow(percent)), [left, top, right]);
            }
        }
        public void Draw()
        {
            float xx, yy;
            int index = 0;
            TriangleVertex currentVertex = TriangleVertex.Uninitialized;
            Triangle t = new(this.graphics);
            for (float angle = 0f; angle <= Math.PI * 2; angle += 0.001f, index++)
            {
                xx = x + radius * (float)Math.Cos(angle);
                yy = y + radius * (float)Math.Sin(angle);
                const int spikesRotationCount = 17;
                float spikeRotationPercent = 0f;
                for (int i = 0; i < spikesRotationCount; i++, spikeRotationPercent += 1f / spikesRotationCount)
                {
                    if (index % spikesRotationCount == i)
                    {
                        switch (currentVertex)
                        {
                            case TriangleVertex.Uninitialized:
                                currentVertex = TriangleVertex.Left;
                                break;
                            case TriangleVertex.Left:
                                //currentVertex = TriangleVertex.Top;
                                if (rayTriangleCounter >= this.rayWidth)
                                {
                                    rayTriangleCounter = 0;
                                    currentVertex = TriangleVertex.Top;
                                }
                                else
                                {
                                    rayTriangleCounter++;
                                }
                                t.left = new PointF(xx, yy);
                                break;
                            case TriangleVertex.Top:
                                //currentVertex = TriangleVertex.Right;
                                if (rayTriangleCounter >= this.rayWidth)
                                {
                                    rayTriangleCounter = 0;
                                    currentVertex = TriangleVertex.Right;
                                }
                                else
                                {
                                    rayTriangleCounter++;
                                }
                                xx += (radius / 2 + radius / 3 * MathF.Sin(2 * MathF.PI * spikeRotationPercent)) * (float)Math.Cos(angle);
                                yy += (radius / 2 + radius / 3 * MathF.Sin(2 * MathF.PI * spikeRotationPercent)) * (float)Math.Sin(angle);
                                t.top = new PointF(xx, yy);
                                break;
                            case TriangleVertex.Right:
                                //currentVertex = TriangleVertex.Uninitialized;
                                if (rayTriangleCounter >= this.rayWidth)
                                {
                                    rayTriangleCounter = 0;
                                    currentVertex = TriangleVertex.Uninitialized;
                                }
                                else
                                {
                                    rayTriangleCounter++;
                                }
                                t.right = new PointF(xx, yy);
                                t.Draw(spikeRotationPercent);
                                break;
                        }
                    }
                }
                //graphics.DrawEllipse(Pens.Yellow, xx, yy, 1, 1);
                points.Add(new PointF(xx, yy));
            }
            this.graphics.FillClosedCurve(Pens.Yellow.Brush, points.ToArray());
        }

    }
}
