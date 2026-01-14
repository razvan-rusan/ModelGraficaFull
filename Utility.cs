using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace ModelGraficaFull
{
    public class Utility<T> where T : IFloatingPoint<T>
    {
        //returneaza valorea care corespunde valorii "value" din intervalul [inpLo, inpHi] in intervalul [outLo, outHi]
        static public T Remap(T value, T inpLo, T inpHi, T outLo, T outHi)
        {
            return (value - inpLo) / (inpHi - inpLo) * (outHi - outLo) + outLo;
        }

        static public T Lerp(T value, T start, T stop)
        {
            return start + (stop - start) * value;
        }
    }

    public class ColorUtility
    {
        //returneaza valorea care corespunde valorii "value" din intervalul [inpLo, inpHi] in intervalul [outLo, outHi]
        static public Color Mix(float value, float inpLo, float inpHi, Color colorA, Color colorB)
        {
            float v = Utility<float>.Remap(value, inpLo, inpHi, 0f, 1f);

            v = Math.Clamp(v, 0f, 1f);

            int r = (int)Utility<float>.Lerp(v, colorA.R, colorB.R);
            int g = (int)Utility<float>.Lerp(v, colorA.G, colorB.G);
            int b = (int)Utility<float>.Lerp(v, colorA.B, colorB.B);
            int a = (int)Utility<float>.Lerp(v, colorA.A, colorB.A);

            r = Math.Clamp(r, 0, 255);
            g = Math.Clamp(g, 0, 255);
            b = Math.Clamp(b, 0, 255);
            a = Math.Clamp(a, 0, 255);

            return Color.FromArgb(a, r, g, b);
        }

        static public OklchColor Mix(float value, float inpLo, float inpHi, OklchColor colorA, OklchColor colorB)
        {
            float v = Utility<float>.Remap(value, inpLo, inpHi, 0f, 1f);

            v = Math.Clamp(v, 0f, 1f);

            float l = Utility<float>.Lerp(v, colorA.L, colorB.L);
            float c = Utility<float>.Lerp(v, colorA.C, colorB.C);
            float h = ColorUtility.LerpAngle(v, colorA.H, colorB.H);

            l = Math.Clamp(l, 0, 1);
            c = Math.Clamp(c, 0, 1);
            h = Math.Clamp(h, 0, 360);

            return new OklchColor(l, c, h);
        }

        static public float LerpAngle(float t, float start, float stop)
        {
            float difference = stop - start;

            if (difference > 180f)
            {
                difference -= 360f;
            }
            else if (difference < -180f)
            {
                difference += 360f;
            }

            float result = start + difference * t;

            if (result < 0f) result += 360f;
            if (result > 360f) result -= 360f;

            return result;
        }
    }
}
