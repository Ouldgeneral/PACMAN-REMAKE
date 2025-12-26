using System.Windows.Forms;
using System.Drawing;

namespace ProjetClasse
{
    class Food:Panel
    {
        int radius;
        public int x;
        public int y;
        Brush pen = Brushes.White;
        public Food(int radius,int x,int y)
        {
            this.radius = radius;
            this.x = x;
            this.y = y;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            drawFood(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void drawFood(Graphics g)
        {
            
            g.FillEllipse(pen, 16, 16, radius, radius);
            g.Dispose();
        }
    }
}
