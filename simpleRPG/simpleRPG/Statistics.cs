using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Statistics
    {
        public int Str { get; set; }
        public int Dex { get; set; }
        public int Int { get; set; }
        public int Def { get; set; }
        public int MaxHP { get; set; }
        public Statistics (int strenght, int dexterity, int inteligence, int defence, int health)
        {
            Str = strenght;
            Dex = dexterity;
            Int = inteligence;
            Def = defence;
            MaxHP = health;
        }
        public Statistics (Statistics toCopy)
        {
            Str = toCopy.Str;
            Dex = toCopy.Dex;
            Int = toCopy.Int;
            Def = toCopy.Def;
            MaxHP = toCopy.MaxHP;
        }
    }
}
