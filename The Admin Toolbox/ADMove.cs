using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;


namespace The_Admin_Toolbox
{
    public partial class ADMove : Form
    {
        public ADMove()
        {
            InitializeComponent();
            this.Text = "Move Computer to OU";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        string computername = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        string domain = The_Admin_Toolbox.TheAdminToolBox.domain;
        TheAdminToolBox Admin = new TheAdminToolBox();

 
        private void buttonMovePC_Click(object sender, EventArgs e)
        {
            try
            {
                using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain))
                {
                    // find a computer
                    ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, computername);

                    DirectoryEntry de = (DirectoryEntry)computer.GetUnderlyingObject();
                    de.MoveTo(new DirectoryEntry("LDAP://" + comboBoxOUList.Text));
                    de.CommitChanges();
                    de.Dispose();
                    computer.Dispose();
                }
                System.Windows.Forms.MessageBox.Show(computername + " has been moved to "+comboBoxOUList.Text, "Moving computer to OU", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }
    }
}
