using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace simpleRPG 
{
    class Hero : GameObject, IFightAble, IMoveAble
    {
        public int SelectIndex { get; private set; }
        int selectMax;
        public Statistics Stats { get; private set; }
        private int level;
        private int exp;
        public int UpgradePoints { get; private set; }
        public int CurrHP { get; private set; }
        public int Money { set; get; }
        private WearableItem currWepon;
        private WearableItem currArmor;
        public List<Item> Items { get; private set; }
        private Hero()
        {
            Reset();
        }
        private static Hero instance;
        public void Reset()
        {
            Stats = new Statistics(3, 3, 0, 2, 20);
            CurrHP = Stats.MaxHP;
            level = 0;
            UpgradePoints = 3;
            ObjectPosition = new System.Drawing.Rectangle(0, 0, Global.CharacterSize, Global.CharacterSize);
            currWepon = new WearableItem("patyk", 1, new Statistics(0, 0, 0, 0, 0), ItemType.Wepon);
            currArmor = new WearableItem("łachamny", 1, new Statistics(0, 0, 0, 0, 0), ItemType.Armor);
            exp = 0;
            Items = new List<Item>();
            Name = "HERO";
        }
        public void SetSelect(int max)
        {
            SelectIndex = 0;
            selectMax = max;
        }
        public void ChangeSelect()
        {
            if (KeyInput.IsPressed(Keys.Down) && SelectIndex + 1 < selectMax)
            {
                KeyInput.SetKeyState(Keys.Down, false);
                SelectIndex++;
            }
            if (KeyInput.IsPressed(Keys.Up) && SelectIndex > 0)
            {
                KeyInput.SetKeyState(Keys.Up, false);
                SelectIndex--;
            }
        }
        public static Hero GetInstance()
        {
            if (instance == null)
                instance = new Hero();
            return instance;
        }
        public void AddExp(int value)
        {
            exp += value;
            if (Global.LevelDiffrence(level, exp) > 0)
            {
                UpgradePoints += Global.LevelDiffrence(level, exp) * Global.PointsPerLevel();
                level += Global.LevelDiffrence(level, exp);
            }
        }
        public int Attack()
        {
            Random rand = new Random();

            if (KeyInput.IsPressed(Keys.D1))
            {
                if (rand.Next(100) < 10 * (Stats.Dex + currWepon.Stats.Dex))
                    return (int)(1.7 * (Stats.Str + currWepon.Stats.Str));
                else
                    return (int)(0.75 * (Stats.Str + currWepon.Stats.Str));
            }

            else if (KeyInput.IsPressed(Keys.D2))
            {

                if (rand.Next(100) < 7 * (Stats.Dex + currWepon.Stats.Dex))
                    return 2 * (Stats.Str + currWepon.Stats.Str);
                else
                    return Stats.Str + currWepon.Stats.Str;
            }
            else if (KeyInput.IsPressed(Keys.D3))
            {
                if (rand.Next(100) < 4 * (Stats.Dex + currWepon.Stats.Dex))
                    return (int)(2.5 * (Stats.Str + currWepon.Stats.Str));
                else
                    return (int)(1.3 * Stats.Str + currWepon.Stats.Str);
            }
            else return 0;



        }
        public int ReciveDemage(int demage)
        {
            Random rand = new Random();
            int aux = 0;
            if (rand.Next(100) > 5 * (Stats.Dex + currArmor.Stats.Dex))// 
            {
                if (demage - Stats.Def - currArmor.Stats.Def > 0)
                {
                    aux = demage - Stats.Def - currArmor.Stats.Def;

                }
            }
            CurrHP -= aux;
            return aux;
        }
        public void Upgrade()
        {
            if (UpgradePoints > 0)
            {
                switch (SelectIndex)
                {
                    case 0:
                        Stats.Str++;
                        break;
                    case 1:
                        Stats.Dex++;
                        break;
                    case 2:
                        Stats.Int++;
                        break;
                    case 3:
                        Stats.Def++;
                        break;
                }
                UpgradePoints--;
            }
        }
        public bool OpenStats()
        {
            return KeyInput.UseKey(Keys.J);
        }
        public bool OpenInventory()
        {
            return KeyInput.UseKey(Keys.I);
        }
        public bool OpenQuestsLog()
        {
            return KeyInput.UseKey(Keys.N);
        }
        public void ChangeWearedItem(int index)
        {
            if (index < 0 || index > Items.Count)
                throw new ArgumentException();
            if (Items[index] is WearableItem aux)
            {
                if (aux.Type == ItemType.Wepon)
                {
                    WearableItem c = currWepon;
                    currWepon = aux;
                    Items[index] = c;
                }
                
                if (aux.Type == ItemType.Armor)
                {
                    WearableItem c = currArmor;
                    currArmor = aux;
                    Items[index] = c;
                }
            };
        }
        public void Move(IEnumerable<Texture> textures)
        {
            var position = new Tuple<int, int>(ObjectPosition.X, ObjectPosition.Y);
            if (KeyInput.IsPressed(Keys.Up))
                ObjectPosition.Y -= Global.MovementSpeed;
            if (KeyInput.IsPressed(Keys.Down))
                ObjectPosition.Y += Global.MovementSpeed;
            if (KeyInput.IsPressed(Keys.Right))
                ObjectPosition.X += Global.MovementSpeed;
            if (KeyInput.IsPressed(Keys.Left))
                ObjectPosition.X -= Global.MovementSpeed;
            foreach (var x in textures)
                if (Collid(x))
                {
                    ObjectPosition.X = position.Item1;
                    ObjectPosition.Y = position.Item2;
                    return;
                }
        }
        public bool DoAction ()
        {
            return KeyInput.UseKey(Keys.Space);
        }
        public bool IsAlive()
        {
            return CurrHP > 0;
        }
        public void Heal()
        {
            CurrHP = Stats.MaxHP;
        }
    }
}
