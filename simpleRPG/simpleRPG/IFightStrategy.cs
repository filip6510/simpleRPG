using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    interface IFightStrategy//strategia
    {
        int DoAttack(Statistics stats);
        int ReciveDemage(Statistics stats,int dmg);
       // bool IsUsing();
    }
    class LazyEnemyFightStrategy : IFightStrategy
    {
        bool state;
        public LazyEnemyFightStrategy()
        {
            state = false;
        }
        public int DoAttack (Statistics stats)
        {
            state = !state;
            if (state)
            {
                return  2 * stats.Str + stats.Dex;
            }
            return 0;
        }
        public int ReciveDemage (Statistics stats, int dmg)
        {
            return dmg - stats.Def - stats.Dex;
        }
    }
    class NormalEnemyFightStrategy : IFightStrategy
    {
        public int DoAttack(Statistics stats)
        {
                return 2 * stats.Str + stats.Dex;
        }
        public int ReciveDemage(Statistics stats, int dmg)
        {
            return dmg - stats.Def;
        }
    }
}
