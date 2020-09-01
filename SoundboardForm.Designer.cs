namespace SoundBoard
{
    partial class SoundboardForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SoundboardForm));
            this.lvKeySounds = new System.Windows.Forms.ListView();
            this.chHotkey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cbAudioDevices = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cbRecDevice = new System.Windows.Forms.ComboBox();
            this.cbPlaybackDevices = new System.Windows.Forms.ComboBox();
            this.cbLoopbackDevices = new System.Windows.Forms.ComboBox();
            this.btnReloadDevices = new System.Windows.Forms.Button();
            this.btnStopAllSounds = new System.Windows.Forms.Button();
            this.btnPlaySelectedSound = new System.Windows.Forms.Button();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbEnable = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.texttospeechToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainTimer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.pushToTalkKeyTimer = new System.Windows.Forms.Timer(this.components);
            this.gbSetVolume = new System.Windows.Forms.GroupBox();
            this.bSetVolume = new System.Windows.Forms.Button();
            this.tbSetVolume = new System.Windows.Forms.TextBox();
            this.soundboardToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbPushToTalk = new System.Windows.Forms.GroupBox();
            this.btnReloadWindows = new System.Windows.Forms.Button();
            this.cbEnablePushToTalk = new System.Windows.Forms.CheckBox();
            this.cbWindows = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPushToTalkKey = new System.Windows.Forms.TextBox();
            this.lvBoards = new System.Windows.Forms.ListView();
            this.chBoard = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cmsListView = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cbAudioDevices.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.gbSetVolume.SuspendLayout();
            this.gbPushToTalk.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvKeySounds
            // 
            this.lvKeySounds.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvKeySounds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvKeySounds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chHotkey,
            this.chFileName});
            this.lvKeySounds.FullRowSelect = true;
            this.lvKeySounds.GridLines = true;
            this.lvKeySounds.HideSelection = false;
            this.lvKeySounds.Location = new System.Drawing.Point(12, 27);
            this.lvKeySounds.MultiSelect = false;
            this.lvKeySounds.Name = "lvKeySounds";
            this.lvKeySounds.Size = new System.Drawing.Size(532, 316);
            this.lvKeySounds.TabIndex = 1;
            this.lvKeySounds.UseCompatibleStateImageBehavior = false;
            this.lvKeySounds.View = System.Windows.Forms.View.Details;
            // 
            // chHotkey
            // 
            this.chHotkey.Text = "Hotkey";
            this.chHotkey.Width = 150;
            // 
            // chFileName
            // 
            this.chFileName.Text = "FileName";
            this.chFileName.Width = 300;
            // 
            // cbAudioDevices
            // 
            this.cbAudioDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAudioDevices.Controls.Add(this.label1);
            this.cbAudioDevices.Controls.Add(this.label2);
            this.cbAudioDevices.Controls.Add(this.label5);
            this.cbAudioDevices.Controls.Add(this.cbRecDevice);
            this.cbAudioDevices.Controls.Add(this.cbPlaybackDevices);
            this.cbAudioDevices.Controls.Add(this.cbLoopbackDevices);
            this.cbAudioDevices.Controls.Add(this.btnReloadDevices);
            this.cbAudioDevices.Location = new System.Drawing.Point(12, 378);
            this.cbAudioDevices.Name = "cbAudioDevices";
            this.cbAudioDevices.Size = new System.Drawing.Size(363, 120);
            this.cbAudioDevices.TabIndex = 27;
            this.cbAudioDevices.TabStop = false;
            this.cbAudioDevices.Text = "Audio devices";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Playback";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Loopback";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Rec Device";
            // 
            // cbRecDevice
            // 
            this.cbRecDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbRecDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRecDevice.FormattingEnabled = true;
            this.cbRecDevice.Location = new System.Drawing.Point(72, 77);
            this.cbRecDevice.Name = "cbRecDevice";
            this.cbRecDevice.Size = new System.Drawing.Size(240, 21);
            this.cbRecDevice.TabIndex = 33;
            this.cbRecDevice.TabStop = false;
            // 
            // cbPlaybackDevices
            // 
            this.cbPlaybackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPlaybackDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlaybackDevices.FormattingEnabled = true;
            this.cbPlaybackDevices.Location = new System.Drawing.Point(72, 23);
            this.cbPlaybackDevices.Name = "cbPlaybackDevices";
            this.cbPlaybackDevices.Size = new System.Drawing.Size(240, 21);
            this.cbPlaybackDevices.TabIndex = 10;
            this.cbPlaybackDevices.TabStop = false;
            // 
            // cbLoopbackDevices
            // 
            this.cbLoopbackDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLoopbackDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLoopbackDevices.FormattingEnabled = true;
            this.cbLoopbackDevices.Location = new System.Drawing.Point(72, 50);
            this.cbLoopbackDevices.Name = "cbLoopbackDevices";
            this.cbLoopbackDevices.Size = new System.Drawing.Size(240, 21);
            this.cbLoopbackDevices.TabIndex = 11;
            this.cbLoopbackDevices.TabStop = false;
            // 
            // btnReloadDevices
            // 
            this.btnReloadDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadDevices.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadDevices.Image")));
            this.btnReloadDevices.Location = new System.Drawing.Point(318, 50);
            this.btnReloadDevices.Name = "btnReloadDevices";
            this.btnReloadDevices.Size = new System.Drawing.Size(22, 22);
            this.btnReloadDevices.TabIndex = 12;
            this.btnReloadDevices.TabStop = false;
            this.btnReloadDevices.UseVisualStyleBackColor = true;
            this.btnReloadDevices.Click += new System.EventHandler(this.btnReloadDevices_Click);
            // 
            // btnStopAllSounds
            // 
            this.btnStopAllSounds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStopAllSounds.Location = new System.Drawing.Point(550, 272);
            this.btnStopAllSounds.Name = "btnStopAllSounds";
            this.btnStopAllSounds.Size = new System.Drawing.Size(75, 43);
            this.btnStopAllSounds.TabIndex = 23;
            this.btnStopAllSounds.TabStop = false;
            this.btnStopAllSounds.Text = "Stop all sounds";
            this.btnStopAllSounds.UseVisualStyleBackColor = true;
            this.btnStopAllSounds.Click += new System.EventHandler(this.btnStopAllSounds_Click);
            // 
            // btnPlaySelectedSound
            // 
            this.btnPlaySelectedSound.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlaySelectedSound.Location = new System.Drawing.Point(550, 223);
            this.btnPlaySelectedSound.Name = "btnPlaySelectedSound";
            this.btnPlaySelectedSound.Size = new System.Drawing.Size(75, 43);
            this.btnPlaySelectedSound.TabIndex = 22;
            this.btnPlaySelectedSound.TabStop = false;
            this.btnPlaySelectedSound.Text = "Play sound";
            this.btnPlaySelectedSound.UseVisualStyleBackColor = true;
            this.btnPlaySelectedSound.Click += new System.EventHandler(this.btnPlaySelectedSound_Click);
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveAs.Location = new System.Drawing.Point(381, 349);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(145, 23);
            this.btnSaveAs.TabIndex = 26;
            this.btnSaveAs.TabStop = false;
            this.btnSaveAs.Text = "Save As";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Location = new System.Drawing.Point(550, 174);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 43);
            this.btnClear.TabIndex = 21;
            this.btnClear.TabStop = false;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLoad.Location = new System.Drawing.Point(79, 349);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(145, 23);
            this.btnLoad.TabIndex = 24;
            this.btnLoad.TabStop = false;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(230, 349);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(145, 23);
            this.btnSave.TabIndex = 25;
            this.btnSave.TabStop = false;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(550, 125);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 43);
            this.btnRemove.TabIndex = 20;
            this.btnRemove.TabStop = false;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(550, 76);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 43);
            this.btnEdit.TabIndex = 19;
            this.btnEdit.TabStop = false;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(550, 27);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 43);
            this.btnAdd.TabIndex = 18;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // cbEnable
            // 
            this.cbEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cbEnable.AutoSize = true;
            this.cbEnable.Location = new System.Drawing.Point(550, 349);
            this.cbEnable.Name = "cbEnable";
            this.cbEnable.Size = new System.Drawing.Size(59, 17);
            this.cbEnable.TabIndex = 29;
            this.cbEnable.TabStop = false;
            this.cbEnable.Text = "Enable";
            this.cbEnable.UseVisualStyleBackColor = true;
            this.cbEnable.CheckedChanged += new System.EventHandler(this.cbEnable_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.texttospeechToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(797, 24);
            this.menuStrip1.TabIndex = 30;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click_1);
            // 
            // texttospeechToolStripMenuItem
            // 
            this.texttospeechToolStripMenuItem.Name = "texttospeechToolStripMenuItem";
            this.texttospeechToolStripMenuItem.Size = new System.Drawing.Size(98, 20);
            this.texttospeechToolStripMenuItem.Text = "Text-to-speech";
            this.texttospeechToolStripMenuItem.Click += new System.EventHandler(this.texttospeechToolStripMenuItem_Click_1);
            // 
            // mainTimer
            // 
            this.mainTimer.Interval = 50;
            this.mainTimer.Tick += new System.EventHandler(this.mainTimer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "Minimized to the tray.";
            this.notifyIcon1.BalloonTipTitle = "JN Soundboard";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "JN Soundboard";
            // 
            // pushToTalkKeyTimer
            // 
            this.pushToTalkKeyTimer.Tick += new System.EventHandler(this.pushToTalkKeyTimer_Tick);
            // 
            // gbSetVolume
            // 
            this.gbSetVolume.Controls.Add(this.bSetVolume);
            this.gbSetVolume.Controls.Add(this.tbSetVolume);
            this.gbSetVolume.Location = new System.Drawing.Point(631, 27);
            this.gbSetVolume.Name = "gbSetVolume";
            this.gbSetVolume.Size = new System.Drawing.Size(154, 100);
            this.gbSetVolume.TabIndex = 32;
            this.gbSetVolume.TabStop = false;
            this.gbSetVolume.Text = "Set Volume for sound file";
            // 
            // bSetVolume
            // 
            this.bSetVolume.Location = new System.Drawing.Point(6, 71);
            this.bSetVolume.Name = "bSetVolume";
            this.bSetVolume.Size = new System.Drawing.Size(142, 23);
            this.bSetVolume.TabIndex = 2;
            this.bSetVolume.TabStop = false;
            this.bSetVolume.Text = "Set Volume";
            this.bSetVolume.UseVisualStyleBackColor = true;
            this.bSetVolume.Click += new System.EventHandler(this.bSetVolume_Click);
            // 
            // tbSetVolume
            // 
            this.tbSetVolume.Location = new System.Drawing.Point(6, 20);
            this.tbSetVolume.Name = "tbSetVolume";
            this.tbSetVolume.Size = new System.Drawing.Size(142, 20);
            this.tbSetVolume.TabIndex = 1;
            this.tbSetVolume.TabStop = false;
            // 
            // gbPushToTalk
            // 
            this.gbPushToTalk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPushToTalk.Controls.Add(this.btnReloadWindows);
            this.gbPushToTalk.Controls.Add(this.cbEnablePushToTalk);
            this.gbPushToTalk.Controls.Add(this.cbWindows);
            this.gbPushToTalk.Controls.Add(this.label4);
            this.gbPushToTalk.Controls.Add(this.label3);
            this.gbPushToTalk.Controls.Add(this.tbPushToTalkKey);
            this.gbPushToTalk.Location = new System.Drawing.Point(381, 404);
            this.gbPushToTalk.Name = "gbPushToTalk";
            this.gbPushToTalk.Size = new System.Drawing.Size(254, 94);
            this.gbPushToTalk.TabIndex = 35;
            this.gbPushToTalk.TabStop = false;
            this.gbPushToTalk.Text = "Auto press push to talk key";
            // 
            // btnReloadWindows
            // 
            this.btnReloadWindows.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadWindows.Image = ((System.Drawing.Image)(resources.GetObject("btnReloadWindows.Image")));
            this.btnReloadWindows.Location = new System.Drawing.Point(226, 45);
            this.btnReloadWindows.Name = "btnReloadWindows";
            this.btnReloadWindows.Size = new System.Drawing.Size(22, 22);
            this.btnReloadWindows.TabIndex = 15;
            this.btnReloadWindows.TabStop = false;
            this.btnReloadWindows.UseVisualStyleBackColor = true;
            this.btnReloadWindows.Click += new System.EventHandler(this.btnReloadWindows_Click);
            // 
            // cbEnablePushToTalk
            // 
            this.cbEnablePushToTalk.AutoSize = true;
            this.cbEnablePushToTalk.Location = new System.Drawing.Point(10, 72);
            this.cbEnablePushToTalk.Name = "cbEnablePushToTalk";
            this.cbEnablePushToTalk.Size = new System.Drawing.Size(59, 17);
            this.cbEnablePushToTalk.TabIndex = 16;
            this.cbEnablePushToTalk.TabStop = false;
            this.cbEnablePushToTalk.Text = "Enable";
            this.cbEnablePushToTalk.UseVisualStyleBackColor = true;
            this.cbEnablePushToTalk.CheckedChanged += new System.EventHandler(this.cbEnablePushToTalk_CheckedChanged);
            // 
            // cbWindows
            // 
            this.cbWindows.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbWindows.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbWindows.FormattingEnabled = true;
            this.cbWindows.Location = new System.Drawing.Point(59, 45);
            this.cbWindows.Name = "cbWindows";
            this.cbWindows.Size = new System.Drawing.Size(161, 21);
            this.cbWindows.TabIndex = 14;
            this.cbWindows.TabStop = false;
            this.cbWindows.SelectedIndexChanged += new System.EventHandler(this.cbWindows_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Key";
            // 
            // tbPushToTalkKey
            // 
            this.tbPushToTalkKey.Location = new System.Drawing.Point(59, 19);
            this.tbPushToTalkKey.Name = "tbPushToTalkKey";
            this.tbPushToTalkKey.ReadOnly = true;
            this.tbPushToTalkKey.Size = new System.Drawing.Size(161, 20);
            this.tbPushToTalkKey.TabIndex = 13;
            this.tbPushToTalkKey.TabStop = false;
            this.tbPushToTalkKey.Enter += new System.EventHandler(this.tbPushToTalkKey_Enter);
            this.tbPushToTalkKey.Leave += new System.EventHandler(this.tbPushToTalkKey_Leave);
            // 
            // lvBoards
            // 
            this.lvBoards.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvBoards.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chBoard});
            this.lvBoards.GridLines = true;
            this.lvBoards.HideSelection = false;
            this.lvBoards.Location = new System.Drawing.Point(635, 133);
            this.lvBoards.Name = "lvBoards";
            this.lvBoards.Size = new System.Drawing.Size(150, 371);
            this.lvBoards.TabIndex = 36;
            this.lvBoards.UseCompatibleStateImageBehavior = false;
            this.lvBoards.View = System.Windows.Forms.View.Details;
            this.lvBoards.SelectedIndexChanged += new System.EventHandler(this.lvBoards_SelectedIndexChanged);
            // 
            // chBoard
            // 
            this.chBoard.Text = "Boards";
            this.chBoard.Width = 150;
            // 
            // cmsListView
            // 
            this.cmsListView.Name = "cmsListView";
            this.cmsListView.Size = new System.Drawing.Size(61, 4);
            this.cmsListView.Opening += new System.ComponentModel.CancelEventHandler(this.cmsListView_Opening);
            // 
            // SoundboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 516);
            this.Controls.Add(this.lvBoards);
            this.Controls.Add(this.gbPushToTalk);
            this.Controls.Add(this.gbSetVolume);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.cbAudioDevices);
            this.Controls.Add(this.btnStopAllSounds);
            this.Controls.Add(this.btnPlaySelectedSound);
            this.Controls.Add(this.btnSaveAs);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cbEnable);
            this.Controls.Add(this.lvKeySounds);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SoundboardForm";
            this.Text = "SoundboardForm";
            this.Load += new System.EventHandler(this.SoundboardForm_Load);
            this.cbAudioDevices.ResumeLayout(false);
            this.cbAudioDevices.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbSetVolume.ResumeLayout(false);
            this.gbSetVolume.PerformLayout();
            this.gbPushToTalk.ResumeLayout(false);
            this.gbPushToTalk.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ListView lvKeySounds;
        internal System.Windows.Forms.ColumnHeader chHotkey;
        internal System.Windows.Forms.ColumnHeader chFileName;
        private System.Windows.Forms.GroupBox cbAudioDevices;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbPlaybackDevices;
        private System.Windows.Forms.ComboBox cbLoopbackDevices;
        private System.Windows.Forms.Button btnReloadDevices;
        private System.Windows.Forms.Button btnStopAllSounds;
        private System.Windows.Forms.Button btnPlaySelectedSound;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.CheckBox cbEnable;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem texttospeechToolStripMenuItem;
        internal System.Windows.Forms.Timer mainTimer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Timer pushToTalkKeyTimer;
        private System.Windows.Forms.GroupBox gbSetVolume;
        private System.Windows.Forms.TextBox tbSetVolume;
        private System.Windows.Forms.Button bSetVolume;
        private System.Windows.Forms.ToolTip soundboardToolTip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbRecDevice;
        private System.Windows.Forms.GroupBox gbPushToTalk;
        private System.Windows.Forms.Button btnReloadWindows;
        private System.Windows.Forms.CheckBox cbEnablePushToTalk;
        private System.Windows.Forms.ComboBox cbWindows;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPushToTalkKey;
        internal System.Windows.Forms.ColumnHeader chBoard;
        private System.Windows.Forms.ContextMenuStrip cmsListView;
        internal System.Windows.Forms.ListView lvBoards;
    }
}