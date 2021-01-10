using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//singleton wysyłą informacje do obserwatorów
namespace simpleRPG
{
    class QuestsLog 
    {
        public List<Quest> CurrQuests { get; private set; }
        public List<Quest> EndedQuests { get; private set; }
        private static QuestsLog questsLog;
        public void Invoke (string msg)
        {
            foreach (var quest in CurrQuests)
            {
                quest.OnNext(msg);
                if (quest.IsCompleted())
                {
                    EndedQuests.Add(quest);
                    //CurrQuests.Remove(quest);
                }
            }
            foreach (var q in EndedQuests)
                CurrQuests.Remove(q);
        }
       
        private QuestsLog ()
        {
            CurrQuests = new List<Quest>();
            EndedQuests = new List<Quest>();
        }
        public void Reset()
        {
            CurrQuests.Clear();
            EndedQuests.Clear();
        }
        static public QuestsLog GetInstance ()
        {
            if (questsLog == null)
                questsLog = new QuestsLog();
            return questsLog;
        }
        public void AddQuest(Quest quest)
        {
            CurrQuests.Add(quest);
        }
    }
}
