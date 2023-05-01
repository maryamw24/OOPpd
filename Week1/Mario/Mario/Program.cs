using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Game.BL;


namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] maze = new char[17, 212];
            // Mario coordinates
            Mario mario = new Mario();
            mario.X = 12;
            mario.Y = 12;
            char marioDirection = 'l';
            // Minion coordinates
            Minion minion = new Minion();
            minion.X = 50;
            minion.Y = 11;
            char minionDirection = 'l';
            bool minionActive = false;
            bool gamerunning = true;
            int hit = 0;
            // Mario Right array
            char c1 = (char)234;
            char c2 = (char)222;
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
            char[] bulletStatus = new char[100];
            int[] bulletX = new int[100];
            int[] bulletY = new int[100];
            int bulletcount = 0;
            int[] minionBulletStatus = new int[100];
            int[] minionBulletX = new int[100];
            int[] minionBulletY = new int[100];
            char[] minionBulletDirection = new char[100];
            int minionBulletCount = 0;
            LoadMaze(maze);
            PrintMaze(maze);
            int timer = 0;
            PrintMarioRight(MarioRight, MarioX, MarioY, MarioDirection);

        }
    }
}
