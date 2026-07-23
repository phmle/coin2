using System;

namespace CoinCollect
{
    public class Enemy
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        private int cooldown;
        private static readonly Random Rng = new Random();

        public Enemy(int x, int y)
        {
            X = x;
            Y = y;
          //cooldown = 180;
        }

        //enemy move 1 step, when cooldown reach 0
        public void Update(GameState gamestate)
        {
            if (gamestate.IsGameOver) return;

            cooldown--;
            if (cooldown > 0) return;

            cooldown = 15;  

            //random direction
            int d = Rng.Next(4);  //random 0-3
            int nx = X;
            int ny = Y;
            if (d == 0)
            {
                nx++;
            } else if(d == 1)
            {
                nx--;
            }else if(d == 2)
            {
                ny++;
            }else
            {
                ny--;
            }

            if (gamestate.CanEnemyMoveTo(nx, ny))
            {
                X = nx;
                Y = ny;
            }
        }
    }
}
