namespace The_Admin_Toolbox
{
    partial class MoveUser
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
            this.OUcomboBox = new System.Windows.Forms.ComboBox();
            this.moveToLabel = new System.Windows.Forms.Label();
            this.moveOUButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // OUcomboBox
            // 
            this.OUcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.OUcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.OUcomboBox.FormattingEnabled = true;
            this.OUcomboBox.Items.AddRange(new object[] {
            "OU=Contacts,OU=Users,OU=US,DC=global,DC=gg,DC=group",
            "OU=Functional,OU=Users,OU=US,DC=global,DC=gg,DC=group",
            "OU=Functional Mailboxes,OU=Users,OU=US,DC=global,DC=gg,DC=group",
            "OU=Personal,OU=Users,OU=US,DC=global,DC=gg,DC=group",
            "OU=Special Accounts,OU=Users,OU=US,DC=global,DC=gg,DC=group"});
            this.OUcomboBox.Location = new System.Drawing.Point(13, 38);
            this.OUcomboBox.Name = "OUcomboBox";
            this.OUcomboBox.Size = new System.Drawing.Size(259, 21);
            this.OUcomboBox.TabIndex = 0;
            // 
            // moveToLabel
            // 
            this.moveToLabel.AutoSize = true;
            this.moveToLabel.Location = new System.Drawing.Point(85, 19);
            this.moveToLabel.Name = "moveToLabel";
            this.moveToLabel.Size = new System.Drawing.Size(81, 13);
            this.moveToLabel.TabIndex = 1;
            this.moveToLabel.Text = "Move to where:";
            // 
            // moveOUButton
            // 
            this.moveOUButton.Location = new System.Drawing.Point(88, 65);
            this.moveOUButton.Name = "moveOUButton";
            this.moveOUButton.Size = new System.Drawing.Size(75, 23);
            this.moveOUButton.TabIndex = 2;
            this.moveOUButton.Text = "Move";
            this.moveOUButton.UseVisualStyleBackColor = true;
            this.moveOUButton.Click += new System.EventHandler(this.moveOUButton_Click);
            // 
            // MoveUser
            // 
            this.AcceptButton = this.moveOUButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 96);
            this.Controls.Add(this.moveOUButton);
            this.Controls.Add(this.moveToLabel);
            this.Controls.Add(this.OUcomboBox);
            this.Name = "MoveUser";
            this.Text = "MoveUser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox OUcomboBox;
        private System.Windows.Forms.Label moveToLabel;
        private System.Windows.Forms.Button moveOUButton;
    }
}