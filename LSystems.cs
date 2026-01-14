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

        public class AcaciaTree(Graphics g, string axiom, float angle, PointF pos, float segmentLength, int numIterations)
        : LSystem(g, axiom, angle, pos, segmentLength, numIterations)
        {
            private Stack<(PointF pos, float angle)> stack = new();

            public override void Draw()
            {
                PointF disp = new PointF(MathF.Cos(currentAngle) * segmentLength, MathF.Sin(currentAngle) * segmentLength);
                for (int i = 0; i < contents.Length; i++)
                {
                    switch (contents[i])
                    {
                        case 'F':
                            {
                                float nextX = pos.X + disp.X;
                                float nextY = pos.Y + disp.Y;
                                g.DrawLine(Pens.Black, pos.X, pos.Y, nextX, nextY);
                                pos = new PointF(nextX, nextY);
                                break;
                            }
                        case '+':
                            {
                                currentAngle += angle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                        case '[':
                            {
                                stack.Push((pos, currentAngle));
                                break;
                            }
                        case ']':
                            {
                                var (savedPos, savedAngle) = stack.Pop();
                                pos = savedPos;
                                currentAngle = savedAngle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                    }
                }
            }

            protected override void Update()
            {
                String next = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    switch (contents[i])
                    {
                        case 'F':
                            next += "FF";
                            break;
                        case 'X':
                            next += "F+[[X]-X]-F[-FX]+X";
                            break;
                        default:
                            next += contents[i];
                            break;
                    }
                }
                contents = next;
            }
        }

        public class Algae1(Graphics g, string axiom, float angle, PointF pos, float segmentLength, int numIterations)
        : LSystem(g, axiom, angle, pos, segmentLength, numIterations)
        {
            public override void Draw()
            {
                PointF disp = new PointF(segmentLength, 0);
                for (int i = 0; i < contents.Length; i++)
                {
                    float iX = pos.X;
                    float iY = pos.Y;
                    switch (contents[i])
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
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                    }
                    g.DrawLine(Pens.Green, iX, iY, pos.X, pos.Y);
                }
            }

            protected override void Update()
            {
                String next = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    switch (contents[i])
                    {
                        case 'A':
                            next += "AB";
                            break;
                        case 'B':
                            next += "A";
                            break;
                        default:
                            next += contents[i];
                            break;
                    }
                }
                contents = next;
            }
        }

        public class Algae2(Graphics g, string axiom, float angle, PointF pos, float segmentLength, int numIterations)
        : LSystem(g, axiom, angle, pos, segmentLength, numIterations)
        {
            public override void Draw()
            {
                PointF disp = new PointF(segmentLength, 0);
                for (int i = 0; i < contents.Length; i++)
                {
                    float iX = pos.X;
                    float iY = pos.Y;
                    switch (contents[i])
                    {
                        case 'X':
                        case 'Y':
                            {
                                pos.X += disp.X;
                                pos.Y += disp.Y;
                                break;
                            }
                        case '+':
                            {
                                currentAngle += angle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                        case '-':
                            {
                                currentAngle -= angle;
                                disp.X = MathF.Cos(currentAngle) * segmentLength;
                                disp.Y = MathF.Sin(currentAngle) * segmentLength;
                                break;
                            }
                    }
                    g.DrawLine(Pens.DarkGreen, iX, iY, pos.X, pos.Y);
                }
            }

            protected override void Update()
            {
                String next = "";
                for (int i = 0; i < contents.Length; i++)
                {
                    switch (contents[i])
                    {
                        case 'X':
                            next += "X+YF++YF-FX--FXFX-YF+";
                            break;
                        case 'Y':
                            next += "-FX+YFYF++YF+FX--FX-Y";
                            break;
                        default:
                            next += contents[i];
                            break;
                    }
                }
                contents = next;
            }
        }
    }
}
