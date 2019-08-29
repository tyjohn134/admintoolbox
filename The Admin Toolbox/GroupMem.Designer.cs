namespace The_Admin_Toolbox
{
    partial class GroupMem
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonGetGroup = new System.Windows.Forms.Button();
            this.groupcomboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(83, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please select group:";
            // 
            // buttonGetGroup
            // 
            this.buttonGetGroup.Location = new System.Drawing.Point(86, 55);
            this.buttonGetGroup.Name = "buttonGetGroup";
            this.buttonGetGroup.Size = new System.Drawing.Size(90, 23);
            this.buttonGetGroup.TabIndex = 2;
            this.buttonGetGroup.Text = "Get Members";
            this.buttonGetGroup.UseVisualStyleBackColor = true;
            this.buttonGetGroup.Click += new System.EventHandler(this.buttonGetGroup_Click);
            // 
            // groupcomboBox
            // 
            this.groupcomboBox.FormattingEnabled = true;
            this.groupcomboBox.Location = new System.Drawing.Point(12, 28);
            this.groupcomboBox.Name = "groupcomboBox";
            this.groupcomboBox.Size = new System.Drawing.Size(268, 21);
            this.groupcomboBox.TabIndex = 3;
            // 
            // GroupMem
            // 
            this.AcceptButton = this.buttonGetGroup;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 85);
            this.Controls.Add(this.groupcomboBox);
            this.Controls.Add(this.buttonGetGroup);
            this.Controls.Add(this.label1);
            this.Name = "GroupMem";
            this.Text = "GroupMem";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonGetGroup;
        private System.Windows.Forms.ComboBox groupcomboBox;
    }
}