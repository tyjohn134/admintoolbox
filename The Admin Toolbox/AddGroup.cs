using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;

namespace The_Admin_Toolbox
{
    public partial class AddGroup : Form
    {
        TheAdminToolBox Admin = new TheAdminToolBox();
        public AddGroup()
        {
            InitializeComponent();
            this.Text = "Add User to AD Group";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        string adtext = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        string addomain = The_Admin_Toolbox.TheAdminToolBox.domain;
        public void AddUserToGroup(string userId, string groupName)
        {
       
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain,addomain))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, groupName);
                    group.Members.Add(pc, IdentityType.SamAccountName, userId);
                    group.Save();
                    MessageBox.Show(adtext + " was added to " + group.DistinguishedName.ToString());
                }
                this.Close();
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                System.Windows.Forms.MessageBox.Show(E.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }

        public void RemoveUserFromGroup(string userId, string groupName)
        {

            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, addomain))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, IdentityType.Name, groupName);
                    group.Members.Remove(pc, IdentityType.SamAccountName, userId);
                    group.Save();
                     MessageBox.Show(adtext + " was removed to " + group.DistinguishedName.ToString());
                }
                this.Close();
            }
                
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                System.Windows.Forms.MessageBox.Show(E.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, addomain);

                //Create a "user object" in the context
                UserPrincipal user = new UserPrincipal(domainContext);

                //Specify the search parameters
                bool fHasSpace = adtext.Contains(" ");
                if (fHasSpace)
                {
                    string[] ssize = adtext.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    string first = ssize[0];
                    string last = ssize[1];
                    user.GivenName = first;
                    user.Surname = last;
                }
                else
                {
                    user.SamAccountName = adtext;
                }

                //Create the searcher
                //pass (our) user object
                PrincipalSearcher pS = new PrincipalSearcher();
                pS.QueryFilter = user;

                //Perform the search
                PrincipalSearchResult<Principal> results = pS.FindAll();

                //If necessary, request more details
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();

                //Output first result of the test
                // try
                // {
                //Gets SamAcctName
                string sam = pc.SamAccountName.ToString();
                AddUserToGroup(sam, groupcomboBox.Text);
               
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, addomain);

                //Create a "user object" in the context
                UserPrincipal user = new UserPrincipal(domainContext);

                //Specify the search parameters
                bool fHasSpace = adtext.Contains(" ");
                if (fHasSpace)
                {
                    string[] ssize = adtext.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
                    string first = ssize[0];
                    string last = ssize[1];
                    user.GivenName = first;
                    user.Surname = last;
                }
                else
                {
                    user.SamAccountName = adtext;
                }

                //Create the searcher
                //pass (our) user object
                PrincipalSearcher pS = new PrincipalSearcher();
                pS.QueryFilter = user;

                //Perform the search
                PrincipalSearchResult<Principal> results = pS.FindAll();

                //If necessary, request more details
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();

                //Output first result of the test
                // try
                // {
                //Gets SamAcctName
                string sam = pc.SamAccountName.ToString();
                RemoveUserFromGroup(sam, groupcomboBox.Text);
                
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }
    }
}
