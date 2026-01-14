using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public struct OklchColor(float l, float c, float h)
    {
        public float L = l;
        public float C = c;
        public float H = h;

        public static implicit operator System.Drawing.Color(OklchColor oklch)
        {
            return OklchConverter.FromOklch(oklch.L, oklch.C, oklch.H);
        }
    }
}
