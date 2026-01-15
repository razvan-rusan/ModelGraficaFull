using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public struct Triangle(float x, float y, float z)
    {
        public float X = x, Y = y, Z = z;

        // A helper to mix two points (find the middle)
        public static Triangle Average(Triangle a, Triangle b)
        {
            return new Triangle((a.X + b.X) / 2, (a.Y + b.Y) / 2, (a.Z + b.Z) / 2);
        }
    }
}
