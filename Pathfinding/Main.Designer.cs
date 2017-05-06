namespace Pathfinding
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.randomizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDimensionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveBoardToFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.musicOnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableSoundEffectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.randomizeToolStripMenuItem,
            this.changeDimensionsToolStripMenuItem,
            this.loadFileToolStripMenuItem,
            this.saveBoardToFileToolStripMenuItem,
            this.musicOnToolStripMenuItem,
            this.enableSoundEffectsToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(184, 158);
            // 
            // randomizeToolStripMenuItem
            // 
            this.randomizeToolStripMenuItem.Name = "randomizeToolStripMenuItem";
            this.randomizeToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.randomizeToolStripMenuItem.Text = "Reset (Hotkey: R)";
            this.randomizeToolStripMenuItem.Click += new System.EventHandler(this.ResetToolStripMenuItem_Click);
            // 
            // changeDimensionsToolStripMenuItem
            // 
            this.changeDimensionsToolStripMenuItem.Name = "changeDimensionsToolStripMenuItem";
            this.changeDimensionsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.changeDimensionsToolStripMenuItem.Text = "Change level size";
            this.changeDimensionsToolStripMenuItem.Click += new System.EventHandler(this.ChangeDimensionsToolStripMenuItem_Click);
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.loadFileToolStripMenuItem.Text = "Load level file";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.LoadFileToolStripMenuItem_Click);
            // 
            // saveBoardToFileToolStripMenuItem
            // 
            this.saveBoardToFileToolStripMenuItem.Name = "saveBoardToFileToolStripMenuItem";
            this.saveBoardToFileToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.saveBoardToFileToolStripMenuItem.Text = "Save level to file";
            this.saveBoardToFileToolStripMenuItem.Click += new System.EventHandler(this.SaveBoardToFileToolStripMenuItem_Click);
            // 
            // musicOnToolStripMenuItem
            // 
            this.musicOnToolStripMenuItem.CheckOnClick = true;
            this.musicOnToolStripMenuItem.Name = "musicOnToolStripMenuItem";
            this.musicOnToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.musicOnToolStripMenuItem.Text = "Enable music";
            this.musicOnToolStripMenuItem.Click += new System.EventHandler(this.MusicOnToolStripMenuItem_Click);
            // 
            // enableSoundEffectsToolStripMenuItem
            // 
            this.enableSoundEffectsToolStripMenuItem.Checked = true;
            this.enableSoundEffectsToolStripMenuItem.CheckOnClick = true;
            this.enableSoundEffectsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableSoundEffectsToolStripMenuItem.Name = "enableSoundEffectsToolStripMenuItem";
            this.enableSoundEffectsToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.enableSoundEffectsToolStripMenuItem.Text = "Enable sound effects";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.ContextMenuStrip = this.contextMenuStrip;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.Text = "Pathfinding.exe";
            this.Load += new System.EventHandler(this.Main_Load);
            this.Click += new System.EventHandler(this.Main_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawBoard);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        
        private System.Windows.Forms.ToolStripMenuItem randomizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDimensionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveBoardToFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem musicOnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableSoundEffectsToolStripMenuItem;
    }
}

