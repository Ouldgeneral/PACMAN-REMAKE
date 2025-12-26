
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Levels:Panel
    {
        public int level;
        List<char[][]> levels;
        char[][] level1 =
         {

            "XXXXXXXXXXXXXXXXXXXXX".ToCharArray(),
            "XVvvvvvvvvXvvvvvvvvVX".ToCharArray(),
            
            "XvXXXvXXXvXvXXXvXXXvX".ToCharArray(),
            "XvvvvvvvvvVvvvvvvvvvX".ToCharArray(),
            "XvXXXvXvXXXXXvXvXXXvX".ToCharArray(),
            "XVvvvvXvvvXvvvXvvvvVX".ToCharArray(),
            "XXXXXvXXXvXvXXXvXXXXX".ToCharArray(),
            "LLLLXvXvvvvvvvXvXLLLL".ToCharArray(),
            "LLLLXvXvXXoXXvXvXLLLL".ToCharArray(),
            "XXXXXvvvXrbpXvvvXXXXX".ToCharArray(),
            "vvvvvvXvXXXXXvXvvvvvv".ToCharArray(),
            "XXXXXvXvvvVvvvXvXXXXX".ToCharArray(),
            "XVvvvvvvXXvXXvvvvvvVX".ToCharArray(),
            "XvXXXvvvvXvXvvvvXXXvX".ToCharArray(),
            "XvvXXvvvvvPvvvvvXXvvX".ToCharArray(),
            "XXvvXvXvXXXXXvXvXvvXX".ToCharArray(),
            "XvvvvvXvvvXvvvXvvvvvX".ToCharArray(),
            "XvXXXXXXXvXvXXXXXXXvX".ToCharArray(),
            "XVvvvvvvvvvvvvvvvvvVX".ToCharArray(),
            "XXXXXXXXXXXXXXXXXXXXX".ToCharArray(),
            

        };
        char[][] level2=
         {

            "XXXXXXXXXXXvXXXXXXXXX".ToCharArray(),
            "XXVvvvvvvvvvvvvvvvvVX".ToCharArray(),
            "XXvXXXXXvXXvXXXXXvXvX".ToCharArray(),
            "vvvvvvvvvXXvvvvvXvXvv".ToCharArray(),
            "XXvXXXXXXXXvXXXvXvXvX".ToCharArray(),
            "XXvvvvvvvvvvXXXvXvXvX".ToCharArray(),
            "lXvXXXvXXvXvXXXvXvXVX".ToCharArray(),
            "lXvvvXvvvrbpovvvXvXXX".ToCharArray(),
            "lXvXXXvXvvvvvXXvXvXXX".ToCharArray(),
            "lXvvvvvXXXXXXXXvvvvvX".ToCharArray(),
            "lXvXXvXXXXXvvvvvXvXvX".ToCharArray(),
            "lXvXXvvvPvvvXvXvXvXvX".ToCharArray(),
            "lXvvvvXXvXXXXvXvXvXvX".ToCharArray(),
            "lXVXXvvvvvvvXvXvXvXvX".ToCharArray(),
            "XXvXXvXXXXXvXvXvXvXvX".ToCharArray(),
            "vvvvvvvvvvvvvvvvvvvvv".ToCharArray(),
            "XXvXXvXXXXXXXvXvXXXXX".ToCharArray(),
            "XvvvvvvvvvvvvvXvvvvVX".ToCharArray(),
            "XXXXXXXXXXXVXXXXXXXXX".ToCharArray()
        };
        public Levels()
        {
            
            levels = new List<char[][]>();
            levels.Add(level1);
            levels.Add(level2);
        }
        public char[][] getLevel(int level)
        {
            this.level = level;
            if (level <=1 || level -1> levels.Count - 1) return level1;
            return levels[level - 1];
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            drawLevel(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        private void drawLevel(Graphics g)
        {
            if (level <= 1 || level - 1 > levels.Count - 1)level=1;
            TextRenderer.DrawText(g,"Niveau:" + level, this.Font,new Point(0,0), Color.White,Color.Transparent);
            g.Dispose();
        }
    }
}
