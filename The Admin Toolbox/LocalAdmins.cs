using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Management.Automation;
using System.Net;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Specialized;

namespace The_Admin_Toolbox
{
    public partial class LocalAdminsForm : Form
    {
        TheAdminToolBox Admin = new TheAdminToolBox();
        //Get computer name from the ComputerNameTextBox
        string computername = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        //Get current selected domain
        string domain = The_Admin_Toolbox.TheAdminToolBox.domain;
        public LocalAdminsForm()
        {
            InitializeComponent();
            this.Text = "Add/Remove Users From Local Admins Group";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        private void buttonAddAdmin_Click(object sender, EventArgs e)
        {
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                 domain);
            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(domainContext);
            PrincipalContext localContext = new PrincipalContext(ContextType.Machine, computername+"$");

            //Check if it's the SamAccountName or if it's first name and last name
            string adtext = textBoxUser.Text;
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
             PrincipalSearcher pS = new PrincipalSearcher();
             pS.QueryFilter = user;

            // Perform the search
           try
           {

                //Add user from local admin group
                PrincipalSearchResult<Principal> results = pS.FindAll();
                Principal pc = results.ToList()[0];
                string sam = pc.SamAccountName;
                DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + computername);
                DirectoryEntry admGroup = localMachine.Children.Find("Administrators", "group");
                admGroup.Invoke("Add", "WinNT://"+ domain + "/"+sam+",user");
                admGroup.CommitChanges();
                admGroup.Dispose();
                admGroup.Close();
                localMachine.Close();
                System.Windows.Forms.MessageBox.Show("User has been added!", "Adding user to local admins group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LocalAdminsForm.ActiveForm.Close();
           }
           catch (SystemException err)
           {
               System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
           }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                 domain);

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(domainContext);
            PrincipalContext localContext = new PrincipalContext(ContextType.Machine, computername);

            //Check if it's the SamAccountName or if it's first name and last name
            string adtext = textBoxUser.Text;
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
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search
           try
            {
                //Remove user from local admin group
                PrincipalSearchResult<Principal> results = pS.FindAll();
                Principal pc = results.ToList()[0];
                string sam = pc.SamAccountName;

                 DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + computername);
                 DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
                 admGroup.Invoke("Remove", "WinNT://"+ domain + "/" + sam + ",user");
                 admGroup.CommitChanges();
                 admGroup.Dispose();
                 admGroup.Close();
                 localMachine.Close();
                 System.Windows.Forms.MessageBox.Show("User has been removed!", "Removing user from local admins group", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 this.Close();
            }
           catch (SystemException err) {System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
           this.Close();
           }
        }
    }
}
