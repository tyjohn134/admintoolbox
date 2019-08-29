namespace The_Admin_Toolbox
{
    partial class MapPrinter
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
            this.path_textBox = new System.Windows.Forms.TextBox();
            this.add_Button = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.remove_Button = new System.Windows.Forms.Button();
            this.prtr_checkBox = new System.Windows.Forms.CheckBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path to printer:";
            // 
            // path_textBox
            // 
            this.path_textBox.Location = new System.Drawing.Point(42, 171);
            this.path_textBox.Name = "path_textBox";
            this.path_textBox.Size = new System.Drawing.Size(344, 20);
            this.path_textBox.TabIndex = 1;
            // 
            // add_Button
            // 
            this.add_Button.Location = new System.Drawing.Point(166, 207);
            this.add_Button.Name = "add_Button";
            this.add_Button.Size = new System.Drawing.Size(75, 23);
            this.add_Button.TabIndex = 2;
            this.add_Button.Text = "Add";
            this.add_Button.UseVisualStyleBackColor = true;
            this.add_Button.Click += new System.EventHandler(this.map_Button_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView1.Location = new System.Drawing.Point(13, 13);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(500, 110);
            this.listView1.TabIndex = 3;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            // 
            // remove_Button
            // 
            this.remove_Button.Location = new System.Drawing.Point(248, 207);
            this.remove_Button.Name = "remove_Button";
            this.remove_Button.Size = new System.Drawing.Size(75, 23);
            this.remove_Button.TabIndex = 4;
            this.remove_Button.Text = "Remove";
            this.remove_Button.UseVisualStyleBackColor = true;
            this.remove_Button.Click += new System.EventHandler(this.remove_Button_Click);
            // 
            // prtr_checkBox
            // 
            this.prtr_checkBox.AutoSize = true;
            this.prtr_checkBox.Location = new System.Drawing.Point(409, 173);
            this.prtr_checkBox.Name = "prtr_checkBox";
            this.prtr_checkBox.Size = new System.Drawing.Size(91, 17);
            this.prtr_checkBox.TabIndex = 5;
            this.prtr_checkBox.Text = "Set as default";
            this.prtr_checkBox.UseVisualStyleBackColor = true;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(349, 207);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 6;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // MapPrinter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 278);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.prtr_checkBox);
            this.Controls.Add(this.remove_Button);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.add_Button);
            this.Controls.Add(this.path_textBox);
            this.Controls.Add(this.label1);
            this.Name = "MapPrinter";
            this.Text = "Map Network Printers";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MapPrinter_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox path_textBox;
        private System.Windows.Forms.Button add_Button;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button remove_Button;
        private System.Windows.Forms.CheckBox prtr_checkBox;
        private System.Windows.Forms.Button refreshButton;
    }
}