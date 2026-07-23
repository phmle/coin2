using System.Collections.Generic;

namespace CoinCollect
{
    public static class MapValidator
    {
        public static void ValidateMap(List<char[]> map)
        {
            if (map == null || map.Count == 0)
                MapLoader.ErrorExit("Error: Map is empty");

            CheckRectangular(map);
            CheckWalls(map);
            CheckElements(map);
        }

        //every row must have the same length
        private static void CheckRectangular(List<char[]> map)
        {
            int width = map[0].Length;

            for (int i = 0; i < map.Count; i++)
            {
                if (map[i].Length != width)
                    MapLoader.ErrorExit("Error: Map is not rectangular");
            }
        }

        //the outer border of the map must be walls(1)
        private static void CheckWalls(List<char[]> map)
        {
            int height = map.Count;
            int width = map[0].Length;

            for (int x = 0; x < width; x++)
            {
                if (map[0][x] != '1' || map[height - 1][x] != '1')
                    MapLoader.ErrorExit("Error: Map is not enclosed by walls");
            }

            for (int y = 0; y < height; y++)
            {
                if (map[y][0] != '1' || map[y][width - 1] != '1')
                    MapLoader.ErrorExit("Error: Map is not enclosed by walls");
            }
        }

        //must have exactly 1 player, 1 exit, and at least 1 coin
        private static void CheckElements(List<char[]> map)
        {
            int players = 0, exits = 0, coins = 0;

            for (int y = 0; y < map.Count; y++)
                for (int x = 0; x < map[y].Length; x++)
                {
                    char c = map[y][x];
                    if (c == 'P') players++;
                    else if (c == 'E') exits++;
                    else if (c == 'C') coins++;
                    else if (c != '0' && c != '1' && c != 'M')
                        MapLoader.ErrorExit("Error: Invalid map element '" + c + "'");
                }

            if (players != 1 || exits != 1 || coins < 1)
                MapLoader.ErrorExit("Error: Invalid number of map elements (need exactly 1 P, 1 E, at least 1 C)");
        }
    }
}
