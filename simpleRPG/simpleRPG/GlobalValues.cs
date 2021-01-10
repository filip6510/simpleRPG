using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    static class Global
    {
        public const int GameWidth = 600;
        public const int GameHeight = 300;
       // private static Location[][] GameMap;
        public const  int CharacterSize = 20;
        public const int ItemSize = 10;
        public const int MovementSpeed = 2;
        public static void Swap<T>(T a, T b)
        {
            T aux = a;
            a = b;
            b = aux;
        }
        public static int LevelDiffrence(int level, int exp)
        {
            return exp / 100 - level;
        }
        public static int PointsPerLevel()
        {
            return 2;
        }

    }
}
