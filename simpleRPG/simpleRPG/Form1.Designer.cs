namespace simpleRPG
{
    partial class Form1
    {
        /// <summary>
        ///Filip Orzechowski
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameScreen = new System.Windows.Forms.PictureBox();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.msgBox = new System.Windows.Forms.Label();
            this.DieLabel = new System.Windows.Forms.Label();
            this.fightLabel = new System.Windows.Forms.Label();
            this.StatsLabel = new System.Windows.Forms.Label();
            this.taskLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GameScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // GameScreen
            // 
            this.GameScreen.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.GameScreen.Location = new System.Drawing.Point(12, 12);
            this.GameScreen.Name = "GameScreen";
            this.GameScreen.Size = new System.Drawing.Size(817, 374);
            this.GameScreen.TabIndex = 0;
            this.GameScreen.TabStop = false;
            this.GameScreen.Click += new System.EventHandler(this.GameScreen_Click);
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 60;
            // 
            // msgBox
            // 
            this.msgBox.AutoSize = true;
            this.msgBox.Location = new System.Drawing.Point(12, 389);
            this.msgBox.Name = "msgBox";
            this.msgBox.Size = new System.Drawing.Size(0, 17);
            this.msgBox.TabIndex = 1;
            // 
            // DieLabel
            // 
            this.DieLabel.AutoSize = true;
            this.DieLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DieLabel.Location = new System.Drawing.Point(157, 97);
            this.DieLabel.Name = "DieLabel";
            this.DieLabel.Size = new System.Drawing.Size(0, 46);
            this.DieLabel.TabIndex = 2;
            this.DieLabel.Visible = false;
            // 
            // fightLabel
            // 
            this.fightLabel.AutoSize = true;
            this.fightLabel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.fightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fightLabel.Location = new System.Drawing.Point(55, 264);
            this.fightLabel.Name = "fightLabel";
            this.fightLabel.Size = new System.Drawing.Size(53, 20);
            this.fightLabel.TabIndex = 3;
            this.fightLabel.Text = "label1";
            // 
            // StatsLabel
            // 
            this.StatsLabel.AutoSize = true;
            this.StatsLabel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.StatsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.StatsLabel.Location = new System.Drawing.Point(246, 97);
            this.StatsLabel.Name = "StatsLabel";
            this.StatsLabel.Size = new System.Drawing.Size(186, 150);
            this.StatsLabel.TabIndex = 4;
            this.StatsLabel.Text = "Statystyki:\r\nStr: 10\r\nDex : 3\r\nInt:  5\r\nDef: 2\r\nPunkty Ulepszeń : 5\r\n";
            // 
            // taskLabel
            // 
            this.taskLabel.AutoSize = true;
            this.taskLabel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.taskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.taskLabel.Location = new System.Drawing.Point(474, 97);
            this.taskLabel.Name = "taskLabel";
            this.taskLabel.Size = new System.Drawing.Size(170, 108);
            this.taskLabel.TabIndex = 5;
            this.taskLabel.Text = "opis długi itkd o co biega\r\nw zadaniu\r\ntask1\r\ntask2\r\ntask3\r\ntask4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 503);
            this.Controls.Add(this.taskLabel);
            this.Controls.Add(this.StatsLabel);
            this.Controls.Add(this.fightLabel);
            this.Controls.Add(this.DieLabel);
            this.Controls.Add(this.msgBox);
            this.Controls.Add(this.GameScreen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.GameScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameScreen;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Label msgBox;
        private System.Windows.Forms.Label DieLabel;
        private System.Windows.Forms.Label fightLabel;
        private System.Windows.Forms.Label StatsLabel;
        private System.Windows.Forms.Label taskLabel;
    }
}

