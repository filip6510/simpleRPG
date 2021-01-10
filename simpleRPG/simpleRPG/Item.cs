using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Item
    {
        public string Id { get; protected set; }
        public int Price { get; protected set; }
        public bool QuestConnected { get; protected set; }
        public Item (string itemId, int itemPrice)
        {
            Id = itemId;
            Price = itemPrice;
            QuestConnected = false;
        }
        public Item(string itemId, int itemPrice,bool questConnected)
        {
            Id = itemId;
            Price = itemPrice;
            QuestConnected = questConnected;
        }
    }
    class WearableItem : Item
    {
        public Statistics Stats { get; private set; }
        public ItemType Type { get; private set; }
        public WearableItem(string itemId,int itemPrice, Statistics initStats,ItemType type)
               : base(itemId, itemPrice)
        {
            Stats = new Statistics(initStats);
            Type = type;
        }
        public WearableItem(string itemId, int itemPrice, ItemType type)
            : base (itemId,itemPrice)
        {
            Stats = new Statistics(0, 0, 0, 0, 0);
            Type = type;
        }
        public WearableItem(string itemId, int itemPrice, Statistics initStats, bool questConnected, ItemType type)
             : base(itemId, itemPrice,questConnected)
        {
            Stats = new Statistics(initStats);
            Type = type;
        }
        public WearableItem(string itemId, int itemPrice, bool questConnected, ItemType type)
            : base(itemId, itemPrice,questConnected)
        {
            Stats = new Statistics(0, 0, 0, 0, 0);
            Type = type;
        }
    }
}
