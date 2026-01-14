using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class OklchConverter
    {
        public static Color FromOklch(float l, float c, float h)
        {
            float hRad = h * (MathF.PI / 180f);

            float L = l;
            float a = c * MathF.Cos(hRad);
            float b = c * MathF.Sin(hRad);

            float l_ = L + 0.3963377774f * a + 0.2158037573f * b;
            float m_ = L - 0.1055613458f * a - 0.0638541728f * b;
            float s_ = L - 0.0894841775f * a - 1.2914855480f * b;


            float l_lin = l_ * l_ * l_;
            float m_lin = m_ * m_ * m_;
            float s_lin = s_ * s_ * s_;

            float rLin = 4.0767416621f * l_lin - 3.3077115913f * m_lin + 0.2309699292f * s_lin;
            float gLin = -1.2684380046f * l_lin + 2.6097574011f * m_lin - 0.3413193965f * s_lin;
            float bLin = -0.0041960863f * l_lin - 0.7034186147f * m_lin + 1.7076147010f * s_lin;


            byte r = LinearToSrgb(rLin);
            byte g = LinearToSrgb(gLin);
            byte b2 = LinearToSrgb(bLin);

            return Color.FromArgb(255, r, g, b2);
        }


        private static byte LinearToSrgb(float x)
        {
            if (x <= 0.0f) return 0;
            if (x >= 1.0f) return 255;


            float val = (x <= 0.0031308f)
                ? (12.92f * x)
                : (1.055f * MathF.Pow(x, 1.0f / 2.4f) - 0.055f);

            return (byte)MathF.Round(val * 255f);
        }
    }
}
