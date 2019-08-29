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
using System.Runtime.InteropServices;


namespace The_Admin_Toolbox
{
    public partial class ADFunc : Form
    {
        private TheAdminToolBox m_form = null;

        TheAdminToolBox Admin = new TheAdminToolBox();
        string computername = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        string domain = The_Admin_Toolbox.TheAdminToolBox.domain;
        public ADFunc(TheAdminToolBox f)
        {
            InitializeComponent();
            m_form = f;
            TextBoxWatermarkExtensionMethod.SetWatermark(textBoxDomainUserName, "admin_username@domain");
            TextBoxWatermarkExtensionMethod.SetWatermark(textBoxLocalUserName, "RemoteComputerName\\Administrator");
            TextBoxWatermarkExtensionMethod.SetWatermark(textBoxNewName, "Optional field for when renaming");
            textBoxLocalUserName.Text = "Administrator";
            textBoxDomainUserName.Text = Environment.UserName+"@"+ domain;
            this.Text = "Active Directory Domain Functions";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            comboBoxOUs.Items.Clear();
            if(Admin.getCurrentDomain() == "domain")
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://");
                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.Filter = "(objectCategory=organizationalUnit)";
                searcher.SearchScope = SearchScope.Subtree;
                foreach (SearchResult res in searcher.FindAll())
                {
                    if (res.Path.Contains(".Computer"))
                    {
                        comboBoxOUs.Items.Add(res.Path.Remove(0, 7));
                    }
                    
                }

            }
            else
            {
                DirectoryEntry entry = new DirectoryEntry("LDAP://OU=Computers,OU=US,DC=global,DC=gg,DC=group");
                DirectorySearcher searcher = new DirectorySearcher(entry);
                searcher.Filter = "(objectCategory=organizationalUnit)";
                searcher.SearchScope = SearchScope.Subtree;
                foreach (SearchResult res in searcher.FindAll())
                {
                   comboBoxOUs.Items.Add(res.Path.Remove(0, 7));
                }

                comboBoxOUs.Items.RemoveAt(0);
            }



        }
       public string partof;
       ADComp adcomp = new ADComp();
        
       public bool checkIfOnDomain(string computername)
        {
           
                System.Security.SecureString sec = convertToSecureString(textBoxLocalPassword.Text);
                NetworkCredential local = new NetworkCredential(computername + "\\" + textBoxLocalUserName.Text, textBoxLocalPassword.Text);
                ConnectionOptions conn = new ConnectionOptions
                {

                    Authentication = AuthenticationLevel.PacketPrivacy,
                    //the rest of wmi objects will use the local admin so when joining to domain
                    //you must use your domain account 
                    Impersonation = ImpersonationLevel.Impersonate,
                    EnablePrivileges = true,
                    //needs to be local admin account that connects to machine
                    //local admin account
                    Username = local.UserName,
                    //local admin account password
                    SecurePassword = local.SecurePassword,
                };
                ManagementScope scope0 = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
                try
                {
                scope0.Connect();
                }
                catch (SystemException err) { MessageBox.Show(err.Message.ToString()); }
                WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                ManagementObjectSearcher searcher0 =
                    new ManagementObjectSearcher(scope0, wqlQuery0);
                foreach (ManagementObject n in searcher0.Get())
                {
                    partof = n["PartOfDomain"].ToString();
                }
                return Convert.ToBoolean(partof);
        }

       public bool checkLocalCreds(string localuser, string localpass)
       {
               
               System.Security.SecureString sec = convertToSecureString(localpass);
               NetworkCredential local = new NetworkCredential(computername + "\\"+localuser, localpass);
               ConnectionOptions conn = new ConnectionOptions
               {

                   Authentication = AuthenticationLevel.PacketPrivacy,
                   //the rest of wmi objects will use the local admin so when joining to domain
                   //you must use your domain account 
                   Impersonation = ImpersonationLevel.Impersonate,
                   EnablePrivileges = true,
                   //needs to be local admin account that connects to machine
                   //local admin account
                   Username = local.UserName,
                   //local admin account password
                   SecurePassword = local.SecurePassword,
               };
               ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
               try
               {
                   scope.Connect();
               }
               catch (SystemException err) { MessageBox.Show(err.Message.ToString()); }
               if (scope.IsConnected == false || scope == null) { MessageBox.Show("Local admin credentials are wrong.", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error); }
               return scope.IsConnected;
        
       }

       public bool checkDomainCreds(string domainuser, string domainpass)
       {
           bool isValid;
            using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, domain))
            {
                // validate the credentials
                isValid = pc.ValidateCredentials(domainuser, domainpass);
            }
           if (isValid == false) { MessageBox.Show("Domain admin credentials are wrong.", "Wrong credentials", MessageBoxButtons.OK, MessageBoxIcon.Error); }
           return isValid;
       }

        

        public string convertToUNSecureString(System.Security.SecureString secstrPassword)
        {
            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secstrPassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
  
        public System.Security.SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new System.Security.SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        public string newname;
        public string ouname;
        public string localuser;
        public string localpass;
        
        public void bw_joinDomain(object sender, DoWorkEventArgs e)
        {
           
        }

        public object returnval0;
        public object returnval1;
        private void buttonJoin_Click(object sender, EventArgs e)
        {
            m_form.Focus();
            m_form.OutputBox.AppendText("\r\nJoining computer to domain, please wait...");
            this.Hide();
            if (((checkIfOnDomain(computername)) == false) && (checkDomainCreds(textBoxDomainUserName.Text, textBoxDomainPassword.Text) == true) && (checkLocalCreds(textBoxLocalUserName.Text, textBoxLocalPassword.Text) == true) && (!String.IsNullOrEmpty(domainNametextBox.Text)))
            {
                try
                {
                     /*
                    System.Security.SecureString localsec = convertToSecureString(textBoxLocalPassword.Text);
                    NetworkCredential local = new NetworkCredential(computername + @"\" + textBoxLocalUserName.Text, textBoxLocalPassword.Text);
                    NetworkCredential domain = new NetworkCredential(textBoxDomainUserName.Text, textBoxDomainPassword.Text);
                    
                    MessageBox.Show("Join " + val);
                  */
                    //comboBoxOUs.Enabled = false;
                    textBoxNewName.Enabled = true;


                    System.Security.SecureString sec = convertToSecureString(textBoxLocalPassword.Text);
                    NetworkCredential local = new NetworkCredential(computername + "\\" + textBoxLocalUserName.Text, sec);

                    ConnectionOptions conn = new ConnectionOptions
                    {

                        Authentication = AuthenticationLevel.PacketPrivacy,
                        //the rest of wmi objects will use the local admin so when joining to domain
                        //you must use your domain account 
                        Impersonation = ImpersonationLevel.Impersonate,
                        EnablePrivileges = true,
                        //needs to be local admin account that connects to machine
                        //local admin account
                        Username = local.UserName,
                        //local admin account password
                        SecurePassword = local.SecurePassword,
                    };
                    ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
                    
                    object[] methodArgs = {domainNametextBox.Text, textBoxDomainPassword.Text, textBoxDomainUserName.Text, comboBoxOUs.Text, 3 };
                    object[] methodArgs2 = { textBoxNewName.Text };
                    if (!(String.IsNullOrEmpty(textBoxNewName.Text)))
                    {
                        m_form.OutputBox.AppendText("\r\nRenaming....");
                        foreach (ManagementObject n in searcher.Get())
                        {
                           returnval0 = n.InvokeMethod("Rename", methodArgs2);
                        }
                        MessageBox.Show("Rename " + returnval0);
                    }
                    
                    m_form.OutputBox.AppendText("\r\nJoining....");
                    foreach (ManagementObject n in searcher.Get())
                    {
                       returnval1 = n.InvokeMethod("JoinDomainOrWorkgroup", methodArgs);
                    }
                    if (Convert.ToInt32(returnval1) == 2224) {}

                    MessageBox.Show("Join " + returnval1);
                    methodArgs = null;
                    methodArgs2 = null;
                    
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to restart this computer?", "Restart remote computer", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        WqlObjectQuery wqlQuery0 =
                  new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
                        ManagementObjectSearcher searcher0 =
                            new ManagementObjectSearcher(scope, wqlQuery0);
                        object[] methodArgs0 = { };

                        foreach (ManagementObject n in searcher0.Get())
                        {
                            n.InvokeMethod("Reboot", methodArgs0);
                            methodArgs0 = null;
                        }
                        System.Windows.Forms.MessageBox.Show("Computer is now being restarted. Please check computer to ensure it has been properly added to the domain", "Join successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The computer will not be completely joined until it is restarted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                    }
                    
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
            else
            {
                System.Windows.Forms.MessageBox.Show("Computer is already on the domain.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }

        

        private void textBoxDomainUserName_Enter(object sender, EventArgs e)
        {

            
        }

        public void bw_RenameComp(object sender, DoWorkEventArgs e)
        {

           
        }


        private void buttonRename_Click(object sender, EventArgs e)
        {
            m_form.Focus();
            m_form.OutputBox.AppendText("\r\nRenaming computer, please wait...");
            this.Hide();
            if ((checkIfOnDomain(computername) == true) && (checkDomainCreds(textBoxDomainUserName.Text, textBoxDomainPassword.Text) == true) && (checkLocalCreds(textBoxLocalUserName.Text, textBoxLocalPassword.Text) == true))
            {
                try
                {
                    System.Security.SecureString sec = convertToSecureString(textBoxLocalPassword.Text);
                    NetworkCredential local = new NetworkCredential(computername + "\\" + textBoxLocalUserName.Text, sec);

                    ConnectionOptions conn = new ConnectionOptions
                    {
                        Authentication = AuthenticationLevel.PacketPrivacy,
                        //the rest of wmi objects will use the local admin so when joining to domain
                        //you must use your domain account 
                        Impersonation = ImpersonationLevel.Impersonate,
                        EnablePrivileges = true,
                        //needs to be local admin account that connects to machine
                        //local admin account
                        Username = local.UserName,
                        //local admin account password
                        SecurePassword = sec,
                    };
                    ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);

                    object[] methodArgs = { textBoxNewName.Text, textBoxDomainPassword.Text, textBoxDomainUserName.Text};

                    foreach (ManagementObject n in searcher.Get())
                    {

                        n.InvokeMethod("Rename", methodArgs);
                        methodArgs = null;
                    }

                  
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to restart this computer?", "Restart remote computer", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        Process process = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = @"C:\Windows\System32\shutdown.exe";
                        psi.UseShellExecute = false;
                        psi.CreateNoWindow = true;
                        string path = "\\\\" + computername;
                        string hh = string.Format("\"{0}\"", path);
                        psi.Arguments = @"/m " + path + " /r /t 0";
                        process.StartInfo = psi;
                        process.Start();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The computer will not show it is renamed until it is restarted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.Close();
                    }

                    DialogResult moveOu = System.Windows.Forms.MessageBox.Show("Do you want to move this computer to another OU?", "Move remote computer", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                   MessageBoxDefaultButton.Button1,
                   MessageBoxOptions.DefaultDesktopOnly);
                    if (moveOu == DialogResult.Yes)
                    {
                        using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain,domain))
                        {
                         
                            ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, computername);
                  
                                DirectoryEntry de = (DirectoryEntry)computer.GetUnderlyingObject();
                                de.MoveTo(new DirectoryEntry("LDAP://" + comboBoxOUs.Text));
                                de.CommitChanges();
                                
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
            if ((checkIfOnDomain(computername) == false) && (checkLocalCreds(textBoxLocalUserName.Text, textBoxLocalPassword.Text) == true))
            {

                System.Security.SecureString sec = convertToSecureString(textBoxLocalPassword.Text);
                NetworkCredential local = new NetworkCredential(computername + "\\" + textBoxLocalUserName.Text, sec);
                try
                {
                    ConnectionOptions conn = new ConnectionOptions
                    {
                        Authentication = AuthenticationLevel.PacketPrivacy,
                        //the rest of wmi objects will use the local admin so when joining to domain
                        //you must use your domain account 
                        Impersonation = ImpersonationLevel.Impersonate,
                        EnablePrivileges = true,
                        //needs to be local admin account that connects to machine
                        //local admin account
                        Username = local.UserName,
                        //local admin account password
                        SecurePassword = sec,
                    };
                    ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
               
                   
                    object[] methodArgs = { textBoxNewName.Text };

                    foreach (ManagementObject n in searcher.Get())
                    {
                        n.InvokeMethod("Rename", methodArgs);
                        methodArgs = null;
                    }
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to restart this computer?", "Restart remote computer", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        WqlObjectQuery wqlQuery0 =
                    new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
                        ManagementObjectSearcher searcher0 =
                            new ManagementObjectSearcher(scope, wqlQuery0);
                        object[] methodArgs0 = { };

                        foreach (ManagementObject n in searcher0.Get())
                        {
                            n.InvokeMethod("Reboot", methodArgs0);
                            methodArgs0 = null;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The computer will not be show it is renamed until it is restarted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.Close();
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

        public void bw_UnjoinDomain(object sender, DoWorkEventArgs e)
        {
           
        }

        private void buttonUnjoin_Click(object sender, EventArgs e)
        {
            m_form.Focus();
            m_form.OutputBox.AppendText("\r\nUnjoining computer from domain, please wait...");
            this.Hide();
            if ((checkIfOnDomain(computername) == true) && (checkDomainCreds(textBoxDomainUserName.Text, textBoxDomainPassword.Text) == true) && (checkLocalCreds(textBoxLocalUserName.Text, textBoxLocalPassword.Text) == true))
            {

                try
                {
                    System.Security.SecureString sec = convertToSecureString(textBoxLocalPassword.Text);
                    NetworkCredential local = new NetworkCredential(computername + "\\" + textBoxLocalUserName.Text, sec);

                    ConnectionOptions conn = new ConnectionOptions
                    {

                        Authentication = AuthenticationLevel.PacketPrivacy,
                        //the rest of wmi objects will use the local admin so when joining to domain
                        //you must use your domain account 
                        Impersonation = ImpersonationLevel.Impersonate,
                        EnablePrivileges = true,
                        //needs to be local admin account that connects to machine
                        //local admin account
                        Username = local.UserName,
                        //local admin account password
                        SecurePassword = local.SecurePassword,
                    };
                    ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2", conn);
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
                    object[] methodArgs = { textBoxDomainPassword.Text, textBoxDomainUserName.Text, 0 };

                    foreach (ManagementObject n in searcher.Get())
                    {
                        n.InvokeMethod("UnjoinDomainOrWorkgroup", methodArgs);
                    }
                    //creats psexec to restart remote computer
                    DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to restart this computer?", "Restart remote computer", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                         MessageBoxDefaultButton.Button1,
                         MessageBoxOptions.DefaultDesktopOnly);
                    if (result == DialogResult.Yes)
                    {
                        WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
                        ManagementObjectSearcher searcher0 =
                            new ManagementObjectSearcher(scope, wqlQuery0);
                        object[] methodArgs0 = { };

                        foreach (ManagementObject n in searcher0.Get())
                        {
                            n.InvokeMethod("Reboot", methodArgs0);
                            methodArgs0 = null;
                        }
                        System.Windows.Forms.MessageBox.Show("Computer is now being restarted. Please check computer to ensure it has been properly removed from the domain", "Unjoin successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("The computer will not be completely unjoined until it is restarted", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly);
                        this.Close();
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
            else
            {
                System.Windows.Forms.MessageBox.Show("Computer is not on the domain or your account credentials are wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                     MessageBoxDefaultButton.Button1,
                     MessageBoxOptions.DefaultDesktopOnly);
                this.Close();
            }
        }
      }
   }

