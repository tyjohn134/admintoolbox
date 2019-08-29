using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Management.Automation;

namespace The_Admin_Toolbox
{
    public partial class GroupMem : Form
    {
        The_Admin_Toolbox.TheAdminToolBox frm1;
        string addomain = The_Admin_Toolbox.TheAdminToolBox.domain;
        public GroupMem(TheAdminToolBox parent)
        {
            InitializeComponent();
            frm1 = parent;
            this.Text = "Get Members of Groups";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        TheAdminToolBox Admin = new TheAdminToolBox();
        
        private void buttonGetGroup_Click(object sender, EventArgs e)
        {
                        // set up domain context
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain, addomain);

            // find the group in question
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx,IdentityType.Name, groupcomboBox.Text);
            try
            {
                // if found....
                if (group != null)
                {
                    frm1.OutputBox.AppendText("Members of " + groupcomboBox.Text + ":");
                    // iterate over members
                    foreach (Principal p in group.GetMembers())
                    {
                        frm1.OutputBox.AppendText("\r\n");
                        frm1.OutputBox.AppendText(p.DisplayName.ToString());
                    }
                    frm1.OutputBox.AppendText("\r\n");
                    frm1.OutputBox.AppendText("\r\n");
                    frm1.OutputBox.AppendText("############");
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
            this.Close();
        }
    }
}
