

using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Wall:Panel
    {
        Brush pen = Brushes.Brown;
        protected override void OnPaint(PaintEventArgs e)
        {
            drawWall(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void drawWall(Graphics g)
        {
            
            g.FillRectangle(pen,0,1,32,31);
            g.DrawRectangle(Pens.Black, 0, 11, 32, 10);
            g.DrawLine(Pens.Black, 16, 1, 16, 11);
            g.DrawLine(Pens.Black, 16, 21, 16, 31);
            g.Dispose();

        }
    }
}
