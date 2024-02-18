namespace HammerPP_Manager
{
    partial class Form_CrashWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CrashWindow));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonSaveDump = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textboxCrashLog = new HammerPP_Manager.ReadOnlyTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HammerPP_Manager.Properties.Resources.gameend;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 55);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // buttonSaveDump
            // 
            this.buttonSaveDump.Location = new System.Drawing.Point(384, 252);
            this.buttonSaveDump.Name = "buttonSaveDump";
            this.buttonSaveDump.Size = new System.Drawing.Size(82, 23);
            this.buttonSaveDump.TabIndex = 12;
            this.buttonSaveDump.Text = "Save Dump";
            this.buttonSaveDump.UseVisualStyleBackColor = true;
            this.buttonSaveDump.Click += new System.EventHandler(this.buttonSaveDump_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.Location = new System.Drawing.Point(472, 252);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(82, 23);
            this.buttonExit.TabIndex = 11;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(227, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Please send the crash log to rmod8 on GitHub,";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(318, 25);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hammer++ Manager Crashed";
            // 
            // textboxCrashLog
            // 
            this.textboxCrashLog.BackColor = System.Drawing.Color.White;
            this.textboxCrashLog.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textboxCrashLog.Location = new System.Drawing.Point(12, 69);
            this.textboxCrashLog.Multiline = true;
            this.textboxCrashLog.Name = "textboxCrashLog";
            this.textboxCrashLog.ReadOnly = true;
            this.textboxCrashLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxCrashLog.Size = new System.Drawing.Size(542, 177);
            this.textboxCrashLog.TabIndex = 8;
            // 
            // Form_CrashWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 281);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonSaveDump);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textboxCrashLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_CrashWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "Hammer++ Manager - Crash";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonSaveDump;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private ReadOnlyTextBox textboxCrashLog;
    }
}