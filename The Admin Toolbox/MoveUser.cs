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
    public partial class MoveUser : Form
    {
        public MoveUser()
        {
            InitializeComponent();
            this.Text = "Move User to OU";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }
        string adtext = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        TheAdminToolBox Admin = new TheAdminToolBox();
        private void moveOUButton_Click(object sender, EventArgs e)
        {
            try
            {
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, The_Admin_Toolbox.TheAdminToolBox.domain);

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
                MessageBox.Show(pc.DistinguishedName);
                DirectoryEntry usermove = new DirectoryEntry(@"LDAP://"+pc.DistinguishedName);
                usermove.MoveTo(new DirectoryEntry(@"LDAP://"+OUcomboBox.Text));
                MessageBox.Show(adtext+" was moved to "+OUcomboBox.Text+" successfully.");
                this.Close();
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
