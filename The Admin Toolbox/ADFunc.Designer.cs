namespace The_Admin_Toolbox
{
    partial class ADFunc
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
            this.buttonJoin = new System.Windows.Forms.Button();
            this.textBoxLocalUserName = new System.Windows.Forms.TextBox();
            this.textBoxLocalPassword = new System.Windows.Forms.TextBox();
            this.UsrNameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.comboBoxOUs = new System.Windows.Forms.ComboBox();
            this.textBoxDomainUserName = new System.Windows.Forms.TextBox();
            this.textBoxDomainPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNewName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonRename = new System.Windows.Forms.Button();
            this.buttonUnjoin = new System.Windows.Forms.Button();
            this.domainNametextBox = new System.Windows.Forms.TextBox();
            this.domainNamelabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonJoin
            // 
            this.buttonJoin.Location = new System.Drawing.Point(12, 303);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(78, 23);
            this.buttonJoin.TabIndex = 0;
            this.buttonJoin.Text = "Join";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // textBoxLocalUserName
            // 
            this.textBoxLocalUserName.Location = new System.Drawing.Point(12, 25);
            this.textBoxLocalUserName.Name = "textBoxLocalUserName";
            this.textBoxLocalUserName.Size = new System.Drawing.Size(301, 20);
            this.textBoxLocalUserName.TabIndex = 1;
            this.textBoxLocalUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxLocalPassword
            // 
            this.textBoxLocalPassword.Location = new System.Drawing.Point(12, 64);
            this.textBoxLocalPassword.Name = "textBoxLocalPassword";
            this.textBoxLocalPassword.PasswordChar = '*';
            this.textBoxLocalPassword.Size = new System.Drawing.Size(301, 20);
            this.textBoxLocalPassword.TabIndex = 2;
            this.textBoxLocalPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // UsrNameLabel
            // 
            this.UsrNameLabel.AutoSize = true;
            this.UsrNameLabel.Location = new System.Drawing.Point(104, 9);
            this.UsrNameLabel.Name = "UsrNameLabel";
            this.UsrNameLabel.Size = new System.Drawing.Size(84, 13);
            this.UsrNameLabel.TabIndex = 3;
            this.UsrNameLabel.Text = "Local Username";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(103, 48);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(82, 13);
            this.PasswordLabel.TabIndex = 4;
            this.PasswordLabel.Text = "Local Password";
            // 
            // comboBoxOUs
            // 
            this.comboBoxOUs.FormattingEnabled = true;
            this.comboBoxOUs.Location = new System.Drawing.Point(12, 220);
            this.comboBoxOUs.Name = "comboBoxOUs";
            this.comboBoxOUs.Size = new System.Drawing.Size(301, 21);
            this.comboBoxOUs.TabIndex = 5;
            // 
            // textBoxDomainUserName
            // 
            this.textBoxDomainUserName.Location = new System.Drawing.Point(12, 103);
            this.textBoxDomainUserName.Name = "textBoxDomainUserName";
            this.textBoxDomainUserName.Size = new System.Drawing.Size(301, 20);
            this.textBoxDomainUserName.TabIndex = 6;
            this.textBoxDomainUserName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxDomainUserName.Enter += new System.EventHandler(this.textBoxDomainUserName_Enter);
            // 
            // textBoxDomainPassword
            // 
            this.textBoxDomainPassword.Location = new System.Drawing.Point(12, 142);
            this.textBoxDomainPassword.Name = "textBoxDomainPassword";
            this.textBoxDomainPassword.PasswordChar = '*';
            this.textBoxDomainPassword.Size = new System.Drawing.Size(301, 20);
            this.textBoxDomainPassword.TabIndex = 7;
            this.textBoxDomainPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Domain Admin Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Domain Admin Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(126, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "OUs";
            // 
            // textBoxNewName
            // 
            this.textBoxNewName.Location = new System.Drawing.Point(12, 181);
            this.textBoxNewName.Name = "textBoxNewName";
            this.textBoxNewName.Size = new System.Drawing.Size(301, 20);
            this.textBoxNewName.TabIndex = 11;
            this.textBoxNewName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(104, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "New Name";
            // 
            // buttonRename
            // 
            this.buttonRename.Location = new System.Drawing.Point(129, 303);
            this.buttonRename.Name = "buttonRename";
            this.buttonRename.Size = new System.Drawing.Size(75, 23);
            this.buttonRename.TabIndex = 13;
            this.buttonRename.Text = "Rename";
            this.buttonRename.UseVisualStyleBackColor = true;
            this.buttonRename.Click += new System.EventHandler(this.buttonRename_Click);
            // 
            // buttonUnjoin
            // 
            this.buttonUnjoin.Location = new System.Drawing.Point(238, 303);
            this.buttonUnjoin.Name = "buttonUnjoin";
            this.buttonUnjoin.Size = new System.Drawing.Size(75, 23);
            this.buttonUnjoin.TabIndex = 14;
            this.buttonUnjoin.Text = "Unjoin";
            this.buttonUnjoin.UseVisualStyleBackColor = true;
            this.buttonUnjoin.Click += new System.EventHandler(this.buttonUnjoin_Click);
            // 
            // domainNametextBox
            // 
            this.domainNametextBox.Location = new System.Drawing.Point(12, 262);
            this.domainNametextBox.Name = "domainNametextBox";
            this.domainNametextBox.Size = new System.Drawing.Size(301, 20);
            this.domainNametextBox.TabIndex = 15;
            // 
            // domainNamelabel
            // 
            this.domainNamelabel.AutoSize = true;
            this.domainNamelabel.Location = new System.Drawing.Point(119, 246);
            this.domainNamelabel.Name = "domainNamelabel";
            this.domainNamelabel.Size = new System.Drawing.Size(43, 13);
            this.domainNamelabel.TabIndex = 16;
            this.domainNamelabel.Text = "Domain";
            // 
            // ADFunc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 338);
            this.Controls.Add(this.domainNamelabel);
            this.Controls.Add(this.domainNametextBox);
            this.Controls.Add(this.buttonUnjoin);
            this.Controls.Add(this.buttonRename);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxNewName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDomainPassword);
            this.Controls.Add(this.textBoxDomainUserName);
            this.Controls.Add(this.comboBoxOUs);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UsrNameLabel);
            this.Controls.Add(this.textBoxLocalPassword);
            this.Controls.Add(this.textBoxLocalUserName);
            this.Controls.Add(this.buttonJoin);
            this.Name = "ADFunc";
            this.Text = "Computer AD Tasks";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.TextBox textBoxLocalUserName;
        private System.Windows.Forms.TextBox textBoxLocalPassword;
        private System.Windows.Forms.Label UsrNameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.ComboBox comboBoxOUs;
        private System.Windows.Forms.TextBox textBoxDomainUserName;
        private System.Windows.Forms.TextBox textBoxDomainPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNewName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonRename;
        private System.Windows.Forms.Button buttonUnjoin;
        private System.Windows.Forms.TextBox domainNametextBox;
        private System.Windows.Forms.Label domainNamelabel;
    }
}