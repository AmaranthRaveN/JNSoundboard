using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SoundBoard;

namespace SoundBoard
{
    public partial class AddEditHotkeyForm : Form
    {
        internal class ListViewItemComparer : IComparer
        {
            private int col;

            public ListViewItemComparer()
            {
                col = 0;
            }

            public ListViewItemComparer(int column)
            {
                col = column;
            }

            public int Compare(object x, object y)
            {
                return string.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
            }
        }

        internal string[] editSoundKeys = null;
        internal int editIndex = -1;

        // change to soundboardForm
        SoundboardForm mainForm;
        SettingsForm settingsForm;

        public AddEditHotkeyForm()
        {
            InitializeComponent();
            btnOK.TabStop = false;
            btnCancel.TabStop = false;
            tbLocation.TabStop = false;
            tbKeys.TabStop = false;
            btnBrowseSoundLoc.TabStop = false;
        }

        private void AddEditSoundKeys_Load(object sender, EventArgs e)
        {
            if (SettingsForm.addingEditingLoadXMLFile)
            {
                settingsForm = Application.OpenForms[1] as SettingsForm;

                this.Text = "Add/edit keys and XML location";

                if (editIndex != -1)
                {
                    tbKeys.Text = editSoundKeys[0];
                    tbLocation.Text = editSoundKeys[1];
                }
            }
            else
            {
                mainForm = Application.OpenForms[0] as SoundboardForm;

                labelLoc.Text += " (use a semi-colon (;) to seperate multiple locations)";

                if (editIndex != -1)
                {
                    tbKeys.Text = editSoundKeys[0];
                    tbLocation.Text = editSoundKeys[1];
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbLocation.Text))
            {
                MessageBox.Show("Location is empty");
                return;
            }

            if (SettingsForm.addingEditingLoadXMLFile && string.IsNullOrWhiteSpace(tbKeys.Text))
            {
                MessageBox.Show("No keys entered");
                return;
            }

            string[] soundLocs = null;
            string errorMessage = "";

            if (!SettingsForm.addingEditingLoadXMLFile)
            {
                if (!SettingsForm.addingEditingLoadXMLFile && Helper.soundLocsArrayFromString(tbLocation.Text, out soundLocs, out errorMessage))
                {
                    if (soundLocs.Any(x => string.IsNullOrWhiteSpace(x) || !File.Exists(x)))
                    {
                        MessageBox.Show("The file/one of the files does not exist");

                        this.Close();

                        return;
                    }
                }

                if (soundLocs == null)
                {
                    MessageBox.Show(errorMessage);
                    return;
                }
            }

            Keys[] keysArr;

            if (!Helper.keysArrayFromString(tbKeys.Text, out keysArr, out errorMessage)) keysArr = new Keys[] { };


            if (SettingsForm.addingEditingLoadXMLFile)
            {
                if (editIndex != -1)
                {
                    settingsForm.lvKeysLocs.Items[editIndex].Text = tbKeys.Text;
                    settingsForm.lvKeysLocs.Items[editIndex].SubItems[1].Text = tbLocation.Text;

                    settingsForm.loadJSONFilesList[editIndex].Keys = keysArr;
                    settingsForm.loadJSONFilesList[editIndex].JSONLocation = tbLocation.Text;
                }
                else
                {

                    var item = new ListViewItem(tbKeys.Text);
                    item.SubItems.Add(tbLocation.Text);

                    settingsForm.lvKeysLocs.Items.Add(item);

                    settingsForm.loadJSONFilesList.Add(new JSONSettings.LoadJSONFile(keysArr, tbLocation.Text));
                }
            }
            else
            {
                if (editIndex > -1)
                {
                    ///
                    /// Fix editing sound file (changing sound file)
                    /// -- change filename with associated selected key
                    ///

                    //SoundFile sf = new SoundFile();
                    SoundFile tempSF = new SoundFile();
                    string eM;
                    mainForm.soundFileData.TryGetValue(editIndex, out tempSF);
                    //mainForm.allSounds.TryGetValue(editIndex, out tempSF);
                    if (tbKeys.Text.Contains('+'))
                    {
                        var tempStr = tbKeys.Text.Split('+');
                        tempSF.keys = Helper.stringArrayToKeysArray(tempStr);
                        //tempSF.keys = Helper.keysArrayFromString(tbKeys.Text, out keys, out eM);
                        var tempArr = Helper.keysArrayToStringArray(tempSF.keys);
                        var keySoundsText = "";
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
                        tempSF.filePath = tbLocation.Text;
                        tempSF.hotKey = keySoundsText;

                        mainForm.lvKeySounds.Items[editIndex].Text = tempSF.hotKey;
                        mainForm.lvKeySounds.Items[editIndex].SubItems[1].Text = tempSF.filePath;
                    }
                    else
                    {
                        
                        //Enum.TryParse(tbKeys.Text, out tempHK);
                        //mainForm.lvKeySounds.Items[editIndex].Text = Helper.stringToKey(tbKeys.Text).ToString();
                        tempSF.hotKey = tbKeys.Text;
                        tempSF.pressedKey = Helper.stringToKey(tbKeys.Text);
                        tempSF.keys = new Keys[] { tempSF.pressedKey };
                        tempSF.filePath = tbLocation.Text;
                        mainForm.lvKeySounds.Items[editIndex].Text = tbKeys.Text;
                        mainForm.lvKeySounds.Items[editIndex].SubItems[1].Text = tbLocation.Text;


                        //Helper.keysArrayFromString(tempSF.hotKey, out keys, out eM);
                        //tempSF.pressedKey = keys[1];
                    }

                    //mainForm.keysSounds[editIndex] = new XMLSettings.KeysSounds(keysArr, soundLocs);


                }
                else
                {

                    SoundFile sf = helperAddFile(tbKeys.Text, tbLocation.Text);

                }

                mainForm.chHotkey.Width = -2;
                mainForm.chFileName.Width = -2;
            }

            this.Close();
        }

        private SoundFile helperAddFile(string hotkeyStr, string fileName)
        {
            SoundFile returningSF = new SoundFile();
            if (fileName.Contains(';'))
            {
                string[] temp = fileName.Split(';');
                int count = 0;

                foreach (string file in temp)
                {
                    returningSF = new SoundFile();
                    returningSF.filePath = file;
                    //Enum.TryParse(tbKeys.Text, out tempHK);
                    returningSF.hotKey = hotkeyStr;
                    returningSF.playCount = 0;
                    if (hotkeyStr.Contains('+'))
                    {
                        var tempStr = hotkeyStr.Split('+');
                        returningSF.keys = Helper.stringArrayToKeysArray(tempStr);
                        //tempSF.keys = Helper.keysArrayFromString(tbKeys.Text, out keys, out eM);
                    }
                    else
                    {
                        returningSF.filePath = file;
                        returningSF.playCount = 0;
 
                        returningSF.pressedKey = Helper.stringToKey(hotkeyStr);
                        returningSF.keys = new Keys[] { returningSF.pressedKey };
                    }
                    ListViewItem item = new ListViewItem();
                    item.Text = returningSF.hotKey;
                    item.SubItems.Add(returningSF.filePath);
                    mainForm.lvKeySounds.Items.Add(item);
                    mainForm.soundFileData.Add(mainForm.soundFileData.Count, returningSF);
                    mainForm.allSounds.Add(mainForm.allSounds.Count, returningSF);
                    count++;

                }
                
            }
            else
            {
                if (hotkeyStr.Contains('+'))
                {
                    var tempStr = hotkeyStr.Split('+');
                    returningSF.keys = Helper.stringArrayToKeysArray(tempStr);
                    //tempSF.keys = Helper.keysArrayFromString(tbKeys.Text, out keys, out eM);
                }
                
                else
                    returningSF.keys = new Keys[] { returningSF.pressedKey };

                MessageBox.Show(returningSF.keys.Length.ToString());

                returningSF.filePath = fileName;
                returningSF.playCount = 0;
                returningSF.pressedKey = Helper.stringToKey(hotkeyStr);


                //Enum.TryParse(tbKeys.Text, out tempHK);
                returningSF.hotKey = hotkeyStr;
                mainForm.soundFileData.Add(mainForm.soundFileData.Count, returningSF);
                mainForm.allSounds.Add(mainForm.allSounds.Count, returningSF);

                var newItem = new ListViewItem(hotkeyStr);
                newItem.SubItems.Add(fileName);

                mainForm.lvKeySounds.Items.Add(newItem);

            }
            return returningSF;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBrowseSoundLoc_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();

            diag.Multiselect = !SettingsForm.addingEditingLoadXMLFile;

            diag.Filter = (SettingsForm.addingEditingLoadXMLFile ? "XML file containing keys and sounds|*.xml" : "Supported audio formats|*.mp3;*.m4a;*.wav;*.wma;*.ac3;*.aiff;*.mp2|All files|*.*");

            var result = diag.ShowDialog();
            

            if (result == DialogResult.OK)
            {
                string text = "";
                //MessageBox.Show(result.ToString());
                //diag.FileName
                string files = "";
                int fileNum = 1;
                /*
                foreach(string file in diag.FileNames)
                {
                    if (fileNum < diag.FileNames.Length)
                    {
                        files += file;
                        files += ";";
                    }
                    fileNum++;
                }
                if (files.Contains(';'))
                {
                    int count = 0;
                    string[] temp = files.Split(';');
                    
                    foreach(string file in temp)
                    {
                        string fileName = diag.FileNames[count];
                        ListViewItem item = new ListViewItem();
                        item.Text = "Hotkey";
                        item.SubItems.Add(fileName);
                        mainForm.lvKeySounds.Items.Add(item);
                        
                        
                        count++;
                    }
                    tbLocation.Text = files;
                }
                */
                //else
                //{
                    for (int i = 0; i < diag.FileNames.Length; i++)
                    {
                        string fileName = diag.FileNames[i];

                        if (fileName != "") text += (i == 0 ? "" : ";") + fileName;
                    }

                    tbLocation.Text = text;
                //}
                
            }
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

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbKeys_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
