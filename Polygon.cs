using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public class Polygon(List<PointF> points)
    {
        private List<PointF> points = points;
        public PointF[] Points
        {
            get { return [.. points]; }
            set { points = [.. value]; }
        }
    }
}
