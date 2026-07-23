using System.Collections.Generic;

namespace CoinCollect
{
    
    public static class coinCollect
    {
        public static int Main(string[] args)
        {
            string mapPath;
            if (args.Length >= 1)
            {
                mapPath = args[0];
            }
            else
            {
                mapPath = "maps/valid.txt";
            }

            //read map
            List<char[]> map = MapLoader.LoadMap(mapPath);

            //map shape and contents are valid
            MapValidator.ValidateMap(map);

            
            GameState game = new GameState(map);
            hubGame.Run(game);

            return 0;
        }
    }
}
