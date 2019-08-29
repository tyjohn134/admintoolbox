namespace The_Admin_Toolbox
{
    partial class GetSCCMByModelForm
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
            this.modelComboBox = new System.Windows.Forms.ComboBox();
            this.SCCMlabel = new System.Windows.Forms.Label();
            this.GetModelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // modelComboBox
            // 
            this.modelComboBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.modelComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.modelComboBox.FormattingEnabled = true;
            this.modelComboBox.Items.AddRange(new object[] {
            "ThinkPad X1 Carbon 4th",
            "ThinkPad T460",
            "ThinkPad T460s",
            "ThinkPad T450",
            "ThinkPad T450s",
            "ThinkPad T440p",
            "ThinkPad T440",
            "ThinkPad T440s",
            "ThinkPad T430s",
            "ThinkPad T430",
            "ThinkPad T420s",
            "ThinkPad T420",
            "ThinkPad T410",
            "ThinkPad T400",
            "ThinkPad W550",
            "ThinkPad W540",
            "ThinkPad W530",
            "ThinkPad W520",
            "ThinkPad P50",
            "ThinkPad P51",
            "Think Centre M800",
            "Think Centre M83",
            "Think Centre M82",
            "Think Centre M81",
            "Think Centre M58"});
            this.modelComboBox.Location = new System.Drawing.Point(13, 25);
            this.modelComboBox.Name = "modelComboBox";
            this.modelComboBox.Size = new System.Drawing.Size(259, 21);
            this.modelComboBox.TabIndex = 0;
            // 
            // SCCMlabel
            // 
            this.SCCMlabel.AutoSize = true;
            this.SCCMlabel.Location = new System.Drawing.Point(86, 9);
            this.SCCMlabel.Name = "SCCMlabel";
            this.SCCMlabel.Size = new System.Drawing.Size(95, 13);
            this.SCCMlabel.TabIndex = 1;
            this.SCCMlabel.Text = "Get SCCM Info Of:";
            // 
            // GetModelButton
            // 
            this.GetModelButton.Location = new System.Drawing.Point(89, 52);
            this.GetModelButton.Name = "GetModelButton";
            this.GetModelButton.Size = new System.Drawing.Size(75, 23);
            this.GetModelButton.TabIndex = 2;
            this.GetModelButton.Text = "Submit";
            this.GetModelButton.UseVisualStyleBackColor = true;
            this.GetModelButton.Click += new System.EventHandler(this.GetModelButton_Click);
            // 
            // GetSCCMByModelForm
            // 
            this.AcceptButton = this.GetModelButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 85);
            this.Controls.Add(this.GetModelButton);
            this.Controls.Add(this.SCCMlabel);
            this.Controls.Add(this.modelComboBox);
            this.Name = "GetSCCMByModelForm";
            this.Text = "GetSCCMByModelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox modelComboBox;
        private System.Windows.Forms.Label SCCMlabel;
        private System.Windows.Forms.Button GetModelButton;
    }
}