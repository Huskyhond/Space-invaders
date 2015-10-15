namespace Game1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.amountOfPlayersComboBox = new System.Windows.Forms.ComboBox();
            this.applySettingsButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.controllerP1ComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.controllerP2ComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.controllerP3ComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.controllerP4ComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // amountOfPlayersComboBox
            // 
            this.amountOfPlayersComboBox.FormattingEnabled = true;
            this.amountOfPlayersComboBox.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.amountOfPlayersComboBox.Location = new System.Drawing.Point(61, 11);
            this.amountOfPlayersComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.amountOfPlayersComboBox.Name = "amountOfPlayersComboBox";
            this.amountOfPlayersComboBox.Size = new System.Drawing.Size(44, 21);
            this.amountOfPlayersComboBox.TabIndex = 0;
            this.amountOfPlayersComboBox.Text = "<Players>";
            // 
            // applySettingsButton
            // 
            this.applySettingsButton.Location = new System.Drawing.Point(162, 11);
            this.applySettingsButton.Margin = new System.Windows.Forms.Padding(2);
            this.applySettingsButton.Name = "applySettingsButton";
            this.applySettingsButton.Size = new System.Drawing.Size(56, 19);
            this.applySettingsButton.TabIndex = 1;
            this.applySettingsButton.Text = "OK";
            this.applySettingsButton.UseVisualStyleBackColor = true;
            this.applySettingsButton.Click += new System.EventHandler(this.applySettingsButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Players:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 1 controller";
            // 
            // controllerP1ComboBox
            // 
            this.controllerP1ComboBox.FormattingEnabled = true;
            this.controllerP1ComboBox.Items.AddRange(new object[] {
            "Mouse",
            "Keyboard",
            "GamePad"});
            this.controllerP1ComboBox.Location = new System.Drawing.Point(108, 58);
            this.controllerP1ComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.controllerP1ComboBox.Name = "controllerP1ComboBox";
            this.controllerP1ComboBox.Size = new System.Drawing.Size(110, 21);
            this.controllerP1ComboBox.TabIndex = 3;
            this.controllerP1ComboBox.Text = "Controller";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Player 2 controller";
            // 
            // controllerP2ComboBox
            // 
            this.controllerP2ComboBox.FormattingEnabled = true;
            this.controllerP2ComboBox.Items.AddRange(new object[] {
            "Mouse",
            "Keyboard",
            "GamePad"});
            this.controllerP2ComboBox.Location = new System.Drawing.Point(108, 83);
            this.controllerP2ComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.controllerP2ComboBox.Name = "controllerP2ComboBox";
            this.controllerP2ComboBox.Size = new System.Drawing.Size(110, 21);
            this.controllerP2ComboBox.TabIndex = 5;
            this.controllerP2ComboBox.Text = "Controller";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Player 3 controller";
            // 
            // controllerP3ComboBox
            // 
            this.controllerP3ComboBox.FormattingEnabled = true;
            this.controllerP3ComboBox.Items.AddRange(new object[] {
            "Mouse",
            "Keyboard",
            "GamePad"});
            this.controllerP3ComboBox.Location = new System.Drawing.Point(108, 107);
            this.controllerP3ComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.controllerP3ComboBox.Name = "controllerP3ComboBox";
            this.controllerP3ComboBox.Size = new System.Drawing.Size(110, 21);
            this.controllerP3ComboBox.TabIndex = 7;
            this.controllerP3ComboBox.Text = "Controller";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Player 4 controller";
            // 
            // controllerP4ComboBox
            // 
            this.controllerP4ComboBox.FormattingEnabled = true;
            this.controllerP4ComboBox.Items.AddRange(new object[] {
            "Mouse",
            "Keyboard",
            "GamePad"});
            this.controllerP4ComboBox.Location = new System.Drawing.Point(108, 130);
            this.controllerP4ComboBox.Margin = new System.Windows.Forms.Padding(2);
            this.controllerP4ComboBox.Name = "controllerP4ComboBox";
            this.controllerP4ComboBox.Size = new System.Drawing.Size(110, 21);
            this.controllerP4ComboBox.TabIndex = 9;
            this.controllerP4ComboBox.Text = "Controller";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 164);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.controllerP4ComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.controllerP3ComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.controllerP2ComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.controllerP1ComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.applySettingsButton);
            this.Controls.Add(this.amountOfPlayersComboBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox amountOfPlayersComboBox;
        private System.Windows.Forms.Button applySettingsButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox controllerP1ComboBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox controllerP2ComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox controllerP3ComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox controllerP4ComboBox;
    }
}