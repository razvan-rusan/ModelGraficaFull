using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public class Algae : LSystems.LSystem
    {
        public Algae(Graphics g, string axiom, float angle, PointF pos, float segmentLength, int numIterations) : base(g, axiom, angle, pos, segmentLength, numIterations)
        {
        }

        public override void Draw()
        {
            //for (int )
        }

        protected override void Update()
        {
            //throw new NotImplementedException();
        }
    }
}
