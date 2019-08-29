namespace The_Admin_Toolbox
{
    partial class ADMove
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
            this.labelOUs = new System.Windows.Forms.Label();
            this.comboBoxOUList = new System.Windows.Forms.ComboBox();
            this.buttonMovePC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelOUs
            // 
            this.labelOUs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelOUs.AutoSize = true;
            this.labelOUs.Location = new System.Drawing.Point(149, 14);
            this.labelOUs.Name = "labelOUs";
            this.labelOUs.Size = new System.Drawing.Size(61, 13);
            this.labelOUs.TabIndex = 0;
            this.labelOUs.Text = "List of OU\'s";
            this.labelOUs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxOUList
            // 
            this.comboBoxOUList.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.comboBoxOUList.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxOUList.FormattingEnabled = true;
            this.comboBoxOUList.Location = new System.Drawing.Point(12, 30);
            this.comboBoxOUList.Name = "comboBoxOUList";
            this.comboBoxOUList.Size = new System.Drawing.Size(327, 21);
            this.comboBoxOUList.TabIndex = 1;
            // 
            // buttonMovePC
            // 
            this.buttonMovePC.Location = new System.Drawing.Point(135, 57);
            this.buttonMovePC.Name = "buttonMovePC";
            this.buttonMovePC.Size = new System.Drawing.Size(75, 23);
            this.buttonMovePC.TabIndex = 2;
            this.buttonMovePC.Text = "Move";
            this.buttonMovePC.UseVisualStyleBackColor = true;
            this.buttonMovePC.Click += new System.EventHandler(this.buttonMovePC_Click);
            // 
            // ADMove
            // 
            this.AcceptButton = this.buttonMovePC;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 90);
            this.Controls.Add(this.buttonMovePC);
            this.Controls.Add(this.comboBoxOUList);
            this.Controls.Add(this.labelOUs);
            this.Name = "ADMove";
            this.Text = "Move AD Computer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelOUs;
        private System.Windows.Forms.ComboBox comboBoxOUList;
        private System.Windows.Forms.Button buttonMovePC;
    }
}