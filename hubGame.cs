using Raylib_cs;

namespace CoinCollect
{
    //raylib window. input, rendering
    public static class hubGame
    {
        private const int TileSize = 48;

        public static void Run(GameState gamestate)
        {
            int w = gamestate.Width * TileSize;
            int h = gamestate.Height * TileSize;

            Raylib.InitWindow(w, h, "CoinCollect");
            Raylib.SetTargetFPS(30);

            while (!Raylib.WindowShouldClose() && !gamestate.IsGameOver)
            {
                HandleInput(gamestate);
                gamestate.UpdateEnemies();
                gamestate.CheckEnemyCollision();

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.DarkGray);
                DrawMap(gamestate);
                DrawPlayer(gamestate);
                DrawEnemies(gamestate);
                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }

        private static void HandleInput(GameState gamestate)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.W) || Raylib.IsKeyPressed(KeyboardKey.Up))
                gamestate.ExecuteMove(gamestate.PlayerX, gamestate.PlayerY - 1);
            else if (Raylib.IsKeyPressed(KeyboardKey.S) || Raylib.IsKeyPressed(KeyboardKey.Down))
                gamestate.ExecuteMove(gamestate.PlayerX, gamestate.PlayerY + 1);
            else if (Raylib.IsKeyPressed(KeyboardKey.A) || Raylib.IsKeyPressed(KeyboardKey.Left))
                gamestate.ExecuteMove(gamestate.PlayerX - 1, gamestate.PlayerY);
            else if (Raylib.IsKeyPressed(KeyboardKey.D) || Raylib.IsKeyPressed(KeyboardKey.Right))
                gamestate.ExecuteMove(gamestate.PlayerX + 1, gamestate.PlayerY);
            else if (Raylib.IsKeyPressed(KeyboardKey.Escape))
                gamestate.Quit();
        }

        private static void DrawMap(GameState gamestate)
        {
            for (int y = 0; y < gamestate.Height; y++)
                for (int x = 0; x < gamestate.Width; x++)
                {
                    int px = x * TileSize;
                    int py = y * TileSize;
                    char tile = gamestate.Map[y][x];

                    //draw floor or wall
                    if (tile == '1')
                        Raylib.DrawRectangle(px, py, TileSize, TileSize, Color.Brown);
                    else
                    {
                        Raylib.DrawRectangle(px, py, TileSize, TileSize, Color.DarkGreen);
                        Raylib.DrawRectangle(px + 2, py + 2, TileSize - 4, TileSize - 4, Color.Green);
                    }

                    //draw coin or exit
                    if (tile == 'C')
                        Raylib.DrawCircle(px + TileSize / 2, py + TileSize / 2, TileSize * 0.2f, Color.Yellow);
                    else if (tile == 'E')
                    {
                        Color c;
                        if (gamestate.CollectCount == 0) c = Color.Green;
                        else c = Color.Red;

                        string exitText;
                        if (gamestate.CollectCount == 0) exitText = "EXIT";
                        else exitText = "LOCK";

                        Raylib.DrawRectangle(px + 4, py + 4, TileSize - 8, TileSize - 8, c);
                        Raylib.DrawText(exitText, px + 10, py + TileSize / 2 - 8, 12, Color.White);
                    }
                }
        }

        private static void DrawPlayer(GameState gamestate)
        {
            int px = gamestate.PlayerX * TileSize + TileSize / 2;
            int py = gamestate.PlayerY * TileSize + TileSize / 2;
            Raylib.DrawCircle(px, py, TileSize * 0.3f, Color.Red);
        }

        private static void DrawEnemies(GameState gamestate)
        {
            foreach (Enemy enemy in gamestate.Enemies)
            {
                int px = enemy.X * TileSize + TileSize / 2;
                int py = enemy.Y * TileSize + TileSize / 2;
                Raylib.DrawCircle(px, py, TileSize * 0.3f, Color.Blue);
            }
        }

    }
}
