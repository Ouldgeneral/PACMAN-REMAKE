

using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Pacman:Element
    {
        SoundPlayer pacmanEatFruit = new SoundPlayer(Sounds.pacman_eatfruit);
        SoundPlayer pacmanEatGhost = new SoundPlayer(Sounds.pacman_eatghost);
        SoundPlayer pacmanDead = new SoundPlayer(Sounds.pacman_death);
        Brush pen = Brushes.Yellow;
        int startAngle;

        public Pacman(int posX,int posY, char symbol,char direction,bool isVulnerable): base(posX, posY, symbol,direction,isVulnerable)
        {
            
        }
       
        public void eatFruit()
        {
            pacmanEatFruit.Play();
        }
        public void eatGhost()
        {
            pacmanEatGhost.Play();
        }
        public void die()
        {
            pacmanDead.Play();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            drawElement(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }
        protected override void drawElement(Graphics g)
        {
            
            switch (direction)
            {
                case 'u': startAngle = 300; break;
                case 'l': startAngle = 210; break;
                case 'd':startAngle = 120;break;
                default:startAngle = 30;break;
            }
            g.FillPie(pen, 0, 0, 32, 32, startAngle, 300);
            g.Dispose();
        }
    }
}
