using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class NpcDialogPart
    {

        public string Dialog { get; private set; }
        public List<HeroDialogPart> Answers { get; }
        public int TypedAnswers;
        public NpcDialogPart(string npcPart)
        {
            Dialog = npcPart;
            Answers = new List<HeroDialogPart>();
        }
        public void AddAnswer(string text, string id)
        {
            var aux = new HeroDialogPart(text, id, false);
            Answers.Add(aux);
        }
        
    }
    class HeroDialogPart
    {
        public string Id { get; private set; }
        public string Dialog { get; private set; }
        public NpcDialogPart Answer;
        public Quest Mission;
        public bool ToDel { get; private set; }
        public HeroDialogPart(string text, string id,bool toDel)
        {
            Id = id;
            Answer = null;
            Mission = null;
            Dialog = text;
            ToDel = toDel;
        }
        public HeroDialogPart(string text, Quest mission, string id, bool toDel)
        {
            ToDel = toDel;
            Id = id;
            Answer = null;
            Mission = mission;
            Dialog = text;
        }
        public void AddAnswer(string text)
        {
            if (Answer == null)
            {
                Answer = new NpcDialogPart(text);
            }
        }
    }
}
