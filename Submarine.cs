using System;
using System.Collections.Generic;
using System.Text;

namespace ModelGraficaFull
{
    public static class Submarine
    {
        static float radius = 20f;
        public static void Draw(Graphics g, float x, float y, float wBody, float hBody) {
            using (Bitmap ironImage = new Bitmap("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\minecraft_style_iron.png")) {
                using (TextureBrush ironBrush = new TextureBrush(ironImage)) {
                    ironBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                    ironBrush.ScaleTransform(1.25f, 1.25f);
                    PointF rectCenter = new PointF(x - wBody / 2f, y - hBody / 2f),
                        ellipse1Center = new PointF(x - wBody / 2f - 30f, y - hBody / 2f),
                        ellispe2Center = new PointF(x + wBody / 2f - 30f, y - hBody / 2f);
                    
                    g.FillRectangle(ironBrush, rectCenter.X, rectCenter.Y, wBody, hBody);
                    g.FillEllipse(ironBrush, ellipse1Center.X, ellipse1Center.Y, hBody, hBody);
                    g.FillEllipse(ironBrush, ellispe2Center.X, ellispe2Center.Y,  hBody, hBody);
                    
                }
            }
                
        }

        public static void Draw(Graphics g, float x, float y, float wBody, float hBody, PointFTransformation tr)
        {
            using (Bitmap ironImage = new Bitmap("C:\\Users\\razvan\\source\\repos\\ModelGraficaFull\\minecraft_style_iron.png"))
            {
                using (TextureBrush ironBrush = new TextureBrush(ironImage))
                {
                    ironBrush.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                    ironBrush.ScaleTransform(1.25f, 1.25f);
                    PointF rectCenter = new PointF(x - wBody / 2f, y - hBody / 2f),
                        ellipse1Center = new PointF(x - wBody / 2f - 30f, y - hBody / 2f),
                        ellispe2Center = new PointF(x + wBody / 2f - 30f, y - hBody / 2f);
                    rectCenter = tr(rectCenter);
                    ellipse1Center = tr(ellipse1Center);
                    ellispe2Center = tr(ellispe2Center);
                    g.FillRectangle(ironBrush, rectCenter.X, rectCenter.Y, wBody, hBody);
                    g.FillEllipse(ironBrush, ellipse1Center.X, ellipse1Center.Y, hBody, hBody);
                    g.FillEllipse(ironBrush, ellispe2Center.X, ellispe2Center.Y, hBody, hBody);
                    float yOff = 70f;
                    Polygon p = new Polygon([
                        new(rectCenter.X, rectCenter.Y + hBody - yOff),
                        new(rectCenter.X, rectCenter.Y + hBody - 50f - yOff),
                        new(rectCenter.X + 50f, rectCenter.Y + hBody - 50f - yOff),
                        new(rectCenter.X + 50f, rectCenter.Y + hBody - yOff)
                    ]);
                    g.FillPolygon(ironBrush, p.Points);
                }
            }

        }
    }
}
