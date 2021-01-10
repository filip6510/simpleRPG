using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Enemy : GameObject,IFightAble
    {
        private Statistics stats;
        private IFightStrategy strategy;
        public int CurrHP { get; private set; }
        public Enemy(Statistics initStats,IFightStrategy fightStrategy,int xPos, int yPos,string name)
        {
            Name = name;
            if (initStats == null || fightStrategy == null)
                throw new ArgumentNullException();
            stats = new Statistics(initStats);
            strategy = fightStrategy;// połączenie strategi z obiektem
            ObjectPosition = new System.Drawing.Rectangle(xPos, yPos, Global.CharacterSize, Global.CharacterSize);
            CurrHP = stats.MaxHP;
        }
        public int ReciveDemage (int dmg)
        {
            int aux =strategy.ReciveDemage(stats,dmg);
            if (aux < 0)
                return 0;
            CurrHP -= aux;
            return aux;
        }
        public int Attack ()
        {
            return strategy.DoAttack(stats);
        }
        public bool IsAlive()
        {
            return CurrHP > 0;
        }
    }
    internal class MovingEnemy : Enemy,IMoveAble
    {
        public MovingEnemy(Statistics initStats, IFightStrategy fightStrategy, int xPos, int yPos,string name)
           : base(initStats, fightStrategy, xPos, yPos,name)
        { }
        public void Move(IEnumerable<Texture> textures)
        {
            var position =new  Tuple<int, int> (ObjectPosition.X, ObjectPosition.Y ); 
            if (Math.Abs(ObjectPosition.X - Hero.GetInstance().ObjectPosition.X) > Math.Abs(ObjectPosition.Y - Hero.GetInstance().ObjectPosition.Y))
                if (ObjectPosition.X > Hero.GetInstance().ObjectPosition.X)
                    ObjectPosition.X -= Global.MovementSpeed;
                else
                    ObjectPosition.X += Global.MovementSpeed;
            else if (ObjectPosition.Y > Hero.GetInstance().ObjectPosition.Y)
                ObjectPosition.Y -= Global.MovementSpeed;
            else
                ObjectPosition.Y += Global.MovementSpeed;
            foreach (var x in textures)
                if (Collid(x))
                {
                    ObjectPosition.X = position.Item1;
                    ObjectPosition.Y = position.Item2;
                    return;
                }
        }
      
    }
}
