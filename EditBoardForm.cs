using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SoundBoard;

namespace SoundBoard
{
    public partial class EditBoardForm : Form
    {
        internal int boardEditIndex = -1;
        internal string[] editBoardKeys;
        SoundboardForm mainForm;

        public EditBoardForm()
        {
            InitializeComponent();
        }



        private void btnOK_Click(object sender, EventArgs e)
        {
            Board tempBoard;

            if (tbKeys.Text.Contains('+'))
            {
                var tempStr = tbKeys.Text.Split('+');
                mainForm.soundBoardData.AllBoards.TryGetValue(mainForm.lvBoards.FocusedItem.Text, out tempBoard);
                tempBoard.rndKeys = Helper.stringArrayToKeysArray(tempStr);

                var keySoundsText = "";
                var tempArr = Helper.keysArrayToStringArray(tempBoard.rndKeys);
                foreach (string item in tempArr)
                {
                    if (item != tempArr[tempArr.Length - 1])
                    {
                        keySoundsText += item;
                        keySoundsText += "+";
                    }
                    else
                        keySoundsText += item;

                }
                tempBoard.rndHotkey = keySoundsText;
            }
            else
            {
                mainForm.soundBoardData.AllBoards.TryGetValue(mainForm.lvBoards.FocusedItem.Text, out tempBoard);
                tempBoard.rndHotkey = tbKeys.Text;
                tempBoard.rndPressedKey = Helper.stringToKey(tbKeys.Text);
                tempBoard.rndKeys = new Keys[] { tempBoard.rndPressedKey };
                
                
            }
            this.Dispose();
           
        }

            private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void tbKeys_Enter(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        
        private void tbKeys_Leave(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        int lastAmountPressed = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            int amountPressed = 0;

            if (Keyboard.IsKeyDown(Keys.Escape))
            {
                lastAmountPressed = 50;

                tbKeys.Text = "";
            }
            else
            {
                var pressedKeys = new List<Keys>();

                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (Keyboard.IsKeyDown(key))
                    {
                        amountPressed++;
                        pressedKeys.Add(key);
                    }
                }

                if (amountPressed > lastAmountPressed)
                {
                    tbKeys.Text = Helper.keysToString(pressedKeys.ToArray());
                }

                lastAmountPressed = amountPressed;
            }
        }

        private void EditBoardForm_Load(object sender, EventArgs e)
        {
            mainForm = Application.OpenForms[0] as SoundboardForm;
            tbKeys.Text = editBoardKeys[0];
        }
    }
}
