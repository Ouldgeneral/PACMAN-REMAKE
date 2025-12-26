
using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Play:Panel
    {

        protected override void OnPaint(PaintEventArgs e)
        {
            drawPlay(e.Graphics);
            base.OnPaint(e);
        }
        private void drawPlay(Graphics g)
        {
            TextRenderer.DrawText(g, "Clicker sur espace pour jouer", this.Font, new Point(0, 0), Color.White, Color.Transparent);
            g.Dispose();
        }
    }
}
