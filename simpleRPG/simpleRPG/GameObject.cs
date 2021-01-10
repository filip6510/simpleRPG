using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.Drawing;
using System.Windows;

namespace simpleRPG
{
    abstract class GameObject
    {
        public string Name { get; protected set; }
        public Rectangle ObjectPosition;
        public Boolean Collid (GameObject x)
        {
            return this.ObjectPosition.IntersectsWith(x.ObjectPosition);
        }
    }
    class DroppedItem : GameObject
    {
        public Item Item { get; set; }
        public DroppedItem(Item initItem, int x, int y)
        {
            Item = initItem;
            ObjectPosition = new Rectangle(x, y, Global.ItemSize, Global.ItemSize);
            Name = initItem.Id + "_D";
        }
    }
    class Texture : GameObject
    {
        public Texture (int x, int y, int widith ,int height)
        {
            ObjectPosition = new Rectangle(x, y, widith, height);
            Name = "texture";
        }

    }
    class MapEdge : GameObject
    {
        public Location NextMap { get; set; }
        public MapEdge(int x, int y, int widith, int height, Location location,string name)
        {
            ObjectPosition = new Rectangle(x, y, widith, height);
            Name = name;
        }

    }


}
