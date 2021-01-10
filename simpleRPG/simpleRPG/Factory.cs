using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Factory
    {
        public static Enemy CreateEasyNMEnemy (int x, int y, string name)
        {
            return new Enemy(CreateStatisticsForEasyEnemy(), new NormalEnemyFightStrategy(), x, y, name);
        }
        public static Enemy CreateEasyMEnemy(int x, int y, string name)
        {
            return new MovingEnemy(CreateStatisticsForEasyEnemy(), new LazyEnemyFightStrategy(), x, y, name);
        }
        public static Enemy CreateHardNMEnemy(int x, int y, string name)
        {
            return new Enemy(CreateStatisticsForHardEnemy(), new NormalEnemyFightStrategy(), x, y, name);
        }
        public static Statistics CreateStatisticsForEasyEnemy()
        {
            return new Statistics(1, 1, 1, 1, 20);
        }
        public static Statistics CreateStatisticsForHardEnemy()
        {
            return new Statistics(3, 3, 3, 3, 40);
        }
        public static Location CreateLocation (string name)
        {
            Location loc = new Location(name);
            loc.AddObject(new MapEdge(-5, -5, Global.GameWidth + 10, 4, null,"top"));
            loc.AddObject(new MapEdge(-5, -5, 4,Global.GameHeight + 10 , null,"left"));
            loc.AddObject(new MapEdge(-5, Global.GameHeight + 5, Global.GameWidth + 10, 4, null, "bot"));
            loc.AddObject(new MapEdge(Global.GameWidth + 5, -5, 4, Global.GameHeight  +10 , null,"right"));
            loc.Textures.Add(new Texture(-10, -10, Global.GameWidth + 20, 4));
            loc.Textures.Add(new Texture(-10, -10, 4, Global.GameHeight + 20));
            loc.Textures.Add(new Texture(Global.GameWidth + 15, -10, 4, Global.GameHeight + 20));
            loc.Textures.Add(new Texture(-10, Global.GameHeight + 10, Global.GameWidth + 20, 4));
            return loc;
        }

    }
}
