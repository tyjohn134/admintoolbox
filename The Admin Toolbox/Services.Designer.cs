namespace The_Admin_Toolbox
{
    partial class Services
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
            this.listBoxServices = new System.Windows.Forms.ListBox();
            this.labelListServices = new System.Windows.Forms.Label();
            this.buttonRunning = new System.Windows.Forms.Button();
            this.buttonShowAll = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonDisabled = new System.Windows.Forms.Button();
            this.buttonStopped = new System.Windows.Forms.Button();
            this.buttonAuto = new System.Windows.Forms.Button();
            this.buttonEnable = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // listBoxServices
            // 
            this.listBoxServices.FormattingEnabled = true;
            this.listBoxServices.Location = new System.Drawing.Point(12, 25);
            this.listBoxServices.Name = "listBoxServices";
            this.listBoxServices.Size = new System.Drawing.Size(313, 173);
            this.listBoxServices.TabIndex = 0;
            this.listBoxServices.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // labelListServices
            // 
            this.labelListServices.AutoSize = true;
            this.labelListServices.Location = new System.Drawing.Point(12, 6);
            this.labelListServices.Name = "labelListServices";
            this.labelListServices.Size = new System.Drawing.Size(101, 13);
            this.labelListServices.TabIndex = 1;
            this.labelListServices.Text = "Full List of Services:";
            // 
            // buttonRunning
            // 
            this.buttonRunning.Location = new System.Drawing.Point(12, 214);
            this.buttonRunning.Name = "buttonRunning";
            this.buttonRunning.Size = new System.Drawing.Size(101, 23);
            this.buttonRunning.TabIndex = 2;
            this.buttonRunning.Text = "Show Running";
            this.buttonRunning.UseVisualStyleBackColor = true;
            this.buttonRunning.Click += new System.EventHandler(this.buttonRunning_Click);
            // 
            // buttonShowAll
            // 
            this.buttonShowAll.Location = new System.Drawing.Point(224, 214);
            this.buttonShowAll.Name = "buttonShowAll";
            this.buttonShowAll.Size = new System.Drawing.Size(101, 23);
            this.buttonShowAll.TabIndex = 3;
            this.buttonShowAll.Text = "Show All";
            this.buttonShowAll.UseVisualStyleBackColor = true;
            this.buttonShowAll.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 272);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(101, 23);
            this.buttonStart.TabIndex = 4;
            this.buttonStart.Text = "Start Service";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(224, 272);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(101, 23);
            this.buttonDelete.TabIndex = 5;
            this.buttonDelete.Text = "Disable Service";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(119, 272);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(101, 23);
            this.buttonStop.TabIndex = 6;
            this.buttonStop.Text = "Stop Service";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonDisabled
            // 
            this.buttonDisabled.Location = new System.Drawing.Point(119, 214);
            this.buttonDisabled.Name = "buttonDisabled";
            this.buttonDisabled.Size = new System.Drawing.Size(101, 23);
            this.buttonDisabled.TabIndex = 7;
            this.buttonDisabled.Text = "Show Disabled";
            this.buttonDisabled.UseVisualStyleBackColor = true;
            this.buttonDisabled.Click += new System.EventHandler(this.buttonDisabled_Click);
            // 
            // buttonStopped
            // 
            this.buttonStopped.Location = new System.Drawing.Point(12, 243);
            this.buttonStopped.Name = "buttonStopped";
            this.buttonStopped.Size = new System.Drawing.Size(101, 23);
            this.buttonStopped.TabIndex = 8;
            this.buttonStopped.Text = "Show Stopped";
            this.buttonStopped.UseVisualStyleBackColor = true;
            this.buttonStopped.Click += new System.EventHandler(this.buttonStopped_Click);
            // 
            // buttonAuto
            // 
            this.buttonAuto.Location = new System.Drawing.Point(119, 243);
            this.buttonAuto.Name = "buttonAuto";
            this.buttonAuto.Size = new System.Drawing.Size(101, 23);
            this.buttonAuto.TabIndex = 9;
            this.buttonAuto.Text = "Show Auto";
            this.buttonAuto.UseVisualStyleBackColor = true;
            this.buttonAuto.Click += new System.EventHandler(this.buttonAuto_Click);
            // 
            // buttonEnable
            // 
            this.buttonEnable.Location = new System.Drawing.Point(226, 243);
            this.buttonEnable.Name = "buttonEnable";
            this.buttonEnable.Size = new System.Drawing.Size(101, 23);
            this.buttonEnable.TabIndex = 10;
            this.buttonEnable.Text = "Enable Service";
            this.buttonEnable.UseVisualStyleBackColor = true;
            this.buttonEnable.Click += new System.EventHandler(this.buttonEnable_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(24, 311);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(282, 23);
            this.progressBar1.TabIndex = 11;
            // 
            // Services
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 353);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonEnable);
            this.Controls.Add(this.buttonAuto);
            this.Controls.Add(this.buttonStopped);
            this.Controls.Add(this.buttonDisabled);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonShowAll);
            this.Controls.Add(this.buttonRunning);
            this.Controls.Add(this.labelListServices);
            this.Controls.Add(this.listBoxServices);
            this.Name = "Services";
            this.Text = "Services";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxServices;
        private System.Windows.Forms.Label labelListServices;
        private System.Windows.Forms.Button buttonRunning;
        private System.Windows.Forms.Button buttonShowAll;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonDisabled;
        private System.Windows.Forms.Button buttonStopped;
        private System.Windows.Forms.Button buttonAuto;
        private System.Windows.Forms.Button buttonEnable;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}