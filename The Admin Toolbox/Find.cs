using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace The_Admin_Toolbox
{
    public partial class Find : Form
    {
        private TheAdminToolBox m_form = null;
        public Find(TheAdminToolBox f)
        {
            InitializeComponent();
            m_form = f;
            this.FormClosed += new FormClosedEventHandler(FindForm_FormClosing);
            this.Text = "Find";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        
        private void FindForm_FormClosing(object sender, FormClosedEventArgs e)
        {
            //m_form.OutputBox.BackColor = System.Drawing.Color.Black;
          //  m_form.OutputBox.ForeColor = System.Drawing.Color.Green;
           
        }

        private void FindNextButton_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            int StartPosition;
            CompareMethod SearchType;
            if (chkMatchCase.Checked == true)
            {
                SearchType = CompareMethod.Binary;
            }
            else
            {
                SearchType = CompareMethod.Text;
            }

            StartPosition = Strings.InStr(1, m_form.OutputBox.Text, txtSearchTerm.Text, SearchType);
           
            if (StartPosition == 0)
            {
                MessageBox.Show("Cannot find: \"" + txtSearchTerm.Text.ToString()+"\"", "No Matches",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
             m_form.OutputBox.Select(StartPosition - 1, txtSearchTerm.Text.Length);
             m_form.OutputBox.ScrollToCaret();
           
             m_form.OutputBox.Focus();
            
        }

        private void FindNextButton_Click_1(object sender, EventArgs e)
        {
            this.TopMost = true;
            int StartPosition = m_form.OutputBox.SelectionStart + 2;
            CompareMethod SearchType;
            if (chkMatchCase.Checked == true)
            {
                SearchType = CompareMethod.Binary;
            }
            else
            {
                SearchType = CompareMethod.Text;
            }
            StartPosition = Strings.InStr(StartPosition, m_form.OutputBox.Text, txtSearchTerm.Text, SearchType);
            if (StartPosition == 0)
            {
                MessageBox.Show("Cannot find: \"" + txtSearchTerm.Text.ToString()+"\"", "No Matches",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            m_form.OutputBox.Select(StartPosition - 1, txtSearchTerm.Text.Length);
            m_form.OutputBox.ScrollToCaret();
           
            m_form.OutputBox.Focus();
           
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
