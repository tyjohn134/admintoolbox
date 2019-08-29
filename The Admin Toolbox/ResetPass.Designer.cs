namespace The_Admin_Toolbox
{
    partial class ResetPass
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
            this.buttonSubmit = new System.Windows.Forms.Button();
            this.labelPass = new System.Windows.Forms.Label();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.labelNewPass = new System.Windows.Forms.Label();
            this.mustChangeCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonSubmit
            // 
            this.buttonSubmit.Location = new System.Drawing.Point(12, 51);
            this.buttonSubmit.Name = "buttonSubmit";
            this.buttonSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonSubmit.TabIndex = 0;
            this.buttonSubmit.Text = "Submit";
            this.buttonSubmit.UseVisualStyleBackColor = true;
            this.buttonSubmit.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // labelPass
            // 
            this.labelPass.AutoSize = true;
            this.labelPass.Location = new System.Drawing.Point(109, 9);
            this.labelPass.Name = "labelPass";
            this.labelPass.Size = new System.Drawing.Size(0, 13);
            this.labelPass.TabIndex = 1;
            this.labelPass.Click += new System.EventHandler(this.labelPass_Click);
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(12, 25);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.Size = new System.Drawing.Size(191, 20);
            this.textBoxPass.TabIndex = 2;
            this.textBoxPass.UseSystemPasswordChar = true;
            // 
            // labelNewPass
            // 
            this.labelNewPass.AutoSize = true;
            this.labelNewPass.Location = new System.Drawing.Point(66, 9);
            this.labelNewPass.Name = "labelNewPass";
            this.labelNewPass.Size = new System.Drawing.Size(81, 13);
            this.labelNewPass.TabIndex = 3;
            this.labelNewPass.Text = "New Password:";
            // 
            // mustChangeCheckbox
            // 
            this.mustChangeCheckbox.AutoSize = true;
            this.mustChangeCheckbox.Location = new System.Drawing.Point(112, 55);
            this.mustChangeCheckbox.Name = "mustChangeCheckbox";
            this.mustChangeCheckbox.Size = new System.Drawing.Size(160, 17);
            this.mustChangeCheckbox.TabIndex = 5;
            this.mustChangeCheckbox.Text = "User must change password";
            this.mustChangeCheckbox.UseVisualStyleBackColor = true;
            this.mustChangeCheckbox.Click += new System.EventHandler(this.mustChangeCheckbox_Click);
            // 
            // ResetPass
            // 
            this.AcceptButton = this.buttonSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 98);
            this.Controls.Add(this.mustChangeCheckbox);
            this.Controls.Add(this.labelNewPass);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.labelPass);
            this.Controls.Add(this.buttonSubmit);
            this.Name = "ResetPass";
            this.Text = "Reset Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSubmit;
        private System.Windows.Forms.Label labelPass;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Label labelNewPass;
        private System.Windows.Forms.CheckBox mustChangeCheckbox;
    }
}