using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using EZInput;
using MarioGame.Game.BL;

namespace MarioGame
{
    class Program
    {
        /*static int score = 0;*/
        static int hit = 0;
        static int score = 0;
        static int marioHit = 0;
        static int health = 5;
        static void Main(string[] args)
        {
            char[,] maze = new char[17, 212];
            // Mario coordinates
            Mario mario = new Mario();
            mario.X = 11;
            mario.Y = 9;
            
            // Minion coordinates
            Minion minion = new Minion();
            minion.X = 50;
            minion.Y = 10;

            bool gameRunning = true;
            /*int hit = 0;*/
            char c1 = (char)234;
            char c2 = (char)222;
            /*int hit = 0;*/
            // Mario Right array
            char[,] MarioRight = new char[3, 5] {
                { ' ', c1, ' ', ' ', ' '},
                             { '/', c2, '\\', '-', '>'},
                             { '/', ' ', '\\', ' ', ' '}
            };
            //Mario Left array
            char[,] MarioLeft = new char[3, 5] {
                { ' ', ' ', ' ', c1, ' '},
                            { '<', '-', '/', c2, '\\'},
                            { ' ', ' ', '/', ' ', '\\'}
            };
            // Minion Array
            char[,] Minion = new char[4, 5] {
                { ' ', '=', '=', '=', ' '},
                          { '|', '0', ' ', '0', '|'},
                          { '|', ' ', '_', ' ', '|'},
                          { ' ', '=', '=', '=', ' '}
            };
            List<MarioBullet> marioBullet = new List<MarioBullet>();
            List<MinionBullet> minionBullet = new List<MinionBullet>();
            minion.direction = 'l';
            LoadMaze(maze);
            PrintMaze(maze);
            int timer = 0;
            PrintMarioRight(MarioRight, mario, maze);
            while (gameRunning)
            {
                if(hit == 5)
                {
                    minion.active = false;
                    eraseMinion(minion);
                    hit = 0;
                    score += 1;
                }
                if(marioHit== 5)
                {
                    gameRunning = false;
                }
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    MoveMarioRight(MarioRight, mario, maze);
                }
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {
                    MoveMarioLeft(MarioLeft, mario, maze);
                }
                if (Keyboard.IsKeyPressed(Key.UpArrow))
                {
                    JumpMario(MarioRight, MarioLeft, mario,  maze);
                }
                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    if (mario.Direction == 'r')
                    {
                        generateBulletRight(marioBullet, mario);
                    }
                    else
                    {
                        generateBulletLeft(marioBullet, mario);
                    }
                }
                if (timer == 40 || timer == 60 || timer == 80)
                {
                    generateBulletMinion(minion, mario, minionBullet);
                }
                if (timer == 100)
                {
                    timer = 0;
                }
                if (timer == 50)
                {
                    PrintMinion(Minion, minion, maze);
                }
                MoveMinion(Minion, minion, maze);
                moveMinionBullet(minionBullet, minion, maze);
                moveBullet(marioBullet, maze);
                MoveMarioDown(MarioRight, MarioLeft, mario, maze);
                bulletAndMinion(marioBullet, minion);
                MinionAdnMario(minion, mario);
                Console.SetCursorPosition(20, 20);
                Console.WriteLine("Score: " + score);
                Console.SetCursorPosition(20, 10);
                Console.WriteLine("Health: " + health);
                Thread.Sleep(30);
                timer++;
            }
            Console.WriteLine("Game Over!!");
            Console.WriteLine("Score: " + score);

        }
        static void LoadMaze(char[,] maze)
        {
            string path = "D:\\C#Game\\MarioGame\\Maze.txt";
            if (File.Exists(path))
            {
                char temp;
                int row = 0; int column = 0;
                StreamReader stage = new StreamReader(path);
                do
                {
                    temp = (char)stage.Read();
                    if (temp == '\n')
                    {
                        row++;
                        column = 0;
                    }
                    else
                    {
                        maze[row, column] = temp;
                        column++;
                    }
                }
                while (!stage.EndOfStream);
                stage.Close();

            }
        }
        static void PrintMaze(char[,] maze)
        {
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 212; j++)
                {
                    Console.Write(maze[i, j]);
                }
                Console.WriteLine("");
            }
        }
        static void PrintMarioRight(char[,] MarioRight, Mario mario, char[,] maze)
        {
            int x = mario.X;
            int y = mario.Y;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(MarioRight[i, j]);
                }
                y++;
            }

        }
        static void PrintMarioLeft(char[,] MarioLeft, Mario mario, char[,] maze)
        {
            int x = mario.X;
            int y = mario.Y;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(MarioLeft[i, j]);
                }
                y++;
            }
           
        }
       /* static void MariotoArray(char[,] maze, Mario mario)
        {
            int x = mario.X;
            int y = mario.Y;
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    maze[y+1, x] = 'p';
                }
                x++;
            }
        }*/
        static void EraseMario(Mario mario)
        {
            int x = mario.X;
            int y = mario.Y;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" ");
                }
                y++;
            }
        }
        static void MoveMarioRight(char[,] MarioRight, Mario mario, char[,] maze)
        {
            mario.Direction = 'r';
            if (maze[mario.Y, mario.X + 5] == ' ' && maze[mario.Y + 1, mario.X + 5] == ' ' && maze[mario.Y + 2, mario.X+5] == ' ')
            {
                EraseMario(mario);
                mario.X += 1;
                PrintMarioRight(MarioRight, mario, maze);
                
            }
            Thread.Sleep(50);
        }
        static void MoveMarioLeft(char[,] MarioLeft, Mario mario, char[,] maze)
        {
            mario.Direction = 'l';
            if (maze[mario.Y, mario.X - 1] == ' ' && maze[mario.Y + 1, mario.X - 1] == ' ' && maze[mario.Y + 2, mario.X - 1] == ' ')
            {
                EraseMario(mario);
                mario.X -= 1;
                PrintMarioLeft(MarioLeft, mario, maze);
              
            }
            Thread.Sleep(50);
        }
        static void MoveMarioUp(char[,] MarioRight, char[,] MarioLeft, Mario mario, char[,] maze)
        {
            if (maze[mario.Y - 1, mario.X] == ' ' && maze[mario.Y - 1, mario.X + 1] == ' ' && maze[mario.Y - 1, mario.X + 2] == ' ' && maze[mario.Y - 1, mario.X + 3] == ' ' && maze[mario.Y - 1, mario.X + 4] == ' ')
            {
                EraseMario(mario);
                mario.Y -= 1;
                
                if (mario.Direction == 'r')
                {
                    PrintMarioRight(MarioRight, mario, maze);
                }
                else
                {
                    PrintMarioLeft(MarioLeft, mario, maze);
                }
            }
            Thread.Sleep(50);
        }
        static void MoveMarioDown(char[,] MarioRight, char[,] MarioLeft, Mario mario, char[,] maze)
        {
            if (maze[mario.Y + 3, mario.X] == ' ' && maze[mario.Y + 3, mario.X + 1] == ' ' && maze[mario.Y + 3, mario.X + 2] == ' ' && maze[mario.Y + 3, mario.X + 3] == ' ' && maze[mario.Y + 3, mario.X + 4] == ' ')
            {
                EraseMario(mario);
                mario.Y += 1;
                /*MariotoArray(maze, mario);*/
                if (mario.Direction == 'r')
                {
                    PrintMarioRight(MarioRight, mario, maze);
                }
                else
                {
                    PrintMarioLeft(MarioLeft, mario, maze);
                }
            }
            Thread.Sleep(50);
        }
        static void JumpMario(char[,] MarioRight, char[,] MarioLeft, Mario mario, char[,] maze)
        {
            int jumpcount = 0;
            while (true)
            {
                jumpcount++;
                if (jumpcount <= 3)
                {
                    MoveMarioUp(MarioRight, MarioLeft, mario, maze);
                }
                else if (jumpcount == 5)
                {
                    jumpcount = 0;
                    break;
                }
            }
        }

        static void PrintMinion(char[,] Minion, Minion minion, char[,] maze)
        {
            minion.active = true;
            int x = minion.X;
            int y = minion.Y;
            for(int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(x, y);
                for(int j = 0; j <5;j++)
                {
                    Console.Write(Minion[i, j]);
                }
                y++;
             }
            

        }
        static void MiniontoArray(char[,] maze, Minion minion)
        {
            int x = minion.X;
            int y = minion.Y;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    maze[y+1, x] = 'k';
                }
                y++;
            }
        }
        static void eraseMinion(Minion minion)
        {
            int x = minion.X;
            int y = minion.Y;
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(x, y);
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(" ");
                }
                y++;
            }
        }
        static void MoveMinion(char[,] Minion, Minion minion,char[,] maze)
        {
            if(minion.active == true)
            {
                if (minion.direction == 'l')
                {
                    if (maze[minion.Y, minion.X - 1] == ' ' && maze[minion.Y - 1, minion.X - 1] == ' ' && maze[minion.Y - 2, minion.X - 1] == ' ' && maze[minion.Y - 3, minion.X - 1] == ' ')
                    {
                        eraseMinion(minion);
                        minion.X--;
                        PrintMinion(Minion, minion, maze);
                    }
                    else
                    {
                        minion.direction = 'r';
                    }
                }
                if (minion.direction == 'r')
                {
                    if (maze[minion.Y, minion.X + 4] == ' ' && maze[minion.Y +1, minion.X + 4] == ' ' && maze[minion.Y + 2, minion.X + 4] == ' ' && maze[minion.Y + 3, minion.X + 4] == ' ')
                    {
                        eraseMinion(minion);
                        minion.X++;
                        PrintMinion(Minion, minion, maze);
                    }
                    else
                    {
                        minion.direction = 'l';
                    }
                }
            }

        }
        static void generateBulletRight(List<MarioBullet> marioBullet, Mario mario)
        {
            
            char direction = 'r';
            int X = mario.X + 5;
            int Y= mario.Y + 1;
            MarioBullet b = new MarioBullet(X, Y, direction);
            Console.SetCursorPosition(b.X, b.Y);
            Console.Write("-");
            marioBullet.Add(b);
        }
static void generateBulletLeft(List<MarioBullet> marioBullet, Mario mario)
        {
            char direction = 'l';
            int X = mario.X - 1;
            int Y = mario.Y + 1;
            MarioBullet b = new MarioBullet(X, Y, direction);
            Console.SetCursorPosition(mario.X - 1, mario.Y + 1);
            Console.Write("-");
            marioBullet.Add(b);
        }
        static void generateBulletMinion(Minion minion, Mario mario, List<MinionBullet> minionBullet)
        {
            if (minion.active == true)
            {
                if (mario.X < minion.X)
                {
                    
                    char direction= 'l';
                    int X= minion.X - 1;
                    int Y = minion.Y + 1;
                    MinionBullet m = new MinionBullet(X, Y, direction);
                    printMinionBullet(m.X, m.Y);
                    minionBullet.Add(m);
                }
                else  if (mario.X > minion.X)
                {
                    char direction = 'r';
                    int X = minion.X + 6;
                    int Y = minion.Y + 1;
                    MinionBullet m = new MinionBullet(X, Y, direction);
                    printMinionBullet(m.X, m.Y);
                    minionBullet.Add(m);
                }
            }
        }
static  void printMinionBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(".");
        }
static void printBullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("-");
        }
static  void erasebullet(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(" ");
        }
        static void moveBullet(List<MarioBullet> marioBullet, char[,] maze)
        {
            for (int x = 0; x < marioBullet.Count; x++)
            {
                if (marioBullet[x].direction== 'r')
                {

                    if (maze[marioBullet[x].Y , marioBullet[x].X + 1] !=' ')
                    {
                        erasebullet(marioBullet[x].X, marioBullet[x].Y);
                        removeBulletFromArray(marioBullet, x);
                    }
                    else
                    {
                        erasebullet(marioBullet[x].X, marioBullet[x].Y);
                        marioBullet[x].X = marioBullet[x].X + 1;
                        printBullet(marioBullet[x].X, marioBullet[x].Y);
                    }
                }
                else
                {
                    if (maze[marioBullet[x].Y, marioBullet[x].X - 1] != ' ')
                    {
                        erasebullet(marioBullet[x].X, marioBullet[x].Y);
                        removeBulletFromArray(marioBullet, x);
                    }
                    else
                    {
                        erasebullet(marioBullet[x].X, marioBullet[x].Y);
                        marioBullet[x].X = marioBullet[x] .X - 1;
                        printBullet(marioBullet[x].X, marioBullet[x].Y);
                    }
                }
            }
        }
 static void moveMinionBullet(List<MinionBullet> minionBullet,Minion minion, char[,] maze)
        {
            if (minion.active == true)
            {
                for (int i = 0; i < minionBullet.Count; i++)
                {
                    if (minionBullet[i].direction== 'l')
                    {
                        if (maze[minionBullet[i].Y, minionBullet[i].X -1] != ' ')
                        {
                            erasebullet(minionBullet[i].X, minionBullet[i].Y);
                            removeMinionBulletFromArray(minionBullet, i);
                        }
                        else
                        {
                            erasebullet(minionBullet[i].X, minionBullet[i].Y);
                            minionBullet[i].X = minionBullet[i].X - 1;
                            printMinionBullet(minionBullet[i].X, minionBullet[i].Y);
                        }
                    }
                    else if (minionBullet[i].direction == 'r')
                    {

                        if (maze[minionBullet[i].Y, minionBullet[i].X + 1] != ' ')
                        {
                            erasebullet(minionBullet[i].X, minionBullet[i].Y);
                            removeMinionBulletFromArray(minionBullet, i);
                        }
                        else
                        {
                            erasebullet(minionBullet[i].X, minionBullet[i].Y);
                            minionBullet[i].X = minionBullet[i].X + 1;
                            printMinionBullet(minionBullet[i].X, minionBullet[i].Y);
                        }
                    }
                }
            }
        }
static  void removeBulletFromArray( List<MarioBullet> marioBullet, int index)
        {
            marioBullet.RemoveAt(index);
        }
 static void removeMinionBulletFromArray(List<MinionBullet> minionBullet, int index)
        {
            minionBullet.RemoveAt(index);
        }

        static void bulletAndMinion(List<MarioBullet> marioBullet, Minion minion)
        {
            int MinionStartX = minion.X;
            int MinionEndX = minion.X + 4;
            int MinionStartY = minion.Y;
            int MinionEndY = minion.Y + 3;
            for(int col = MinionStartY; col<= MinionEndY; col++)
            {
                for(int row = MinionStartX;row <= MinionEndX; row++)
                {
                    for(int i = 0; i < marioBullet.Count; i++)
                    {
                        if(row == marioBullet[i].X && col == marioBullet[i].Y)
                        {
                            hit++;
                            erasebullet(marioBullet[i].X, marioBullet[i].Y);
                            removeBulletFromArray(marioBullet, i);
                        }
                        
                      
                    }
                }
            }
        }
        static void bulletAndMario(List<MinionBullet> minionBullet, Mario mario)
        {
            int MarioStartX = mario.X;
            int MarioEndX = mario.X + 3;
            int MarioStartY = mario.Y;
            int MarioEndY = mario.Y + 3;
            for (int col = MarioStartY; col <= MarioEndY; col++)
            {
                for (int row = MarioStartX; row <= MarioEndX; row++)
                {
                    for (int i = 0; i < minionBullet.Count; i++)
                    {
                        if (row == minionBullet[i].X && col == minionBullet[i].Y)
                        {
                            marioHit++;
                            erasebullet(minionBullet[i].X, minionBullet[i].Y);
                            removeMinionBulletFromArray(minionBullet, i);
                            
                        }


                    }
                }
            }
           
        }
        static void MinionAdnMario(Minion minion, Mario mario)
        {
            int MarioStartX = mario.X;
            int MarioEndX = mario.X + 3;
            int MarioStartY = mario.Y;
            int MarioEndY = mario.Y + 3;
            for (int col = MarioStartY; col <= MarioEndY; col++)
            {
                for (int row = MarioStartX; row <= MarioEndX; row++)
                {
                    
                            if (row == minion.X && col == minion.Y)
                            {
                                if (health > 0)
                                {
                                    health--;
                                }
                                else
                                {
                                    health = 5;
                                }
                             

                            }
                        


                    
                }
            }
        }
    }
}

