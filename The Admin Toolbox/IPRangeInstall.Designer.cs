namespace The_Admin_Toolbox
{
    partial class IPRangeInstall
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
            this.ipRangelabel = new System.Windows.Forms.Label();
            this.ipStartRangetextBox = new System.Windows.Forms.TextBox();
            this.ipListView = new System.Windows.Forms.ListView();
            this.ipAddrColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ipRangeInstallbutton = new System.Windows.Forms.Button();
            this.endRangetextBox = new System.Windows.Forms.TextBox();
            this.stRangelabel = new System.Windows.Forms.Label();
            this.endRangelabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ipRangelabel
            // 
            this.ipRangelabel.AutoSize = true;
            this.ipRangelabel.Location = new System.Drawing.Point(108, 9);
            this.ipRangelabel.Name = "ipRangelabel";
            this.ipRangelabel.Size = new System.Drawing.Size(52, 13);
            this.ipRangelabel.TabIndex = 0;
            this.ipRangelabel.Text = "IP Range";
            // 
            // ipStartRangetextBox
            // 
            this.ipStartRangetextBox.Location = new System.Drawing.Point(13, 44);
            this.ipStartRangetextBox.Name = "ipStartRangetextBox";
            this.ipStartRangetextBox.Size = new System.Drawing.Size(104, 20);
            this.ipStartRangetextBox.TabIndex = 1;
            // 
            // ipListView
            // 
            this.ipListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ipAddrColumnHeader,
            this.statusColumnHeader});
            this.ipListView.Location = new System.Drawing.Point(13, 71);
            this.ipListView.Name = "ipListView";
            this.ipListView.Size = new System.Drawing.Size(259, 136);
            this.ipListView.TabIndex = 2;
            this.ipListView.UseCompatibleStateImageBehavior = false;
            this.ipListView.View = System.Windows.Forms.View.Details;
            // 
            // ipAddrColumnHeader
            // 
            this.ipAddrColumnHeader.Text = "IP Address";
            // 
            // statusColumnHeader
            // 
            this.statusColumnHeader.Text = "Status";
            this.statusColumnHeader.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ipRangeInstallbutton
            // 
            this.ipRangeInstallbutton.Location = new System.Drawing.Point(99, 213);
            this.ipRangeInstallbutton.Name = "ipRangeInstallbutton";
            this.ipRangeInstallbutton.Size = new System.Drawing.Size(75, 23);
            this.ipRangeInstallbutton.TabIndex = 3;
            this.ipRangeInstallbutton.Text = "Install";
            this.ipRangeInstallbutton.UseVisualStyleBackColor = true;
            this.ipRangeInstallbutton.Click += new System.EventHandler(this.ipRangeInstallbutton_Click);
            // 
            // endRangetextBox
            // 
            this.endRangetextBox.Location = new System.Drawing.Point(168, 44);
            this.endRangetextBox.Name = "endRangetextBox";
            this.endRangetextBox.Size = new System.Drawing.Size(104, 20);
            this.endRangetextBox.TabIndex = 4;
            // 
            // stRangelabel
            // 
            this.stRangelabel.AutoSize = true;
            this.stRangelabel.Location = new System.Drawing.Point(12, 28);
            this.stRangelabel.Name = "stRangelabel";
            this.stRangelabel.Size = new System.Drawing.Size(67, 13);
            this.stRangelabel.TabIndex = 5;
            this.stRangelabel.Text = "Start Range:";
            // 
            // endRangelabel
            // 
            this.endRangelabel.AutoSize = true;
            this.endRangelabel.Location = new System.Drawing.Point(165, 28);
            this.endRangelabel.Name = "endRangelabel";
            this.endRangelabel.Size = new System.Drawing.Size(64, 13);
            this.endRangelabel.TabIndex = 6;
            this.endRangelabel.Text = "End Range:";
            // 
            // IPRangeInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.endRangelabel);
            this.Controls.Add(this.stRangelabel);
            this.Controls.Add(this.endRangetextBox);
            this.Controls.Add(this.ipRangeInstallbutton);
            this.Controls.Add(this.ipListView);
            this.Controls.Add(this.ipStartRangetextBox);
            this.Controls.Add(this.ipRangelabel);
            this.Name = "IPRangeInstall";
            this.Text = "IPRangeInstall";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ipRangelabel;
        private System.Windows.Forms.TextBox ipStartRangetextBox;
        private System.Windows.Forms.ListView ipListView;
        private System.Windows.Forms.Button ipRangeInstallbutton;
        private System.Windows.Forms.ColumnHeader ipAddrColumnHeader;
        private System.Windows.Forms.ColumnHeader statusColumnHeader;
        private System.Windows.Forms.TextBox endRangetextBox;
        private System.Windows.Forms.Label stRangelabel;
        private System.Windows.Forms.Label endRangelabel;
    }
}