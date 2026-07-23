using System;
using System.Collections.Generic;
using System.IO;

namespace CoinCollect
{
    //read .txt map file 
    public static class MapLoader
    {
        public static List<char[]> LoadMap(string path)
        {
            if (!File.Exists(path))
            {
                ErrorExit("Error: not exist '" + path + "'");
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(path);
            }
            catch (IOException)
            {
                ErrorExit("Error: Could not read map file '" + path + "'");
                return null;
            }

            //each non-empty line becomes one row in the map grid
            List<char[]> map = new List<char[]>();
            foreach (string rawLine in lines)
            {
                string line = rawLine;
                if (line.Length == 0)
                {
                    continue;
                }
                map.Add(line.ToCharArray());
            }

            if (map.Count == 0)
            {
                ErrorExit("Error: Map is empty");
            }

            return map;
        }

        //print error and stop the program
        public static void ErrorExit(string message)
        {
            if (message == null)
            {
                Console.Error.WriteLine("Error");
            }
            else
            {
                Console.Error.WriteLine(message);
            }
            Environment.Exit(1);
        }
    }
}
