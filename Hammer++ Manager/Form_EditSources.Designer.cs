namespace HammerPP_Manager
{
    partial class Form_EditSources
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_EditSources));
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.listboxSources = new System.Windows.Forms.ListBox();
            this.buttonAddFolder = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAddVPKFolder = new System.Windows.Forms.Button();
            this.buttonAddVPK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(378, 187);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.groupBox1.Controls.Add(this.listboxSources);
            this.groupBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(441, 169);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sources:";
            // 
            // listboxSources
            // 
            this.listboxSources.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listboxSources.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listboxSources.FormattingEnabled = true;
            this.listboxSources.Location = new System.Drawing.Point(6, 19);
            this.listboxSources.Name = "listboxSources";
            this.listboxSources.ScrollAlwaysVisible = true;
            this.listboxSources.Size = new System.Drawing.Size(429, 143);
            this.listboxSources.TabIndex = 0;
            // 
            // buttonAddFolder
            // 
            this.buttonAddFolder.Location = new System.Drawing.Point(12, 187);
            this.buttonAddFolder.Name = "buttonAddFolder";
            this.buttonAddFolder.Size = new System.Drawing.Size(75, 23);
            this.buttonAddFolder.TabIndex = 2;
            this.buttonAddFolder.Text = "Add Folder";
            this.buttonAddFolder.UseVisualStyleBackColor = true;
            this.buttonAddFolder.Click += new System.EventHandler(this.buttonAddFolder_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Location = new System.Drawing.Point(286, 187);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAddVPKFolder
            // 
            this.buttonAddVPKFolder.Location = new System.Drawing.Point(93, 187);
            this.buttonAddVPKFolder.Name = "buttonAddVPKFolder";
            this.buttonAddVPKFolder.Size = new System.Drawing.Size(106, 23);
            this.buttonAddVPKFolder.TabIndex = 4;
            this.buttonAddVPKFolder.Text = "Add VPKs in Folder";
            this.buttonAddVPKFolder.UseVisualStyleBackColor = true;
            this.buttonAddVPKFolder.Click += new System.EventHandler(this.buttonAddVPKFolder_Click);
            // 
            // buttonAddVPK
            // 
            this.buttonAddVPK.Location = new System.Drawing.Point(205, 187);
            this.buttonAddVPK.Name = "buttonAddVPK";
            this.buttonAddVPK.Size = new System.Drawing.Size(75, 23);
            this.buttonAddVPK.TabIndex = 5;
            this.buttonAddVPK.Text = "Add VPK";
            this.buttonAddVPK.UseVisualStyleBackColor = true;
            this.buttonAddVPK.Click += new System.EventHandler(this.buttonAddVPK_Click);
            // 
            // Form_EditSources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 219);
            this.Controls.Add(this.buttonAddVPK);
            this.Controls.Add(this.buttonAddVPKFolder);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAddFolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonClose);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimizeBox = false;
            this.Name = "Form_EditSources";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hammer++ Manager";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listboxSources;
        private System.Windows.Forms.Button buttonAddFolder;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonAddVPKFolder;
        private System.Windows.Forms.Button buttonAddVPK;
    }
}