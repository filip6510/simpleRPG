using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class Task
    {
        public string ObjectiveID { get; private set; }
        public Task Next { get; private set; }
        public string Text { get; private set; }
        public Task(string objective,string text, Task next)
        {
            Text = text;
            ObjectiveID = objective;
            Next = next;
        }
    }
    class RewardTask : Task
    {
        public Item Reward { get; private set; }
        public int ExpReward;
        public RewardTask(string objective, string text, Task next, Item reward,int exp)
            :base (objective,text,next)
        {
            ExpReward = exp;
            Reward = reward;
        }
    }
    class DialogTask : Task
    {
        public HeroDialogPart Dialog { get; private set; }
        public FriendlyCharacter DialogReciver { get; private set; }
        public DialogTask(string objective, string text, Task next,HeroDialogPart dialog, FriendlyCharacter dialogReciver)
            : base(objective,text, next)
        {
            DialogReciver = dialogReciver;
            Dialog = dialog;
        }
    }

    class Quest : IObserver<string>//obserwator
    {
        
        public string Id { get; private set; }
        public string Description { get; private set; }
        public List<Task> Tasks { get; private set; }
        public void OnNext (string objectiveId)
        {
            for (int i = 0; i < Tasks.Count; i++)
                if (Tasks[i].ObjectiveID == objectiveId)
                    Complete(i);
        }
        public void OnError(Exception exception)
        {
        }
        public void OnCompleted()
        {
        }
        private void Complete (int taskIndex)
        {
            Task task = Tasks[taskIndex];
            if (task.Next != null)
                Tasks.Add(task.Next);
            if (task is DialogTask dialogTask)
              dialogTask.DialogReciver.Dialog.Answers.Add(dialogTask.Dialog);
            if (task is RewardTask rewardTask)
            {
                Hero.GetInstance().Items.Add(rewardTask.Reward);
                Hero.GetInstance().AddExp(rewardTask.ExpReward);
            }
            Tasks.RemoveAt(taskIndex);
        }
        void Add (Task task)
        {
            Tasks.Add(task);
        }
        public Quest(string id, string description)
        {
            Tasks = new List<Task>();
            Id = id;
            Description = description;
        }
        public Quest(string id, string description,Task task)
            :this (id, description)
        {
            Tasks.Add(task);
        }
        public bool IsCompleted()
        {
            return Tasks.Count == 0;
        }
    }
}
