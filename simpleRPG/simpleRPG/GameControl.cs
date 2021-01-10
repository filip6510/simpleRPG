using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
//most model
namespace simpleRPG
{
    class GameControl
    {

        public Location CurrLocation { get; private set; }
        public GameStates GameState { get; private set;}
        public Enemy CurrEnemy { get; private set;}
        public NpcDialogPart CurrDialog { get; private set; }
        public bool LoopEnd { get; private set; }
        Desktop desktop;
        private Hero gameHero;
        private QuestsLog questsLog;
        
        public GameControl()
        {
            gameHero = Hero.GetInstance();
            questsLog = QuestsLog.GetInstance();
            Reset();

        }
        void Reset()
        {
            if(desktop != null)
                desktop.Reset();
            gameHero.Reset();
            questsLog.Reset();
            CurrLocation = GameInit.InitGame();

            LoopEnd = true;
            GameState = GameStates.Exploring;

        }
        public void Connect (Desktop d)
        {
            desktop = d;
        }
        public void GameTick()
        {
            LoopEnd = false;
         
            switch (GameState)
            {
                case GameStates.Exploring:
                    Exploring();
                    break;
                case GameStates.Fight:
                    Fight();
                    break;
                case GameStates.Conversation:
                    Converse();
                    break;
                case GameStates.HeroDead:
                    EndGame();
                    break;
                case GameStates.WatchStats:
                    WatchStats();
                    break;
                case GameStates.WatchQuests:
                    WatchQuests();
                    break;
                case GameStates.WatchEQ:
                    WatchEQ();
                    break;
            }
           
            LoopEnd = true;

        }
        private void Exploring ()
        {
            if (gameHero.OpenStats())
            {
                desktop.PrintText("Przeglądasz statystyki");
                gameHero.SetSelect(4);
                GameState = GameStates.WatchStats;
                return;
                // KeyInput.SetKeyState(Keys.J, false);

            }
            if (gameHero.OpenQuestsLog())
            {
                desktop.PrintText("Przeglądasz Dziennik zadań");
                gameHero.SetSelect(questsLog.CurrQuests.Count);
                GameState = GameStates.WatchQuests;
                return;
                // KeyInput.SetKeyState(Keys.J, false);

            }
            if (gameHero.OpenInventory())
            {
                desktop.PrintText("Przeglądsz Inwentarz");
                gameHero.SetSelect(gameHero.Items.Count);
                GameState = GameStates.WatchEQ;
                return;


            }
            List<GameObject> toDelete = new List<GameObject>();
            gameHero.Move(CurrLocation.Textures);
          //  desktop.PrintText(gameHero.ObjectPosition.X.ToString() + "   " + gameHero.ObjectPosition.Y.ToString());
            desktop.Print();
            foreach (var obj in CurrLocation.Objects)
            {
                if (obj is IMoveAble movingObj)
                {
                    movingObj.Move(CurrLocation.Textures);
                    desktop.Print();
                }

                if (obj.Collid(gameHero))
                {
                    if (obj is Enemy enemy)
                    {
                        desktop.PrintText("Walczysz z ." + enemy.Name);
                        CurrEnemy = enemy;
                        GameState = GameStates.Fight;
                        return;
                    }
                    else if (obj is DroppedItem item && gameHero.DoAction())
                    {
                        desktop.PrintText("Podnosisz " + item.Name);
                        gameHero.Items.Add(item.Item);
                        QuestsLog.GetInstance().Invoke(item.Item.Id);
                        toDelete.Add(item);
                    }
                    else if (obj is FriendlyCharacter friendly && gameHero.DoAction())
                    {
                        desktop.PrintText("Rozmawiasz z " + friendly.Name);
                        CurrDialog = friendly.Dialog;
                        gameHero.SetSelect(friendly.Dialog.Answers.Count);
                        GameState = GameStates.Conversation;
                    }
                    else if (obj is MapEdge edge)
                    {
                        if (edge.NextMap != null)
                        {
                            CurrLocation = edge.NextMap;
                            switch (edge.Name)
                            {
                                case "top":
                                    gameHero.ObjectPosition.Y = 23;
                                    break;
                                case "bot":
                                    gameHero.ObjectPosition.Y = Global.GameHeight - 23;
                                    break;
                                case "left":
                                    gameHero.ObjectPosition.X = Global.GameWidth - 23;//Global.GameWidth + 23;
                                    break;
                                case "right":
                                    gameHero.ObjectPosition.X = 23;
                                    break;
                            }
                            return;
                        }
                    }
                }


            }
            foreach (var obj in toDelete)
                CurrLocation.Objects.Remove(obj);
        }
        private void WatchStats()
        {
            gameHero.ChangeSelect();
            desktop.Print();
            if (gameHero.DoAction())
            {
                gameHero.Upgrade();
               // desktop.PrintText(" zmieniono statystyki" + gameHero.Stats.Str + " " + gameHero.Stats.Dex + " " + gameHero.Stats.Int + " " + gameHero.Stats.Def);
            }
            if (gameHero.OpenStats())
            {
                desktop.EndState();
                GameState = GameStates.Exploring;
            }
        }
        public Quest GetSelectedQuest()
        {
            return questsLog.CurrQuests.ElementAt(gameHero.SelectIndex);
        }
        private void WatchQuests()
        {
            gameHero.ChangeSelect();
            desktop.Print();

            if (gameHero.OpenQuestsLog())
            {
                desktop.EndState();
                GameState = GameStates.Exploring;
            }
        }
        private void Fight()
        {
            int dealdmg = gameHero.Attack();
            if (dealdmg == 0)
                return;
            desktop.PrintText("zadajesz " + CurrEnemy.ReciveDemage(dealdmg).ToString());
            Thread.Sleep(500);
            if (CurrEnemy.IsAlive())
            {
                desktop.PrintText("otrzymujesz " + gameHero.ReciveDemage(CurrEnemy.Attack()).ToString());
                if (!gameHero.IsAlive())
                {
                    desktop.EndState();
                    GameState = GameStates.HeroDead;
                }
            }
            else
            {
                questsLog.Invoke(CurrEnemy.Name);
                CurrLocation.Objects.Remove(CurrEnemy);
                gameHero.Heal();
                gameHero.AddExp(50);
                desktop.PrintText("Pokonałeś " + CurrEnemy.Name + " Zdobywasz 50 Doświadczenia");
                desktop.EndState();
                GameState = GameStates.Exploring;
            }
            desktop.Print();
        }
        private void EndGame()
        {
            desktop.Print();
            if (gameHero.DoAction())
                Reset();

        }
        private void Converse()
        {
            gameHero.ChangeSelect();
            desktop.Print();
            if (gameHero.DoAction())
            {
                if (CurrDialog.Answers.Count != 0)
                {
                    HeroDialogPart aux = CurrDialog.Answers[gameHero.SelectIndex];
                    if (aux.Mission != null)
                        questsLog.AddQuest(aux.Mission);
                    questsLog.Invoke(aux.Id);
                    if (aux.ToDel)
                        CurrDialog.Answers.RemoveAt(gameHero.SelectIndex);
                    if (aux.Answer == null)
                    {
                        desktop.EndState();
                        GameState = GameStates.Exploring;
                    }
                    else
                    {
                        
                        CurrDialog =aux.Answer;
                        gameHero.SetSelect(CurrDialog.Answers.Count);
                    }
                }
                else
                {
                    desktop.EndState();
                    GameState = GameStates.Exploring;
                }
            }

        }
        private void WatchEQ()
        {
           
            gameHero.ChangeSelect();
            desktop.Print();
            if(gameHero.DoAction())
            {
                desktop.PrintText("zmieniono broń/zbroje");
                gameHero.ChangeWearedItem(gameHero.SelectIndex);
            }
            if (gameHero.OpenInventory())
            {
                desktop.EndState();
                GameState = GameStates.Exploring;
            }
        }
    }

}
