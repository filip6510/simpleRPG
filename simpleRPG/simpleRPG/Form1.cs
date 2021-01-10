using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleRPG
{
    public partial class Form1 : Form
    {
        private GameControl game;
        Desktop desktop; 
        public Form1()
        {
            InitializeComponent();
            StartGame();
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            GameScreen.Paint += desktop.GameScreenPaint;
            gameTimer.Interval = 30;
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

        }
        public void StartGame()
        {
            game = new GameControl();
            desktop = new Desktop(game, new MessageBox(msgBox), GameScreen, DieLabel,fightLabel,StatsLabel,taskLabel);
            game.Connect(desktop);
        }
        void GameLoop (object sender, EventArgs e)
        {
            if (game.LoopEnd)
                 game.GameTick();
            //msgBox.Text = game.MsgBox.GetText();
            //GameScreen.Invalidate();
        }
      
        

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            KeyInput.SetKeyState(e.KeyCode, true);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        { 
            KeyInput.SetKeyState(e.KeyCode, false);
        }

        private void GameScreen_Click(object sender, EventArgs e)
        {

        }
    }
}
