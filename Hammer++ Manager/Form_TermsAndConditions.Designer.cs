namespace HammerPP_Manager
{
    partial class Form_TermsAndConditions
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_TermsAndConditions));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.labelCopyright = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.pictureboxIcon = new System.Windows.Forms.PictureBox();
            this.labelProductName = new System.Windows.Forms.Label();
            this.textboxLicense = new HammerPP_Manager.ReadOnlyTextBox();
            this.buttonPrevious = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelLicenseCount = new System.Windows.Forms.Label();
            this.labelLicenseName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(9, 67);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(63, 13);
            this.linkLabel1.TabIndex = 36;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "GitHub Link";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // labelCopyright
            // 
            this.labelCopyright.AutoSize = true;
            this.labelCopyright.BackColor = System.Drawing.SystemColors.Control;
            this.labelCopyright.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelCopyright.Location = new System.Drawing.Point(76, 51);
            this.labelCopyright.Name = "labelCopyright";
            this.labelCopyright.Size = new System.Drawing.Size(35, 13);
            this.labelCopyright.TabIndex = 34;
            this.labelCopyright.Text = "label1";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.SystemColors.Control;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelVersion.Location = new System.Drawing.Point(76, 38);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(35, 13);
            this.labelVersion.TabIndex = 33;
            this.labelVersion.Text = "label1";
            // 
            // okButton
            // 
            this.okButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.okButton.BackColor = System.Drawing.SystemColors.Control;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.okButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.okButton.Location = new System.Drawing.Point(401, 325);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 31;
            this.okButton.Text = "Close";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // pictureboxIcon
            // 
            this.pictureboxIcon.BackColor = System.Drawing.SystemColors.Control;
            this.pictureboxIcon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pictureboxIcon.Image = global::HammerPP_Manager.Properties.Resources.hpp64;
            this.pictureboxIcon.Location = new System.Drawing.Point(12, 12);
            this.pictureboxIcon.Name = "pictureboxIcon";
            this.pictureboxIcon.Size = new System.Drawing.Size(55, 55);
            this.pictureboxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureboxIcon.TabIndex = 37;
            this.pictureboxIcon.TabStop = false;
            // 
            // labelProductName
            // 
            this.labelProductName.AutoSize = true;
            this.labelProductName.BackColor = System.Drawing.SystemColors.Control;
            this.labelProductName.Enabled = false;
            this.labelProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelProductName.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelProductName.Location = new System.Drawing.Point(71, 13);
            this.labelProductName.Name = "labelProductName";
            this.labelProductName.Size = new System.Drawing.Size(223, 25);
            this.labelProductName.TabIndex = 38;
            this.labelProductName.Text = "Hammer++ Manager";
            // 
            // textboxLicense
            // 
            this.textboxLicense.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.textboxLicense.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textboxLicense.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textboxLicense.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.textboxLicense.Location = new System.Drawing.Point(21, 131);
            this.textboxLicense.Multiline = true;
            this.textboxLicense.Name = "textboxLicense";
            this.textboxLicense.ReadOnly = true;
            this.textboxLicense.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textboxLicense.Size = new System.Drawing.Size(446, 182);
            this.textboxLicense.TabIndex = 39;
            // 
            // buttonPrevious
            // 
            this.buttonPrevious.Location = new System.Drawing.Point(12, 325);
            this.buttonPrevious.Name = "buttonPrevious";
            this.buttonPrevious.Size = new System.Drawing.Size(75, 23);
            this.buttonPrevious.TabIndex = 40;
            this.buttonPrevious.Text = "Previous";
            this.buttonPrevious.UseVisualStyleBackColor = true;
            this.buttonPrevious.Click += new System.EventHandler(this.buttonPrevious_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(93, 325);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(75, 23);
            this.buttonNext.TabIndex = 41;
            this.buttonNext.Text = "Next";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(76, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(372, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "This program uses multiple technologies. Their licenses can be viewed below:";
            // 
            // labelLicenseCount
            // 
            this.labelLicenseCount.AutoSize = true;
            this.labelLicenseCount.Location = new System.Drawing.Point(184, 330);
            this.labelLicenseCount.Name = "labelLicenseCount";
            this.labelLicenseCount.Size = new System.Drawing.Size(30, 13);
            this.labelLicenseCount.TabIndex = 43;
            this.labelLicenseCount.Text = "1 / 3";
            // 
            // labelLicenseName
            // 
            this.labelLicenseName.AutoSize = true;
            this.labelLicenseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLicenseName.Location = new System.Drawing.Point(8, 95);
            this.labelLicenseName.Name = "labelLicenseName";
            this.labelLicenseName.Size = new System.Drawing.Size(241, 20);
            this.labelLicenseName.TabIndex = 44;
            this.labelLicenseName.Text = "License for Hammer++ Manager:";
            // 
            // Form_TermsAndConditions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 356);
            this.Controls.Add(this.labelLicenseName);
            this.Controls.Add(this.labelLicenseCount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.buttonPrevious);
            this.Controls.Add(this.textboxLicense);
            this.Controls.Add(this.labelProductName);
            this.Controls.Add(this.pictureboxIcon);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.labelCopyright);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.okButton);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Form_TermsAndConditions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hammer++ Manager - Terms & Conditions";
            ((System.ComponentModel.ISupportInitialize)(this.pictureboxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox pictureboxIcon;
        private System.Windows.Forms.Label labelProductName;
        private ReadOnlyTextBox textboxLicense;
        private System.Windows.Forms.Button buttonPrevious;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelLicenseCount;
        private System.Windows.Forms.Label labelLicenseName;
    }
}