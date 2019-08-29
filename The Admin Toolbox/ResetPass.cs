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
using System.Data.Common;

namespace The_Admin_Toolbox
{
    public partial class ResetPass : Form
    {
        string adtext = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        bool userset = false;
        string addomain = The_Admin_Toolbox.TheAdminToolBox.domain;
        TheAdminToolBox Admin;
        public ResetPass()
        {
           
           InitializeComponent();

            this.Text = "AD Password Reset";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (!(String.IsNullOrEmpty(labelPass.Text)))
            {
                labelPass.Text = "Resetting password for " + adtext;
            }
            //MessageBox.Show("Resetting password for " + adtext + " on " + addomain + " domain");
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  addomain);

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
          
            

            //Output first result of the test
            try
            {
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();
                //Gets SamAcctName
                string sam = pc.SamAccountName.ToString();
                //Finds account using sam
                UserPrincipal usr = UserPrincipal.FindByIdentity(domainContext, sam);
                //Checks to see if the user has ever logged in before, if they haven't they must change their password upon next logon
                if (usr.LastPasswordSet.HasValue == false && usr.PasswordNeverExpires == false)
                { 
                        mustChangeCheckbox.CheckState = CheckState.Checked;
                }
                else
                {
                    mustChangeCheckbox.CheckState = CheckState.Unchecked;
                }
                pS.Dispose();
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                 MessageBoxDefaultButton.Button1,
                 MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
            
        }

        public TheAdminToolBox mainform = new TheAdminToolBox();

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
                                  
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                 addomain);

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
         

            //Output first result of the test
           try
           {
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();
                //Gets SamAcctName
                string sam = pc.SamAccountName.ToString();
                //Finds account using sam
                UserPrincipal usr = UserPrincipal.FindByIdentity(domainContext, sam);
                //Test to see if account is locked, if it is unlocks account
                string password = this.textBoxPass.Text;

                if (usr.IsAccountLockedOut())
                {
                    usr.UnlockAccount();
                    usr.SetPassword(password);
                    pc.Dispose();
                    //ResetPass.ActiveForm.Close();
                    long filetime = TheAdminToolBox.ConvertADSLargeIntegerToInt64(de.Properties["pwdLastSet"].Value);
                    DateTime pwdSet = DateTime.FromFileTime(filetime);
                    System.Windows.Forms.MessageBox.Show("Account is now unlocked"+"\r\nPassword has been changed.","Password set", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    usr.SetPassword(password);
                    de.CommitChanges();
                    long filetime = TheAdminToolBox.ConvertADSLargeIntegerToInt64(de.Properties["pwdLastSet"].Value);
                    DateTime pwdSet = DateTime.FromFileTime(filetime);
                    System.Windows.Forms.MessageBox.Show("Password has been changed.","Password set",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    de.Close();
                    pc.Dispose();
                    this.Close();
                }

                pS.Dispose();
           }
           catch (SystemException err)
           {
               System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
               this.Close();
           }


        }

        private void labelPass_Click(object sender, EventArgs e)
        {

        }

        private void mustChangeCheckbox_Click(object sender, EventArgs e)
        {
            
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  addomain);

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

            
            try
            {
                //If necessary, request more details
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();
                //Gets SamAcctName
                string sam = pc.SamAccountName.ToString();
                //Finds account using sam
                UserPrincipal usr = UserPrincipal.FindByIdentity(domainContext, sam);
                //Output first result of the test
                if (mustChangeCheckbox.Checked == false)
              {
                    
                 
                  if (!(object.ReferenceEquals(null, de.Properties["pwdLastSet"].Value)))
                  {
                      de.Properties["pwdLastSet"].Value = -1;
                      de.CommitChanges();
                      System.Windows.Forms.MessageBox.Show("User must change password at next logon was removed!");
                  }
              }
              if (mustChangeCheckbox.Checked == true)
              {
                  
                  if (!(object.ReferenceEquals(null, de.Properties["pwdLastSet"].Value)))
                  {
                    
                        usr.ExpirePasswordNow();
                        System.Windows.Forms.MessageBox.Show("User must change password at next logon is now set!");
                  }
              }
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
