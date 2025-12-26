using System.Drawing;
using System.Windows.Forms;

namespace ProjetClasse
{
    class Ghost:Element
    {
        Brush pen;
        System.DateTime vulnerableUntil;
        public char mode;
        public bool isEaten;
        public char initialMode;
        
        //Corps
        Point tete = new Point(16, 0);
        Point p0 = new Point(1, 5);
        Point p1 = new Point(1, 32);
        Point p2 = new Point(6, 27);
        Point p3 = new Point(11, 32);
        Point p4 = new Point(16, 27);
        Point p5 = new Point(21, 32);
        Point p6 = new Point(26, 27);
        Point p7 = new Point(31, 32);
        Point p8 = new Point(31, 5);
        Point[] points;
        public Ghost(Brush pen,char mode,bool isVulnerable,int posX,int posY, char symbol, char direction) :base(posX,posY,symbol,direction,isVulnerable)
        {
            this.pen = pen;
            this.mode = mode;
            isEaten = false;
            initialMode = mode;
            points = new Point[10] { tete, p0, p1, p2, p3, p4, p5, p6, p7, p8 };
            

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            drawElement(e.Graphics);
            base.OnPaint(e);
            e.Dispose();
        }

        protected override void drawElement(Graphics g)
        {
            if (isVulnerable)
            {
                if (System.DateTime.Now.Subtract(vulnerableUntil).TotalSeconds > 60 && !isEaten)
                {
                    isVulnerable = false;
                    mode = initialMode;
                }
            }
            
            isEaten = (mode == 'e') ? true : false;
            //corps
            if(!isEaten)g.FillPolygon((isVulnerable) ? Brushes.White : pen, points);
            //Yeux
            g.FillPie(Brushes.Black, 9, 7, 7, 7, 0, 360);
            g.FillPie(Brushes.Black, 20, 7, 7, 7, 0, 360);
            //Popieres
            g.FillPie(Brushes.White, 10, 8, 4, 4, 0, 360);
            g.FillPie(Brushes.White, 21, 8, 4, 4, 0, 360);
            g.Dispose();
        }
        public void makeVulnerable()
        {
            if (!isEaten)
            {
                isVulnerable = true;
                mode = 'f';
                vulnerableUntil = System.DateTime.Now;
            }
            
        }
        public void revive()
        {
            isEaten = false;
            mode = initialMode;
            direction = initialDirection;
            isVulnerable = false;
        }
    }
}
