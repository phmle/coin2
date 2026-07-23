using System.Collections.Generic;

namespace CoinCollect
{
    //map, player position, coin count, enemies
    public class GameState
    {
        public List<char[]> Map { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public int PlayerX { get; private set; }
        public int PlayerY { get; private set; }
        public int StartX { get; private set; }
        public int StartY { get; private set; }
        public int CollectCount { get; private set; }
        public int Lives { get; private set; }

        public List<Enemy> Enemies { get; private set; }

        public bool IsGameOver { get; private set; }

        public GameState(List<char[]> map)
        {
            Map = map;
            Height = map.Count;
            Width = map[0].Length;
            Lives = 3;
            Enemies = new List<Enemy>();

            //find player, count coins, enemies
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (Map[y][x] == 'P')
                    {
                        PlayerX = x; PlayerY = y;
                        StartX = x; StartY = y;
                    }
                    else if (Map[y][x] == 'C')
                    {
                        CollectCount++;
                    }
                    else if (Map[y][x] == 'M')
                    {
                        Enemies.Add(new Enemy(x, y));
                        Map[y][x] = '0';
                    }
                }
        }

        //moving
        public void ExecuteMove(int newX, int newY)
        {
          
            char target = Map[newY][newX];

                if (target == '1') {
                    return;
                }

            if (target == 'E' && CollectCount == 0)
            {
                PlayerX = newX; PlayerY = newY;
                IsGameOver = true;
                return;
            }

            if (target == 'E')
            {
                return;
            }

            if (target == 'C')  //collect coin
            {
                CollectCount--;
                Map[newY][newX] = '0';
            }

            PlayerX = newX; PlayerY = newY;
        }

        //can enemy walk on this tile
        public bool CanEnemyMoveTo(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height) return false;

            char tile = Map[y][x];
            if (tile != '0' && tile != 'C') return false;

            foreach (Enemy enemy in Enemies)
                if (enemy.X == x && enemy.Y == y) return false;

            if (PlayerX == x && PlayerY == y) return false;

            return true;
        }

        //move all enemies
        public void UpdateEnemies()
        {
            foreach (Enemy enemy in Enemies)
                enemy.Update(this);
        }

        //if hit enemy, lose a life or game over
        public void CheckEnemyCollision()
        {
            if (IsGameOver) return;

            foreach (Enemy enemy in Enemies)
            {
                if (enemy.X == PlayerX && enemy.Y == PlayerY)
                {
                    Lives--;
                    if (Lives <= 0) IsGameOver = true;
                    else { PlayerX = StartX; PlayerY = StartY; }
                    return;
                }
            }
        }

        public void Quit() { IsGameOver = true; }
    }
}
