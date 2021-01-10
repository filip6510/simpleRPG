using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Location
    {
        public List<GameObject> Objects { get; private set; }
        public List<Texture> Textures { get; private set; }
        public string Name{ get; private set;}
        public Location(string locName)
        {
            Name = locName;
            Objects = new List<GameObject>();
            Textures = new List<Texture>();
        }
        public void AddObject(GameObject gameObject)
        {
            Objects.Add(gameObject);
        }
        public void DelObject(GameObject gameObject)
        {
            Objects.Remove(gameObject);
        }
        public void DelObject(int index)
        {
            Objects.RemoveAt(index);
        }
        public void AddNext(string where,Location toAdd)
        {
            GameObject aux = Objects.First(x => x.Name == where);
            if (aux is MapEdge edge)
            {
                if (edge.NextMap == null)
                {
                    string opposite = "";
                    edge.NextMap = toAdd;
                    switch (where)
                    {
                        case "left":
                            opposite = "right";
                            break;
                        case "right":
                            opposite = "left";
                            break;
                        case "top":
                            opposite = "bot";
                            break;
                        case "bot":
                            opposite = "top";
                            break;
                    }
                    GameObject toAddEdge = toAdd.Objects.First(x => x.Name == opposite);
                    if (toAddEdge is MapEdge addEdge)
                        addEdge.NextMap = this;
                }
            }
        }
    }
}
