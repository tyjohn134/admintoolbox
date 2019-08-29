namespace The_Admin_Toolbox
{
    partial class Settings
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
            this.sccmSettingTextBox = new System.Windows.Forms.TextBox();
            this.sccmServerLabel = new System.Windows.Forms.Label();
            this.saveSettingButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // sccmSettingTextBox
            // 
            this.sccmSettingTextBox.Location = new System.Drawing.Point(22, 36);
            this.sccmSettingTextBox.Name = "sccmSettingTextBox";
            this.sccmSettingTextBox.Size = new System.Drawing.Size(230, 20);
            this.sccmSettingTextBox.TabIndex = 0;
            // 
            // sccmServerLabel
            // 
            this.sccmServerLabel.AutoSize = true;
            this.sccmServerLabel.Location = new System.Drawing.Point(22, 17);
            this.sccmServerLabel.Name = "sccmServerLabel";
            this.sccmServerLabel.Size = new System.Drawing.Size(71, 13);
            this.sccmServerLabel.TabIndex = 1;
            this.sccmServerLabel.Text = "SCCM Server";
            // 
            // saveSettingButton
            // 
            this.saveSettingButton.Location = new System.Drawing.Point(93, 78);
            this.saveSettingButton.Name = "saveSettingButton";
            this.saveSettingButton.Size = new System.Drawing.Size(75, 23);
            this.saveSettingButton.TabIndex = 2;
            this.saveSettingButton.Text = "Sumbit";
            this.saveSettingButton.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 128);
            this.Controls.Add(this.saveSettingButton);
            this.Controls.Add(this.sccmServerLabel);
            this.Controls.Add(this.sccmSettingTextBox);
            this.Name = "Settings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox sccmSettingTextBox;
        private System.Windows.Forms.Label sccmServerLabel;
        private System.Windows.Forms.Button saveSettingButton;
    }
}