using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class LSystems
    {
        public abstract class LSystem
        {
            protected String contents;
            protected float angle;
            protected float currentAngle;
            protected PointF pos;
            protected float segmentLength;
            protected float initialSegmentLength;
            protected int iterations;
            protected Graphics g;

            public LSystem(Graphics g, String axiom, float angle, PointF pos, float segmentLength, int numIterations)
            {
                this.contents = axiom;
                this.angle = angle;
                this.segmentLength = segmentLength;
                this.initialSegmentLength = segmentLength;
                this.iterations = numIterations;
                this.currentAngle = 0f;
                this.pos = pos;
                this.g = g;

                for (int _ = 0; _ < numIterations; _++)
                {
                    Update();
                }
                // Scale segment length so that grammars that expand segments (e.g. F -> FF)
                // keep reasonable visual sizes across different iteration counts.
                // Acacia's rule doubles segments each iteration, so halve length per iteration.
                if (numIterations > 0)
                {
                    this.segmentLength = this.initialSegmentLength * MathF.Pow(0.5f, numIterations);
                }
            }

            abstract protected void Update();

            abstract public void Draw();
        }

        public class Gosper(Graphics g, string axiom, float angle, PointF pos, float segmentLength, int numIterations)
        : LSystem(g, axiom, angle, pos, segmentLength, numIterations)
        {
            public override void Draw()
            {
                PointF disp = new PointF(0, this.segmentLength);
                for (int i = 0; i < this.contents.Length; i++)
                {
                    float iX = pos.X;
                    float iY = pos.Y;
                    switch (this.contents[i])
                    {
                        case 'A':
                        case 'B':
                            {
                                pos.X += disp.X;
                                pos.Y += disp.Y;
                                break;
                            }
                        case '+':
                            {
                                currentAngle += angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                    }
                    g.DrawLine(Pens.Black, iX, iY, pos.X, pos.Y);
                }
            }

            public void Draw(Pen p)
            {
                PointF disp = new PointF(0, this.segmentLength);
                for (int i = 0; i < this.contents.Length; i++)
                {
                    float iX = pos.X;
                    float iY = pos.Y;
                    switch (this.contents[i])
                    {
                        case 'A':
                        case 'B':
                            {
                                pos.X += disp.X;
                                pos.Y += disp.Y;
                                break;
                            }
                        case '+':
                            {
                                currentAngle += angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                    }
                    g.DrawLine(p, iX, iY, pos.X, pos.Y);
                }
            }

            public void Draw(Pen p, XYTransformation tr)
            {
                PointF disp = new PointF(0, this.segmentLength);
                for (int i = 0; i < this.contents.Length; i++)
                {
                    float iX = pos.X;
                    float iY = pos.Y;
                    switch (this.contents[i])
                    {
                        case 'A':
                        case 'B':
                            {
                                pos.X += disp.X;
                                pos.Y += disp.Y;
                                break;
                            }
                        case '+':
                            {
                                currentAngle += angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * this.segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * this.segmentLength;
                                break;
                            }
                    }
                    float jX, jY;
                    (iX, iY) = tr(iX,iY);
                    (jX, jY) = tr(pos.X, pos.Y);
                    g.DrawLine(p, iX, iY, pos.X, pos.Y);
                }
            }

            

            protected override void Update()
            {
                String next = "";
                for (int i = 0; i < this.contents.Length; i++)
                {
                    switch (this.contents[i])
                    {
                        case 'A':
                            next += "A-B--B+A++AA+B-";
                            break;
                        case 'B':
                            next += "+A-BB--B-A++A+B";
                            break;
                        default:
                            next += this.contents[i];
                            break;
                    }
                }
                this.contents = next;
            }
        }

    }
}
