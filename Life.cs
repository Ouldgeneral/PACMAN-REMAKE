

using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Life:Panel
    {
        Brush pen;
        Point p1;
        Point p2;
        Point p3;
        Point[] points;
        public Life()
        {
            pen = Brushes.Red;
            p1 = new Point(0, 2);
            p2 = new Point(8, 16);
            p3 = new Point(16, 2);
            points = new Point[3] { p1,p2,p3};

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            drawLife(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void drawLife(Graphics g)
        {
            
            g.FillPie(pen, 0, 0, 8, 5, 180, 180);
            g.FillPie(pen, 8, 0, 8, 5, 180, 180);
            g.FillPolygon(pen, points);
            g.Dispose();
        }
    }
}
