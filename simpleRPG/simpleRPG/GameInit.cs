using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simpleRPG
{
    class GameInit
    {
        public static Location InitGame()
        {
            Location startLocation = InitLocation();
            InitQuests();
            return startLocation;
        }
        private static Location InitLocation()
        {
            Location location = Factory.CreateLocation("StartMap");
            Location second = Factory.CreateLocation("Map With Enemy");
            location.AddNext("left",second);
            location.AddObject(new DroppedItem(new Item("misja", 0), 100, 100));
            NpcDialogPart bobPart = new NpcDialogPart("Witaj jestem Bob");
            FriendlyCharacter Bob = new FriendlyCharacter(50, 50, "Bob", bobPart);
            location.AddObject(Bob);
            initBob(Bob);
            second.AddObject(Factory.CreateEasyMEnemy(60, 150, "Biegający Przeciwnik"));
            second.AddObject(Factory.CreateEasyNMEnemy(260, 250, "Mprzeciwnik"));
            second.AddObject(Factory.CreateHardNMEnemy(10, 10, "Mprzeciwnik2"));
            location.Textures.Add(new Texture(150, 0, 40, 230));
            location.Textures.Add(new Texture(300, 0, 80, 90));
            second.Textures.Add(new Texture(70, 0, 80, 90));
            second.Textures.Add(new Texture(300, 0, 80, 90));
            return location;
        }
        private static void initBob(FriendlyCharacter Bob)
        {
            NpcDialogPart BobDialog = Bob.Dialog;
            BobDialog.AddAnswer("Witaj", "-");
            BobDialog.Answers.Add(new HeroDialogPart("Czy mogę ci w czymś pomóc", "-",true));
            BobDialog.AddAnswer("żegnaj","-");
            BobDialog.Answers[0].AddAnswer("Piękny dziś dzień, nie sądzisz?");
            BobDialog.Answers[0].Answer.AddAnswer("Bardzo łądny","-");
            BobDialog.Answers[0].Answer.AddAnswer("Okropny","-");
            BobDialog.Answers[0].Answer.Answers[0].AddAnswer("Dziękuję za rozmowę, żegnaj");
            BobDialog.Answers[1].AddAnswer("Tak, męczy mnie pewien bandyta, czy mógłbyś mi pomóc>");
            Task endTask = new RewardTask("bobq", "wróć do boba", null, new WearableItem("zbrojaBoba", 12, new Statistics(0, 2, 0, 3,10), ItemType.Armor), 10);
            HeroDialogPart endDial = new HeroDialogPart("wykonałem zadanie dla ciebie", "bobq",true);
            endDial.AddAnswer("będę ci wdzięczny do końca mojego życia");
            Task dialTask = new DialogTask("Biegający Przeciwnik", "pokonaj bandytę atakującego boba", endTask, endDial, Bob);
            Quest quest = new Quest("zadanie dla Boba", "zabij bandytę dla boba",dialTask);
            BobDialog.Answers[1].Answer.Answers.Add(new HeroDialogPart("Oczywiście",quest,"-",false));
            BobDialog.Answers[1].Answer.Answers.Add(new HeroDialogPart("nie","-",false));
            BobDialog.Answers[1].Answer.Answers[0].AddAnswer("Dziękuję, poczekam aż wrócisz");
            BobDialog.Answers[1].Answer.Answers[1].AddAnswer("Szkoda");


        }
        private static void InitQuests()
        {
            QuestsLog quests = QuestsLog.GetInstance();
            Quest aux = new Quest("pokazowy 1", "misja początkowa do prezętacji gry");
            quests.AddQuest(aux);
            aux.Tasks.Add(new Task("misja", "podnieś przedmiot", null));
            aux = new Quest("pokazowy 2", "misja początkowa do prezętacji gry");
            quests.AddQuest(aux);
            aux.Tasks.Add(new Task("Mprzeciwnik", "zabij przeciwnika", null));
            aux.Tasks.Add(new Task("Mprzeciwnik2", "zabij przeciwnika", null));
            aux.Tasks.Add(new RewardTask("misja", "podnieś przedmiot", null,new WearableItem ("miecz",10,new Statistics(2,2,0,0,0),ItemType.Wepon),250));
            aux = new Quest("Informacja", "sterowanie strzałkami, interakcja spacja \n miłej zabawy");
            quests.AddQuest(aux);
            aux.Tasks.Add(new Task("brak", "brak celów", null));

        }
    }
}
