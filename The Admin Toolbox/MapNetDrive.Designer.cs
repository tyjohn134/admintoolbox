namespace The_Admin_Toolbox
{
    partial class MapNetDrives
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
            this.add_Button = new System.Windows.Forms.Button();
            this.path_textBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.driveLetter_Label = new System.Windows.Forms.Label();
            this.driveLetter_TextBox = new System.Windows.Forms.TextBox();
            this.currentDrives = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.remove_Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // add_Button
            // 
            this.add_Button.Location = new System.Drawing.Point(188, 215);
            this.add_Button.Name = "add_Button";
            this.add_Button.Size = new System.Drawing.Size(94, 23);
            this.add_Button.TabIndex = 5;
            this.add_Button.Text = "Add";
            this.add_Button.UseVisualStyleBackColor = true;
            this.add_Button.Click += new System.EventHandler(this.map_Button_Click);
            // 
            // path_textBox
            // 
            this.path_textBox.Location = new System.Drawing.Point(46, 189);
            this.path_textBox.Name = "path_textBox";
            this.path_textBox.Size = new System.Drawing.Size(249, 20);
            this.path_textBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 173);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Path to network drive:";
            // 
            // driveLetter_Label
            // 
            this.driveLetter_Label.AutoSize = true;
            this.driveLetter_Label.Location = new System.Drawing.Point(298, 173);
            this.driveLetter_Label.Name = "driveLetter_Label";
            this.driveLetter_Label.Size = new System.Drawing.Size(65, 13);
            this.driveLetter_Label.TabIndex = 6;
            this.driveLetter_Label.Text = "Drive Letter:";
            // 
            // driveLetter_TextBox
            // 
            this.driveLetter_TextBox.Location = new System.Drawing.Point(301, 189);
            this.driveLetter_TextBox.Name = "driveLetter_TextBox";
            this.driveLetter_TextBox.Size = new System.Drawing.Size(218, 20);
            this.driveLetter_TextBox.TabIndex = 7;
            // 
            // currentDrives
            // 
            this.currentDrives.AutoSize = true;
            this.currentDrives.Location = new System.Drawing.Point(198, 14);
            this.currentDrives.Name = "currentDrives";
            this.currentDrives.Size = new System.Drawing.Size(97, 13);
            this.currentDrives.TabIndex = 9;
            this.currentDrives.Text = "Current Net Drives:";
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(46, 33);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(473, 123);
            this.listView1.TabIndex = 10;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Drive Letter";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Path";
            // 
            // remove_Button
            // 
            this.remove_Button.Location = new System.Drawing.Point(288, 215);
            this.remove_Button.Name = "remove_Button";
            this.remove_Button.Size = new System.Drawing.Size(75, 23);
            this.remove_Button.TabIndex = 11;
            this.remove_Button.Text = "Remove";
            this.remove_Button.UseVisualStyleBackColor = true;
            this.remove_Button.Click += new System.EventHandler(this.remove_Button_Click);
            // 
            // MapNetDrives
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 269);
            this.Controls.Add(this.remove_Button);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.currentDrives);
            this.Controls.Add(this.driveLetter_TextBox);
            this.Controls.Add(this.driveLetter_Label);
            this.Controls.Add(this.add_Button);
            this.Controls.Add(this.path_textBox);
            this.Controls.Add(this.label1);
            this.Name = "MapNetDrives";
            this.Text = "Map Network Drives";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapNetDrives_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add_Button;
        private System.Windows.Forms.TextBox path_textBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label driveLetter_Label;
        private System.Windows.Forms.TextBox driveLetter_TextBox;
        private System.Windows.Forms.Label currentDrives;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button remove_Button;
    }
}