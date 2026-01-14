using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class Transforms
    {
        public static PointFTransformation Rotate(float radius, float angle) {
            return (PointF p) => new(p.X + radius * MathF.Sin(angle), p.Y + radius * MathF.Cos(angle));
        } 

        public static PointFTransformation Translate(float dx, float dy) {
            return (PointF p) => new(p.X + dx, p.Y + dy);
        }

        public static PointFTransformation Scale(float scl)
        {
            return (PointF p) => new(p.X * scl, p.Y * scl);
        }

        public static PointFTransformation Scale(float sclX, float sclY)
        {
            return (PointF p) => new(p.X * sclX, p.Y * sclY);
        }

        public static PointFTransformation Compose(PointFTransformation a, PointFTransformation b)
        {
            return (PointF p) => a(b(p));
        }
    }
}
