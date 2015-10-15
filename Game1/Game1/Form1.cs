using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game1
{
    public enum ControllerOptions
    {
        Mouse,
        Keyboard,
        GamePad
    }

    public partial class Form1 : Form
    {
        public int amountOfPlayers { get; set; }
        public List<ControllerOptions> PlayerControllerChoices { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PlayerControllerChoices = new List<ControllerOptions>();
            amountOfPlayersComboBox.SelectedIndex = 0;

            controllerP1ComboBox.SelectedIndex = (int)ControllerOptions.Mouse;
            controllerP2ComboBox.SelectedIndex = (int)ControllerOptions.Keyboard;
            controllerP3ComboBox.SelectedIndex = (int)ControllerOptions.GamePad;
            controllerP4ComboBox.SelectedIndex = (int)ControllerOptions.GamePad;
        }

        private void applySettingsButton_Click(object sender, EventArgs e)
        {
            amountOfPlayers = amountOfPlayersComboBox.SelectedIndex + 1;
            DialogResult = DialogResult.OK;
            PlayerControllerChoices.Add((ControllerOptions)controllerP1ComboBox.SelectedIndex);
            PlayerControllerChoices.Add((ControllerOptions)controllerP2ComboBox.SelectedIndex);
            PlayerControllerChoices.Add((ControllerOptions)controllerP3ComboBox.SelectedIndex);
            PlayerControllerChoices.Add((ControllerOptions)controllerP4ComboBox.SelectedIndex);
        }

    }
}
