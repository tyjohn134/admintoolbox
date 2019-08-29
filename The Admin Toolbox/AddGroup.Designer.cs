namespace The_Admin_Toolbox
{
    partial class AddGroup
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
            this.grpNamelabel = new System.Windows.Forms.Label();
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.groupcomboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // grpNamelabel
            // 
            this.grpNamelabel.AutoSize = true;
            this.grpNamelabel.Location = new System.Drawing.Point(57, 9);
            this.grpNamelabel.Name = "grpNamelabel";
            this.grpNamelabel.Size = new System.Drawing.Size(191, 13);
            this.grpNamelabel.TabIndex = 1;
            this.grpNamelabel.Text = "Add/Remove User From Which Group:";
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(60, 51);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(141, 51);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(75, 23);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // groupcomboBox
            // 
            this.groupcomboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.groupcomboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.groupcomboBox.FormattingEnabled = true;
            this.groupcomboBox.Location = new System.Drawing.Point(12, 24);
            this.groupcomboBox.Name = "groupcomboBox";
            this.groupcomboBox.Size = new System.Drawing.Size(260, 21);
            this.groupcomboBox.TabIndex = 4;
            // 
            // AddGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 85);
            this.Controls.Add(this.groupcomboBox);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.grpNamelabel);
            this.Name = "AddGroup";
            this.Text = "AddGroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label grpNamelabel;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.ComboBox groupcomboBox;
    }
}