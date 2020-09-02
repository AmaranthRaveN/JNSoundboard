using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Threading;
using System.Media;
using NAudio.Wave;
using System.Diagnostics;
using Newtonsoft.Json;
using NAudio.CoreAudioApi;
using Microsoft.VisualBasic;


namespace SoundBoard
{
    public partial class SoundboardForm : Form
    {
        private WaveIn loopbackSourceStream = null;
        private BufferedWaveProvider loopbackWaveProvider = null;
        private WaveOut loopbackWaveOut = null;
        private WaveOut playbackWaveOut = null;

        // Recording stoofs
        //WasapiCapture recordWave = null;
        private WaveIn recordWave = null;

        WaveFileWriter waveFileWriter = null;
        private bool recording = false;
        //int recordedNum;

        Random rand = new Random();

        bool keyUpPushToTalkKey = false;

        bool playing = false;
        private int oldBoardCount;
        private string lastSelectedBoard = "";
        //internal List<XMLSettings.KeysSounds> keysSounds = new List<XMLSettings.KeysSounds>();
        internal List<JSONSettings.KeysSounds> keysSounds = new List<JSONSettings.KeysSounds>();
        Keys pushToTalkKey;
        //Keys keyJustPressed;
        internal string xmlLoc = "";
        internal string jsonLoc = "";
        internal IDictionary<int, SoundFile> soundFileData;
        internal IDictionary<int, SoundFile> allSounds;
        internal IDictionary<string, Board> allBoards;
        internal Boards soundBoardData;
        //public event EventHandler<VolumeEventArgs> VolumeChanged;

        /// <summary>
        /// TO DO:
        /// 
        /// Fix remove item line 574 (DONE)
        /// fix clear button SoundboardForm [DESIGN] - (DONE)
        /// volume slider - HIGH (Done.... WIP)
        /// 
        /// Add modifier keys to hotkey binding - LOW (DONE)
        /// 
        /// Add stop all sounds button - LOW (DONE)
        /// 
        /// Edit file (Changing sound file) does not remove + add in same position - HIGH (DONE)
        /// Separate volume for each file - LOW (DONE)
        /// 
        /// Add recording function to record from an audio device (DONE)
        /// Fix performance hitching on several loops in timertick checking for StopsoundKey/RecKey/StopRecKey (DONE?)
        /// 
        /// v 0.3.1
        /// Added handling for recording/stoprecording if user tries to call these without setting it up properly
        /// Added multikey functionality on ADDing file (already present on editing file)
        /// 
        /// v 0.3.2
        /// Implement play count on the soundfile viewer along side Hotkey + Filename? (WIP)
        /// Add Push-To-Talk Auto key functionality (DONE)
        /// Start adding List View for "Category/Board" - A soundboard in itself that can be selected, or set a hotkey to play a random sound from to minimize the view of MANY sounds
        /// 
        /// v 0.3.3
        /// Added play count as column in list view (? Get feedback if this is desired)
        /// Added support for multiple boards 
        /// IE: You can add x sounds and save as a board. Clear board or relaunch app to start a new board to save. A separate view for the BOARDS is available
        /// selecting a board name will display only the sounds saved to that specific board.
        /// This added feature only changes display of sounds on main view, you can still play sounds from other boards that are not displayed.
        /// Changed: I have changed key listening to listen for all saved hotkeys, so if you have a hotkey on board1 and another hotkey on board2, you can press either hotkey and the sounds will play.
        /// GITHUB INTEGRATION AS OF  v 0.3.3 finished changes
        /// -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// 
        /// 
        /// 
        /// v 0.4
        /// Changed logic structure for Boards.cs (split to Board.cs and Boards.cs) to add support for multiple sound boards -> 
        /// Changed Edit button to check if a board is being edited or a sound file within board.
        /// Added random hotkey functionality to play a random sounds from a board when pressed.
        /// Added restart button to restart the soundboard when pressed.
        ///
        /// 
        /// 
        /// 
        /// </summary>
        public SoundboardForm()
        {
            InitializeComponent();
            soundFileData = new Dictionary<int, SoundFile>();
            allSounds = new Dictionary<int, SoundFile>();
            allBoards = new Dictionary<string, Board>();
            soundBoardData = new Boards();
            var tooltip = new ToolTip();
            tooltip.SetToolTip(btnReloadDevices, "Refresh sound devices");
            tooltip.SetToolTip(btnReloadWindows, "Reload windows");

            ToolTip sbToolTip = new ToolTip();
            sbToolTip.ShowAlways = true;
            //sbToolTip.ReshowDelay = 500;
            sbToolTip.SetToolTip(tbSetVolume, "Type a number between 0.0 - 1.0");
            sbToolTip.SetToolTip(bSetVolume, "Enter a number between 0.0 - 1.0 in the box above, then click this button");

            // Boards List view tooltip
            sbToolTip.SetToolTip(lvBoards, "Click a board's name to load the board in the soundfile view to the right.");
            
            loadSoundDevices();
            loadWindows();
            JSONSettings.LoadSoundboardSettingsJSON();
            //XMLSettings.LoadSoundboardSettingsXML();

            if (cbPlaybackDevices.Items.Contains(JSONSettings.soundboardSettings.LastPlaybackDevice)) cbPlaybackDevices.SelectedItem = JSONSettings.soundboardSettings.LastPlaybackDevice;

            if (cbLoopbackDevices.Items.Contains(JSONSettings.soundboardSettings.LastLoopbackDevice)) cbLoopbackDevices.SelectedItem = JSONSettings.soundboardSettings.LastLoopbackDevice;

            //add events after settings have been loaded
            cbPlaybackDevices.SelectedIndexChanged += cbPlaybackDevices_SelectedIndexChanged;
            cbLoopbackDevices.SelectedIndexChanged += cbLoopbackDevices_SelectedIndexChanged;


            tooltip.Dispose();
        }

        private void tbKeyListener_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void loadWindows()
        {
            cbWindows.Items.Clear();

            cbWindows.Items.Add("");

            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!string.IsNullOrEmpty(process.MainWindowTitle))
                {
                    cbWindows.Items.Add(process.MainWindowTitle);
                }
            }
        }
        private void loadSoundDevices()
        {
            var playbackSources = new List<WaveOutCapabilities>();
            var loopbackSources = new List<WaveInCapabilities>();
            var recSources = new List<WaveInCapabilities>();

            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                playbackSources.Add(WaveOut.GetCapabilities(i));
                //recSources.Add(WaveOut.GetCapabilities(i));

            }

            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                loopbackSources.Add(WaveIn.GetCapabilities(i));
                recSources.Add(WaveIn.GetCapabilities(i));
            }


            cbPlaybackDevices.Items.Clear();
            cbLoopbackDevices.Items.Clear();

            foreach (var source in playbackSources)
            {
                cbPlaybackDevices.Items.Add(source.ProductName);
            }

            if (cbPlaybackDevices.Items.Count > 0)
                cbPlaybackDevices.SelectedIndex = 0;

            cbLoopbackDevices.Items.Add("");

            foreach (var source in loopbackSources)
            {
                cbLoopbackDevices.Items.Add(source.ProductName);
                cbRecDevice.Items.Add(source.ProductName);

            }

            cbLoopbackDevices.SelectedIndex = 0;
        }
        private void startLoopback()
        {
            stopLoopback();

            int deviceNumber = cbLoopbackDevices.SelectedIndex - 1;


            if (loopbackSourceStream == null)
                loopbackSourceStream = new WaveIn();
            loopbackSourceStream.DeviceNumber = deviceNumber;
            loopbackSourceStream.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels);
            loopbackSourceStream.BufferMilliseconds = 25;
            loopbackSourceStream.NumberOfBuffers = 5;
            loopbackSourceStream.DataAvailable += loopbackSourceStream_DataAvailable;

            loopbackWaveProvider = new BufferedWaveProvider(loopbackSourceStream.WaveFormat);
            loopbackWaveProvider.DiscardOnBufferOverflow = true;

            if (loopbackWaveOut == null)
                loopbackWaveOut = new WaveOut();
            loopbackWaveOut.DeviceNumber = cbPlaybackDevices.SelectedIndex;
            loopbackWaveOut.DesiredLatency = 125;
            loopbackWaveOut.Init(loopbackWaveProvider);

            loopbackSourceStream.StartRecording();
            loopbackWaveOut.Play();
        }
        private void stopLoopback()
        {
            try
            {
                keyUpPushToTalkKey = false;
                if (loopbackWaveOut != null)
                {
                    loopbackWaveOut.Stop();
                    loopbackWaveOut.Dispose();
                    loopbackWaveOut = null;
                }

                if (loopbackWaveProvider != null)
                {
                    loopbackWaveProvider.ClearBuffer();
                    loopbackWaveProvider = null;
                }

                if (loopbackSourceStream != null)
                {
                    loopbackSourceStream.StopRecording();
                    loopbackSourceStream.Dispose();
                    loopbackSourceStream = null;
                }
                
                //GC.Collect();
            }
            catch (Exception) { }
        }
        private void stopPlayback()
        {
            try
            {
                if (playbackWaveOut != null && playbackWaveOut.PlaybackState == PlaybackState.Playing)
                {
                    playbackWaveOut.Stop();
                    playbackWaveOut.Dispose();
                    playbackWaveOut = null;
                    //MessageBox.Show("uhmwut");
                }
                               
            }
            catch (Exception) { }
        }

        private void recordSound()
        {
            if (!cbRecDevice.Text.Equals(""))
            {
                int deviceNumber = cbRecDevice.SelectedIndex;
                var lbSources = new List<WaveInCapabilities>();
                for (int i = 0; i < WaveIn.DeviceCount; i++)
                {
                    lbSources.Add(WaveIn.GetCapabilities(i));
                }


                string directory = JSONSettings.soundboardSettings.recordDirectory;
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                //MMDeviceEnumerator devices = new MMDeviceEnumerator();
                //recordLoopBack();
                //var device = devices.GetDevice(cbRecDevice.SelectedItem.ToString());
                //WasapiCapture recordWave = new WasapiCapture(device);
                //var device = MessageBox.Show(devices.GetDevice(cbRecDevice.Items[deviceNumber].ToString()));
                /*
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "Wave Files (*.wav)|*.wav;";
                if (save.ShowDialog() != DialogResult.OK) return;
                */
                string dev = cbRecDevice.Text;
                recordWave = new WaveIn();
                //WaveOut wo = new WaveOut();
                recordWave.DeviceNumber = deviceNumber;
                recordWave.WaveFormat = new WaveFormat(44100, WaveIn.GetCapabilities(deviceNumber).Channels);

                recordWave.DataAvailable += new EventHandler<WaveInEventArgs>(recordWave_DataAvailable);
                recordWave.RecordingStopped += new EventHandler<StoppedEventArgs>(recordWave_Stopped);
                recording = true;
                waveFileWriter = new WaveFileWriter(JSONSettings.soundboardSettings.recordDirectory + $"\\Recording{JSONSettings.soundboardSettings.recordedNum}.wav", recordWave.WaveFormat);

                recordWave.StartRecording();
            }
            else
                MessageBox.Show("You do not have a recording device selected to record.");
  
        }

        void stopRecordSound()
        {
            //recordWave.StopRecording();
   
            MessageBox.Show($"Stopped Recording: file = Recording{JSONSettings.soundboardSettings.recordedNum}.wav");
            JSONSettings.soundboardSettings.recordedNum += 1;
            JSONSettings.SaveSoundboardSettingsJSON();
            recording = false;

        }

        void recordWave_DataAvailable(object sender, WaveInEventArgs e)
        {
 
            if(waveFileWriter != null)
            {
                waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                waveFileWriter.Flush();
            }
            
        }

        void recordWave_Stopped(object sender, StoppedEventArgs e)
        {
            if (recordWave != null)
            {
                recordWave.Dispose();
                recordWave.StopRecording();

                recordWave = null;
                
            }

            if (waveFileWriter != null)
            {
                waveFileWriter.Dispose();
                waveFileWriter = null;
            }           
        }

        private void playSound(string file)
        {
            playing = true;

            int deviceNumber = cbPlaybackDevices.SelectedIndex;

            if (!JSONSettings.soundboardSettings.PlaySoundsOverEachOther) stopPlayback();

            if (playbackWaveOut == null) playbackWaveOut = new WaveOut();

            playbackWaveOut.DeviceNumber = deviceNumber;
            //playbackWaveOut.Volume = 
            //playbackWaveOut.Volume = volumeBar.Value / 10.0f;
            //playbackWaveVolumeSlider.VolumeChanged
            try
            {
                //playbackWaveOut.Volume = .8f;
                playbackWaveOut.Init(new AudioFileReader(file));
                //MessageBox.Show("Hi I made it here before breaking");
                playbackWaveOut.Play(); // breaks on specific audio file :thonking:


            }
            catch (FormatException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (NAudio.MmException ex)
            {
                SystemSounds.Beep.Play();
                string msg = ex.ToString();
                MessageBox.Show((msg.Contains("UnspecifiedError calling waveOutOpen") ? "Something is wrong with either the sound you tried to play (" + file.Substring(file.LastIndexOf("\\") + 1) + ") (try converting it to another format) or your sound card driver\n\n" + msg : msg));
            }
        }

        private void playSound(SoundFile sf)
        {
            playing = true;
            int deviceNumber = cbPlaybackDevices.SelectedIndex;

            if (!JSONSettings.soundboardSettings.PlaySoundsOverEachOther) stopPlayback();


            if (playbackWaveOut == null) playbackWaveOut = new WaveOut();
            //playbackWaveOut = new WaveOut();

            playbackWaveOut.DeviceNumber = deviceNumber;
            

            //playbackWaveOut.Volume = volumeBar.Value / 10.0f;
            //playbackWaveVolumeSlider.VolumeChanged
            try
            {
                playbackWaveOut.Volume = sf.volume;
                //playbackWaveOut.Volume = .8f;
                //playbackWaveOut.Init(new AudioFileReader(file));
                playbackWaveOut.Init(new AudioFileReader(sf.filePath));

                //MessageBox.Show("Hi I made it here before breaking");
                playbackWaveOut.Play(); // breaks on specific audio file :thonking:

                //GC.Collect();


            }
            catch (FormatException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (NAudio.MmException ex)
            {
                SystemSounds.Beep.Play();
                string msg = ex.ToString();
                MessageBox.Show((msg.Contains("UnspecifiedError calling waveOutOpen") ? "Something is wrong with either the sound you tried to play (" + sf.filePath.Substring(sf.filePath.LastIndexOf("\\") + 1) + ") (try converting it to another format) or your sound card driver\n\n" + msg : msg));
            }
        }

        private void playSound(string file, SoundFile sf)
        {
            playing = true;
            int deviceNumber = cbPlaybackDevices.SelectedIndex;

            if (!JSONSettings.soundboardSettings.PlaySoundsOverEachOther) stopPlayback();


            if (playbackWaveOut == null) playbackWaveOut = new WaveOut();
                //playbackWaveOut = new WaveOut();

            playbackWaveOut.DeviceNumber = deviceNumber;

            //playbackWaveOut.Volume = volumeBar.Value / 10.0f;
            //playbackWaveVolumeSlider.VolumeChanged
            try
            {
                
                //playbackWaveOut.Volume = .8f;
                //playbackWaveOut.Init(new AudioFileReader(file));
                playbackWaveOut.Init(new AudioFileReader(sf.filePath));
                playbackWaveOut.Volume = sf.volume;
                //MessageBox.Show("Hi I made it here before breaking");
                playbackWaveOut.Play(); // breaks on specific audio file :thonking:

                //GC.Collect();


            }
            catch (FormatException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                SystemSounds.Beep.Play();
                MessageBox.Show(ex.ToString());
            }
            catch (NAudio.MmException ex)
            {
                SystemSounds.Beep.Play();
                string msg = ex.ToString();
                MessageBox.Show((msg.Contains("UnspecifiedError calling waveOutOpen") ? "Something is wrong with either the sound you tried to play (" + file.Substring(file.LastIndexOf("\\") + 1) + ") (try converting it to another format) or your sound card driver\n\n" + msg : msg));
            }
        }
 
        private void playKeySound(int index)
        {
            playing = true;

            SoundFile tempSF;
            string path;
            //soundFileData.TryGetValue(index, out tempSF);
            soundFileData.TryGetValue(index, out tempSF);
            //allSounds.TryGetValue(index, out tempSF);

            path = tempSF.filePath;
            if (File.Exists(tempSF.filePath))
            {

                //if (tempSF.volume >= 0.0f && tempSF.volume <= 1.0f)
                //playbackWaveOut.Volume = tempSF.volume;
                playSound(path, tempSF);
                tempSF = null;
                path = null;


                //Enum.TryParse(tempSF.hotKey, out keysJustPressed);
            }
        }
        private void playKeySound(JSONSettings.KeysSounds currentKeysSounds)
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);

            string path;
            if (currentKeysSounds.SoundLocations.Length > 1)
            {
                //get random sound
                int temp;

                while (true)
                {
                    temp = rand.Next(0, currentKeysSounds.SoundLocations.Length);

                    if (temp != lastIndex && File.Exists(currentKeysSounds.SoundLocations[temp])) break;
                    Thread.Sleep(1);
                }

                lastIndex = temp;

                path = currentKeysSounds.SoundLocations[lastIndex];
            }
            else
                path = currentKeysSounds.SoundLocations[0]; //get first sound

            if (File.Exists(path))
            {

                playSound(path);
                keysJustPressed = currentKeysSounds.Keys;
            }
            else if (!showingMsgBox) //dont run when already showing messagebox (don't want a bunch of these on your screen, do you?)
            {
                SystemSounds.Beep.Play();
                showingMsgBox = true;
                MessageBox.Show("File " + path + " does not exist");
                showingMsgBox = false;
            }
        }
        private void loadXMLFile(string path)
        {
            JSONSettings.Settings s = (JSONSettings.Settings)JSONSettings.ReadJSON(typeof(JSONSettings.Settings), path);

            if (s != null && s.KeysSounds != null && s.KeysSounds.Length > 0)
            {
                var items = new List<ListViewItem>();
                string errors = "";
                string sameKeys = "";
                string keys = "";

                for (int i = 0; i < s.KeysSounds.Length; i++)
                {
                    int kLength = s.KeysSounds[i].Keys.Length;
                    bool keysNull = (kLength >= 1 && !s.KeysSounds[i].Keys.Any(x => x != 0));
                    int sLength = s.KeysSounds[i].SoundLocations.Length;
                    bool soundsNotEmpty = s.KeysSounds[i].SoundLocations.All(x => !string.IsNullOrWhiteSpace(x));
                    Environment.CurrentDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                    bool filesExist = s.KeysSounds[i].SoundLocations.All(x => File.Exists(x));

                    if (keysNull || sLength < 1 || !soundsNotEmpty || !filesExist) //error in XML file
                    {
                        string tempErr = "";

                        if (!keysNull) tempErr = "one or more keys are null";
                        else if (sLength < 1) tempErr = "no sounds provided";
                        else if (!filesExist) tempErr = "one or more sounds do not exist";

                        errors += "Entry #" + i.ToString() + "has an error: " + tempErr + "\r\n";
                    }

                    keys = (kLength < 1 ? "" : Helper.keysToString(s.KeysSounds[i].Keys));

                    if (keys != "" && items.Count > 0 && items[items.Count - 1].Text == keys && !sameKeys.Contains(keys))
                    {
                        sameKeys += (sameKeys != "" ? ", " : "") + keys;
                    }

                }


                lvKeySounds.Items.Clear();

                //var temp = new ListViewItem(keys);
                int tempCount = 0;
                //temp.SubItems.Add((sLength < 1 ? "" : Helper.soundLocsArrayToString(s.KeysSounds[i].SoundLocations)));
                foreach (string soundFile in s.KeysSounds[tempCount].SoundLocations)
                {
                    //ListViewItem item = new ListViewItem(keys);
                    if (soundFile != null)
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = Helper.keysToString(s.KeysSounds[tempCount].Keys);
                        item.SubItems.Add(soundFile);
                        items.Add(item);
                        lvKeySounds.Items.Add(item);
                        //lvKeySounds.Sort();
                    }

                    tempCount++;
                }

                //items.Add(temp); //add even if there was an error, so that the user can fix within the app

                if (items.Count > 0)
                {
                    if (errors != "")
                    {
                        MessageBox.Show((errors == "" ? "" : errors));
                    }

                    if (sameKeys != "")
                    {
                        MessageBox.Show("Multiple entries using the same keys. The keys being used multiple times are: " + sameKeys);
                    }
                    //tempCount = 0;
                    /*
                    foreach(ListViewItem tempItem in items)
                    {
                        lvKeySounds.Items.Add(items[tempCount]);
                        tempCount++;
                    }
                    */
                    keysSounds.Clear();
                    keysSounds.AddRange(s.KeysSounds);

                    //lvKeySounds.Items.Clear();
                    //lvKeySounds.Items.AddRange(items.ToArray());
                    /*
                    string[] temp = tbLocation.Text.Split(';');
                    int count = 0;
                    foreach (string file in temp)
                    {
                        string fileName = temp[count];
                        ListViewItem item = new ListViewItem();
                        item.Text = tbKeys.Text;
                        item.SubItems.Add(fileName);
                        mainForm.lvKeySounds.Items.Add(item);
                        mainForm.keysSounds.Add(new XMLSettings.KeysSounds(keysArr, soundLocs));
                        count++;
                    }
                    */

                    chHotkey.Width = -2;
                    chFileName.Width = -2;

                    xmlLoc = path;
                }
                else
                {
                    SystemSounds.Beep.Play();
                    MessageBox.Show("No entries found, or all entries had errors in them (key being None, sound location behind empty or non-existant)");
                }
            }
            else
            {
                SystemSounds.Beep.Play();
                MessageBox.Show("No entries found, or there was an error reading the settings file");
            }
        }

        private void loadJSONFile(string path)
        {
            oldBoardCount = soundBoardData.AllBoards.Count;
            soundBoardData = Boards.readJSON(path);
            allSounds.Clear();
            lvBoards.Items.Clear();
            allBoards.Clear();
            //XMLSettings.Settings s = (XMLSettings.Settings)XMLSettings.ReadXML(typeof(XMLSettings.Settings), path);
            //soundFileData = SoundFile.readJSON(path);
            //soundFileData = JsonSerializer.Deserialize<IDictionary<int, SoundFile>>(jsonString);
            //foreach()
            //keysSounds.Clear();
            //                                  .soundboards
            foreach (var board in soundBoardData.AllBoards)
            {
                ListViewItem item = new ListViewItem();
                item.Text = board.Key;
                item.SubItems.Add(Helper.GetDescription(board.Value.rndPressedKey));
                lvBoards.Items.Add(item);
                

                
                
            }
            int count = 0;
            foreach( var board in soundBoardData.AllBoards)
            {
                allBoards.Add(board.Key, board.Value);
                foreach(var item in board.Value.SoundBoard)
                {
                    allSounds.Add(count, item.Value);
                    count++;
                }
            }
            /*
            foreach (var obj in soundFileData)
            {
                ListViewItem item = new ListViewItem();
                item.Text = obj.Value.hotKey;
                item.SubItems.Add(obj.Value.filePath);


                //item.Text = Helper.keysToString(s.KeysSounds[tempCount].Keys);
                //item.SubItems.Add(soundFile);
                //items.Add(item);
                lvKeySounds.Items.Add(item);
                //keysSounds.AddRange(s.KeysSounds);
                //lvKeySounds.Sort();
            }
            
            */
        }

        private void loopbackSourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (loopbackWaveProvider != null && loopbackWaveProvider.BufferedDuration.TotalMilliseconds <= 100)
                loopbackWaveProvider.AddSamples(e.Buffer, 0, e.BytesRecorded);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.ShowDialog();
        }

        private void texttospeechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TextToSpeechForm();
            form.ShowDialog();
        }

        private void cbLoopbackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLoopbackDevices.SelectedIndex > 0)
            {
                if (cbEnable.Checked) //start loopback on new device, or stop loopback
                {
                    if ((string)cbLoopbackDevices.SelectedItem == "") stopLoopback();
                    else startLoopback();
                }
                else
                    stopLoopback();
            }

            string deviceName = (string)cbLoopbackDevices.SelectedItem;
            JSONSettings.soundboardSettings.LastLoopbackDevice = deviceName;

            JSONSettings.SaveSoundboardSettingsJSON();
        }

        private void cbPlaybackDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //start loopback on new device and stop all sounds playing
            if (loopbackWaveOut != null && loopbackSourceStream != null && cbEnable.Checked)
                startLoopback();

            stopPlayback();

            string deviceName = (string)cbPlaybackDevices.SelectedItem;
            JSONSettings.soundboardSettings.LastPlaybackDevice = deviceName;

            JSONSettings.SaveSoundboardSettingsJSON();
        }

        private void frmMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon1.Visible = true;

                this.Hide();
            }
        }
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            notifyIcon1.Visible = false;

            //show form and give focus
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }
        
        private void tbPushToTalkKey_Enter(object sender, EventArgs e)
        {
            if (!cbEnablePushToTalk.Checked)
            {
                cbEnable.Checked = false;
                pushToTalkKeyTimer.Enabled = true;
            }
        }

        private void tbPushToTalkKey_Leave(object sender, EventArgs e)
        {
            pushToTalkKeyTimer.Enabled = false;
        }

        

        private void btnAdd_Click(object sender, EventArgs e)
        {

            var form = new AddEditHotkeyForm();
            form.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0 && lvBoards.Focused == false)
            {
                var form = new AddEditHotkeyForm();
                var tempSF = new SoundFile();
                soundFileData.TryGetValue(lvKeySounds.SelectedIndices[0], out tempSF);
                ListViewItem item = lvKeySounds.SelectedItems[0];
                form.editSoundKeys = new string[2];
                form.editSoundKeys[0] = item.Text;
                form.editSoundKeys[1] = tempSF.filePath;
                //form.editSoundKeys[1] = item.SubItems[1].Text;
                

                form.editIndex = lvKeySounds.SelectedIndices[0];

                form.ShowDialog();
            }
            else if(lvBoards.SelectedItems.Count > 0 && lvKeySounds.SelectedItems.Count == 0)
            {
                var form = new EditBoardForm();
                var board = new Board();
                ListViewItem item = lvBoards.SelectedItems[0];
                soundBoardData.AllBoards.TryGetValue(item.Text, out board);
                form.editBoardKeys = new string[1];                
                form.editBoardKeys[0] = board.rndHotkey;
                form.boardEditIndex = lvBoards.SelectedIndices[0];
                form.ShowDialog();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            
            if (lvKeySounds.SelectedItems.Count > 0 && !(lvBoards.Focused) && MessageBox.Show("Are you sure remove that item?", "Remove", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //SoundFile tempSF;
                //string path;
                int removedKey = lvKeySounds.SelectedIndices[0];
                soundFileData.Remove(removedKey);
                allSounds.Remove(removedKey);
                // Change key of the index after removed value to the removed value's key to avoid index issue
                for (int i = removedKey; i < soundFileData.Count; i++)
                {
                    Helper.ChangeKey(soundFileData, i + 1, i);
                    
                }
                for(int i = removedKey; i < allSounds.Count; i++)
                {
                    Helper.ChangeKey(allSounds, i + 1, i);
                }

                //Helper.ChangeKey(soundFileData, removedKey + 1, removedKey);
                //keysSounds.RemoveAt(lvKeySounds.SelectedIndices[0]);
                lvKeySounds.Items.Remove(lvKeySounds.SelectedItems[0]);

                if (lvKeySounds.Items.Count == 0) cbEnable.Checked = false;
            }
            else if(lvBoards.SelectedItems.Count > 0 && lvKeySounds.FocusedItem == null && MessageBox.Show("Are you sure you want to remove that board?", "Remove", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                int removedKey = lvBoards.SelectedIndices[0];
                string removedBoardName = lastSelectedBoard;
                //string removedBoardName = soundBoardData.SoundBoards.v
                soundBoardData.AllBoards.Remove(removedBoardName);
                //soundBoardData.SoundBoards.Remove(removedBoardName);
                lvBoards.Items.Remove(lvBoards.SelectedItems[0]);
                //lvBoards.Sort();
                Boards.writeJSON(soundBoardData, jsonLoc);
                loadJSONFile(jsonLoc);
                if (lvBoards.Items.Count == 0) cbEnable.Checked = false;

                
            }
        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all items?", "Clear", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                keysSounds.Clear();
                lvKeySounds.Items.Clear();
                lvBoards.Items.Clear();
                soundFileData.Clear();
                allSounds.Clear();                                
                //GC.Collect();
                soundFileData = new Dictionary<int, SoundFile>();
                allSounds = new Dictionary<int, SoundFile>();
                cbEnable.Checked = false;
            }
            
             //(WIP) clear only sounds from selected board...
             /*
            else if(MessageBox.Show("Would you like to only clear the currently selected board?", "Clear", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var board = new Board();
                allBoards.TryGetValue(lvBoards.SelectedItems[0].Text, out board);
                allBoards.Remove(board.boardName);
                lvBoards.Items.RemoveAt(lvBoards.SelectedIndices[0]);
                lvKeySounds.Items.Clear();
                soundFileData.Clear();
                soundFileData = new Dictionary<int, SoundFile>();
                cbEnable.Checked = false;
                
                //cbEnable.Checked = false;
                
            }
           */
        }

        private void btnPlaySelectedSound_Click(object sender, EventArgs e)
        {
            if (lvKeySounds.SelectedItems.Count > 0)
            {
                var tempSF = new SoundFile();
                soundBoardData.AllBoards[lastSelectedBoard].SoundBoard.TryGetValue(lvKeySounds.SelectedIndices[0], out tempSF);
                //tempBoard.SoundBoard.TryGetValue(lvKeySounds.SelectedIndices[0], out tempSF);
                playSound(tempSF.filePath, tempSF);
                //playKeySound(lvKeySounds.SelectedIndices[0]);
            }
            

            
        }

        private void btnStopAllSounds_Click(object sender, EventArgs e)
        {
            stopPlayback();
            stopLoopback();
            
        }

        private void cbEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnable.Checked)
            {

                //enable timer if there are any keys to check. start loopback
                //soundFileData instead of allSounds
                if (allSounds != null )//&& allSounds.Count > 0)
                    mainTimer.Enabled = true;

                else
                    cbEnable.Checked = false;



                if (cbEnable.Checked && cbPlaybackDevices.Items.Count > 0 && cbLoopbackDevices.SelectedIndex > 0)
                    startLoopback();

            }
            else
            {
                //disable timer, sounds, and loopback
                mainTimer.Enabled = false;

                stopPlayback();
                stopLoopback();
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {

            //soundBoardData.SoundBoards.Clear();
            //MessageBox.Show("Hi2");

            /*
            string last = jsonLoc;
            jsonLoc = SoundFile.userGetJSONLoc();
            if (jsonLoc == "")
                jsonLoc = last;
            else if (last != jsonLoc)
            {
                SoundFile.writeJSON(soundFileData, jsonLoc);
                MessageBox.Show("Saved board");
            }
            */
            


                //string last = jsonLoc;
                jsonLoc = Boards.userGetJSONLoc();
            
            if (File.Exists(jsonLoc))
                loadJSONFile(jsonLoc);
                

            //soundBoardData.BoardName = input;
            soundBoardData.JsonPath = jsonLoc;
            //soundBoardData.SoundCount = soundFileData.Count;
            //MessageBox.Show("Hi");
            //                .SoundBoards
            if (oldBoardCount < soundBoardData.AllBoards.Count)
            {
                string input = Interaction.InputBox("Enter a name for the new soundboard you are saving", "Soundboard name", "");
                if (soundBoardData.AllBoards.ContainsKey(input))
                {
                    soundBoardData.AllBoards[input].SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                }
                else
                {
                    Board addedBoard = new Board();
                    addedBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                    soundBoardData.AllBoards.Add(input, addedBoard);

                }
            }
            else
            {
                Board changedBoard = new Board();
                //changedBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                soundBoardData.AllBoards.TryGetValue(lastSelectedBoard, out changedBoard);
                changedBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
            }
            //1MessageBox.Show("Hi");


            Boards.writeJSON(soundBoardData, jsonLoc);
            //repopulate the boards List view
            loadJSONFile(jsonLoc);
            lvKeySounds.Items.Clear();
            MessageBox.Show("Saved boards");
                     
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            /*
            if (jsonLoc == "" || !File.Exists(jsonLoc))
                jsonLoc = SoundFile.userGetJSONLoc();
            if (jsonLoc != "")
            {
                SoundFile.writeJSON(soundFileData, jsonLoc);
                MessageBox.Show("Saved");
            }
            */
            if (jsonLoc == "" || !File.Exists(jsonLoc))
                jsonLoc = Boards.userGetJSONLoc();
            if(jsonLoc != "")
            {
                if(MessageBox.Show("Are you saving a new board?", "Save board", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string input = Interaction.InputBox("If you are saving a new soundboard, enter the name", "Soundboard name", "");
                    if (input != "" && soundBoardData.AllBoards.ContainsKey(input))
                    {
                        soundBoardData.AllBoards[input].SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                    }
                    else
                    {
                        Board savingBoard = new Board();
                        soundBoardData.AllBoards.TryGetValue(lvBoards.SelectedItems[0].Text, out savingBoard);
                        savingBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;


                    }
                }
                else
                {
                    Board savingBoard = new Board();
                    soundBoardData.AllBoards.TryGetValue(lastSelectedBoard, out savingBoard);
                    savingBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                }
                
                Boards.writeJSON(soundBoardData, jsonLoc);
                loadJSONFile(jsonLoc);
                lvKeySounds.Items.Clear();
                MessageBox.Show("Saved");
            }
            /*
            if (xmlLoc == "" || !File.Exists(xmlLoc))
                xmlLoc = Helper.userGetXMLLoc();

            if (xmlLoc != "")
            {
                XMLSettings.WriteXML(new XMLSettings.Settings() { KeysSounds = keysSounds.ToArray() }, xmlLoc);

                MessageBox.Show("Saved");
            }
            */
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            /*
            var diag = new OpenFileDialog();

            diag.Filter = "XML file containing keys and sounds|*.xml";

            var result = diag.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                string path = diag.FileName;
                
                loadXMLFile(path);
            }
            

            var diag = new OpenFileDialog();
            diag.Filter = "json file containing keys and sounds|*.json";
            var result = diag.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = diag.FileName;
                loadJSONFile(path);
            }
            */
            var diag = new OpenFileDialog();
            diag.Filter = "json file containing soundboards|*.json";
            var result = diag.ShowDialog();
            if(result == DialogResult.OK)
            {
                string path = diag.FileName;
                jsonLoc = path;
                loadJSONFile(path);
            }

        }


        Keys[] keysJustPressed = null;
        bool showingMsgBox = false;
        int lastIndex = -1;
        
        private void mainTimer_Tick(object sender, EventArgs e)
        {
            // check that soundboard is enabled
            if (cbEnable.Checked)
            {

                if ( (playbackWaveOut == null || playbackWaveOut.PlaybackState == PlaybackState.Stopped) && playing )
                {
                    if (playbackWaveOut != null)
                        playbackWaveOut = null;
                    playing = false;
                    Hotkey.sendKey(pushToTalkKey, false);
                    keyUpPushToTalkKey = false;
                    //GC.Collect();
                }

                int keysPressed = 0;

                // check that soundfiles are loaded
                //if (soundFileData.Count > 0)
                if (allSounds.Count > 0)
                {
                    // check for key pressed
                    //for (int i = 0; i < soundFileData.Count; i++)
                    for(int i = 0; i < allSounds.Count; i++)
                    {
                        //Check for one of the random keys being pressed
                        
                        //keysPressed = 0;
                        SoundFile tempFile;
                        //soundFileData.TryGetValue(i, out tempFile);
                        allSounds.TryGetValue(i, out tempFile);
                        /*
                        if (cbEnablePushToTalk.Checked && !keyUpPushToTalkKey && !Hotkey.IsKeyDown(pushToTalkKey)
                                    && Helper.isForegroundWindow((string)cbWindows.SelectedItem))
                        {
                           
                            keyUpPushToTalkKey = true;
                            bool result = Hotkey.sendKey(pushToTalkKey, true);
                            Thread.Sleep(100);
                           
                        }
                        */

                        if (JSONSettings.soundboardSettings.StopSoundKeys.Length >= 1)
                        {
                            for (int j = 0; j < JSONSettings.soundboardSettings.StopSoundKeys.Length; j++)
                            {
                                if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.StopSoundKeys[j]))
                                    keysPressed++;
                            }
                            if (keysPressed == JSONSettings.soundboardSettings.StopSoundKeys.Length)
                            {
                                stopPlayback();
                            }
                            keysPressed = 0;

                        }

                        if (JSONSettings.soundboardSettings.RecSoundKeys.Length >= 1)
                        {
                            for (int j = 0; j < JSONSettings.soundboardSettings.RecSoundKeys.Length; j++)
                            {
                                if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.RecSoundKeys[j]))
                                    keysPressed++;
                            }
                            if (keysPressed == JSONSettings.soundboardSettings.RecSoundKeys.Length)
                            {
                                Thread.Sleep(150);
                                if (!recording)
                                    recordSound();
                                else
                                    MessageBox.Show("You are already recording a file.");
                            }
                            keysPressed = 0;


                        }

                        if (JSONSettings.soundboardSettings.StopRecSoundKeys.Length >= 1)
                        {
                            for (int j = 0; j < JSONSettings.soundboardSettings.StopRecSoundKeys.Length; j++)
                            {
                                if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.StopRecSoundKeys[j]))
                                    keysPressed++;
                            }
                            if (keysPressed == JSONSettings.soundboardSettings.StopRecSoundKeys.Length)
                            {
                                Thread.Sleep(150);
                                if (recording)
                                    stopRecordSound();
                                else
                                    MessageBox.Show("You are not recording a file to stop recording.");
                            }
                            keysPressed = 0;


                        }                       
                        // check for random key pressed.
                        if (allBoards.Count > 0)
                        {
                            foreach (var board in allBoards)
                            {
                                //check if rand key pressed
                                if (board.Value.rndKeys != null)
                                {
                                    for (int j = 0; j < board.Value.rndKeys.Length; j++)
                                        if (Hotkey.IsKeyDown(board.Value.rndKeys[j]))
                                            keysPressed++;
                                    if (keysPressed == board.Value.rndKeys.Length)
                                    {
                                        keysPressed = 0;
                                        SoundFile randFile;
                                        randFile = board.Value.randomFile();
                                        playSound(randFile);
                                        Thread.Sleep(150);

                                        return;
                                    }
                                }
                            }



                        }

                        if (tempFile.keys.Length > 1)
                        {

                            for (int j = 0; j < tempFile.keys.Length; j++)
                            {
                                //MessageBox.Show("omegalul");
                                if (Hotkey.IsKeyDown(tempFile.keys[j]))
                                {
                                    keysPressed++;

                                }
                                if (keysPressed == tempFile.keys.Length)
                                {
                                    if (cbEnablePushToTalk.Checked && !keyUpPushToTalkKey && !Hotkey.IsKeyDown(pushToTalkKey)
                                    && Helper.isForegroundWindow((string)cbWindows.SelectedItem))
                                    {

                                        keyUpPushToTalkKey = true;
                                        bool result = Hotkey.sendKey(pushToTalkKey, true);
                                        //Thread.Sleep(100);

                                    }
                                    //playKeySound(i);
                                    playSound(tempFile);

                                    Thread.Sleep(150);
                                }
                            }

                            keysPressed = 0;
                        }

                        else
                        {

                            if (Hotkey.IsKeyDown(tempFile.pressedKey))
                            {
                                if (cbEnablePushToTalk.Checked && !keyUpPushToTalkKey && !Hotkey.IsKeyDown(pushToTalkKey)
                                    && Helper.isForegroundWindow((string)cbWindows.SelectedItem))
                                {

                                    keyUpPushToTalkKey = true;
                                    bool result = Hotkey.sendKey(pushToTalkKey, true);
                                    //Thread.Sleep(100);

                                }
                                //playKeySound(i);
                                playSound(tempFile);
                                Thread.Sleep(150);
                                return;

                            }
                        }

                    }
                    /*
                    if(JSONSettings.soundboardSettings.StopSoundKeys != null && JSONSettings.soundboardSettings.StopSoundKeys.Length > 0)
                    {
                        for (int i = 0; i < JSONSettings.soundboardSettings.StopSoundKeys.Length; i++)
                        {
                            if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.StopSoundKeys[i])) keysPressed++;
                            Thread.Sleep(100);
                        }
                        if (keysPressed == JSONSettings.soundboardSettings.StopSoundKeys.Length)
                        {
                            if (keysJustPressed == null || !keysJustPressed.Intersect(JSONSettings.soundboardSettings.StopSoundKeys).Any())
                            {
                                if (playbackWaveOut != null && playbackWaveOut.PlaybackState == PlaybackState.Playing) playbackWaveOut.Stop();

                                keysJustPressed = JSONSettings.soundboardSettings.StopSoundKeys;

                                return;
                            }
                        }
                        else if (keysJustPressed == JSONSettings.soundboardSettings.StopSoundKeys)
                            keysJustPressed = null;

                        keysPressed = 0;
                    }
                    
                    if(JSONSettings.soundboardSettings.RecSoundKeys != null && JSONSettings.soundboardSettings.RecSoundKeys.Length > 0)
                    {
                        for(int i = 0; i < JSONSettings.soundboardSettings.RecSoundKeys.Length; i++)
                        {
                            if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.RecSoundKeys[i])) keysPressed++;
                            Thread.Sleep(100);
                        }
                        if (keysPressed == JSONSettings.soundboardSettings.RecSoundKeys.Length)
                        {

                            if (keysJustPressed == null || !keysJustPressed.Intersect(JSONSettings.soundboardSettings.RecSoundKeys).Any())
                            {

                                if (recordWave == null && !recording) recordSound();
                                keysJustPressed = JSONSettings.soundboardSettings.RecSoundKeys;
                                return;
                            }
                        }
                        else if (keysJustPressed == JSONSettings.soundboardSettings.RecSoundKeys)
                            keysJustPressed = null;
                        keysPressed = 0;
                    }
                    
                    if (JSONSettings.soundboardSettings.StopRecSoundKeys != null && JSONSettings.soundboardSettings.StopRecSoundKeys.Length > 0)
                    {
                        for (int i = 0; i < JSONSettings.soundboardSettings.StopRecSoundKeys.Length; i++)
                        {
                            if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.StopRecSoundKeys[i])) keysPressed++;
                        }
                        if (keysPressed == JSONSettings.soundboardSettings.StopRecSoundKeys.Length)
                        {
                            if (keysJustPressed == null || !keysJustPressed.Intersect(JSONSettings.soundboardSettings.StopRecSoundKeys).Any())
                            {
                                if (recordWave != null && recording) stopRecordSound();
                                keysJustPressed = JSONSettings.soundboardSettings.StopRecSoundKeys;
                                return;
                            }
                        }
                        else if (keysJustPressed == JSONSettings.soundboardSettings.StopRecSoundKeys)
                            keysJustPressed = null;
                        keysPressed = 0;
                    }
                    
                    if (JSONSettings.soundboardSettings.LoadJSONFiles != null && JSONSettings.soundboardSettings.LoadJSONFiles.Length > 0) //check that required keys are pressed to load XML file
                    {
                        for (int i = 0; i < JSONSettings.soundboardSettings.LoadJSONFiles.Length; i++)
                        {
                            if (JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys.Length == 0) continue;

                            keysPressed = 0;

                            for (int j = 0; j < JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys.Length; j++)
                            {
                                if (Hotkey.IsKeyDown(JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys[j])) keysPressed++;
                            }

                            if (keysPressed == JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys.Length)
                            {
                                if (keysJustPressed == null || !keysJustPressed.Intersect(JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys).Any())
                                {
                                    if (!string.IsNullOrWhiteSpace(JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation) && File.Exists(JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation))
                                    {
                                        keysJustPressed = JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys;

                                        loadXMLFile(JSONSettings.soundboardSettings.LoadJSONFiles[i].JSONLocation);
                                    }

                                    return;
                                }
                            }
                            else if (keysJustPressed == JSONSettings.soundboardSettings.LoadJSONFiles[i].Keys)
                            {
                                keysJustPressed = null;
                            }
                        }

                        keysPressed = 0;
                    }
                    */
                    /*
                    for (int i = 0; i < soundFileData.Count; i++)
                    {
                        keysPressed = 0;
                        
                        //if (keysSounds[i].Keys.Length == 0) continue;

                        for (int j = 0; j < keysSounds[i].Keys.Length; j++)
                        {
                            if (Hotkey.IsKeyDown(keysSounds[i].Keys[j]))
                                keysPressed++;
                        }

                        if (keysPressed == keysSounds[i].Keys.Length)
                        {
                            //if (keysJustPressed == keysSounds[i].Keys) continue;

                            if (keysSounds[i].Keys.Length > 0 && keysSounds[i].Keys.All(x => x != 0) && keysSounds[i].SoundLocations.Length > 0
                                && keysSounds[i].SoundLocations.Length > 0 && keysSounds[i].SoundLocations.Any(x => File.Exists(x)))
                            {
                                if (cbEnablePushToTalk.Checked && !keyUpPushToTalkKey && !Hotkey.IsKeyDown(pushToTalkKey)
                                    && Helper.isForegroundWindow((string)cbWindows.SelectedItem))
                                {
                                    keyUpPushToTalkKey = true;
                                    bool result = Hotkey.sendKey(pushToTalkKey, true);
                                    Thread.Sleep(100);
                                }

                                playKeySound(keysSounds[i]);
                                return;
                            }
                        }
                        else if (keysJustPressed == keysSounds[i].Keys)
                            keysJustPressed = null;
                    }

                    keysPressed = 0;
                }

                /*
                if (XMLSettings.soundboardSettings.StopSoundKeys != null && XMLSettings.soundboardSettings.StopSoundKeys.Length > 0) //check that required keys are pressed to stop all sounds
                {
                    for (int i = 0; i < XMLSettings.soundboardSettings.StopSoundKeys.Length; i++)
                    {
                        if (Hotkey.IsKeyDown(XMLSettings.soundboardSettings.StopSoundKeys[i])) keysPressed++;
                    }

                    if (keysPressed == XMLSettings.soundboardSettings.StopSoundKeys.Length)
                    {
                        if (keysJustPressed == null || !keysJustPressed.Intersect(XMLSettings.soundboardSettings.StopSoundKeys).Any())
                        {
                            if (playbackWaveOut != null && playbackWaveOut.PlaybackState == PlaybackState.Playing) playbackWaveOut.Stop();

                            keysJustPressed = XMLSettings.soundboardSettings.StopSoundKeys;

                            return;
                        }
                    }
                    else if (keysJustPressed == XMLSettings.soundboardSettings.StopSoundKeys)
                        keysJustPressed = null;

                    keysPressed = 0;
                }

                if (XMLSettings.soundboardSettings.LoadXMLFiles != null && XMLSettings.soundboardSettings.LoadXMLFiles.Length > 0) //check that required keys are pressed to load XML file
                {
                    for (int i = 0; i < XMLSettings.soundboardSettings.LoadXMLFiles.Length; i++)
                    {
                        if (XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys.Length == 0) continue;

                        keysPressed = 0;

                        for (int j = 0; j < XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys.Length; j++)
                        {
                            if (Hotkey.IsKeyDown(XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys[j])) keysPressed++;
                        }

                        if (keysPressed == XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys.Length)
                        {
                            if (keysJustPressed == null || !keysJustPressed.Intersect(XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys).Any())
                            {
                                if (!string.IsNullOrWhiteSpace(XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation) && File.Exists(XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation))
                                {
                                    keysJustPressed = XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys;

                                    loadXMLFile(XMLSettings.soundboardSettings.LoadXMLFiles[i].XMLLocation);
                                }

                                return;
                            }
                        }
                        else if (keysJustPressed == XMLSettings.soundboardSettings.LoadXMLFiles[i].Keys)
                        {
                            keysJustPressed = null;
                        }
                    }

                    keysPressed = 0;
                }

                if (keyUpPushToTalkKey)
                {
                    if (!Hotkey.IsKeyDown(pushToTalkKey)) keyUpPushToTalkKey = false;

                    if (playbackWaveOut.PlaybackState != PlaybackState.Playing || !Helper.isForegroundWindow((string)cbWindows.SelectedItem))
                    {
                        keyUpPushToTalkKey = false;
                        Hotkey.sendKey(pushToTalkKey, false);
                    }
                }
                */
                }
            }
        }
        

        private void SoundboardForm_Load(object sender, EventArgs e)
        {

        }

        private void settingsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new SettingsForm();
            form.ShowDialog();
        }
        /*
        private void texttospeechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new TextToSpeechForm();
            form.ShowDialog();
        }
        */

        private void texttospeechToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            var form = new TextToSpeechForm();
            form.ShowDialog();
        }


        
       

        private void bSetVolume_Click(object sender, EventArgs e)
        {
            SoundFile sf;
            //if()

            if (lvKeySounds.Items.Count > 0 && lvKeySounds.CanSelect) {
                soundBoardData.AllBoards[lastSelectedBoard].SoundBoard.TryGetValue(lvKeySounds.SelectedIndices[0], out sf);
                //soundFileData.TryGetValue(lvKeySounds.SelectedIndices[0], out sf);
                sf.volume = float.Parse(tbSetVolume.Text);
            }
            //Boards.writeJSON(soundBoardData, jsonLoc);
            //loadJSONFile(jsonLoc);

        }


        private void cbEnablePushToTalk_CheckedChanged(object sender, EventArgs e)
        {
            if (cbEnablePushToTalk.Checked)
            {
                if (tbPushToTalkKey.Text == "" || (string)cbWindows.SelectedItem == "")
                {
                    cbEnablePushToTalk.Checked = false;
                    MessageBox.Show("There is either no push to talk key entered, or no window selected");
                    return;
                }

                cbWindows.Enabled = false;
            }
            else cbWindows.Enabled = true;
        }

        private void pushToTalkKeyTimer_Tick(object sender, EventArgs e)
        {
            if (Hotkey.IsKeyDown(Keys.Escape))
            {
                tbPushToTalkKey.Text = "";
                pushToTalkKey = default(Keys);
            }
            else
            {
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (Hotkey.IsKeyDown(key))
                    {
                        tbPushToTalkKey.Text = Helper.keysToString(key);
                        pushToTalkKey = key;
                        break;
                    }
                }
            }
        }

        private void btnReloadWindows_Click(object sender, EventArgs e)
        {
            loadWindows();
        }

        private void cbWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbEnable.Checked = false;
        }

        private void lvBoards_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lvBoards.item
            //lvBoards.SelectedItems[0]

            if (lvBoards.SelectedItems.Count > 0)
            {
                soundFileData.Clear();
                loadSoundboard(lvBoards.FocusedItem.Text);
            }
            lastSelectedBoard = lvBoards.FocusedItem.Text;


            

        }

        
        private void loadSoundboard(string boardName)
        {
            
            //IDictionary<int, SoundFile> soundboard;
            if (lvKeySounds.Items != null)
            {
                lvKeySounds.Items.Clear();
            }
            IDictionary<int,SoundFile> currentColl = new Dictionary<int, SoundFile>();
            Board currentBoard = new Board();
            soundBoardData.AllBoards.TryGetValue(boardName, out currentBoard);
            //soundBoardData.SoundBoards.TryGetValue(boardName, out currentColl);
            foreach(var obj in currentBoard.SoundBoard)
            {
                ListViewItem item = new ListViewItem();
                //item.Text = obj.Value.hotKey;
                item.Text = Helper.GetDescription(obj.Value.pressedKey);
                //v - redacted for cleaned file names for user, they only see the name of the file they imported instead of destination alongside file
                //item.SubItems.Add(obj.Value.filePath);
                item.SubItems.Add(Helper.cleanFileName(obj.Value.filePath));
                lvKeySounds.Items.Add(item);
                soundFileData.Add(soundFileData.Count, obj.Value);
            }
            // Unselect the board after loading it (probably remove this)
            //if (lvBoards.SelectedItems.Count > 0)
                //lvBoards.SelectedItems[0].Selected = false;


            //MessageBox.Show(soundFileData.Count.ToString());
            //lvBoards.FocusedItem = null;
        }

        private void btnReloadDevices_Click(object sender, EventArgs e)
        {
            loadSoundDevices();
        }

        private void reloadApp()
        {
            Application.Restart();
            Environment.Exit(0);
        }

        private void cmsListView_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void lvKeySounds_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if(lvBoards.SelectedItems.Count > 0)
            {                
                //lastSelectedBoard = lvBoards.SelectedItems[lvBoards.SelectedIndices[0]].Text;
                //lastLVKeysSoundsSelectedIndex = lvKeySounds.SelectedIndices[0];
                //lvBoards.Items[lvBoards.SelectedIndices[0]].Selected = false;
            }
            */
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            reloadApp();
        }

        private void btnNewBoard_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Enter a name for the new board", "Soundboard name", "");
            //string last = jsonLoc;
            jsonLoc = Boards.userGetJSONLoc();

            if (File.Exists(jsonLoc))
                loadJSONFile(jsonLoc);


            //soundBoardData.BoardName = input;
            soundBoardData.JsonPath = jsonLoc;
            //soundBoardData.SoundCount = soundFileData.Count;
            //MessageBox.Show("Hi");
            //                .SoundBoards
            soundFileData.Clear();
            if (soundBoardData.AllBoards.ContainsKey(input))
            {
                if (MessageBox.Show("That board already exists in the JSON file. Would you like to overwrite it?", "Overwrite", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    soundBoardData.AllBoards[input].SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                    //Board addedBoard = new Board();
                    //addedBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;

                }
                //MessageBox.Show("Are you sure remove that item?", "Remove", MessageBoxButtons.YesNo) == DialogResult.Yes
            }
            else
            {
                Board addedBoard = new Board();
                addedBoard.SoundBoard = (Dictionary<int, SoundFile>)soundFileData;
                soundBoardData.AllBoards.Add(input, addedBoard);
            }
            //1MessageBox.Show("Hi");


            Boards.writeJSON(soundBoardData, jsonLoc);
            //repopulate the boards List view
            loadJSONFile(jsonLoc);
            lvKeySounds.Items.Clear();

        }
    }
    public class VolumeEventArgs : EventArgs
    {
        public VolumeEventArgs(float volume)
        {
            Volume = volume;
        }

        public float Volume { get; private set; }
    }


}
