using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarioGame.Game.BL
{
    class Mario
    {
        public int X;
        public int Y;
        public char Direction;
       
    }
    class Minion
    {
        public int X;
        public int Y;
        public bool active;
        public char direction;
    }
    class MarioBullet
    {
        public int X;
        public int Y;
        public char direction;
        public MarioBullet(int X, int Y, char direction)
        {
            this.X = X;
            this.Y = Y;
            this.direction = direction;
        }
    }
    class MinionBullet
    {
        public int X;
        public int Y;
        public char direction;
       public MinionBullet(int X,int Y,char direction)
        {
            this.X = X;
            this.Y = Y;
            this.direction = direction;
        }
    }
}
