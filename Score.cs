using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Score:Panel
    {
        public int score;
        public int initialScore;

        public Score(int score)
        {
            this.score = score;
            this.initialScore = score;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            drawScore(e.Graphics);
            e.Dispose();
        }
        private void drawScore(Graphics g)
        {

            TextRenderer.DrawText(g, "Score:" + score, this.Font, new Point(0, 0), Color.White, Color.Transparent);
            g.Dispose();
        }
    }
}
