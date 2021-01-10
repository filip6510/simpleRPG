using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
//Wyświetlacz część mostu
namespace simpleRPG
{
    class Desktop
    {
        private GameControl game;
        private  MessageBox msgBox;
        private PictureBox pictureBox;
        private Image heroPic;
        private Image enemyPic;
        private Image friendlyPic;
        private Image itemPic;
        private Label endGameLabel;
        private Label fightLabel;
        private Label generalLabel;
        private Label taskLabel;
        public Desktop (GameControl gameControl, MessageBox messageBox, PictureBox picture, 
            Label dieLabel, Label fight,Label general,Label task)
        {
            taskLabel = task;
            task.Visible = false;
            generalLabel = general;
            generalLabel.Visible = false;
            endGameLabel = dieLabel;
            endGameLabel.Visible = false;
            dieLabel.Text = "Poległeś wciśnij Spację,\n aby zagrać od nowa";
            fightLabel = fight;
            fightLabel.Visible = false;
            
            heroPic =  Image.FromFile("hero.png");
            enemyPic =  Image.FromFile("enemy.png");
            friendlyPic =  Image.FromFile("friendly.png");
            itemPic = Image.FromFile("item.png");
            pictureBox = picture;
            msgBox = messageBox;
            game = gameControl;
        }
        private void  PrintInventory(Graphics graphics)
        {
            graphics.FillRectangle(Brushes.AliceBlue, 174, 80, 15, 100);
            if (Hero.GetInstance().Items.Count > 0)
            {
                int pos = 100;
                taskLabel.Visible = true;
                taskLabel.Left = 200;
                taskLabel.Top = pos;
                Item aux = Hero.GetInstance().Items[Hero.GetInstance().SelectIndex];
                taskLabel.Text = aux.Id;
                if (aux is WearableItem wearable)
                    taskLabel.Text += "\n Str: " + wearable.Stats.Str + "\n Dex: " + wearable.Stats.Dex
                        + "\n Int: " + wearable.Stats.Int + "\n Def" +
                        wearable.Stats.Def;
                foreach (var i in Hero.GetInstance().Items)
                {

                    graphics.DrawImage(Image.FromFile(i.Id + ".png"), 180, pos);
                    pos += 20;
                }
            }
        }
        public void PrintText(string text)
        {
            msgBox.SendMsg(text);
        }
        private void PrintExploring(Graphics graphics)
        {
            graphics.DrawImage(heroPic, Hero.GetInstance().ObjectPosition);
            foreach (var obj in game.CurrLocation.Objects)
            {
                if (obj is Enemy enemy)
                    graphics.DrawImage(enemyPic, enemy.ObjectPosition);
                else if (obj is FriendlyCharacter friendly)
                    graphics.DrawImage(friendlyPic, friendly.ObjectPosition);
                else if (obj is DroppedItem item)
                    graphics.DrawImage(itemPic, item.ObjectPosition);
            }
            foreach (var t in game.CurrLocation.Textures)
                graphics.FillRectangle(Brushes.Brown, t.ObjectPosition);
        }
        public void GameScreenPaint(object sender, PaintEventArgs e)
        {
            switch (game.GameState)
            {
                case GameStates.HeroDead:
                    endGameLabel.Visible = true;
                    break;
                case GameStates.Exploring:
                    PrintExploring(e.Graphics);
                    break;
                case GameStates.Fight:
                    PrintExploring(e.Graphics);
                    PrintFight();
                    break;
                case GameStates.WatchStats:
                    PrintExploring(e.Graphics);
                    PrintStats();
                    SelectDot(e.Graphics);
                    break;
                case GameStates.WatchQuests:
                    PrintExploring(e.Graphics);
                    PrintQuests();
                    SelectDot(e.Graphics);
                    break;
                case GameStates.Conversation:
                    PrintExploring(e.Graphics);
                    PrintConversation();
                    SelectDot(e.Graphics);
                    break;
                case GameStates.WatchEQ:
                    PrintExploring(e.Graphics);
                    PrintInventory(e.Graphics);
                    SelectDot(e.Graphics);
                    break;

            }

        }
        private void PrintQuests()
        {
            generalLabel.Visible = true;
            taskLabel.Visible = true;
            generalLabel.Text = "Obecne zadania : ";
            foreach (var q in QuestsLog.GetInstance().CurrQuests)
                generalLabel.Text += "\n"+ q.Id;
            taskLabel.Top = generalLabel.Top;
            taskLabel.Left = generalLabel.Left + generalLabel.Width;
            taskLabel.Text = game.GetSelectedQuest().Description;
            foreach (var t in game.GetSelectedQuest().Tasks)
                taskLabel.Text += "\n  " + t.Text;
        }
        private void PrintStats()
        {
            Hero hero = Hero.GetInstance();
            generalLabel.Visible = true;
            generalLabel.Text = "Statystyki:\n Str: " + hero.Stats.Str + "\n Dex: " + hero.Stats.Dex +
                "\n Int: " + hero.Stats.Int + "\n Def: " + hero.Stats.Def + "\n Punkty ulepszeń: " + hero.UpgradePoints;
        }
        private void SelectDot(Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Red, 164, 96 + 20 * (Hero.GetInstance().SelectIndex), 10, 10);
        }
        private void PrintFight()
        {
            fightLabel.Visible = true;
            fightLabel.Text = "[1] Szybki atak\n[2] Normalny atak\n[3] Silny atak\n Twoje Zdrowie : " 
                + Hero.GetInstance().CurrHP.ToString() + "\n Zdrowie Przeciwnika : " + game.CurrEnemy.CurrHP.ToString();
        }
        public void EndState()
        {
            switch (game.GameState)
            {
                case GameStates.HeroDead:
                    endGameLabel.Visible = false;
                    break;
                case GameStates.Fight:
                    fightLabel.Visible = false;
                    break;
                case GameStates.WatchStats:
                    generalLabel.Visible = false;
                    break;
                case GameStates.WatchQuests:
                    generalLabel.Visible = false;
                    taskLabel.Visible = false;
                    break;
                case GameStates.Conversation:
                    generalLabel.Visible = false;
                    break;
                case GameStates.WatchEQ:
                    taskLabel.Visible = false;
                    break;
            }

        }
        public void Reset()
        {
            endGameLabel.Visible = false;

        }
        public void Print()
        {
            pictureBox.Invalidate();
        }
        private void PrintConversation()
        {
            generalLabel.Visible = true;
            generalLabel.Text = game.CurrDialog.Dialog;
            foreach (var a in game.CurrDialog.Answers)
                generalLabel.Text += "\n  " + a.Dialog;
        }

    }
}
