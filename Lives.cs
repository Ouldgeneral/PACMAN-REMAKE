

using System.Windows.Forms;

namespace ProjetClasse
{
    
    class Lives:Panel
    {
        public int lives;
        public int initialLives;
        public Lives(int lives)
        {
            this.lives = lives;
            this.initialLives = lives;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            for (int i = 0; i < lives; i++)
            {
                Life life = new Life();
                life.SetBounds(i * 16, 0, 16, 16);
                Controls.Add(life);
            }
            base.OnPaint(e);
            e.Dispose();
        }
    }
}
