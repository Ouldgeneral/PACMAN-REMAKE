using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    abstract class Element:Panel
    {
        public int posX;
        public int posY;
        public char direction;
        public char symbol;
        public Point initialPosition;
        public bool isVulnerable;
        public char initialDirection;
        public int lastX;
        public int lastY;
        public Element(int posX,int posY, char symbol,char direction,bool isVulnerable)
        {
            this.posX = posX;
            this.posY = posY;
            this.symbol= symbol;
            this.direction = direction;
            this.isVulnerable = isVulnerable;
            this.initialPosition = new Point(posX, posY);
            initialDirection = direction;
            this.lastX = posX;
            this.lastY = posY;


        }
        protected abstract void drawElement(Graphics g);
        public void update(int x,int y,char direction)
        {
            lastY = posY;
            lastX = posX;
            posY = y;
            posX = x;
            this.direction = direction;
        }
        public void reInitialise()
        {
            posX = initialPosition.X;
            posY = initialPosition.Y;
            lastX = posX;
            lastY = posY;
            direction = initialDirection;
            isVulnerable = false;
        }
    }
    
}
