using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SoundBoard
{
    public partial class SettingsForm : Form
    {
        //internal List<XMLSettings.LoadXMLFile> loadXMLFilesList = new List<XMLSettings.LoadXMLFile>(XMLSettings.soundboardSettings.LoadXMLFiles); //list so can dynamically add/remove
        internal List<JSONSettings.LoadJSONFile> loadJSONFilesList = new List<JSONSettings.LoadJSONFile>(JSONSettings.soundboardSettings.LoadJSONFiles);
        internal static bool addingEditingLoadXMLFile = false;
        internal static bool addingEditingLoadJSONFile = false;

        private bool stopSoundTextPressed = false;
        private bool recSoundTextPressed = false;
        private bool stopRecSoundTextPressed = false;
        private bool recDirectoryTextPressed = false;

        public SettingsForm()
        {
            InitializeComponent();

            for (int i = 0; i < JSONSettings.soundboardSettings.LoadJSONFiles.Length; i++)
            {
                var item = new ListViewItem((JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys.Length > 0 ? string.Join("+", JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys) : ""));
                item.SubItems.Add(((string.IsNullOrWhiteSpace(JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation) || !File.Exists(JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation)) ? "" : JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation));
                /*
                var item = new ListViewItem((XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys.Length > 0 ? string.Join("+", XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys) : ""));
                item.SubItems.Add(((string.IsNullOrWhiteSpace(XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation) || !File.Exists(XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation)) ? "" : XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation));

                lvKeysLocs.Items.Add(item);
                */
            }
            tbStopSoundKeys.Text = Helper.keysToString(JSONSettings.soundboardSettings.StopSoundKeys);
            tbRecKey.Text = Helper.keysToString(JSONSettings.soundboardSettings.RecSoundKeys);
            tbStopRecKey.Text = Helper.keysToString(JSONSettings.soundboardSettings.StopRecSoundKeys);
            tbRecDirectory.Text = JSONSettings.soundboardSettings.recordDirectory;
            cbMinimizeToTray.Checked = JSONSettings.soundboardSettings.MinimizeToTray;
            cbPlaySoundsOverEachOther.Checked = JSONSettings.soundboardSettings.PlaySoundsOverEachOther;


            tbStopSoundKeys.TabStop = false;
            tbRecKey.TabStop = false;
            tbStopRecKey.TabStop = false;
            tbRecDirectory.TabStop = false;
            //tbStopSoundKeys.Text = Helper.keysToString(XMLSettings.soundboardSettings.StopSoundKeys);

            //cbMinimizeToTray.Checked = XMLSettings.soundboardSettings.MinimizeToTray;

            //cbPlaySoundsOverEachOther.Checked = XMLSettings.soundboardSettings.PlaySoundsOverEachOther;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //addingEditingLoadXMLFile = true;
            addingEditingLoadJSONFile = true;

            var form = new AddEditHotkeyForm();
            form.ShowDialog();

            addingEditingLoadJSONFile = false;
            //addingEditingLoadXMLFile = false;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0)
            {
                //addingEditingLoadXMLFile = true;
                addingEditingLoadJSONFile = true;

                var form = new AddEditHotkeyForm();

                form.editIndex = lvKeysLocs.SelectedIndices[0];
                form.editSoundKeys = new string[] { lvKeysLocs.SelectedItems[0].Text, lvKeysLocs.SelectedItems[0].SubItems[1].Text };

                form.ShowDialog();

                addingEditingLoadJSONFile = false;
                //addingEditingLoadXMLFile = false;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvKeysLocs.SelectedIndices.Count > 0 && MessageBox.Show("Are you sure?", "Are you sure?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int index = lvKeysLocs.SelectedIndices[0];

                lvKeysLocs.Items.RemoveAt(index);
                //loadXMLFilesList.RemoveAt(index);
                loadJSONFilesList.RemoveAt(index);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Keys[] keysArr = null;
            Keys[] recArr = null;
            Keys[] stopRecArr = null;
            string error = "";
            string recError = "";
            string stopRecError = "";

            if (string.IsNullOrWhiteSpace(tbStopSoundKeys.Text) || Helper.keysArrayFromString(tbStopSoundKeys.Text, out keysArr, out error) && string.IsNullOrWhiteSpace(tbRecKey.Text) || Helper.keysArrayFromString(tbRecKey.Text, out recArr, out recError) && string.IsNullOrWhiteSpace(tbStopSoundKeys.Text) || Helper.keysArrayFromString(tbStopRecKey.Text, out stopRecArr, out stopRecError))
            {
                if (loadJSONFilesList.Count == 0 || loadJSONFilesList.All(x => x.Keys.Length > 0 && !string.IsNullOrWhiteSpace(x.JSONLocation) && File.Exists(x.JSONLocation)))
                {
                    JSONSettings.soundboardSettings.StopSoundKeys = (keysArr == null ? new Keys[] { } : keysArr);
                    JSONSettings.soundboardSettings.RecSoundKeys = (recArr == null ? new Keys[] { } : recArr);
                    JSONSettings.soundboardSettings.StopRecSoundKeys = (stopRecArr == null ? new Keys[] { } : stopRecArr);
                    JSONSettings.soundboardSettings.LoadJSONFiles = loadJSONFilesList.ToArray();
                    JSONSettings.soundboardSettings.MinimizeToTray = cbMinimizeToTray.Checked;
                    JSONSettings.soundboardSettings.PlaySoundsOverEachOther = cbPlaySoundsOverEachOther.Checked;
                    JSONSettings.soundboardSettings.recordDirectory = "";
                    JSONSettings.SaveSoundboardSettingsJSON();
                    //XMLSettings.soundboardSettings.StopSoundKeys = (keysArr == null ? new Keys[] { } : keysArr);

                    //XMLSettings.soundboardSettings.LoadXMLFiles = loadXMLFilesList.ToArray();

                    //XMLSettings.soundboardSettings.MinimizeToTray = cbMinimizeToTray.Checked;

                    //XMLSettings.soundboardSettings.PlaySoundsOverEachOther = cbPlaySoundsOverEachOther.Checked;

                    //XMLSettings.SaveSoundboardSettingsXML();

                    this.Close();
                }
                else if (!string.IsNullOrWhiteSpace(tbStopSoundKeys.Text) || !string.IsNullOrWhiteSpace(tbRecKey.Text) || !string.IsNullOrWhiteSpace(tbStopRecKey.Text) || !string.IsNullOrWhiteSpace(tbRecDirectory.Text))
                {
                    if (!string.IsNullOrWhiteSpace(tbRecKey.Text))
                        Helper.keysArrayFromString(tbRecKey.Text, out recArr, out recError);
                    if (!string.IsNullOrWhiteSpace(tbStopSoundKeys.Text))
                        Helper.keysArrayFromString(tbStopSoundKeys.Text, out keysArr, out error);
                    if (!string.IsNullOrWhiteSpace(tbStopRecKey.Text))
                        Helper.keysArrayFromString(tbStopRecKey.Text, out stopRecArr, out stopRecError);
                    if (!string.IsNullOrWhiteSpace(tbRecDirectory.Text))
                        JSONSettings.soundboardSettings.recordDirectory = tbRecDirectory.Text;

                    JSONSettings.soundboardSettings.StopSoundKeys = (keysArr == null ? new Keys[] { } : keysArr);
                    JSONSettings.soundboardSettings.RecSoundKeys = (recArr == null ? new Keys[] { } : recArr);
                    JSONSettings.soundboardSettings.StopRecSoundKeys = (stopRecArr == null ? new Keys[] { } : stopRecArr);
                    JSONSettings.soundboardSettings.LoadJSONFiles = loadJSONFilesList.ToArray();
                    JSONSettings.soundboardSettings.MinimizeToTray = cbMinimizeToTray.Checked;
                    JSONSettings.soundboardSettings.PlaySoundsOverEachOther = cbPlaySoundsOverEachOther.Checked;
                    
                    JSONSettings.SaveSoundboardSettingsJSON();
                    //XMLSettings.soundboardSettings.StopSoundKeys = (keysArr == null ? new Keys[] { } : keysArr);

                    //XMLSettings.soundboardSettings.LoadXMLFiles = loadXMLFilesList.ToArray();

                    //XMLSettings.soundboardSettings.MinimizeToTray = cbMinimizeToTray.Checked;

                    //XMLSettings.soundboardSettings.PlaySoundsOverEachOther = cbPlaySoundsOverEachOther.Checked;

                    //XMLSettings.SaveSoundboardSettingsXML();

                    this.Close();

                }
                else MessageBox.Show("One or more entries either have no keys added, the location is empty, or the file the location points to does not exist");
            }
            else if (error != "")
            {
                MessageBox.Show(error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvKeysLocs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnEdit_Click(null, null);
        }

        private void tbStopSoundKeys_Enter(object sender, EventArgs e)
        {
            stopSoundTextPressed = true;
            timer1.Enabled = true;
        }

        private void tbStopSoundKeys_Leave(object sender, EventArgs e)
        {
            stopSoundTextPressed = false;
            timer1.Enabled = false;
        }

        private void tbRecKey_Enter(object sender, EventArgs e)
        {
            recSoundTextPressed = true;
            timer1.Enabled = true;
        }
        private void tbRecKey_Leave(object sender, EventArgs e)
        {
            recSoundTextPressed = false;
            timer1.Enabled = false;
        }

        private void tbStopRecKey_Enter(object sender, EventArgs e)
        {
            stopRecSoundTextPressed = true;
            timer1.Enabled = true;
        }
        private void tbStopRecKey_Leave(object sender, EventArgs e)
        {
            stopRecSoundTextPressed = false;
            timer1.Enabled = false;
        }

        private void tbRecDirectory_Enter(object sender, EventArgs e)
        {
            recDirectoryTextPressed = true;
            timer1.Enabled = true;
        }

        private void tbRecDirectory_Leave(object sender, EventArgs e)
        {
            recDirectoryTextPressed = false;
            timer1.Enabled = false;
        }


        int lastAmountPressed = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            int amountPressed = 0;

            if (Hotkey.IsKeyDown(Keys.Escape))
            {
                lastAmountPressed = 50;
                if (stopSoundTextPressed)
                    tbStopSoundKeys.Text = "";
                else if (recSoundTextPressed)
                    tbRecKey.Text = "";
                else if (stopRecSoundTextPressed)
                    tbStopRecKey.Text = "";
                else if (recDirectoryTextPressed)
                    tbRecDirectory.Text = "";
            }
            else
            {
                var pressedKeys = new List<Keys>();

                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (Hotkey.IsKeyDown(key))
                    {
                        amountPressed++;
                        pressedKeys.Add(key);
                    }
                }

                if (amountPressed > lastAmountPressed)
                {
                    if (stopSoundTextPressed && tbStopSoundKeys.Focused)
                        tbStopSoundKeys.Text = Helper.keysToString(pressedKeys.ToArray());
                    else if (recSoundTextPressed && tbRecKey.Focused)
                        tbRecKey.Text = Helper.keysToString(pressedKeys.ToArray());
                    else if (stopRecSoundTextPressed && tbStopRecKey.Focused)
                        tbStopRecKey.Text = Helper.keysToString(pressedKeys.ToArray());
                }

                lastAmountPressed = amountPressed;
            }
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }

        private void tbRecKey_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnBrowseRecDirectory_Click(object sender, EventArgs e)
        {
            var diag = new FolderBrowserDialog();
            diag.SelectedPath = tbRecDirectory.Text;
            diag.ShowNewFolderButton = true;
            DialogResult result = diag.ShowDialog();
            if (result == DialogResult.OK)
            {
                tbRecDirectory.Text = diag.SelectedPath;
                Environment.SpecialFolder root = diag.RootFolder;

            }
            
        }
    }
}