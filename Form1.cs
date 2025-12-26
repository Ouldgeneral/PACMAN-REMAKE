using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace ProjetClasse
{
    public partial class PlayGround : Form 
    {
        
        static int HEIGHT;//longueur
        static int WIDTH;//largeur
        static int PIXEL_SIZE=32;//taille pixel a occuppe
        int currentLevel;//le niveau actuel
        int foodNumber;//nombre de nouriture
        Levels levels;//les niveaux de jeux
        Score score;//score
        Pacman pacman;//joueur pacman
        Ghost red;//enemi phantome rouge
        Ghost blue;//enemi phantom bleu
        Ghost orange;//enemi phantom orange
        Ghost pink;//enemi phantom rose
        Timer timer;//timer pour la mise a jour graphique
        char[][] map;//carte charge selon le niveau
        Food[,] foods;//nouriture pacman
        Lives lives;//nombre de vie du joueur
        Dictionary<char, char> inverseDirections;
        Dictionary<char, Ghost> ghosts;
        SoundPlayer interMission = new SoundPlayer(Sounds.pacman_intermission);
        
        SoundPlayer gameStart = new SoundPlayer(Sounds.pacman_beginning);
        
        bool playing;
        Play play;
        public PlayGround()
        {


            /**
             * Pacman Remake de Malick Ould Hamdi et Khene Rafik 
             * Ce remake est presente comme projet de fin de semestre 2 de la specialite DMPF a l'iteem
             * Cette copie de code est la seule copie originale du travail demender 
             * Toute modifiaction dans le code qui entrainent son disfonctionnement n'est pas notre responsabilite
             * Alger le 5/12/2025
             * */
            //initialisation des composantes
            inverseDirections = new Dictionary<char, char>();
            inverseDirections.Add('u', 'd');
            inverseDirections.Add('r', 'l');
            inverseDirections.Add('d', 'u');
            inverseDirections .Add('l', 'r');
            currentLevel = 2;
            play = new Play();
            levels = new Levels();//initialisations des niveaux
            lives = new Lives(10);//nombre de vie du joueur
            score = new Score(0);//score par defaut 0
            buildMap();//Construction initial de la carte
            InitializeComponent();
            this.KeyPreview = true;
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            //timer.Start();
            
        }
        
        private void buildMap()
        {
            ghosts = new Dictionary<char, Ghost>();
            play.SetBounds(280, 352, 150, 16);
            Controls.Add(play);
            playing = false;
            gameStart.Play();
            map = levels.getLevel(currentLevel);
            levels.level = currentLevel;
            WIDTH = map[0].Length;//recuperation de la largeur de la carte map
            HEIGHT = map.Length;//recuperation de la longueur de la carte map
            foodNumber = 0;
            foods = new Food[HEIGHT, WIDTH];//matrices de la nouriture 
            lives.lives = lives.initialLives;
            score.score = score.initialScore;
            lives.SetBounds(PIXEL_SIZE*(WIDTH-lives.lives), 0, 16*(lives.lives), 16);
            Controls.Add(lives);
            score.SetBounds(0, 0, 60, 15);
            score.BackColor = Color.Brown;
            Controls.Add(score);
            levels.SetBounds(0, 16, 60, 15);
            levels.BackColor = Color.Brown;
            Controls.Add(levels);
            char thing;
            //Emplacement des caracteres principales d'abord (phantomes et Pacman)
            //Emplacement des murs qui permettront de controler le deplacement des caracteres et de la nourriture pour pacman
            for (int i = 0; i < HEIGHT; i++)
            {
                for (int j = 0; j < WIDTH; j++)
                {
                    thing = map[i][j];
                    if (thing == 'o')
                    {
                        orange = new Ghost(Brushes.Orange, 's',false, j, i, 'o', 'r');
                        orange.Location = new Point(j * PIXEL_SIZE, i * PIXEL_SIZE);
                        orange.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(orange);
                        ghosts.Add('o', orange);
                    }
                    else if (thing == 'p')
                    {

                        pink = new Ghost(Brushes.Pink,'c', false, j, i, 'p', 'u');
                        pink.Location = new Point(j * PIXEL_SIZE, i * PIXEL_SIZE);
                        pink.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(pink);
                        ghosts.Add('p', pink);
                    }
                    else if (thing == 'b')
                    {
                        blue = new Ghost(Brushes.Blue,'c', false, j, i, 'b', 'd');
                        blue.Location = new Point(j * PIXEL_SIZE, i * PIXEL_SIZE);
                        blue.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(blue);
                        ghosts.Add('b', blue);
                    }
                    else if (thing == 'r')
                    {
                        red = new Ghost(Brushes.Red,'s', false, j, i, 'r', 'l');
                        red.Location = new Point(j * PIXEL_SIZE, i * PIXEL_SIZE);
                        red.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(red);
                        ghosts.Add('r', red);
                    }
                    else if (thing == 'P')
                    {
                        pacman = new Pacman(j, i, 'P', 'l', true);
                        pacman.Location = new Point(j * PIXEL_SIZE, i * PIXEL_SIZE);
                        pacman.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(pacman);
                    }
                    else if (thing == 'X')
                    {
                        Wall wall = new Wall();
                        wall.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);
                        Controls.Add(wall);
                    }
                    else if (thing == 'v' || thing=='V')
                    {
                        Food food = new Food(thing=='v'?5:10,j,i);

                        food.SetBounds(j * PIXEL_SIZE, i * PIXEL_SIZE, PIXEL_SIZE, PIXEL_SIZE);

                        Controls.Add(food);
                        foods[i, j] = food;
                        foodNumber++;
                    }

                }
            }
            if(pacman!=null)pacman.BringToFront();
            if (pink != null) pink.BringToFront();
            if (blue != null) blue.BringToFront();
            if (orange != null) orange.BringToFront();
            if (red != null) red.BringToFront();
        }
        private void nextLevel()
        {
            playing = false;
            interMission.Play();
            timer.Stop();
            DialogResult r = MessageBox.Show("Vous avez gagné votre score est:" + score.score + "\nVoulez vous jouer au niveau suivant","Félicitations",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                score.initialScore = score.score;
                currentLevel++;
                
            }else if (r != DialogResult.No)
            {
                this.Close();
            }
            playing = true;
            Controls.Clear();
            buildMap();
            timer.Start();
            
        }
        private void reInitialiseElement(Element element)
        {
            map[element.posY][element.posX]='v';
            element.reInitialise();
            element.Location = new Point(element.posX * PIXEL_SIZE, element.posY * PIXEL_SIZE);

        }
        private void gameOver()
        {
            
            playing = false;
            pacman.die();
            timer.Stop();
            DialogResult r = MessageBox.Show("Vous avez perdu votre score est:" + score.score + " \nVoulez vous rejouer", "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                reInitialiseElement(pink);
                reInitialiseElement(red);
                reInitialiseElement(blue);
                reInitialiseElement(orange);
                reInitialiseElement(pacman);
                foreach (Food food in foods)
                {
                    if (food == null) continue;
                    int x = food.x;
                    int y = food.y;
                    map = levels.getLevel(currentLevel);
                    if (!Controls.Contains(food))
                    {
                        Controls.Add(food);
                    }
                }
                score.score = 0;
                lives.lives = lives.initialLives;
                timer.Start();
            }
            else
            {
                this.Close();
            }
        }
        private void Timer_Tick(object sender,EventArgs e)
        {
            if (lives.lives <= 0)
            {
                gameOver();
            }
            else if (foodNumber <= 0) nextLevel();
            if (!playing)
            {
                play.SetBounds(280, 352, 150, 15);
                Controls.Add(play);
                play.BringToFront();
                timer.Stop();
                return;
            }
            
            move(pink, moveGhost(pink));
            move(red, moveGhost(red));
            move(blue, moveGhost(blue));
            move(orange, moveGhost(orange));
            move(pacman, pacman.direction);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (lives.lives <= 0)
            {
                gameOver();
            }
            char direction=pacman.direction;
            switch (e.KeyCode)
            {
                case Keys.Space:
                    playing = !playing;
                    if (playing)
                    {
                        
                        Controls.Remove(play);
                        timer.Start();
                    }
                    else
                    {
                        
                        play.SetBounds(280, 352, 150, 15);
                        Controls.Add(play);
                        play.BringToFront();
                        timer.Stop();
                    }
                    break;
                case Keys.Up:
                    direction = 'u';
                    break;
                case Keys.Down:
                    direction = 'd';
                    break;
                case Keys.Left:
                    direction = 'l';
                    break;
                default:
                    direction = 'r';
                    break;
            }
            if (playing)move(pacman, direction);
            
            
        }
        private void makeGhostsVulnerable()
        {
            pacman.isVulnerable = false;
            if (pink != null)
            {
                pink.makeVulnerable();
                pink.Refresh();
            }
            if (blue != null)
            {
                blue.makeVulnerable();
                blue.Refresh();
            }
            if (red != null)
            {
                red.makeVulnerable();
                red.Refresh();
            }
            if (orange != null)
            {
                orange.makeVulnerable();
                orange.Refresh();
            }
        }
        private void die(Element element)
        {
            if (element.symbol != 'P')
            {
                if (!element.isVulnerable) return;
                killGhost((Ghost)element);
                return;
            }
            lives.lives--;
            lives.SetBounds(PIXEL_SIZE * (WIDTH - lives.lives), 0, 16 * (lives.lives), 16);
            lives.Refresh();
            map[element.posY][element.posX] = 'v';
            element.posX = element.initialPosition.X;
            element.posY = element.initialPosition.Y;
            element.isVulnerable = false;
            element.direction = element.initialDirection;
            element.Location = new Point(element.posX * PIXEL_SIZE, element.posY * PIXEL_SIZE);
        }
        private void killGhost(Ghost ghost)
        {
            pacman.eatGhost();
            score.score += 50;
            ghost.mode = 'e';
            swapPlaces(ghost, pacman);
            ghost.Location = new Point(ghost.posX * PIXEL_SIZE, ghost.posY * PIXEL_SIZE);
            
        }
        private bool moveElement(Element element, char direction)
        {
            int x = element.posX;
            int y = element.posY;
            switch (direction)
            {
                case 'u':y = v(y - 1);break;
                case 'd':y = v(y + 1);break;
                case 'l':x = h(x - 1);break;
                case 'r':x = h(x + 1);break;
            }
            char thing = map[y][x];
            if (thing == 'v' || thing == 'V')
            {
                Food food = foods[element.posY, element.posX];

                if (element.symbol == 'P')
                {
                    if (Controls.Contains(food))
                    {
                        if (thing == 'V')
                        {
                            makeGhostsVulnerable();
                            score.score += 10;
                            pacman.eatFruit();
                        }
                        else
                        {
                            score.score++;
                        }
                        foodNumber--;
                        score.Refresh();
                        Controls.Remove(food);
                    }
                }
                map[element.posY][element.posX] = (element.symbol == 'P') ? 'v' : thing;
                map[y][x] = element.symbol;
                element.update(x,y,direction);
            }
            else if (thing != 'X')
            {
                if (element.symbol == 'P')
                {
                    if (ghosts[thing].isVulnerable) killGhost(ghosts[thing]);
                    else
                    {
                        element.isVulnerable = true;
                        die(element);
                    }
                }
                else
                {
                    if (element.isVulnerable && thing == 'P') die(element);
                    else if (!element.isVulnerable && thing == 'P') die(pacman);
                    else return false;
                }
            }
            else return false;
            return true;


        }
        private void swapPlaces(Element element1,Element element2)
        {
            map[element1.posY][element1.posX] = 'v';
            element1.posY = element2.lastY;
            element1.posX = element2.lastX;
            map[element1.posY][element1.posX] = element1.symbol;
        }
        private void move(Element element,char? direction)
        {
            if (element == null || direction==null) return;
            bool moving=moveElement(element,direction.Value);
            if (!moving) return;
            element.Refresh();
            element.Location = new Point(element.posX * PIXEL_SIZE, element.posY * PIXEL_SIZE);
            this.Invalidate();
        }
        char? moveGhost(Ghost ghost)
        {
            //cette methode est responsable du mouvement des phantomes intelligemment selon le mode elle recoit 4 mode s,c,f,e
            //Elle est le 100% du cerveau du phantome
            //le mode s=scatter est le mode de promenade normale dans ce mode le phantome va juste se balader dans le jeu 
            //il n'as pas pacman comme enemmi mais si pacman se dirige vers lui en ce mode pacman sera mort
            //le mode f=freightened mode effraye le phantome va essayer de s'eloigner de pacman
            //la logique de cette mode est de calculer toutes les distances qui mene a pacman et choisir la plus longue pour simuler la fuite
            //le mode c=chase mode chasse le phantome va essayer de se rapprocher de pacman pour le tuer
            //la logique de cette mode est de calculer toutes les distances qui mene a pacman et choisir la plus courte pour simuler la chasse de pacman
            //le mode e=eaten c'est le mode mort du phantome il va essayer de revivre
            //la logique de cette mode est de calculer toutes les distances qui mene a la position initial du phantome et choisir la plus courte pour simuler la resurrection
            if (ghost == null || pacman == null) return null;
            int targetX = (ghost.mode == 'e') ? ghost.initialPosition.X : pacman.posX;
            int targetY = (ghost.mode == 'e') ? ghost.initialPosition.Y : pacman.posY;
            int x = ghost.posX;
            int y = ghost.posY;
            double? distance;
            char mode = ghost.mode;
            Dictionary<double, char> posssibleDirections = new Dictionary<double, char>();
            SortedDictionary<double, char> sortedDirections;// = new SortedDictionary<double, char>(posssibleDirections);
            char direction = ghost.direction;
            foreach( char direction1 in inverseDirections.Keys)
            {
                //eviter d'enregistrer les directions inverse pour ne pas tomber dans une boucle infini de mouvement entre deux points
                if (inverseDirections[direction1]==direction) continue;
                distance = calculateDistance(x, y, targetX, targetY, direction1, mode);
                if (distance != null)
                {
                    if (mode == 's') return direction1;//mode s
                    posssibleDirections[distance.Value] = direction1;
                }
            }
            if (posssibleDirections.Count == 0)
            {
                return inverseDirections[direction];
            }
            sortedDirections = new SortedDictionary<double, char>(posssibleDirections);
            if (sortedDirections.First().Key < 2 && ghost.mode == 'e')//mode e
            {
                ghost.revive();
            }
            if (ghost.mode == 'f') return sortedDirections.Last().Value;//mode f
            return sortedDirections.First().Value;//mode e,c
        }
       
        //calcule la distance entre deux points
        private double? calculateDistance(int x1,int y1,int x2,int y2,char direction,char mode)
        {
            switch (direction)
            {
                case 'u':y1 = v(y1 - 1);break;
                case 'd':y1 = v(y1 + 1);break;
                case 'l':x1 = h(x1 - 1);break;
                case 'r':x1 = h(x1 + 1);break;

            }
            if (map[y1][x1] == 'P' && mode == 'c') die(pacman);
            if (map[y1][x1] != 'v' && map[y1][x1] != 'V') return null;
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        //Calcule un emplacement verticale
        private int v(int cell)
        {
            return (HEIGHT + cell) % HEIGHT;
        }
        //calcule un emplacement horizontale
        private int h(int cell)
        {
            return (WIDTH + cell) % WIDTH;
        }
    }
}
