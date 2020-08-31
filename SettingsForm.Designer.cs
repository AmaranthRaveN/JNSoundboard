﻿namespace SoundBoard
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbStopSoundKeys = new System.Windows.Forms.TextBox();
            this.lvKeysLocs = new System.Windows.Forms.ListView();
            this.chKeys = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chXMLLoc = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbMinimizeToTray = new System.Windows.Forms.CheckBox();
            this.cbPlaySoundsOverEachOther = new System.Windows.Forms.CheckBox();
            this.tbRecKey = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbStopRecKey = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRecDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseRecDirectory = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Stop all sounds keys";
            // 
            // tbStopSoundKeys
            // 
            this.tbStopSoundKeys.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStopSoundKeys.Location = new System.Drawing.Point(122, 12);
            this.tbStopSoundKeys.Name = "tbStopSoundKeys";
            this.tbStopSoundKeys.ReadOnly = true;
            this.tbStopSoundKeys.Size = new System.Drawing.Size(388, 20);
            this.tbStopSoundKeys.TabIndex = 0;
            this.tbStopSoundKeys.Enter += new System.EventHandler(this.tbStopSoundKeys_Enter);
            this.tbStopSoundKeys.Leave += new System.EventHandler(this.tbStopSoundKeys_Leave);
            // 
            // lvKeysLocs
            // 
            this.lvKeysLocs.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvKeysLocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvKeysLocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chKeys,
            this.chXMLLoc});
            this.lvKeysLocs.FullRowSelect = true;
            this.lvKeysLocs.GridLines = true;
            this.lvKeysLocs.HideSelection = false;
            this.lvKeysLocs.Location = new System.Drawing.Point(6, 19);
            this.lvKeysLocs.MultiSelect = false;
            this.lvKeysLocs.Name = "lvKeysLocs";
            this.lvKeysLocs.Size = new System.Drawing.Size(483, 238);
            this.lvKeysLocs.TabIndex = 1;
            this.lvKeysLocs.UseCompatibleStateImageBehavior = false;
            this.lvKeysLocs.View = System.Windows.Forms.View.Details;
            this.lvKeysLocs.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvKeysLocs_MouseDoubleClick);
            // 
            // chKeys
            // 
            this.chKeys.Text = "Keys";
            this.chKeys.Width = 150;
            // 
            // chXMLLoc
            // 
            this.chXMLLoc.Text = "XML location";
            this.chXMLLoc.Width = 300;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(354, 420);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 7;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAdd.Location = new System.Drawing.Point(6, 263);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEdit.Location = new System.Drawing.Point(87, 263);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemove.Location = new System.Drawing.Point(168, 263);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 4;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(435, 420);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.lvKeysLocs);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnRemove);
            this.groupBox1.Controls.Add(this.btnEdit);
            this.groupBox1.Location = new System.Drawing.Point(12, 108);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(495, 292);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Load XML file with keys";
            // 
            // cbMinimizeToTray
            // 
            this.cbMinimizeToTray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbMinimizeToTray.AutoSize = true;
            this.cbMinimizeToTray.Location = new System.Drawing.Point(12, 406);
            this.cbMinimizeToTray.Name = "cbMinimizeToTray";
            this.cbMinimizeToTray.Size = new System.Drawing.Size(216, 17);
            this.cbMinimizeToTray.TabIndex = 5;
            this.cbMinimizeToTray.Text = "Minimize button sends application to tray";
            this.cbMinimizeToTray.UseVisualStyleBackColor = true;
            // 
            // cbPlaySoundsOverEachOther
            // 
            this.cbPlaySoundsOverEachOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbPlaySoundsOverEachOther.AutoSize = true;
            this.cbPlaySoundsOverEachOther.Location = new System.Drawing.Point(12, 429);
            this.cbPlaySoundsOverEachOther.Name = "cbPlaySoundsOverEachOther";
            this.cbPlaySoundsOverEachOther.Size = new System.Drawing.Size(161, 17);
            this.cbPlaySoundsOverEachOther.TabIndex = 6;
            this.cbPlaySoundsOverEachOther.Text = "Play sounds over each other";
            this.cbPlaySoundsOverEachOther.UseVisualStyleBackColor = true;
            // 
            // tbRecKey
            // 
            this.tbRecKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecKey.Location = new System.Drawing.Point(122, 36);
            this.tbRecKey.Name = "tbRecKey";
            this.tbRecKey.ReadOnly = true;
            this.tbRecKey.Size = new System.Drawing.Size(388, 20);
            this.tbRecKey.TabIndex = 9;
            this.tbRecKey.TextChanged += new System.EventHandler(this.tbRecKey_TextChanged);
            this.tbRecKey.Enter += new System.EventHandler(this.tbRecKey_Enter);
            this.tbRecKey.Leave += new System.EventHandler(this.tbRecKey_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Record sound key\r\n";
            // 
            // tbStopRecKey
            // 
            this.tbStopRecKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbStopRecKey.Location = new System.Drawing.Point(122, 59);
            this.tbStopRecKey.Name = "tbStopRecKey";
            this.tbStopRecKey.ReadOnly = true;
            this.tbStopRecKey.Size = new System.Drawing.Size(388, 20);
            this.tbStopRecKey.TabIndex = 11;
            this.tbStopRecKey.Enter += new System.EventHandler(this.tbStopRecKey_Enter);
            this.tbStopRecKey.Leave += new System.EventHandler(this.tbStopRecKey_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Stop recording key";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Recording Directory";
            // 
            // tbRecDirectory
            // 
            this.tbRecDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbRecDirectory.Location = new System.Drawing.Point(122, 85);
            this.tbRecDirectory.Name = "tbRecDirectory";
            this.tbRecDirectory.ReadOnly = true;
            this.tbRecDirectory.Size = new System.Drawing.Size(354, 20);
            this.tbRecDirectory.TabIndex = 14;
            this.tbRecDirectory.Enter += new System.EventHandler(this.tbRecDirectory_Enter);
            this.tbRecDirectory.Leave += new System.EventHandler(this.tbRecDirectory_Leave);
            // 
            // btnBrowseRecDirectory
            // 
            this.btnBrowseRecDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseRecDirectory.Location = new System.Drawing.Point(482, 85);
            this.btnBrowseRecDirectory.Name = "btnBrowseRecDirectory";
            this.btnBrowseRecDirectory.Size = new System.Drawing.Size(28, 23);
            this.btnBrowseRecDirectory.TabIndex = 15;
            this.btnBrowseRecDirectory.Text = "...";
            this.btnBrowseRecDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseRecDirectory.Click += new System.EventHandler(this.btnBrowseRecDirectory_Click);
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 455);
            this.Controls.Add(this.btnBrowseRecDirectory);
            this.Controls.Add(this.tbRecDirectory);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbStopRecKey);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbRecKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbPlaySoundsOverEachOther);
            this.Controls.Add(this.cbMinimizeToTray);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tbStopSoundKeys);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(296, 277);
            this.Name = "SettingsForm";
            this.Text = "Soundboard Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStopSoundKeys;
        internal System.Windows.Forms.ListView lvKeysLocs;
        internal System.Windows.Forms.ColumnHeader chKeys;
        internal System.Windows.Forms.ColumnHeader chXMLLoc;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbMinimizeToTray;
        private System.Windows.Forms.CheckBox cbPlaySoundsOverEachOther;
        private System.Windows.Forms.TextBox tbRecKey;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbStopRecKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRecDirectory;
        private System.Windows.Forms.Button btnBrowseRecDirectory;
    }
}