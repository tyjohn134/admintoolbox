using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Management;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Management.Automation;
using System.Net;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.FileIO;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.ConfigurationManagement;
using Microsoft.ConfigurationManagement.ManagementProvider;
using Microsoft.ConfigurationManagement.ManagementProvider.WqlQueryEngine;
using System.Text.RegularExpressions;
using System.Security.AccessControl;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Diagnostics.Eventing.Reader;
using System.DirectoryServices.ActiveDirectory;
using System.Security;
using System.Security.Cryptography;
using System.Data.SQLite;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using HtmlAgilityPack;

namespace The_Admin_Toolbox
{
    public partial class TheAdminToolBox : Form
    {
        public bool connected = true;
        public TheAdminToolBox()
        {
          
            InitializeComponent();
            
            PingButton.Text = "Ping";
            OutputBox.ForeColor = System.Drawing.Color.LimeGreen;
            OutputBox.BackColor = System.Drawing.Color.Black;
            TextBoxWatermarkExtensionMethod.SetWatermark(ADIdentityBox, "John Doe or jdoe");
            TextBoxWatermarkExtensionMethod.SetWatermark(ComputerNameBox, "Computer Name");
            TextBoxWatermarkExtensionMethod.SetWatermark(SCCMUserBox, "First and Last Name or Username");
            TextBoxWatermarkExtensionMethod.SetWatermark(totextBox, "File/Folder path to remote");
            TextBoxWatermarkExtensionMethod.SetWatermark(fromtextBox, "Local path");
            TextBoxWatermarkExtensionMethod.SetWatermark(getPathAccessTextBox, "Remote path or local path");
            TextBoxWatermarkExtensionMethod.SetWatermark(SerialtextBox, "IP/SN of remote computer");
            timer.Elapsed += OnTimedEvent;
            getCompInfobutton.Enabled = false;
            installServiceCompNameBoxButton.Enabled = false;
            checkServiceStatusButton.Enabled = false;
            uninstallFromComputerButton.Enabled = false;
            installSnagitButton.Enabled = false;
            installAble2ExtractButton.Enabled = false;
            map_NetDrButton.Enabled = false;
            map_PrinterButton.Enabled = false;
            checkConnect.Enabled = false;
            buttonADDelete.Enabled = false;
            buttonADInfoComp.Enabled = false;
            buttonADJoin.Enabled = false;
            buttonADMove.Enabled = false;
            CShareButton.Enabled = false;
            PsExecButton.Enabled = false;
            PingButton.Enabled = false;
            GPUpdateButton.Enabled = false;
            ShutdownButton.Enabled = false;
            RestartButton.Enabled = false;
            RemoteControlViewButton.Enabled = false;
            WarrantyButton.Enabled = false;
            buttonADInfo.Enabled = false;
            buttonUnlock.Enabled = false;
            buttonResetPwd.Enabled = false;
            buttonAddRmAdms.Enabled = false;
            buttonLocalAd.Enabled = false;
            installPrimoButton.Enabled = false;
            buttonGroupMem.Enabled = false;
            buttonMemory.Enabled = false;
            buttonMotherBoard.Enabled = false;
            buttonPrinters.Enabled = false;
            buttonHosts.Enabled = false;
            buttonApps.Enabled = false;
            buttonADInfoComp.Enabled = false;
            buttonADJoin.Enabled = false;
            buttonProcessor.Enabled = false;
            buttonUSB.Enabled = false;
            buttonServices.Enabled = false;
            SCCMByComputerButton.Enabled = false;
            StartUpBtn.Enabled = false;
            SCCMByUserButton.Enabled = false;
            browButoon.Enabled = false;
            sendButton.Enabled = false;
            browFolder.Enabled = false;
            driversButton.Enabled = false;
            installAcroPrBbutton.Enabled = false;
            installAcroStdButton.Enabled = false;
            installChocoButton.Enabled = false;
            installChromeButton.Enabled = false;
            installDropBoxButton.Enabled = false;
            installEyemaxButton.Enabled = false;
            installFileZillaButton.Enabled = false;
            installFireFoxButton.Enabled = false;
            installFlashButton.Enabled = false;
            GetProfilesButton.Enabled = false;
            installiTunesButton.Enabled = false;
            installMBAMButton.Enabled = false;
            installPicasaButton.Enabled = false;
            DeleteSCCMbutton.Enabled = false;
            installSkypeButton.Enabled = false;
            installSmartViewButton.Enabled = false;
            installDriveButton.Enabled = false;
            installJavaButton.Enabled = false;
            installSpotifyButton.Enabled = false;
            MoveUserbutton.Enabled = false;
            addtoGroupButton.Enabled = false;
            exacqVisionButton.Enabled = false;
            getAccessButton.Enabled = false;
            installPrimoButton.Enabled = false;
            getInstalledSoftButton.Enabled = false;
            searchADCompButton.Enabled = false;
            searchADUserButton.Enabled = false;
            SCCMSerialbutton.Enabled = false;
            largestFilesBbutton.Enabled = false;
            findByCompNameButton.Enabled = false;
            getDHCPClientsBtn.Enabled = false;
            findByUserNameDBButton.Enabled = false;

            //Initial launch; Check ping status to ADServer
            try
            {
                Ping pingSender = new Ping();
                PingOptions options = new PingOptions();
                options.DontFragment = true;
                PingReply reply = pingSender.Send(ADServer);

                if (reply.Status != IPStatus.Success)
                {
                    System.Windows.Forms.MessageBox.Show("Cannot reach the domain controller. Check to make sure you are connected to the internal network", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connected = false;
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Cannot reach the domain controller. Check to make sure you are connected to the internal network", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connected = false;

            }


            if (String.IsNullOrEmpty(Properties.Settings.Default["Domain"].ToString()))
            {
                DirectoryContext d = new DirectoryContext(DirectoryContextType.Domain, Environment.UserDomainName);
                if (connected)
                {
                    domainToolStripMenuItem.Text = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(d).ToString();
                }
                else
                {
                    domainToolStripMenuItem.Text = "N/A";
                }
               
            }
            else
            {
                domainToolStripMenuItem.Text = Properties.Settings.Default["Domain"].ToString();
            }
           
            currentUserToolStripMenuItem.Text += " Current User: " + Environment.UserDomainName + @"\" + Environment.UserName;
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string userFilePath = Path.Combine(localAppData, "AdminToolBox");
            if (!Directory.Exists(userFilePath))
                Directory.CreateDirectory(userFilePath);

            string sourceFilePath = Path.Combine(
            System.Windows.Forms.Application.StartupPath, "app.sqlite");
            string destFilePath = Path.Combine(userFilePath, "app.sqlite");
            //if app.sqlite doesn't exist in main project create it
            if (!File.Exists(destFilePath))
            {
                    SQLiteConnection.CreateFile("./app.sqlite");
                    SQLiteConnection m_dbConnection;
                    m_dbConnection = new SQLiteConnection("Data Source=./app.sqlite;Version=3;");
                    m_dbConnection.Open();
                    string sql = "create table history (name varchar(20), created_at text, user varchar(20))";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                    m_dbConnection.Close();
            }
            //if app.sqlite doesn't exist in the local app data then copy the file to projects main stuff
            else
            {
                   File.Copy(destFilePath, sourceFilePath, true);
            }
          
        }


        static string ADServer = Properties.Settings.Default["ADServer"].ToString();
        public string SCCMServer = Properties.Settings.Default["SCCMServer"].ToString();


        const int NO_ERROR = 0;
        const int ERROR_INSUFFICIENT_BUFFER = 122;

        enum SID_NAME_USE
        {
            SidTypeUser = 1,
            SidTypeGroup,
            SidTypeDomain,
            SidTypeAlias,
            SidTypeWellKnownGroup,
            SidTypeDeletedAccount,
            SidTypeInvalid,
            SidTypeUnknown,
            SidTypeComputer
        }


        public List<string> EnumerateOU(string OuDn)
        {
            List<string> alObjects = new List<string>();
            try
            {
                DirectoryEntry directoryObject = new DirectoryEntry("LDAP://" + OuDn);
                foreach (DirectoryEntry child in directoryObject.Children)
                {
                    string childPath = child.Path.ToString();
                    alObjects.Add(childPath.Remove(0, 7));
                    //remove the LDAP prefix from the path

                    child.Close();
                    child.Dispose();
                }
                directoryObject.Close();
                directoryObject.Dispose();
            }
            catch (DirectoryServicesCOMException e)
            {
                Console.WriteLine("An Error Occurred: " + e.Message.ToString());
            }
            return alObjects;
        }


        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool LookupAccountSid(
          string lpSystemName,
          [MarshalAs(UnmanagedType.LPArray)] byte[] Sid,
          StringBuilder lpName,
          ref uint cchName,
          StringBuilder ReferencedDomainName,
          ref uint cchReferencedDomainName,
          out SID_NAME_USE peUse);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool ConvertStringSidToSid(
        string StringSid,
        out IntPtr ptrSid
        );

        [DllImport("advapi32.dll")]
        static extern uint GetLengthSid(IntPtr pSid);

        public void createComputerHistory(string comp, string user)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=./app.sqlite;Version=3;");
            m_dbConnection.Open();
            if (!(String.IsNullOrEmpty(comp)))
            {
                string sqlc = $"SELECT COUNT(*) FROM history WHERE name = '{comp}'";
                SQLiteCommand commandc = new SQLiteCommand(sqlc, m_dbConnection);
                int numRecords = Convert.ToInt32(commandc.ExecuteScalar());
                if (numRecords == 0)
                {
                    string sql = $"insert into history (name, created_at, user) values ('{comp}', datetime('now', 'localtime'), '{user}')";
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                m_dbConnection.Close();
            }
        }


        static byte[] entropy = System.Text.Encoding.Unicode.GetBytes("dac9630aec642a428cd73f4be0a03569");
        public void bw_currentUserStrip(object sender, DoWorkEventArgs e) {
            credentials = Misuzilla.Security.CredentialUI.PromptForWindowsCredentials("Run as Different User", "Please enter your admin credentials. Please use your UPN or @{full domain name}");
            System.Diagnostics.ProcessStartInfo inf = new System.Diagnostics.ProcessStartInfo();
            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                inf.Verb = "runas";
            }
            inf.FileName = System.Reflection.Assembly.GetEntryAssembly().Location;
            inf.UserName = credentials.UserName;
            inf.UseShellExecute = false;
            inf.Password = ToSecureString(credentials.Password);

            var proc = System.Diagnostics.Process.Start(inf);
            Application.Exit();

        }
        
        private static int start_index;
        private static int maxlines;
        private static int count;
        private static int index;
        
        public Misuzilla.Security.PromptCredentialsResult credentials;
        private void MenuClicked(object sender, EventArgs e)
        {
            domainToolStripMenuItem.Text = ((ToolStripMenuItem)sender).Text;
            Properties.Settings.Default["Domain"] = domainToolStripMenuItem.Text;
            Properties.Settings.Default.Save();

        }

        

        private void DeleteLine(int a_line)
        {
            try { Invoke(new Action(() => { start_index = OutputBox.GetFirstCharIndexFromLine(a_line); })); }
            catch (System.Exception) {};
            Invoke(new Action(() => { count = OutputBox.Lines[a_line].Length; }));
            Invoke(new Action(() => { maxlines = OutputBox.Lines.Length - 1; }));
            Invoke(new Action(() => { index = OutputBox.GetFirstCharIndexFromLine(a_line + 1); }));
            // Eat new line chars

            if (a_line < maxlines)
            {
                count += index -
                    ((start_index + count - 1) + 1);
            }
            Invoke(new Action(() => { OutputBox.Text = OutputBox.Text.Remove(start_index, count); }));
        }


public static string EncryptString(System.Security.SecureString input)
        {

            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static SecureString DecryptString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        public static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }

        public bool ControlMapPrinterIsVisible
        {
            get { return map_PrinterButton.Enabled; }
            set { map_PrinterButton.Enabled = value; }
        }

        public bool ControlMapNetDrIsVisible
        {
            get { return map_NetDrButton.Enabled; }
            set { map_NetDrButton.Enabled = value; }
        }

        public static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

        public void progressLoad2(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bg = (BackgroundWorker)e.Argument;
            string scroll = "/-\\|/-\\|";
            int idx = 0;
            StringBuilder builder = new StringBuilder();
            //orgpos.Y += 1;
            var selection = 0;
            while (bg.IsBusy)
            {
                builder.Append("Please wait..."+scroll[idx]);
                Action output28 = () => OutputBox.AppendText(builder.ToString());
                OutputBox.Invoke(output28);
                idx++;
                if (idx >= scroll.Length)
                {
                    idx = 0;
                }
                Thread.Sleep(100);
                DeleteLine(2);
                builder.Clear();
            }

        }


        public void progressLoad(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bg = (BackgroundWorker)e.Argument;
            string scroll = "/-\\|/-\\|";
            int idx = 0;
            StringBuilder builder = new StringBuilder();
            //orgpos.Y += 1;
            var selection = 0;
            while (bg.IsBusy)
            {
                builder.Append("Please wait..." + scroll[idx]);
                Action output28 = () => OutputBox.AppendText(builder.ToString());
                OutputBox.Invoke(output28);
                idx++;
                if (idx >= scroll.Length)
                {
                    idx = 0;
                }
                Thread.Sleep(100);
                DeleteLine(0);
                builder.Clear();
            }

        }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value)
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }
            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }
            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }

        public String getCurrentDomain()
        {
            return domainToolStripMenuItem.Text;
        }

// var credentials2 = Misuzilla.Security.CredentialUI.PromptForWindowsCredentials("Enter Credentials", "Please enter domain admin credentials.");

        private void TheAdminToolBox_Load(object sender, EventArgs e)
        {
           
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            DateTime buildDate = new DateTime(2000, 1, 1)
                                    .AddDays(version.Build).AddSeconds(version.Revision * 2);
            string displayableVersion = $"{version}";
            this.Text = "The Admin Toolbox - Build: " + displayableVersion;
            pingStatus.ForeColor = System.Drawing.Color.Black;
                pingStatus.Text = "N/A";
                PermissionStatus.ForeColor = System.Drawing.Color.Black;
                PermissionStatus.Text = "N/A";
                RemoteUserName.ForeColor = System.Drawing.Color.Black;
                RemoteUserName.Text = "N/A";
                ModelType.ForeColor = System.Drawing.Color.Black;
                ModelType.Text = "N/A";

            if (connected)
            {
                var domains = Forest.GetCurrentForest().Domains;
                foreach (Domain domain in domains)
                {
                    ToolStripItem subItem = new ToolStripMenuItem();
                    subItem.Click += MenuClicked;
                    subItem.Text = domain.Name;
                    domainToolStripMenuItem.DropDownItems.Add(subItem);
                }
            }
           
        }
        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ComputerNameBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ComputerNameBox.Text))
            {
                getCompInfobutton.Enabled = false;
                installServiceCompNameBoxButton.Enabled = false;
                checkServiceStatusButton.Enabled = false;
                uninstallFromComputerButton.Enabled = false;
                GetProfilesButton.Enabled = false;
                DeleteSCCMbutton.Enabled = false;
                map_NetDrButton.Enabled = false;
                map_PrinterButton.Enabled = false;
                checkConnect.Enabled = false;
                buttonADDelete.Enabled = false;
                buttonADInfoComp.Enabled = false;
                buttonADJoin.Enabled = false;
                buttonADMove.Enabled = false;
                CShareButton.Enabled = false;
                PsExecButton.Enabled = false;
                installSnagitButton.Enabled = false;
                installAble2ExtractButton.Enabled = false;
                PingButton.Enabled = false;
                GPUpdateButton.Enabled = false;
                ShutdownButton.Enabled = false;
                RestartButton.Enabled = false;
                RemoteControlViewButton.Enabled = false;
                WarrantyButton.Enabled = false;
                buttonAddRmAdms.Enabled = false;
                buttonLocalAd.Enabled = false;
                pingStatus.ForeColor = System.Drawing.Color.Black;
                pingStatus.Text = "N/A";
                PermissionStatus.ForeColor = System.Drawing.Color.Black;
                PermissionStatus.Text = "N/A";
                RemoteUserName.ForeColor = System.Drawing.Color.Black;
                RemoteUserName.Text = "N/A";
                ModelType.ForeColor = System.Drawing.Color.Black;
                ModelType.Text = "N/A";
                buttonMemory.Enabled = false;
                buttonMotherBoard.Enabled = false;
                buttonPrinters.Enabled = false;
                buttonHosts.Enabled = false;
                buttonApps.Enabled = false;
                buttonADInfoComp.Enabled = false;
                buttonADJoin.Enabled = false;
                buttonProcessor.Enabled = false;
                buttonUSB.Enabled = false;
                buttonServices.Enabled = false;
                SCCMByComputerButton.Enabled = false;
                StartUpBtn.Enabled = false;
                browButoon.Enabled = false;
                sendButton.Enabled = false;
                browFolder.Enabled = false;
                driversButton.Enabled = false;
                installAcroPrBbutton.Enabled = false;
                installAcroStdButton.Enabled = false;
                installChocoButton.Enabled = false;
                installChromeButton.Enabled = false;
                installDropBoxButton.Enabled = false;
                installEyemaxButton.Enabled = false;
                installFileZillaButton.Enabled = false;
                installFireFoxButton.Enabled = false;
                installFlashButton.Enabled = false;
                installiTunesButton.Enabled = false;
                installMBAMButton.Enabled = false;
                installPicasaButton.Enabled = false;
                installSkypeButton.Enabled = false;
                installSmartViewButton.Enabled = false;
                installDriveButton.Enabled = false;
                installJavaButton.Enabled = false;
                installSpotifyButton.Enabled = false;
                exacqVisionButton.Enabled = false;
                installPrimoButton.Enabled = false;
                getInstalledSoftButton.Enabled = false;
                searchADCompButton.Enabled = false;
                getCompInfobutton.Enabled = false;
 
                installPrimoButton.Enabled = false;
                findByCompNameButton.Enabled = false;
            }
            else
            {
                pingStatus.ForeColor = System.Drawing.Color.Black;
                pingStatus.Text = "N/A";
                PermissionStatus.ForeColor = System.Drawing.Color.Black;
                PermissionStatus.Text = "N/A";
                RemoteUserName.ForeColor = System.Drawing.Color.Black;
                RemoteUserName.Text = "N/A";
                ModelType.ForeColor = System.Drawing.Color.Black;
                ModelType.Text = "N/A";
                getCompInfobutton.Enabled = true;
                installServiceCompNameBoxButton.Enabled = true;
                checkServiceStatusButton.Enabled = true;
                uninstallFromComputerButton.Enabled = true;
                DeleteSCCMbutton.Enabled = true;
                GetProfilesButton.Enabled = true;
                getCompInfobutton.Enabled = true;
                map_NetDrButton.Enabled = true;
                map_PrinterButton.Enabled = true;

                installPrimoButton.Enabled = true;
                installAble2ExtractButton.Enabled = true;
                installSnagitButton.Enabled = true;
                checkConnect.Enabled = true;
                buttonADDelete.Enabled = true;
                buttonADInfoComp.Enabled = true;
                buttonADJoin.Enabled = true;
                buttonADMove.Enabled = true;
                CShareButton.Enabled = true;
                PsExecButton.Enabled = true;
                PingButton.Enabled = true;
                GPUpdateButton.Enabled = true;
                ShutdownButton.Enabled = true;
                RestartButton.Enabled = true;
                RemoteControlViewButton.Enabled = true;
                WarrantyButton.Enabled = true;
                buttonAddRmAdms.Enabled = true;
                buttonLocalAd.Enabled = true;
                buttonMemory.Enabled = true;
                buttonMotherBoard.Enabled = true;
                buttonPrinters.Enabled = true;
                buttonHosts.Enabled = true;
                buttonApps.Enabled = true;
                buttonADInfoComp.Enabled = true;
                buttonADJoin.Enabled = true;
                buttonProcessor.Enabled = true;
                buttonUSB.Enabled = true;
                buttonServices.Enabled = true;
                SCCMByComputerButton.Enabled = true;
                StartUpBtn.Enabled = true;
                browButoon.Enabled = true;
                sendButton.Enabled = true;
                browFolder.Enabled = true;
                ComputerNameBox.Text = ComputerNameBox.Text.TrimEnd();
                driversButton.Enabled = true;
                installAcroPrBbutton.Enabled = true;
                installAcroStdButton.Enabled = true;
                installChocoButton.Enabled = true;
                installChromeButton.Enabled = true;
                installDropBoxButton.Enabled = true;
                installEyemaxButton.Enabled = true;
                installFileZillaButton.Enabled = true;
                installFireFoxButton.Enabled = true;
                installFlashButton.Enabled = true;
                installiTunesButton.Enabled = true;
                installMBAMButton.Enabled = true;
                installPicasaButton.Enabled = true;
                installSkypeButton.Enabled = true;
                installSmartViewButton.Enabled = true;
                installDriveButton.Enabled = true;
                installJavaButton.Enabled = true;
                installSpotifyButton.Enabled = true;
                exacqVisionButton.Enabled = true;
                installPrimoButton.Enabled = true;
                getInstalledSoftButton.Enabled = true;
                searchADCompButton.Enabled = true;
                findByCompNameButton.Enabled = true;


            }

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void aboutTheAdminToolboxToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static string sendtext = "";
        public static string domain = "";

        public void checkConnectComp(object sender, EventArgs e)
        {
                        Ping pingSender = new Ping();
                        PingOptions options = new PingOptions();
                        List<String> userlist = new List<string>();
                        // Use the default Ttl value which is 128,
                        // but change the fragmentation behavior.
                        options.DontFragment = true;

                        // Create a buffer of 32 bytes of data to be transmitted.
                        Action userlabel2 = () => CurrentUserLabel.Text = "Remote Current User:";
                        CurrentUserLabel.Invoke(userlabel2);
                        try
                        {

                            PingReply reply = pingSender.Send(ComputerNameBox.Text.ToLower());

                            if (reply.Status == IPStatus.Success)
                            {
                                Action pingcolor = () => pingStatus.ForeColor = System.Drawing.Color.Green;
                                Action pingtext = () => pingStatus.Text = "SUCCESS";
                                pingStatus.Invoke(pingcolor);
                                pingStatus.Invoke(pingtext);

                                //Permissions check 
                                var exists = System.IO.Directory.Exists("\\\\" + ComputerNameBox.Text + @"\C$\Windows");
                                if (exists == true)
                                {
                                    Action permcolor = () => PermissionStatus.ForeColor = System.Drawing.Color.Green;
                                    Action permtext = () => PermissionStatus.Text = "SUCCESS";
                                    PermissionStatus.Invoke(permcolor);
                                    PermissionStatus.Invoke(permtext);
                                }
                                else
                                {
                                    Action permcolorf = () => PermissionStatus.ForeColor = System.Drawing.Color.Red;
                                    Action permtextf = () => PermissionStatus.Text = "FAIL";
                                    PermissionStatus.Invoke(permcolorf);
                                    PermissionStatus.Invoke(permtextf);
                                }
                                //Get model of computer
                                try
                                {
                                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                                    scope.Connect();
                                    WqlObjectQuery wqlQuery =
                                    new WqlObjectQuery("SELECT * FROM Win32_ComputerSystem");
                                    ManagementObjectSearcher searcher =
                                        new ManagementObjectSearcher(scope, wqlQuery);

                                    foreach (ManagementObject n in searcher.Get())
                                    {
                                        Action modelcolor = () => ModelType.ForeColor = System.Drawing.Color.Green;
                                        Action modeltext = () => ModelType.Text = n["model"].ToString();
                                        ModelType.Invoke(modelcolor);
                                        ModelType.Invoke(modeltext);
                                    }
                                }
                                catch (SystemException)
                                {
                                    Action modelcolorf = () => ModelType.ForeColor = System.Drawing.Color.Red;
                                    Action modeltextf = () => ModelType.Text = "N/A";
                                    ModelType.Invoke(modelcolorf);
                                    ModelType.Invoke(modeltextf);
                                }

                                //check remote user
                                try
                                {
                                    

                                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                                    scope.Connect();
                                    WqlObjectQuery wqlQuery =
                                    new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name LIKE 'explorer.exe'");
                                    ManagementObjectSearcher searcher =
                                        new ManagementObjectSearcher(scope, wqlQuery);
                                    int usercount = searcher.Get().Count;
                                    foreach (ManagementObject n in searcher.Get())
                                    {
                                        ManagementBaseObject outParams = n.InvokeMethod("GetOwner", null, null);
                                        Action remotecolor = () => RemoteUserName.ForeColor = System.Drawing.Color.Green;
                                        RemoteUserName.Invoke(remotecolor);
                                        if (String.IsNullOrEmpty(outParams["Domain"].ToString())) { RemoteUserName.Text = "N/A"; }
                                        else
                                        {
                                            if (usercount > 1)
                                            {
                                                Action remotename = () => RemoteUserName.Text = "Check output box below...";
                                                RemoteUserName.Invoke(remotename);
                                                userlist.Add(outParams["Domain"].ToString() + "\\" + outParams["User"].ToString());

                                            }
                                            else
                                            {
                                                Action remotename = () => RemoteUserName.Text = outParams["Domain"].ToString() + "\\" + outParams["User"].ToString();
                                                RemoteUserName.Invoke(remotename);
                                                userlist.Add(outParams["Domain"].ToString() + "\\" + outParams["User"].ToString());
                                            }
                                        }
                                    }
                                    if (usercount > 1)
                                    {
                                        Action output = () => OutputBox.AppendText("List of current users logged in: \r\n");
                                        OutputBox.Invoke(output);
                                        foreach (string name in userlist)
                                        {
                                            Action output2 = () => OutputBox.AppendText(name + "\r\n");
                                            OutputBox.Invoke(output2);
                                        }
                                    }
                                }
                                catch (SystemException)
                                {
                                    string compname;
                                    if (ComputerNameBox.Text.StartsWith("10"))
                                    {
                                        IPHostEntry hostEntry;
                                        hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                                        int pos = hostEntry.HostName.ToString().IndexOf(".");
                                        compname = hostEntry.HostName.ToString().Remove(pos).ToString();
                                    }
                                    else { compname = ComputerNameBox.Text; }

                                    Action userlabel = () => CurrentUserLabel.Text = "Last Logged In User:";
                                    CurrentUserLabel.Invoke(userlabel);
                                    ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                                    scope.Connect();
                                    WqlObjectQuery wqlQuery =
                                    new WqlObjectQuery("SELECT * from sms_r_system WHERE Name='" + compname + "'");
                                    ManagementObjectSearcher searcher =
                                        new ManagementObjectSearcher(scope, wqlQuery);
                                    foreach (ManagementObject n in searcher.Get())
                                    {
                                        if (!(object.ReferenceEquals(null, n["LastLogonUserName"])))
                                        {
                                            Action remotename4 = () => RemoteUserName.Text = "GGGLOBAL\\" + n["LastLogonUserName"].ToString();
                                            RemoteUserName.Invoke(remotename4);
                                            userlist.Add("GGGLOBAL\\" + n["LastLogonUserName"].ToString());
                                            Action remotecolorn = () => RemoteUserName.ForeColor = System.Drawing.Color.Green;
                                            RemoteUserName.Invoke(remotecolorn);
                                        }
                                    }


                                }
                            }
                            else
                            {
                                Action pingcolor2 = () => pingStatus.ForeColor = System.Drawing.Color.Red;
                                Action pingtext2 = () => pingStatus.Text = "FAIL";
                                pingStatus.Invoke(pingcolor2);
                                pingStatus.Invoke(pingtext2);

                            }
                        }
                        catch (SystemException)
                        {
                            Action failcolor0 = () => pingStatus.ForeColor = System.Drawing.Color.Red;
                            pingStatus.Invoke(failcolor0);
                            Action failtext0 = () => pingStatus.Text = "CANNOT PING";
                            pingStatus.Invoke(failtext0);
                            Action failcolor1 = () => RemoteUserName.ForeColor = System.Drawing.Color.Red;
                            pingStatus.Invoke(failcolor1);
                            Action failtext1 = () => RemoteUserName.Text = "N/A";
                            pingStatus.Invoke(failtext1);
                            Action failcolor2 = () => ModelType.ForeColor = System.Drawing.Color.Red;
                            pingStatus.Invoke(failcolor2);
                            Action failtext2 = () => ModelType.Text = "N/A";
                            pingStatus.Invoke(failtext2);
                            Action failcolor3 = () => PermissionStatus.ForeColor = System.Drawing.Color.Red;
                            pingStatus.Invoke(failcolor3);
                            Action failtext3 = () => PermissionStatus.Text = "N/A";
                            pingStatus.Invoke(failtext3);
                        }
            string computername;
            if (ComputerNameBox.Text.StartsWith("10"))
            {
                IPHostEntry hostEntry;
                hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                int pos = hostEntry.HostName.ToString().IndexOf(".");
                computername = hostEntry.HostName.ToString().Remove(pos).ToString();
            }
            else { computername = ComputerNameBox.Text; }
            if (userlist != null && userlist.Count != 0)
            {
                createComputerHistory(computername, userlist.First().ToString());
            }
        }



        private void checkConnect_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += checkConnectComp;
            bw.RunWorkerAsync();
        }

        private void PermissionStatus_Click(object sender, EventArgs e)
        {


        }

        private void PingLabel_Click(object sender, EventArgs e)
        {
            
        }



        public bool buttonclicked;

        public void bw_PingButton(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            try
            {
                Action settext = () => PingButton.Text = "Pinging";
                PingButton.Invoke(settext);
                Process p = new Process();
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = @"C:\Windows\System32\ping.exe";
                p.StartInfo.Arguments = ComputerNameBox.Text.ToLower().ToString();
                p.Start();

                var reader = p.StandardOutput;
                p.Close();

                while (!reader.EndOfStream)
                {
                    if (bw.CancellationPending)
                    {
                        Action settext2 = () => PingButton.Text = "Ping";
                        PingButton.Invoke(settext2);
                        e.Cancel = true;
                        bw.Dispose();
                        Action err = () => OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                        OutputBox.Invoke(err);
                        return;
                    }
                    // the point is that the stream does not end until the process has 
                    // finished all of its output.
                    var nextLine = reader.ReadLine();
                    Action output1 = () => OutputBox.AppendText(nextLine.ToString() + "\r\n");
                    OutputBox.Invoke(output1);
                }
                Action settext3 = () => PingButton.Text = "Ping";
                PingButton.Invoke(settext3);
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException)
            {
                Action excp0 = () => OutputBox.ForeColor = System.Drawing.Color.Red;
                Action excp1 = () => OutputBox.Text = "Ping request could not find " + ComputerNameBox.Text.ToLower().ToString() + ". Please check the name and try again.";
                OutputBox.Invoke(excp0);
                OutputBox.Invoke(excp1);
            }
            // Action enable = () => PingButton.Enabled = true; ;
            // PingButton.Invoke(enable);

        }
        BackgroundWorker bwpinger = new BackgroundWorker();
        private void PingButton_Click(object sender, EventArgs e)
        {

            if (bwpinger.IsBusy)
            {
                bwpinger.CancelAsync();
            }
            else
            {
                bwpinger = new BackgroundWorker();
                bwpinger.DoWork += bw_PingButton;
                bwpinger.WorkerSupportsCancellation = true;
                bwpinger.RunWorkerAsync();
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {


        }

        public void bw_DoRemote(object sender, DoWorkEventArgs e)
        {
            try
            {
                Process SCCM = new Process();
                SCCM.StartInfo.UseShellExecute = false;
                // You can start any process, HelloWorld is a do-nothing example.
                SCCM.StartInfo.FileName = @"C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe";
                SCCM.StartInfo.Arguments = ComputerNameBox.Text.ToLower() + " \\" + SCCMServer;
                SCCM.Start();
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoteControlViewButton_Click(object sender, EventArgs e)
        {

                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += bw_DoRemote;
                        bw.RunWorkerAsync();

        }

        static void openInExplorer(string path)
        {
            string cmd = "explorer.exe";
            string arg = "/select, " + path;
            Process.Start(cmd, arg);
        }
        private void CShareButton_Click(object sender, EventArgs e)
        {

            string user = Environment.UserName;
            var result = user.Substring(user.LastIndexOf('_') + 1);
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + ComputerNameBox.Text.ToLower() + "\\C$";
            psi.Arguments = "/c runas /user:" + getCurrentDomain() + @"\" + result + @" ""explorer /separate, " + path + "";
            process.StartInfo = psi;
            process.Start();

        }

        private void PsExecButton_Click(object sender, EventArgs e)
        {
           
                        try
                        {
                            Process process = new Process();
                            ProcessStartInfo psi = new ProcessStartInfo();
                            psi.FileName = "cmd.exe";
                            psi.UseShellExecute = false;
                            string path = "\\\\" + ComputerNameBox.Text.ToLower();
                            string hh = string.Format("\"{0}\"", path);
                            psi.Arguments = @"/k C:\Windows\System32\psexec.exe -accepteula " + hh + " -h cmd /k";
                            process.StartInfo = psi;
                            process.Start();
                        }
                        catch (SystemException err)
                        {
                            System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        //System.Media.SystemSounds.Hand.Play();
                        // System.Windows.Forms.MessageBox.Show("Please copy psexec to your System32 folder", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           
        }

        public void bw_Dowork(object sender, DoWorkEventArgs e)
        {

            SHDocVw.InternetExplorer IE = new SHDocVw.InternetExplorer();
            string serial;
            mshtml.HTMLDocument myDoc = new mshtml.HTMLDocument();
           
            
            try
            {
                IE.Navigate2("https://csp.lenovo.com/ibapp/il/WarrantyLookup.jsp");
                IE.Visible = true;
                
                while (IE.Busy == true) { System.Threading.Thread.Sleep(2000); }
                
                if(ComputerNameBox.Text.Contains("-"))
                {
                    int index = ComputerNameBox.Text.LastIndexOf("-");
                    serial = ComputerNameBox.Text.Substring(index + 1).ToString();
                    myDoc = (mshtml.HTMLDocument)IE.Document;
                    mshtml.HTMLInputElement serialnumber = (mshtml.HTMLInputElement)myDoc.all.item("serial", 0);
                    serialnumber.value = serial;
                    mshtml.HTMLInputElement bTTn = (mshtml.HTMLInputElement)myDoc.all.item("warrantySubmit2", 0);
                    bTTn.click();
                    Thread.Sleep(1000);

                }
                else
                {
                    ManagementScope scope =
                new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_BIOS");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
                    myDoc = (mshtml.HTMLDocument)IE.Document;
                    mshtml.HTMLInputElement serialnumber = (mshtml.HTMLInputElement)myDoc.all.item("serial", 0);
                    foreach (ManagementObject n in searcher.Get())
                    {
                        serial = n["serialnumber"].ToString();
                        serialnumber.value = serial;
                    }
                    mshtml.HTMLInputElement bTTn = (mshtml.HTMLInputElement)myDoc.all.item("warrantySubmit2", 0);
                    bTTn.click();
                    Thread.Sleep(1000);
                }
                
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                IE.Quit();
            }
        }


        private void WarrantyButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Dowork;
            bw.RunWorkerAsync();
        }

        private void GPUpdateButton_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + ComputerNameBox.Text;
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/k psexec -accepteula -s " + hh + " -h cmd /c gpupdate /force";
                process.StartInfo = psi;
                process.Start();
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
          
            try
            {

                OutputBox.AppendText("Rebooting remote computer....");
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = @"C:\Windows\System32\shutdown.exe";
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                string path = "\\\\" + ComputerNameBox.Text;
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/m " + path + " /r /t 0";
                process.StartInfo = psi;
                process.Start();
                System.Windows.Forms.MessageBox.Show("Check computer to confirm it restarted...", "Check remote PC", MessageBoxButtons.OK, MessageBoxIcon.Information);

                /*
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                string path = "\\\\" + ComputerNameBox.Text;
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + " -h cmd /c shutdown /r /f /t 0";
                process.StartInfo = psi;
                process.Start();
                    */
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           


        }

        private void ShutdownButton_Click(object sender, EventArgs e)
        {
          
            try
            {
                OutputBox.AppendText("Shutting down remote computer....");
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                string path = "\\\\" + ComputerNameBox.Text;
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c C:\Windows\System32shutdown /s /f /t 0";
                process.StartInfo = psi;
                process.Start();
                MessageBox.Show("Check computer to confirm it was shutdown...", "Check remote PC", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        
        }

        private void ADMMCButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\WINDOWS\Sysnative\dsa.msc"))
            {
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(@"C:\Windows\Sysnative\dsa.msc");
                psi.UseShellExecute = true;
                process.StartInfo = psi;
                process.Start();
            }         
        }

        private void CompmgmtButton_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Windows\System32\compmgmt.msc";
            psi.Arguments = "/s";
            psi.UseShellExecute = true;
            process.StartInfo = psi;
            process.Start();
        }

        private void EventVwrButton_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Windows\System32\eventvwr.msc";
            psi.Arguments = "/s";
            psi.UseShellExecute = true;
            process.StartInfo = psi;
            process.Start();
        }

        private void PrintMgmtButton_Click(object sender, EventArgs e)
        {
            if (File.Exists(@"C:\WINDOWS\Sysnative\printmanagement.msc"))
            {
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(@"C:\WINDOWS\Sysnative\printmanagement.msc");
                psi.UseShellExecute = true;
                process.StartInfo = psi;
                process.Start();
            }
        }

        private void commandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void powershellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "powershell.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void powerhsellISEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "powershell_ise.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void internetExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files\Internet Explorer\iexplore.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();

        }

        private void dameWareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files\DameWare\DameWare Mini Remote Control 7.5\DWRCC.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void notepadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "notepad.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void remoteControlViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = @"C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\i386\CmRcViewer.exe";
            psi.UseShellExecute = false;
            process.StartInfo = psi;
            process.Start();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
           
                        //Create a shortcut to the appropriate Windows domain
                        PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  getCurrentDomain().ToString());

                        //Create a "user object" in the context
                        UserPrincipal user = new UserPrincipal(domainContext);

                        string adtext = ADIdentityBox.Text;

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
                        try
                        {
                            PrincipalSearchResult<Principal> results = pS.FindAll();

                            //If necessary, request more details
                            Principal pc = results.ToList()[0];
                            DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();

                            //Output first result of the test

                            //Gets SamAcctName
                            string sam = pc.SamAccountName.ToString();
                            //Finds account using sam
                            UserPrincipal usr = UserPrincipal.FindByIdentity(domainContext, sam);
                            //Test to see if account is locked, if it is unlocks account

                            if (usr.IsAccountLockedOut())
                            {
                                usr.UnlockAccount(); 
                                System.Windows.Forms.MessageBox.Show("Account is now unlocked.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                de.CommitChanges();
                                de.Close();
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("This account is already unlocked", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                        }
                        catch (SystemException err)
                        {
                            System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

            }
        

        public void buttonResetPwd_Click(object sender, EventArgs e)
        {
           
                sendtext = ADIdentityBox.Text;
                domain = getCurrentDomain();
                The_Admin_Toolbox.ResetPass frm = new The_Admin_Toolbox.ResetPass();
                if (!frm.IsDisposed)
                    frm.Show();
           
        }

        public static Int64 ConvertADSLargeIntegerToInt64(object adsLargeInteger)
        {

            var highPart = (Int32)adsLargeInteger.GetType().InvokeMember("HighPart", System.Reflection.BindingFlags.GetProperty, null, adsLargeInteger, null);

            var lowPart = (Int32)adsLargeInteger.GetType().InvokeMember("LowPart", System.Reflection.BindingFlags.GetProperty, null, adsLargeInteger, null);

            return highPart * ((Int64)UInt32.MaxValue + 1) + lowPart;
        }

        private void buttonADInfo_Click(object sender, EventArgs e)
        {
            //Create a shortcut to the appropriate Windows domain
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString());

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(domainContext);

            string adtext = ADIdentityBox.Text;

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
            try
            {
                PrincipalSearchResult<Principal> results = pS.FindAll();
                //If necessary, request more details
                Principal pc = results.ToList()[0];
                DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();
                UserPrincipal acct = UserPrincipal.FindByIdentity(domainContext, pc.SamAccountName);
                OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "LockedOut", acct.IsAccountLockedOut().ToString()));
                //define values to output to textbox

                //city
                if (!(object.ReferenceEquals(null, de.Properties["l"].Value)))
                {
                    string city = de.Properties["l"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "City", city));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "City", ""));
                }

                //country
                if (!(object.ReferenceEquals(null, de.Properties["co"].Value)))
                {
                    string co = de.Properties["co"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Country", co));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Country", ""));
                }

                //company
                if (!(object.ReferenceEquals(null, de.Properties["company"].Value)))
                {
                    string company = de.Properties["company"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Company", company));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Company", ""));
                }

                //whenCreated
                if (!(object.ReferenceEquals(null, de.Properties["whenCreated"].Value)))
                {
                    string created = de.Properties["whenCreated"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Created", created));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Created", ""));
                }

                //Department
                if (!(object.ReferenceEquals(null, de.Properties["Department"].Value)))
                {
                    string dept = de.Properties["Department"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Department", dept));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Department", ""));
                }

                //distinguishedName
                if (!(object.ReferenceEquals(null, de.Properties["distinguishedName"].Value)))
                {
                    string DN = de.Properties["distinguishedName"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "DistinguishedName", DN));
                }

                //usercontrol
                int flags = (int)de.Properties["userAccountControl"].Value;
                bool enabled = !Convert.ToBoolean(flags & 0x0002);
                OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Enabled", enabled.ToString()));

                //givenName
                if (!(object.ReferenceEquals(null, de.Properties["givenName"].Value)))
                {
                    string givenName = de.Properties["givenName"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "GivenName", givenName));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "GivenName", ""));
                }

                //badPasswordTime
                if (!(object.ReferenceEquals(null, de.Properties["badPasswordTime"].Value)))
                {
                    long filetime = ConvertADSLargeIntegerToInt64(de.Properties["badPasswordTime"].Value);
                    DateTime lastBadPwd = DateTime.FromFileTime(filetime);
                    if (lastBadPwd != null)
                    {
                        OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "LastBadPasswordAttempt", lastBadPwd));
                    }
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "LastBadPasswordAttempt", ""));
                }

                //passwordLastSet
                if (!(object.ReferenceEquals(null, de.Properties["pwdLastSet"].Value)))
                {
                    long filetime = ConvertADSLargeIntegerToInt64(de.Properties["pwdLastSet"].Value);
                    DateTime lastBadPwd = DateTime.FromFileTime(filetime);
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "PasswordLastSet", lastBadPwd));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "PasswordLastSet", ""));
                }

                //manager
                if (!(object.ReferenceEquals(null, de.Properties["manager"].Value)))
                {
                    string manager = de.Properties["manager"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Manager", manager));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Manager", ""));
                }

                //officename
                if (!(object.ReferenceEquals(null, de.Properties["phsyicalDeliveryOfficeName"].Value)))
                {
                    string office = de.Properties["physicalDeliveryOfficeName"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Office", office));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Office", ""));
                }

                //name
                if (!(object.ReferenceEquals(null, de.Properties["name"].Value)))
                {
                    string name = de.Properties["name"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Name", name));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Name", ""));
                }

                //sam account
                if (!(object.ReferenceEquals(null, de.Properties["SamAccountName"].Value)))
                {
                    string sam = de.Properties["SamAccountName"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "SamAccountName", sam));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "SamAccountName", ""));
                }

                //state
                if (!(object.ReferenceEquals(null, de.Properties["st"].Value)))
                {
                    string st = de.Properties["st"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "State", st));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "State", ""));
                }

                //telephonenumber
                if (!(object.ReferenceEquals(null, de.Properties["telephoneNumber"].Value)))
                {
                    string num = de.Properties["telephoneNumber"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "telePhoneNumber", num));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "telePhoneNumber", ""));
                }

                //title
                if (!(object.ReferenceEquals(null, de.Properties["title"].Value)))
                {
                    string title = de.Properties["title"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Title", title));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Title", ""));
                }

                //streetaddr
                if (!(object.ReferenceEquals(null, de.Properties["streetAddress"].Value)))
                {
                    string addr = de.Properties["streetAddress"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "StreetAddress", addr));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "StreetAddress", ""));
                }

                //mail
                if (!(object.ReferenceEquals(null, de.Properties["mail"].Value)))
                {
                    string mail = de.Properties["mail"].Value.ToString();
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Email Address", mail));
                }
                else
                {
                    OutputBox.AppendText(String.Format("\r\n{0,-20}: {1}", "Email Address", ""));
                }
                //convert badPasswordTime object into a datetime object

                //check if account is enabled

                //output ad info to textbox


                OutputBox.AppendText("\r\n");
                OutputBox.AppendText("\r\n");
                OutputBox.AppendText("#####################");
                if (!(object.ReferenceEquals(null, de.Properties["memberOf"].Value)))
                {
                    string member = de.Properties["memberOf"].Value.ToString();

                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("\r\nGroup Membership: ");


                    //enurmate member objects with the memberOf property collection
                    foreach (object memberOf in de.Properties["memberOf"])
                    {
                        string memlist = memberOf.ToString();
                        OutputBox.AppendText(String.Format(memlist + "\r\n"));
                    }
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("#####################");
                    OutputBox.AppendText("\r\n");
                }
                else
                {
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("\r\nGroup Membership: ");
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText("#####################");
                    OutputBox.AppendText("\r\n");
                }

                user.Dispose();
                de.Close();
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void buttonADJoin_Click(object sender, EventArgs e)
        {
       
                sendtext = ComputerNameBox.Text;
                The_Admin_Toolbox.ADFunc frm = new The_Admin_Toolbox.ADFunc(this);
                domain = getCurrentDomain();
                frm.Show();
            
        }

        private void buttonADRename_Click(object sender, EventArgs e)
        {
     
                sendtext = ComputerNameBox.Text;
                domain = getCurrentDomain();
                The_Admin_Toolbox.ADFunc frm = new The_Admin_Toolbox.ADFunc(this);
                frm.Show();
            
        }

        private void buttonADMove_Click(object sender, EventArgs e)
        {
         
                sendtext = ComputerNameBox.Text;
                domain = getCurrentDomain();
                The_Admin_Toolbox.ADMove frm = new The_Admin_Toolbox.ADMove();
                frm.Show();
            
        }

        private void buttonADDelete_Click(object sender, EventArgs e)
        {
            
                try
                {
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString()))
                    {
                        // find a computer
                        ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, ComputerNameBox.Text);
                        computer.Delete();
                        computer.Save();
                        computer.Dispose();

                        System.Windows.Forms.MessageBox.Show(ComputerNameBox.Text + " has been deleted", "Deletion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }


        public System.Timers.Timer timer = new System.Timers.Timer();

        private void OnTimedEvent(object source, System.Timers.ElapsedEventArgs e)
        {
           
           
                string adtext = ADIdentityBox.Text;
                timer.Stop();

                try
                {
                    //Create a shortcut to the appropriate Windows domain
                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString());

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

                    //Gets SamAcctName
                    string sam = pc.SamAccountName;
                    //Finds account using sam
                    UserPrincipal usr = UserPrincipal.FindByIdentity(domainContext, sam);
                    //Test to see if account is locked, if it is unlocks account
                    if (usr.IsAccountLockedOut() == false)
                    {
                        Action unlock = () => buttonUnlock.Enabled = false; ;
                        buttonUnlock.Invoke(unlock);
                    }

                }
                catch (SystemException err) { }
            

        }

        private void ADIdentityBox_TextChanged(object sender, EventArgs e)
        {
            timer.AutoReset = false;
           // timer.SynchronizingObject = buttonUnlock;
            timer.Interval = 450;
            timer.Stop();
            if (String.IsNullOrEmpty(ADIdentityBox.Text))
            {
                buttonADInfo.Enabled = false;
                buttonUnlock.Enabled = false;
                buttonResetPwd.Enabled = false;
                //buttonGroupMem.Enabled = false;
                MoveUserbutton.Enabled = false;
                addtoGroupButton.Enabled = false;
                searchADUserButton.Enabled = false;
                buttonGroupMem.Enabled = false;
            }
            else
            {
                buttonADInfo.Enabled = true;
                buttonUnlock.Enabled = true;
                buttonResetPwd.Enabled = true;
                // buttonGroupMem.Enabled = true;
                MoveUserbutton.Enabled = true;
                addtoGroupButton.Enabled = true;
                searchADUserButton.Enabled = true;
                buttonGroupMem.Enabled = true;
            }
            timer.Start();

        }

        private void buttonADInfoComp_Click(object sender, EventArgs e)
        {
           
                try
                {
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString()))
                    {
                        ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, ComputerNameBox.Text);

                        OutputBox.AppendText("\r\nAD Info for " + ComputerNameBox.Text + "\r\n");
                        //OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "AccountExpirationDate", expr.ToString()));
                        if (!(object.ReferenceEquals(null, computer.Description)))
                        {
                            // OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "Description", computer.Description.ToString()));
                            OutputBox.AppendText("\r\nDescription: ".PadRight(10));
                            OutputBox.AppendText(computer.Description.ToString().PadLeft(8));
                        }
                        //OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "DisplayName", computer.Name.ToString()));
                        OutputBox.AppendText("\r\nDisplayName: ".PadRight(10));
                        OutputBox.AppendText(computer.Name.ToString().PadLeft(8));
                        // OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "DistinguishedName", computer.DistinguishedName.ToString()));
                        OutputBox.AppendText("\r\nDistinguishedName: ".PadRight(10));
                        OutputBox.AppendText(computer.DistinguishedName.ToString().PadLeft(8));
                        // OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "Enabled", computer.Enabled.ToString()));
                        OutputBox.AppendText("\r\nEnabled: ".PadRight(10));
                        OutputBox.AppendText(computer.Enabled.ToString().PadLeft(8));
                        // OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "LastLogon", computer.LastLogon.Value.ToString()));
                        OutputBox.AppendText("\r\nLastLogon: ".PadRight(10));
                        OutputBox.AppendText(computer.LastLogon.ToString().PadLeft(8));
                        // OutputBox.AppendText(String.Format("\r\n{0,-20} : {1}", "GUID", computer.Guid.ToString()));
                        OutputBox.AppendText("\r\nGUID: ".PadRight(10));
                        OutputBox.AppendText(computer.Guid.ToString().PadLeft(8));
                        OutputBox.AppendText("\r\n");
                        OutputBox.AppendText("\r\n");
                        OutputBox.AppendText("#####################");
                        OutputBox.AppendText("\r\n");
                    }
                }
                catch (SystemException err) { MessageBox.Show(err.Message.ToString() + "\r\n Also check spelling.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
        }

        public void bw_localAdmin(object sender, EventArgs e)
        {

            if (ComputerNameBox.Text != String.Empty)
            {
                try
                {
                    Action output0 = () => OutputBox.AppendText("\r\nLocal admins on " + ComputerNameBox.Text + "\r\n" + "-------------------------------------------------\r\n"); ;
                    OutputBox.Invoke(output0);
                    DirectoryEntry localMachine = new DirectoryEntry("WinNT://" + ComputerNameBox.Text);
                    DirectoryEntry admGroup = localMachine.Children.Find("administrators", "group");
                    object members = admGroup.Invoke("members", null);
                    foreach (object groupMember in (IEnumerable)members)
                    {
                        DirectoryEntry member = new DirectoryEntry(groupMember);
                        Action output1 = () => OutputBox.AppendText(member.Name + "\r\n");
                        OutputBox.Invoke(output1);

                    }
                    Action output2 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output2);
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                        
                        );
                }
            }
                        

        }


        private void buttonLocalAd_Click(object sender, EventArgs e)
        {
          
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_localAdmin;
                bw.RunWorkerAsync();
            
        }

        private void TheAdminToolBox_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void TheAdminToolBox_FormClosing(object sender, FormClosingEventArgs e)
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string userFilePath = Path.Combine(localAppData, "AdminToolBox");
            string sourceFilePath = Path.Combine(
            System.Windows.Forms.Application.StartupPath, "app.sqlite");
            string destFilePath = Path.Combine(userFilePath, "app.sqlite");
            File.Copy(sourceFilePath, destFilePath, true);
            
            Properties.Settings.Default["Domain"] = getCurrentDomain();
            Properties.Settings.Default.Save();
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TheAdminToolBox_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                notifyIcon.BalloonTipText = "The Admin ToolBox is still running...";
                notifyIcon.BalloonTipTitle = "The Admin ToolBox";

            }

            // Restored!


        }

        private void richTextBoxNotes_KeyDown(object sender, KeyEventArgs e)
        {/*
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                DataFormats.Format df = DataFormats.GetFormat(DataFormats.Bitmap);
                StringCollection strcollect = Clipboard.GetFileDropList();
                if (Clipboard.ContainsFileDropList())
                {
                    Image image = Image.FromFile(strcollect[0]);
                    Clipboard.Clear();
                    Clipboard.SetImage(image);
                    if (Clipboard.ContainsImage())
                    {
                        richTextBoxNotes.Paste(df);
                        e.Handled = true;
                        Clipboard.Clear();
                    }
                }
            }*/
        }

        private void buttonAddRmAdms_Click(object sender, EventArgs e)
        {
           
                sendtext = ComputerNameBox.Text;
                domain = getCurrentDomain();
                The_Admin_Toolbox.LocalAdminsForm frm = new The_Admin_Toolbox.LocalAdminsForm();
                frm.Show();
            

        }

        public void bw_MothboardInfo(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting basic info from remote computer...");
            OutputBox.Invoke(output);
                       
            try
            {
                string model = ComputerInfo.GetModel(ComputerNameBox.Text);
                string sn = ComputerInfo.GetSN(ComputerNameBox.Text);
                string manu = ComputerInfo.GetManufacturer(ComputerNameBox.Text);
                string free = ComputerInfo.GetFreeSpace(ComputerNameBox.Text);
                string size = ComputerInfo.GetSize(ComputerNameBox.Text);
                string os = ComputerInfo.GetOS(ComputerNameBox.Text);
            
                double sp = Convert.ToDouble(free);
                string freesp = ByteSizeLib.ByteSize.FromBytes(sp).GigaBytes.ToString("0.##");

                double si = Convert.ToDouble(size);
                string totalsize = ByteSizeLib.ByteSize.FromBytes(si).GigaBytes.ToString("0.##");
                string usedspace = ByteSizeLib.ByteSize.FromBytes(si - sp).GigaBytes.ToString("0.##");

                double intfree = Convert.ToInt64(free) / 1024.0;
                double intsize = Convert.ToInt64(size) / 1024.0;
                double percentfree = (double)(intfree / intsize * 100);
                
                Action output1 = () => OutputBox.AppendText("\r\n" + "\r\n" + String.Format("{0} : {1}", "ComputerName", ComputerNameBox.Text).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "Serial Number", sn).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "OS", os).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "Model", model).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "Manufacturer", manu).PadRight(10) + "\r\n" + String.Format("{0} : {1} GB", "Total Size", totalsize).PadRight(10) + "\r\n" + String.Format("{0} : {1} GB", "Free Space", freesp).PadRight(10)  + "\r\n" + String.Format("{0} : {1} GB", "Used Space", usedspace).PadRight(10) + "\r\n" + String.Format("{0} : {1}%", "Remaining Free", Math.Round(percentfree, 2).ToString()).PadRight(10) + "\r\n" + "\r\n" + ("#####################" + "\r\n"));
                OutputBox.Invoke(output1);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void buttonOperSys_Click(object sender, EventArgs e)
        {
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_MothboardInfo;
                bw.RunWorkerAsync();
           

        }

        public void bw_USBInfo(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting USB info from remote computer...\r\n");
            OutputBox.Invoke(output);
            try
            {
                ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_USBController");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {

                    Action output1 = () => OutputBox.AppendText(String.Format("{0} : {1}", "Description:", n["Description"].ToString()).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "DeviceID:", n["DeviceID"].ToString()).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "PNPDeviceID:", n["PNPDeviceID"].ToString()).PadRight(10) + "\r\n" + String.Format("{0} : {1}", "Manufacturer", n["Manufacturer"].ToString()).PadRight(10));
                    OutputBox.Invoke(output1);
                }
                Action output2 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output2);

            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    
                    );
            }


        }



        private void buttonUSB_Click(object sender, EventArgs e)
        {
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_USBInfo;
                bw.RunWorkerAsync();
            

        }


        private void button1_Click(object sender, EventArgs e)
        {

            OutputBox.Text = "";
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {

            if (OutputBox.Text != null)
            {
                AutoClosingMessageBox.Show("Output text has been copied to clipboard!", "Copied", 2000);
                Clipboard.SetText(OutputBox.Text);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }


        public void bw_MemoryInfo(object sender, DoWorkEventArgs e)
        {
            Action o = () => OutputBox.AppendText("Getting memory info from remote computer...\r\n");
            OutputBox.Invoke(o);
            try
            {
                ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    long size = Convert.ToInt64(n["Capacity"]);
                    string total = SizeSuffix(size);
                    Action output = () => OutputBox.AppendText("Manufacturer:\t" + n["Manufacturer"].ToString() + "\r\n" + "Description:\t" + n["Description"].ToString() + "\r\n" + "Capacity:\t\t" + total + "\r\n" + "Speed:\t\t" + n["Speed"].ToString() + "\r\n" + "MemoryType:\t" + n["MemoryType"].ToString() + "\r\n" + "ComputerName:\t" + ComputerNameBox.Text + "\r\n" + "\r\n");
                    OutputBox.Invoke(output);
                }
                Action output1 = () => OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output1);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    
                    );
            }
        }


        private void buttonMemory_Click(object sender, EventArgs e)
        {
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_MemoryInfo;
                bw.RunWorkerAsync();
            
        }



        public void bw_ProcessorInfo(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting processor info from remote computer...");
            OutputBox.Invoke(output);

            try
            {
                Action output1 = () => OutputBox.AppendText("\r\n" + "L2Cache:\t\t" + ComputerInfo.GetL2Cache(ComputerNameBox.Text) + "\r\n" + "Name:\t\t" + ComputerInfo.GetName(ComputerNameBox.Text) + "\r\n" + "Cores:\t\t" + ComputerInfo.GetCores(ComputerNameBox.Text) + "\r\n" + "Manufacturer:\t" + ComputerInfo.GetProcessMan(ComputerNameBox.Text) + "\r\n" + "Speed:\t\t" + ComputerInfo.GetProcSpeed(ComputerNameBox.Text) + "\r\n" + "MaxSpeed:\t" + ComputerInfo.GetMaxSpeed(ComputerNameBox.Text) + "\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output1);

            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    
                    );
            }
        }

        private void buttonProcessor_Click(object sender, EventArgs e)
        {
         
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_ProcessorInfo;
                bw.RunWorkerAsync();
           
        }

        public void bw_Printers(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting network printers list from remote computer...");
            OutputBox.Invoke(output);

            try
            {
                if (ComputerNameBox.Text != Environment.MachineName || ComputerNameBox.Text != "LOCALHOST")
                {
                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                    scope.Connect();
                    WqlObjectQuery wqlQuery0 =
                    new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name LIKE 'explorer.exe'");
                    ManagementObjectSearcher searcher0 =
                        new ManagementObjectSearcher(scope, wqlQuery0);
                    string user = "";
                    foreach (ManagementObject n in searcher0.Get())
                    {
                        string[] argList = new string[] { string.Empty, string.Empty };
                        int returnVal = Convert.ToInt32(n.InvokeMethod("GetOwner", argList));
                        if (returnVal == 0)
                        {
                            // return DOMAIN\user
                            user = argList[0].ToString();
                        }
                    }
                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                      getCurrentDomain().ToString());
                    UserPrincipal sid = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, user);
                    string usersid = sid.Sid.ToString();



                    string service = "Remote Registry";

                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);

                    foreach (ManagementObject n in searcher.Get())
                    {
                        ManagementBaseObject outParams = n.InvokeMethod("StartService", null, null);

                    }

                    string remoteName = ComputerNameBox.Text;
                    RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                    RegistryKey printers = environmentKey.OpenSubKey("Printers");
                    RegistryKey connections = printers.OpenSubKey("Connections");
                    string[] lists = connections.GetSubKeyNames();
                    Action output1 = () => OutputBox.AppendText("\r\nNetwork printers on remote computer:\r\n");
                    OutputBox.Invoke(output1);

                    foreach (string n in lists)
                    {
                        Action output2 = () => OutputBox.AppendText(@"" + n.Replace(",", @"\").ToString() + @"" + "\r\n");
                        OutputBox.Invoke(output2);
                    }
                    Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output3);

                    WqlObjectQuery wqlQuery2 =
                    new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                    ManagementObjectSearcher searcher2 =
                        new ManagementObjectSearcher(scope, wqlQuery2);

                    foreach (ManagementObject n in searcher2.Get())
                    {
                        ManagementBaseObject outParams = n.InvokeMethod("StopService", null, null);

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("You cannot run this against your local computer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                        
                        );
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    
                    );
            }

        }

        private void buttonPrinters_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_Printers;
                bw.RunWorkerAsync();
            
        }


        public class Win32_Product
        {
            public ushort? AssignmentType;
            public string Caption;
            public string Description;
            public string IdentifyingNumber;
            public string InstallDate;
            public DateTime? InstallDate2;
            public string InstallLocation;
            public short? InstallState;
            public string HelpLink;
            public string HelpTelephone;
            public string InstallSource;
            public string Language;
            public string LocalPackage;
            public string Name;
            public string PackageCache;
            public string PackageCode;
            public string PackageName;
            public string ProductID;
            public string RegOwner;
            public string RegCompany;
            public string SKUNumber;
            public string Transforms;
            public string URLInfoAbout;
            public string URLUpdateInfo;
            public string Vendor;
            public uint? WordCount;
            public string Version;
        }

        public string softwareName;
        public string uninstall;
        public void bw_Apps(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting applications list from remote computer...");
            OutputBox.Invoke(output);
            if (ComputerNameBox.Text != String.Empty)
            {
                try
                {
                    List<string> programs = new List<string>();

                    ConnectionOptions connectionOptions = new ConnectionOptions();

                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\CIMV2", connectionOptions);
                    scope.Connect();

                    string softwareRegLoc = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";

                    ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
                    ManagementBaseObject inParams = registry.GetMethodParameters("EnumKey");
                    inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
                    inParams["sSubKeyName"] = softwareRegLoc;


                    ManagementBaseObject inParams0 = registry.GetMethodParameters("EnumKey");
                    inParams0["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
                    inParams0["sSubKeyName"] = softwareRegLoc;

                    // Read Registry Key Names 
                    ManagementBaseObject outParams = registry.InvokeMethod("EnumKey", inParams, null);
                    string[] programGuids = outParams["sNames"] as string[];


                    ManagementBaseObject outParams0 = registry.InvokeMethod("EnumKey", inParams, null);


                    List<string> names = new List<string>();
                    List<string> uninstalllist = new List<string>();

                    foreach (string subKeyName in programGuids)
                    {
                        inParams = registry.GetMethodParameters("GetStringValue");
                        inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
                        inParams["sSubKeyName"] = softwareRegLoc + @"\" + subKeyName;
                        inParams["sValueName"] = "DisplayName";

                        inParams0 = registry.GetMethodParameters("GetStringValue");
                        inParams0["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
                        inParams0["sSubKeyName"] = softwareRegLoc + @"\" + subKeyName;
                        inParams0["sValueName"] = "UninstallString";

                        // Read Registry Value 
                        outParams = registry.InvokeMethod("GetStringValue", inParams, null);
                        outParams0 = registry.InvokeMethod("GetStringValue", inParams0, null);

                        if (outParams.Properties["sValue"].Value != null)
                        {

                            softwareName = outParams.Properties["sValue"].Value.ToString();
                            Action output1 = () => OutputBox.AppendText("\r\n" + "Software: \t\t" + softwareName);
                            OutputBox.Invoke(output1);
                        }
                        if (outParams0.Properties["sValue"].Value != null)
                        {

                            uninstall = outParams0.Properties["sValue"].Value.ToString();
                            Action output1 = () => OutputBox.AppendText("\r\n" + "UninstallString: \t" + uninstall + "\r\n");
                            OutputBox.Invoke(output1);
                        }

                    }




                    Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output3);
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void buttonApps_Click(object sender, EventArgs e)
        {
   
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_Apps;
                bw.RunWorkerAsync();
           
         }

        public System.IO.StreamReader streamReader;
        public bool hostButton;

        private void buttonHosts_Click(object sender, EventArgs e)
        {
            
                if (ComputerNameBox.Text != String.Empty)
                {
                    hostButton = true;
                    streamReader = new System.IO.StreamReader("\\\\" + ComputerNameBox.Text + "\\C$\\Windows\\System32\\drivers\\etc\\hosts");
                    string text = streamReader.ReadToEnd();
                    streamReader.Close();
                    streamReader.Dispose();
                    OutputBox.AppendText(text);
                }
            

        }


        private void buttonServices_Click(object sender, EventArgs e)
        {
                    sendtext = ComputerNameBox.Text;

                    The_Admin_Toolbox.Services frm = new The_Admin_Toolbox.Services();
                    frm.Show();
                
            
                        
                    
          
           
        }

        public void bw_NetDrives(object sender, DoWorkEventArgs e)
        {
            Action output = () => OutputBox.AppendText("Getting network drives list from remote computer...\r\n");
            OutputBox.Invoke(output);
            try
            {
                if (ComputerNameBox.Text != Environment.MachineName)
                {
                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                    scope.Connect();
                    WqlObjectQuery wqlQuery0 =
                    new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name LIKE 'explorer.exe'");
                    ManagementObjectSearcher searcher0 =
                        new ManagementObjectSearcher(scope, wqlQuery0);
                    string user = "";
                    foreach (ManagementObject n in searcher0.Get())
                    {
                        string[] argList = new string[] { string.Empty, string.Empty };
                        int returnVal = Convert.ToInt32(n.InvokeMethod("GetOwner", argList));
                        if (returnVal == 0)
                        {
                            // return DOMAIN\user
                            user = argList[0].ToString();
                        }
                    }
                    PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                      getCurrentDomain().ToString());
                    UserPrincipal sid = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, user);
                    string usersid = sid.Sid.ToString();



                    string service = "Remote Registry";

                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);

                    foreach (ManagementObject n in searcher.Get())
                    {
                        ManagementBaseObject outParams = n.InvokeMethod("StartService", null, null);

                    }

                    string remoteName = ComputerNameBox.Text;
                    RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                    RegistryKey connections = environmentKey.OpenSubKey("Network");
                    string[] lists = connections.GetSubKeyNames();
                    Action output1 = () => OutputBox.AppendText("Network drives on remote computer:");
                    OutputBox.Invoke(output1);

                    foreach (string n in lists)
                    {
                        Action output2 = () => OutputBox.AppendText("\r\n\r\nDrive:" + "" + n + "" + "\r\n" + connections.OpenSubKey(n).GetValue("RemotePath").ToString());
                        OutputBox.Invoke(output2);
                    }
                    Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output3);


                    WqlObjectQuery wqlQuery2 =
                    new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                    ManagementObjectSearcher searcher2 =
                        new ManagementObjectSearcher(scope, wqlQuery2);

                    foreach (ManagementObject n in searcher2.Get())
                    {
                        ManagementBaseObject outParams = n.InvokeMethod("StopService", null, null);

                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("You cannot run this against your local computer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning
                        
                        );
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error
                    
                    );
            }
        }


        private void buttonNetDrives_Click(object sender, EventArgs e)
        {
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_NetDrives;
                bw.RunWorkerAsync();
            
        }

        private void buttonGroupMem_Click(object sender, EventArgs e)
        {
            
                domain = getCurrentDomain().ToString();
                The_Admin_Toolbox.GroupMem frm = new The_Admin_Toolbox.GroupMem(this);
                frm.Show();
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
    
        }

        public WqlObjectQuery wqlQuery;


        public void bw_SCCMByComputer(object sender, EventArgs e)
        {


            if (ComputerNameBox.Text != String.Empty)
            {

                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();
                if (ComputerNameBox.Text.StartsWith("10."))
                {
                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                        int pos = hostEntry.HostName.IndexOf(".");
                        string compname = hostEntry.HostName.Remove(pos);
                        wqlQuery = new WqlObjectQuery("SELECT * from sms_r_system WHERE Name='" + compname + "'");
                    }
                    catch (SystemException err)
                    {
                        System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    compname = ComputerNameBox.Text;
                    wqlQuery = new WqlObjectQuery("SELECT * from sms_r_system WHERE Name='" + ComputerNameBox.Text + "'");
                }
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    Action output = () => OutputBox.AppendText("Last User Logged Onto " + ComputerNameBox.Text + ": " + n["LastLogonUserName"] + "\r\n");
                    createComputerHistory(compname, "GGLOBAL\\" + n["LastLogonUserName"].ToString());
                    OutputBox.Invoke(output);
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }

        }


        private void SCCMByComputerButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_SCCMByComputer;
                bw.RunWorkerAsync();
            
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }

        public void bw_SCCMByUser(object sender, EventArgs e)
        {

            if (SCCMUserBox.Text != String.Empty)
            {
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                     getCurrentDomain().ToString());

                //Create a "user object" in the context
                UserPrincipal user = new UserPrincipal(domainContext);

                string adtext = SCCMUserBox.Text;

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
                try
                {
                    PrincipalSearchResult<Principal> results = pS.FindAll();

                    //If necessary, request more details
                    Principal pc = results.ToList()[0];
                    DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();

                    //Output first result of the test

                    //Gets SamAcctName
                    string sam = pc.SamAccountName.ToString();

                    ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                    scope.Connect();
                    WqlObjectQuery wqlQuery =
                    new WqlObjectQuery("SELECT * from sms_r_system WHERE LastLogonUserName='" + sam + "'");
                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
                    foreach (ManagementObject n in searcher.Get())
                    {
                        Action output = () => OutputBox.AppendText("\r\n" + SCCMUserBox.Text + " Last Logged Onto: " + n["Name"] + "\r\n");
                        createComputerHistory(n["Name"].ToString(),"GGLOBAL\\" + SCCMUserBox.Text);
                        OutputBox.Invoke(output);
                    }
                    Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output3);

                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SCCMByUserButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_SCCMByUser;
                bw.RunWorkerAsync();
            
        }

        private void SCCMUserBox_TextChanged(object sender, EventArgs e)
        {
            this.SCCMByUserButton.Enabled = !string.IsNullOrWhiteSpace(this.SCCMUserBox.Text);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        string n;
        string orig;
        OpenFileDialog op;
        FolderBrowserDialog fo;
        bool folderbuttonClick = false;
        bool filebuttonClick = false;
        private void browButoon_Click(object sender, EventArgs e)
        {
            filebuttonClick = true;
            op = new OpenFileDialog();
            if (op.ShowDialog() == DialogResult.OK)
            {
                string t = fromtextBox.Text;
                t = op.FileName;
                FileInfo fi = new FileInfo(fromtextBox.Text = op.FileName);
                n = fi.Name + "." + fi.Length;
                orig = "\\\\" + ComputerNameBox.Text + @"\C$\" + op.SafeFileName;
                totextBox.Text = orig;

            }
        }



        public void folderCopy(object sender, DoWorkEventArgs e)
        {
            FileSystem.CopyDirectory(fromtextBox.Text, totextBox.Text, UIOption.AllDialogs);
        }


        private void fileCopy(object sender, DoWorkEventArgs e)
        {

            FileSystem.CopyFile(fromtextBox.Text, totextBox.Text, UIOption.AllDialogs);
        }


        private void sendButton_Click(object sender, EventArgs e)
        {
           
                if (filebuttonClick == true)
                {
                    try
                    {
                        filebuttonClick = false;

                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += fileCopy;
                        //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
                        bw.RunWorkerAsync();
                        OutputBox.AppendText("\r\n");
                    }
                    catch (SystemException err)
                    {
                        System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (folderbuttonClick == true)
                {
                    try
                    {
                        folderbuttonClick = false;

                        BackgroundWorker bw = new BackgroundWorker();
                        bw.DoWork += folderCopy;
                        //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
                        bw.RunWorkerAsync();
                        OutputBox.AppendText("\r\n");

                    }
                    catch (SystemException err)

                    {
                        System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
            }
        }

        private void browFolder_Click(object sender, EventArgs e)
        {
            folderbuttonClick = true;
            fo = new FolderBrowserDialog();
            if (fo.ShowDialog() == DialogResult.OK)
            {

                fromtextBox.Text = fo.SelectedPath;
                string t = fo.SelectedPath;
                int pos = t.LastIndexOf(@"\") + 1;
                string foldername = t.Substring(pos, t.Length - pos);
                orig = "\\\\" + ComputerNameBox.Text + @"\C$\" + foldername;
                totextBox.Text = orig;

            }
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.OutputBox.AppendText("\r\n");
            this.OutputBox.AppendText("Done!");
        }

        private void microsoftConfigManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                Process.Start(@"C:\Program Files (x86)\Microsoft Configuration Manager\AdminConsole\bin\Microsoft.ConfigurationManagement.exe");
            
        }

        private void CompGrpbutton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> groupsort = new List<string>();
                string domain = getCurrentDomain();
                int i = domain.IndexOf(".");
                domain = i < 0 ? domain : domain.Substring(0, i);
                MessageBox.Show(domain);
                groupsort = EnumerateOU("CN=Computers,DC="+ domain +",DC=gg,DC=group");


                OutputBox.AppendText("Computers in Computers group in " + domain + " domain");
                OutputBox.AppendText("\r\n");
                OutputBox.AppendText("#####################");
                groupsort.Sort();
                foreach (string name in groupsort)
                {
                    OutputBox.AppendText("\r\n");
                    OutputBox.AppendText(name);
                }               
                OutputBox.AppendText("\r\n");
                OutputBox.AppendText("#####################");
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void remoteDesktopConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                Process.Start(@"C:\Windows\system32\mstsc.exe");
            
        }

        public void bw_Name2IP(object sender, EventArgs e)
        {
            if (ComputerNameBox.Text != String.Empty)
            {
                try
                {
                    IPHostEntry hostEntry;

                    hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);

                    //you might get more than one ip for a hostname since 
                    //DNS supports more than one record

                    if (hostEntry.AddressList.Length > 0)
                    {
                        var ip = hostEntry.AddressList[0];
                        Action output = () => ComputerNameBox.Text = ip.MapToIPv4().ToString();
                        OutputBox.Invoke(output);

                    }
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Name2IP_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Name2IP;
            bw.RunWorkerAsync();
        }
        string compname;
        public void bw_IP2Name(object sender, EventArgs e)
        {


            if (ComputerNameBox.Text != String.Empty)
            {
                try
                {
                    IPHostEntry hostEntry;

                    hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                    int pos = hostEntry.HostName.ToString().IndexOf(".");
                    compname = hostEntry.HostName.ToString().Remove(pos);
                    Action output = () => ComputerNameBox.Text = compname;
                    OutputBox.Invoke(output);
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void IP2Name_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_IP2Name;
            bw.RunWorkerAsync();

        }

        public void bw_getdrivers(object sender, EventArgs e)
        {

            Process p = new Process();
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
            p.StartInfo.Arguments = "/c driverquery /s " + ComputerNameBox.Text.ToLower().ToString();
            p.Start();
            Action output0 = () => OutputBox.AppendText("Getting device drivers...please wait...");
            OutputBox.Invoke(output0);
            var reader = p.StandardOutput;
            while (!reader.EndOfStream)
            {
                // the point is that the stream does not end until the process has 
                // finished all of its output.
                var nextLine = reader.ReadLine();
                Action output1 = () => OutputBox.AppendText(nextLine.ToString() + "\r\n");
                OutputBox.Invoke(output1);
            }
            Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
            OutputBox.Invoke(output3);
        }



        private void driversButton_Click(object sender, EventArgs e)
        {
           
                try
                {
                    BackgroundWorker bw = new BackgroundWorker();
                    bw.DoWork += bw_getdrivers;
                    bw.RunWorkerAsync();
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
            
        }

        public void bw_getIPs(object sender, EventArgs e)
        {
            ManagementScope scope;
            try
            {
                scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                scope.Connect();
            }
            catch (SystemException) { return; }
            if (scope.IsConnected == true)
            {
                WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 'TRUE'");
                ManagementObjectSearcher searcher0 =
                    new ManagementObjectSearcher(scope, wqlQuery0);

                foreach (ManagementObject n in searcher0.Get())
                {
                    DateTime dhcp = ManagementDateTimeConverter.ToDateTime(n["DHCPLeaseObtained"].ToString());
                    string[] addresses = (string[])n["IPAddress"];
                    string[] gateways = (string[])n["DefaultIPGateway"];
                    var ipsnadgates = addresses.Zip(gateways, (first, second) => "IPAddress: " + first + "\r\nGateway: " + second);

                    foreach (var item in ipsnadgates)
                    {
                        Action output = () => OutputBox.AppendText("\r\n" + item.ToString() + "\r\n" + "Description: " + n["Description"].ToString() + "\r\n" + "DNSHostName: " + n["DNSHostName"] + "\r\n" + "DHCPLeaseObtained: " + dhcp.ToString() + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
            }
            else
            {
                try
                {
                    IPHostEntry hostEntry;

                    hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);

                    //you might get more than one ip for a hostname since 
                    //DNS supports more than one record

                    if (hostEntry.AddressList.Length > 0)
                    {
                        Action output0 = () => OutputBox.AppendText("List of IP addresses of computer: \r\n");
                        OutputBox.Invoke(output0);

                        for (int i = 0; i < hostEntry.AddressList.Length; i++)
                        {
                            Action output1 = () => OutputBox.AppendText(hostEntry.AddressList[i].ToString() + "\r\n");
                            OutputBox.Invoke(output1);


                        }
                        Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                        OutputBox.Invoke(output3);
                    }
                    else
                    {
                        Action output2 = () => OutputBox.AppendText(hostEntry.AddressList[0].ToString());
                        OutputBox.Invoke(output2);

                    }
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void getIPsbutton_Click(object sender, EventArgs e)
        {
            
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getIPs;
                bw.RunWorkerAsync();
            
        }

        public void bw_installChoco(object sender, EventArgs e)
        {
            try
            {
                bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
                if (exists == false)
                {
                    FileSystem.CopyFile(Properties.Settings.Default.ChocoServerPath, "\\\\" + ComputerNameBox.Text + "\\C$\\chocolateyinstall.bat");
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + ComputerNameBox.Text.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c C:\chocolateyinstall.bat";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    FileSystem.DeleteFile("\\\\" + ComputerNameBox.Text + "\\C$\\chocolateyinstall.bat");
                    if (Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey"))
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey is now installed", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (exists == true)
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey is already installed", "Chocolately already exists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void installChocoButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installChoco;
                bw.RunWorkerAsync();
           
        }

      
        
        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download completed!");
        }
        private void installChromeButton_Click(object sender, EventArgs e)
        {
            
                string comp = ComputerNameBox.Text;
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install googlechrome -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Google\\Chrome"))
                    {
                        System.Windows.Forms.MessageBox.Show("Chrome is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information );
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Chrome is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
        }

        private void installFireFoxButton_Click(object sender, EventArgs e)
        {

               
                string comp = ComputerNameBox.Text;
                try
                {
                    bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                    if (exists == true)
                    {
                        Process process = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "cmd.exe";
                        psi.UseShellExecute = false;
                        string path = "\\\\" + comp.ToLower();
                        string hh = string.Format("\"{0}\"", path);
                        psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install firefox -y";
                        process.StartInfo = psi;
                        process.Start();
                        process.WaitForExit();
                        if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Mozilla Firefox"))
                        {
                            System.Windows.Forms.MessageBox.Show("Firefox is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Firefox is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
   
          
        }

        private void installiTunesButton_Click(object sender, EventArgs e)
        {
           
                string comp = ComputerNameBox.Text;
                try
                {
                    bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                    if (exists == true)
                    {
                        Process process = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "cmd.exe";
                        psi.UseShellExecute = false;
                        string path = "\\\\" + comp.ToLower();
                        string hh = string.Format("\"{0}\"", path);
                        psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install itunes -y";
                        process.StartInfo = psi;
                        process.Start();
                        process.WaitForExit();
                        if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\iTunes"))
                        {
                            System.Windows.Forms.MessageBox.Show("iTunes is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("iTunes is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void installSkypeButton_Click(object sender, EventArgs e)
        {
           
                string comp = ComputerNameBox.Text;
                try
                {
                    bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                    if (exists == true)
                    {
                        Process process = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "cmd.exe";
                        psi.UseShellExecute = false;
                        string path = "\\\\" + ComputerNameBox.Text.ToLower();
                        string hh = string.Format("\"{0}\"", path);
                        psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install skype -y";
                        process.StartInfo = psi;
                        process.Start();
                        process.WaitForExit();
                        if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Skype"))
                        {
                            System.Windows.Forms.MessageBox.Show("Skype is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Skype is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        public void bw_installAcroPro(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                if (!Directory.Exists("\\\\" + comp + "\\C$\\Adobe Acrobat 2017"))
                {
                    FileSystem.CopyDirectory(Properties.Settings.Default.AcroProPath + @"\Adobe Acrobat 2017", "\\\\" + comp + "\\C$\\Adobe Acrobat 2017", UIOption.AllDialogs);
                }
                Thread.Sleep(5000);
                FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\Powershell Software Install\Adobe Config\Adobe Pro\acrobat2017installcmd.bat", "\\\\" + comp + "\\C$\\Adobe Acrobat 2017\\acrobat2017installcmd.bat", UIOption.AllDialogs);

                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + comp.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c ""C:\Adobe Acrobat 2017\acrobat2017installcmd.bat""";
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();

                if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\Adobe\Acrobat Reader DC\"))
                {
                    Process proc = new Process();
                    ProcessStartInfo uninstall = new ProcessStartInfo();
                    uninstall.FileName = "cmd.exe";
                    uninstall.UseShellExecute = false;
                    uninstall.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c MsiExec.exe /x{AC76BA86-7AD7-1033-7B44-AC0F074E4100} /qn";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                }

               // FileSystem.DeleteDirectory("\\\\" + comp + "\\C$\\Adobe Acrobat 2017", DeleteDirectoryOption.DeleteAllContents);
                Thread.Sleep(20000);
                if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\Adobe\Acrobat 2017\"))
                {
                    
                    System.Windows.Forms.MessageBox.Show("Adobe Acrobat Professional 2017 is now installed", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Adobe Acrobat Professional 2017 is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void installAcroPrBbutton_Click(object sender, EventArgs e)
        {
           
            
                BackgroundWorker bw = new BackgroundWorker();
                BackgroundWorker bgw = new BackgroundWorker();
                bw.DoWork += bw_installAcroPro;
                bgw.DoWork += progressLoad;
                bw.RunWorkerAsync();
                Thread.Sleep(1000);
                bgw.RunWorkerAsync(bw);
            
        }

        public void bw_installAcroStd(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                if (!Directory.Exists("\\\\" + comp + "\\C$\\Adobe Acrobat 2017"))
                {
                    FileSystem.CopyDirectory(Properties.Settings.Default.AcroStdPath + @"\Adobe Acrobat 2017", "\\\\" + comp + "\\C$\\Adobe Acrobat 2017", UIOption.AllDialogs);
                }
                Thread.Sleep(5000);
                FileSystem.CopyFile(Properties.Settings.Default.AcroStdPath + @"\acrobat2017installcmd.bat", "\\\\" + comp + "\\C$\\Adobe Acrobat 2017\\acrobat2017installcmd.bat", UIOption.AllDialogs);
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + comp.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c ""C:\Adobe Acrobat 2017\acrobat2017installcmd.bat""";
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\Adobe\Acrobat Reader DC\"))
                {
                    Process proc = new Process();
                    ProcessStartInfo uninstall = new ProcessStartInfo();
                    uninstall.FileName = "cmd.exe";
                    uninstall.UseShellExecute = false;
                    uninstall.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c MsiExec.exe /x{AC76BA86-7AD7-1033-7B44-AC0F074E4100} /qn";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                }
                Thread.Sleep(20000);
                //FileSystem.DeleteDirectory("\\\\" + comp + "\\C$\\Adobe Acrobat Standard 2017", DeleteDirectoryOption.DeleteAllContents);
                if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\Adobe\Acrobat 2017\"))
                {
                    
                    System.Windows.Forms.MessageBox.Show("Adobe Acrobat Standard 2017 is now installed", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Adobe Acrobat Standard 2017 is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void installAcroStdButton_Click(object sender, EventArgs e)
        {
                BackgroundWorker bw = new BackgroundWorker();
                BackgroundWorker bgw = new BackgroundWorker();
                bw.DoWork += bw_installAcroStd;
                bgw.DoWork += progressLoad;
                bw.RunWorkerAsync();
                Thread.Sleep(1000);
                bgw.RunWorkerAsync(bw);
            
        }



        private void installFlashButton_Click(object sender, EventArgs e)
        {
            
                        string comp = ComputerNameBox.Text;
                        try
                        {
                            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
                            if (exists == true)
                            {
                                Process process = new Process();
                                ProcessStartInfo psi = new ProcessStartInfo();
                                psi.FileName = "cmd.exe";
                                psi.UseShellExecute = false;
                                string path = "\\\\" + ComputerNameBox.Text.ToLower();
                                string hh = string.Format("\"{0}\"", path);
                                psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install flashplayeractivex -y";
                                process.StartInfo = psi;
                                process.Start();
                                process.WaitForExit();
                                if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Adobe\\Flash Player"))
                                {
                                    System.Windows.Forms.MessageBox.Show("Flash Player for IE is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Flash Player for IE is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception err)
                        {
                            System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
           
        }

        private void installDropBoxButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install dropbox -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Dropbox"))
                    {
                        System.Windows.Forms.MessageBox.Show("Drop Box is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Drop Box is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void installPicasaButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install picasa -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Google\\Picasa3"))
                    {
                        System.Windows.Forms.MessageBox.Show("Picasa is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Picasa is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void installMBAMButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install malwarebytes -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Malwarebytes Anti-Malware"))
                    {
                        System.Windows.Forms.MessageBox.Show("Malwarebytes is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Malwarebytes is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void installEyemaxButton_Click(object sender, EventArgs e)
        {
            
                string comp = ComputerNameBox.Text;
                try
                {
                    FileSystem.CopyDirectory(Properties.Settings.Default.EyeMaxPath, "\\\\" + comp + "\\C$\\networkdvr", UIOption.AllDialogs);
                    FileSystem.CopyFile("\\\\" + comp + "\\C$\\networkdvr\\EyeMax DVR Software.lnk", "\\\\" + comp + "\\C$\\Users\\Public\\Desktop\\EyeMax DVR Software.lnk", UIOption.AllDialogs);
                    MessageBox.Show("Eyemax Software has been copied to user's computer and a shortcut was added to their desktop.", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }
        public void bw_installSmartView(object sender, EventArgs e)
        {

           
                string comp = ComputerNameBox.Text;
                try
                {
                string chocoPath = Properties.Settings.Default.EyeMaxPath;
                    FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$", UIOption.AllDialogs);
                    var button = sender as Button;
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + ComputerNameBox.Text.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", button.Text, chocoPath);
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\SmartView", DeleteDirectoryOption.DeleteAllContents);
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Oracle\\SmartView"))
                    {
                        System.Windows.Forms.MessageBox.Show("Smart View is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Smart View is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }            
        }


        private void installSmartViewButton_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installSmartView;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey needs to be installed first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void addtoGroupButton_Click(object sender, EventArgs e)
        {
           
                sendtext = ADIdentityBox.Text;
                domain = this.getCurrentDomain();
                The_Admin_Toolbox.AddGroup frm = new The_Admin_Toolbox.AddGroup();
                frm.Show();
           
        }

        private void MoveUserbutton_Click(object sender, EventArgs e)
        {
           
                sendtext = ADIdentityBox.Text;
                The_Admin_Toolbox.MoveUser frm = new The_Admin_Toolbox.MoveUser();
                frm.Show();
           
        }

        private void installFileZillaButton_Click_1(object sender, EventArgs e)
        {
            
                string comp = ComputerNameBox.Text;
                try
                {
                    bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                    if (exists == true)
                    {
                        Process process = new Process();
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.FileName = "cmd.exe";
                        psi.UseShellExecute = false;
                        string path = "\\\\" + comp.ToLower();
                        string hh = string.Format("\"{0}\"", path);
                        psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install filezilla -y";
                        process.StartInfo = psi;
                        process.Start();
                        process.WaitForExit();
                        if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files\\FileZilla FTP Client"))
                        {
                            System.Windows.Forms.MessageBox.Show("FileZilla is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("FileZilla is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception err) { MessageBox.Show(err.Message); }
        }


        public void bw_findSerial(object sender, EventArgs e)
        {
            ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
            scope.Connect();

            wqlQuery = new WqlObjectQuery("select SMS_R_System.NetbiosName, SMS_G_System_PC_BIOS.SerialNumber from SMS_R_System inner join SMS_G_System_PC_BIOS on SMS_G_System_PC_BIOS.ResourceID = SMS_R_System.ResourceId where SMS_G_System_PC_BIOS.SerialNumber ='" + SerialtextBox.Text + "'");

            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            foreach (ManagementObject n in searcher.Get())
            {
                ManagementBaseObject Descriptor = ((ManagementBaseObject)(n.Properties["SMS_R_System"].Value));

                Action output = () => OutputBox.AppendText("Matching Computer Name: " + Descriptor.GetPropertyValue("NetbiosName") + "\r\n");
                OutputBox.Invoke(output);
            }
            Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
            OutputBox.Invoke(output3);
        }

        public void bw_findByIP(object sender, EventArgs e)
        {
            ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
            scope.Connect();

            wqlQuery = new WqlObjectQuery("select SMS_R_System.NetbiosName, SMS_R_System.LastLogonUserName, SMS_R_System.IPAddresses, SMS_G_System_PC_BIOS.SerialNumber from SMS_R_System inner join SMS_G_System_PC_BIOS on SMS_G_System_PC_BIOS.ResourceID = SMS_R_System.ResourceId where SMS_R_System.IPAddresses ='" + SerialtextBox.Text + "'");

            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);

            ManagementObjectCollection coll = searcher.Get();

            ManagementObject firstobj = coll.OfType<ManagementObject>().First();
            ManagementBaseObject sys = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_R_System");

            Regex regex = new Regex("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$");
            foreach (ManagementObject n in searcher.Get())
            {
                ManagementBaseObject Descriptor = ((ManagementBaseObject)(n.Properties["SMS_R_System"].Value));
                foreach (string s in (String[])Descriptor.GetPropertyValue("IPAddresses"))
                {
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        var txt = (string)SerialtextBox.Invoke(new Func<string>(() => SerialtextBox.Text));
                        if (s == txt)
                        {
                            Action output = () => OutputBox.AppendText("Name: " + Descriptor.GetPropertyValue("NetbiosName") + "\r\n" + "Serial Number: " + Descriptor.GetPropertyValue("SerialNumber") + "\r\n" + "Last Logged In User: " + Descriptor.GetPropertyValue("LastLogonUserName") + "\r\n" + "\r\n");
                            OutputBox.Invoke(output);
                        }
                    }

                }
            }
            Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
            OutputBox.Invoke(output3);
        }




            private void SCCMSerialbutton_Click(object sender, EventArgs e)
        {
            
                try
                {
                    if (SerialtextBox.Text != String.Empty && !(Regex.Match(SerialtextBox.Text, "(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$").Success))
                    {

                        BackgroundWorker bw1 = new BackgroundWorker();
                        bw1.DoWork += bw_findSerial;
                        bw1.RunWorkerAsync();                     
                    }
                    else
                    {

                        BackgroundWorker bw2 = new BackgroundWorker();
                        bw2.DoWork += bw_findByIP;
                        bw2.RunWorkerAsync();                        
                    }

                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
           
        }

        private void requireRebootButton_Click(object sender, EventArgs e)
        {
           

                    OutputBox.AppendText("Computers requiring reboot: \r\n");
                    ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                    scope.Connect();

                    wqlQuery = new WqlObjectQuery("select SMS_R_SYSTEM.ResourceID, SMS_R_SYSTEM.ResourceType, SMS_R_SYSTEM.Name, SMS_R_SYSTEM.SMSUniqueIdentifier, SMS_R_SYSTEM.ResourceDomainORWorkgroup, SMS_R_SYSTEM.Client from sms_r_system AS sms_r_system inner join SMS_UpdateComplianceStatus as c on c.machineid=sms_r_system.resourceid where c.LastEnforcementMessageID = 9");

                    ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher(scope, wqlQuery);
                    foreach (ManagementObject n in searcher.Get())
                    {
                        Action output = () => OutputBox.AppendText(n["Name"] + "\r\n");
                        OutputBox.Invoke(output);
                    }
                    Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output3);
                
           

        }

        private void installJavaButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install jre8 -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Java\\jre1.8.0_51"))
                    {
                        System.Windows.Forms.MessageBox.Show("Java 8 Update 51 is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Java 8 Update 51 is not installed. Please check computer to make sure.", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void installSpotifyButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install spotify -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Spotify"))
                    {
                        System.Windows.Forms.MessageBox.Show("Spotify is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Spotify is not installed.", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        private void installDriveButton_Click(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                bool exists = Directory.Exists("\\\\" + comp + "\\C$\\ProgramData\\chocolatey");
                if (exists == true)
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + comp.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c choco install googledrive -y";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\Google\\Drive"))
                    {
                        System.Windows.Forms.MessageBox.Show("Google Drive is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Google Drive is not installed.", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Chocolatey needs to be run first before trying to install any programs.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err) { MessageBox.Show(err.Message); }
        }

        public void bw_getT420(object sender, DoWorkEventArgs e)
        {

            try
            {
                List<string> list = new List<string>();
                Action outputt = () => OutputBox.AppendText("T420 computers in the domain: \r\n################################\r\n");
                OutputBox.Invoke(outputt);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = 'ThinkPad T420' and SMS_R_System.SystemOUName = ''");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getT420Button_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getT420;
                bw.RunWorkerAsync();
         }
       

        public void bw_getT410(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                Action outputt = () => OutputBox.AppendText("T410 computers in the domain: \r\n################################\r\n");
                OutputBox.Invoke(outputt);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = 'ThinkPad T410' and SMS_R_System.SystemOUName = ''");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void getT410Button_Click(object sender, EventArgs e)
        {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getT410;
                bw.RunWorkerAsync();
            
        }

        private void getT420sButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                OutputBox.AppendText("T420s computers in the  domain: \r\n################################\r\n");
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = 'ThinkPad T420s' and SMS_R_System.SystemOUName = ''");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getT400Button_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                OutputBox.AppendText("T400 computers in the  : \r\n################################\r\n");
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = 'ThinkPad T400' and SMS_R_System.SystemOUName = ''");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void bw_installexacq(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                FileSystem.CopyFile(Properties.Settings.Default.exacqVisionPath, "\\\\" + comp + "\\C$\\exacqVisionClient_6_2_5.exe", UIOption.AllDialogs);
                FileSystem.CopyFile(Properties.Settings.Default.exacqVisionPathBat, "\\\\" + comp + "\\C$\\exacqVision.bat", UIOption.AllDialogs);
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + ComputerNameBox.Text.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd /c C:\exacqVisionClient.bat";
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\exacqVisionClient_6_2_5.exe");
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\exacqVisionClient.bat");
                if (Directory.Exists("\\\\" + comp + "\\C$\\Program Files (x86)\\exacqVision"))
                {
                    System.Windows.Forms.MessageBox.Show("exacqVision is now installed.", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("exacqVision is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void exacqVisionButton_Click(object sender, EventArgs e)
        {
          
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installexacq;
                bw.RunWorkerAsync();
            

        }

        
        public void bw_getSoftware(object sender, EventArgs e)
        {
            try
            {
                Action output0 = () => OutputBox.AppendText("Getting installed software for " + ComputerNameBox.Text + ": \r\n################################\r\n");
                OutputBox.Invoke(output0);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_G_System_INSTALLED_SOFTWARE.ProductName, SMS_G_System_INSTALLED_SOFTWARE.UninstallString, SMS_G_System_INSTALLED_SOFTWARE.ProductVersion, SMS_G_System_INSTALLED_SOFTWARE.InstalledLocation, SMS_G_System_INSTALLED_SOFTWARE.InstallDate from  SMS_R_System inner join SMS_G_System_INSTALLED_SOFTWARE on SMS_G_System_INSTALLED_SOFTWARE.ResourceID = SMS_R_System.ResourceId where SMS_R_System.Name = '" + ComputerNameBox.Text + "'");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    if (!(object.ReferenceEquals(null, n["InstallDate"])))
                    {
                        string lastlogon = n["InstallDate"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                        Action output = () => OutputBox.AppendText("Product Name: " + n["ProductName"] + "\r\n" + "Version: " + n["ProductVersion"] + "\r\n" + "Uninstall String: " + n["UninstallString"] + "\r\n" + "Install Date: " + final.ToString() + "\r\n" + "Install Location: " + n["InstalledLocation"] + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getInstalledSoftButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getSoftware;
                bw.RunWorkerAsync();
           
        }

        private void addtoCollectionButton_Click(object sender, EventArgs e)
        {
   
            try
            {
                AddUsersToCollectionMembership("GG300061", ComputerNameBox.Text);
                Action output3 = () => OutputBox.AppendText(ComputerNameBox.Text + " was added to W7-US-IAD1 collection!");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
              
        }
        public void AddUsersToCollectionMembership(string sccmCollectionID, string CompName)
        {

            SmsNamedValuesDictionary namedValues = new SmsNamedValuesDictionary();
            WqlConnectionManager connectionManager = new WqlConnectionManager(namedValues);

            if (string.IsNullOrWhiteSpace(sccmCollectionID) || CompName == null)
                throw new ArgumentException("The parameter collectionID value cannot be null or empty or whitespace.");

            try
            {
                connectionManager.Connect(SCCMServer);
                WqlResultObject collection = (WqlResultObject)connectionManager.GetInstance("SMS_Collection.CollectionID='" + sccmCollectionID + "'");


                string query = "select * from SMS_R_System where Name = '" + CompName + "'";

                //Validate the query before adding the query to SCCM collection
                var validateQueryParam = new Dictionary<string, object>();
                validateQueryParam.Add("WQLQuery", query);
                IResultObject validationResult = connectionManager.ExecuteMethod("SMS_CollectionRuleQuery", "ValidateQuery", validateQueryParam);

                if (validationResult["ReturnValue"].BooleanValue == true)
                {
                    //Create collection rule query instance
                    IResultObject rule = connectionManager.CreateInstance("SMS_CollectionRuleQuery");
                    rule["QueryExpression"].StringValue = query;
                    rule["RuleName"].StringValue = "Members of collection " + sccmCollectionID; //The rule name will reflect in the CM Console

                    //Add the rule into a parameter object
                    var membershipRuleParam = new Dictionary<string, object>();
                    membershipRuleParam.Add("collectionRule", rule);

                    //Add new rule to SCCM collection
                    IResultObject addResult = collection.ExecuteMethod("AddMembershipRule", membershipRuleParam);

                    //NOTE: The added rule will have an ID return. You need to store it somewhere, e.g: database
                    //You need this query ID to delete this rule later
                    int sccmQueryID = addResult["QueryID"].IntegerValue;


                    if (addResult["ReturnValue"].IntegerValue != 0)
                    {
                        Debug.WriteLine("Failed to add membership rule to SCCM Collection.");
                        throw new ApplicationException("Failed to add membership rule to SCCM Collection.");
                    }

                    //Refresh the SCCM collection membership
                    Dictionary<string, object> requestRefreshParameters = new Dictionary<string, object>();
                    requestRefreshParameters.Add("IncludeSubCollections", false);
                    collection.ExecuteMethod("RequestRefresh", requestRefreshParameters);
                }
                else
                {
                    Debug.WriteLine(string.Format("Invalid WQL query: ", query));
                    throw new ApplicationException(string.Format("Invalid WQL query: ", query));
                }
            }
            catch (SmsException smsEx)
            {
                Debug.WriteLine("Failed to run queries. Error: " + smsEx.Details);
                throw;
            }
            catch (UnauthorizedAccessException accessEx)
            {
                Debug.WriteLine("Failed to authenticate. Error:" + accessEx.Message);
                throw;
            }
            finally
            {
                connectionManager.Close();
                connectionManager.Dispose();
            }

        }



        private void OutputBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {

            string user = Environment.UserName;
            string domain = Environment.UserDomainName;
            var result = user.Substring(user.LastIndexOf('_') + 1);
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            psi.Arguments = @"/c runas /user:"+ getCurrentDomain() + @"\" + result + @" ""explorer /separate, " + e.LinkText + "";
            process.StartInfo = psi;
            process.Start();
           

        }

        public void bw_getM83(object sender, DoWorkEventArgs e)
        {
            try
            {
                List<string> list = new List<string>();
                Action outputt = () => OutputBox.AppendText("M83 computers in the domain: \r\n################################\r\n");
                OutputBox.Invoke(outputt);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = 'ThinkCentre M83' and SMS_R_System.SystemOUName = ''");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void wmiFixButton_Click(object sender, EventArgs e)
        {

            if (!(String.IsNullOrEmpty(ComputerNameBox.Text)))
            {
                try
                {
                    Process process = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo();
                    psi.FileName = "cmd.exe";
                    psi.UseShellExecute = false;
                    string path = "\\\\" + ComputerNameBox.Text.ToLower();
                    string hh = string.Format("\"{0}\"", path);
                    psi.Arguments = @"/c psexec -accepteula -s " + hh + @" -h cmd / c netsh advfirewall firewall set rule group=""windows management instrumentation (wmi)"" new enable =yes && netsh advfirewall firewall set service RemoteAdmin enable";
                    process.StartInfo = psi;
                    process.Start();
                    process.WaitForExit();
                    string test = ComputerInfo.GetModel(ComputerNameBox.Text);
                    if (test != null)
                        MessageBox.Show("WMI is now enabled.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }

            }
        }

        private void searchADCompButton_Click(object sender, EventArgs e)
        {
            

                try
                {
                    string comp = ComputerNameBox.Text;
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString()))
                    {
                        ComputerPrincipal computer = new ComputerPrincipal(ctx);
                        computer.Name = comp;
                        PrincipalSearcher srch = new PrincipalSearcher(computer);
                        OutputBox.AppendText("\r\nMatches found: \r\n");
                        foreach (var found in srch.FindAll())
                        {
                            OutputBox.AppendText(found.DistinguishedName + "\r\n");
                        }
                        OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    }
                }
                catch (SystemException err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
          
        }

        private void searchADUserButton_Click(object sender, EventArgs e)
        {
            
                try
                {
                    string user = ADIdentityBox.Text;
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString(), ""))
                    {
                        UserPrincipal usr = new UserPrincipal(ctx);
                        usr.DisplayName = user;


                        PrincipalSearcher srch = new PrincipalSearcher(usr);
                        PrincipalSearchResult<Principal> results = srch.FindAll();


                        //If necessary, request more details

                        int selectedindex = 0;
                        OutputBox.AppendText("\r\nMatches found: \r\n\r\n");
                        foreach (var found in srch.FindAll())
                        {

                            Principal pc = results.ToList()[selectedindex];
                            selectedindex++;
                            DirectoryEntry de = (DirectoryEntry)pc.GetUnderlyingObject();
                            OutputBox.AppendText("Username: " + found.SamAccountName + "\r\nDN: " + found.DistinguishedName + "\r\n" + "DisplayName: " + found.DisplayName + "\r\n" + "LockedOut: " + usr.IsAccountLockedOut().ToString() + "\r\n");
                            if (!(object.ReferenceEquals(null, de.Properties["mail"].Value)))
                            {
                                OutputBox.AppendText("Mail: " + de.Properties["mail"].Value.ToString() + "\r\n\r\n");
                            }
                            else { OutputBox.AppendText("\r\n"); }
                        }
                        OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    }
                }
                catch (SystemException err)
                {
                    MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
           
        }

        private void getPathAccessTextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(getPathAccessTextBox.Text))
            {
                getAccessButton.Enabled = false;
                largestFilesBbutton.Enabled = false;
            }
            else
            {
                getAccessButton.Enabled = true;
                largestFilesBbutton.Enabled = true;
            }
        }

        public void bw_getAccess(object sender, DoWorkEventArgs e)
        {
            try
            {
                string path = getPathAccessTextBox.Text;
                FileAttributes attr = File.GetAttributes(path);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    DirectoryInfo di = new DirectoryInfo(path);
                    DirectorySecurity acl = di.GetAccessControl(AccessControlSections.All);
                    AuthorizationRuleCollection rules = acl.GetAccessRules(true, true, typeof(NTAccount));

                    Action output = () => OutputBox.AppendText("Members with access to " + "<" + path.ToString() + ">" + "\r\n\r\n");
                    OutputBox.Invoke(output);
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString()))
                    {
                        GroupPrincipal grpperm = new GroupPrincipal(ctx);
                        List<GroupPrincipal> collectgroups = new List<GroupPrincipal>();


                        foreach (AuthorizationRule rule in rules)
                        {
                            Action output1 = () => OutputBox.AppendText(rule.IdentityReference.Value.ToString() + "\r\n");
                            OutputBox.Invoke(output1);
                            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, rule.IdentityReference.Value);
                            if (grp != null)
                            {
                                collectgroups.Add(grp);
                            }
                        }
                        Action output2 = () => OutputBox.AppendText("\r\n");
                        OutputBox.Invoke(output2);

                        foreach (GroupPrincipal grpname in collectgroups)
                        {
                            if (grpname.Name != "Administrators" && grpname.Name.Contains("Domain Users") != true && grpname.Name.Contains("Domain Admins") != true) {
                                Action output3 = () => OutputBox.AppendText("Members of " + grpname.Name + ":\r\n");
                                OutputBox.Invoke(output3);
                                foreach (Principal mem in grpname.Members)
                                {
                                    Action output4 = () => OutputBox.AppendText(mem.Name + "\r\n");
                                    OutputBox.Invoke(output4);
                                }
                                Action output5 = () => OutputBox.AppendText("\r\n" + "\r\n");
                                OutputBox.Invoke(output5);
                            }
                        }
                        Action output6 = () => OutputBox.AppendText("\r\nIf you don't see the members of the group you're looking for, make sure you're on the right domain.\r\n");
                        OutputBox.Invoke(output6);
                        Action output7 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                        OutputBox.Invoke(output7);
                    }
                }
                else
                {
                    FileInfo fi = new FileInfo(path);
                    FileSecurity acl = fi.GetAccessControl(AccessControlSections.All);
                    AuthorizationRuleCollection rules = acl.GetAccessRules(true, true, typeof(NTAccount));

                    Action output = () => OutputBox.AppendText("Members with access to " + "<" + path.ToString() + ">" + "\r\n\r\n");
                    OutputBox.Invoke(output);
                    using (PrincipalContext ctx = new PrincipalContext(ContextType.Domain, getCurrentDomain().ToString()))
                    {
                        GroupPrincipal grpperm = new GroupPrincipal(ctx);
                        List<GroupPrincipal> collectgroups = new List<GroupPrincipal>();


                        foreach (AuthorizationRule rule in rules)
                        {
                            Action output1 = () => OutputBox.AppendText(rule.IdentityReference.Value.ToString() + "\r\n");
                            OutputBox.Invoke(output1);
                            GroupPrincipal grp = GroupPrincipal.FindByIdentity(ctx, rule.IdentityReference.Value);
                            if (grp != null)
                            {
                                collectgroups.Add(grp);
                            }
                        }
                        Action output2 = () => OutputBox.AppendText("\r\n");
                        OutputBox.Invoke(output2);

                        foreach (GroupPrincipal grpname in collectgroups)
                        {
                            Action output3 = () => OutputBox.AppendText("Members of " + grpname.Name + ":\r\n");
                            OutputBox.Invoke(output3);
                            foreach (Principal mem in grpname.Members)
                            {
                                Action output4 = () => OutputBox.AppendText(mem.Name + "\r\n");
                                OutputBox.Invoke(output4);
                            }
                            Action output5 = () => OutputBox.AppendText("\r\n" + "\r\n");
                            OutputBox.Invoke(output5);
                        }
                        Action output6 = () => OutputBox.AppendText("#####################" + "\r\n");
                        OutputBox.Invoke(output6);
                    }
                }
            }
            catch (SystemException err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void getAccessButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getAccess;
                bw.RunWorkerAsync();
           
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == (Keys.Control | Keys.F))
            {
                sendtext = OutputBox.Text;
                The_Admin_Toolbox.Find frm = new The_Admin_Toolbox.Find(this);
                frm.Show();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void OutputBox_Enter(object sender, EventArgs e)
        {

        }

        private void SerialtextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(SerialtextBox.Text))
            {
                SCCMSerialbutton.Enabled = false;
            }
            else
            {
                SCCMSerialbutton.Enabled = true;
            }
        }

        private void getCompModelButton_Click(object sender, EventArgs e)
        {
           
                The_Admin_Toolbox.GetSCCMByModelForm frm = new The_Admin_Toolbox.GetSCCMByModelForm(this);
                frm.Show();
           
        }

        private void OutputBox_TextChanged(object sender, EventArgs e)
        {

        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }

        public void bw_getSize(object sender, DoWorkEventArgs e)
        {
   
            Action o1 = () => OutputBox.AppendText("########## Size Information ###########\r\n\r\n");
            OutputBox.Invoke(o1);
            var di = new DirectoryInfo(getPathAccessTextBox.Text);
            Action o2 = () => OutputBox.AppendText("Total Size of " + getPathAccessTextBox.Text + ": " + SizeSuffix(DirSize(di)) + "\r\n\r\n");
            OutputBox.Invoke(o2);
            var result = di.GetFiles("*", System.IO.SearchOption.AllDirectories).OrderByDescending(x => x.Length).Take(20).ToList();
            DirectoryInfo[] dis = di.GetDirectories().OrderByDescending(x => DirSize(x)).Take(20).ToArray();
            Action o3 = () => OutputBox.AppendText("########## Top 20 Directories ###########\r\n\r\n");
            OutputBox.Invoke(o3);
            foreach (DirectoryInfo dir in dis)
            {
                Action o4 = () => OutputBox.AppendText("Name:\t" + dir.Name + "\r\n");
                OutputBox.Invoke(o4);
                Action o5 = () => OutputBox.AppendText("Size:\t" + SizeSuffix(DirSize(dir)) + "\r\n");
                OutputBox.Invoke(o5);
                Action o6 = () => OutputBox.AppendText("FullPath:\t" + "<" + dir.FullName + ">" + "\r\n");
                OutputBox.Invoke(o6);
                Action o7 = () => OutputBox.AppendText("Owner:\t" + System.IO.File.GetAccessControl(dir.FullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString() + "\r\n");
                OutputBox.Invoke(o7);
                Action o8 = () => OutputBox.AppendText("CreationTime:\t" + dir.CreationTime.ToString());
                OutputBox.Invoke(o8);
                Action o9 = () => OutputBox.AppendText("\r\n" + "\r\n" + "\r\n");
                OutputBox.Invoke(o9);
            }
            Action o17 = () => OutputBox.AppendText("########## Top 20 Files ###########\r\n\r\n");
            OutputBox.Invoke(o17);
            foreach (var file in result)
            {
                Action o10 = () =>OutputBox.AppendText("Name:\t" + file.Name + "\r\n");
                OutputBox.Invoke(o10);
                Action o11 = () =>OutputBox.AppendText("Size:\t" + SizeSuffix(file.Length) + "\r\n");
                OutputBox.Invoke(o11);
                Action o12 = () =>OutputBox.AppendText("FullPath:\t" + "<" + file.FullName + ">" + "\r\n");
                OutputBox.Invoke(o12);
                Action o13 = () =>OutputBox.AppendText("Owner:\t" + System.IO.File.GetAccessControl(file.FullName).GetOwner(typeof(System.Security.Principal.NTAccount)).ToString() + "\r\n");
                OutputBox.Invoke(o13);
                Action o14 = () =>OutputBox.AppendText("CreationTime:\t" + file.CreationTime.ToString());
                OutputBox.Invoke(o14);
                Action o15 = () => OutputBox.AppendText("\r\n" + "\r\n" + "\r\n");
                OutputBox.Invoke(o15);
            }
            Action o16 = () => OutputBox.AppendText("\r\n" + "#####################" + "\r\n" + "\r\n");
            OutputBox.Invoke(o16);
        }

        private void largestFilesBbutton_Click(object sender, EventArgs e)
        {
           BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_getSize;
            bw.RunWorkerAsync();
        }
        string msgg;
        public void bw_SendMessage(object sender, DoWorkEventArgs e)
        {
            
            // FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\NIRCMD AND RUNASCURRENTUSER\nircmd.exe", "\\\\" + comp + "\\C$\\Windows\\System32\nircmd.exe");
            // FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\NIRCMD AND RUNASCURRENTUSER\RunAsCurrentUser.exe", "\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
          
        }

        private void sendmsgButton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_SendMessage;
                bw.RunWorkerAsync();
           
        }
        public void createSpreadsheet(ManagementBaseObject sys, ManagementBaseObject os, ManagementBaseObject comppro, ManagementBaseObject compsys, ManagementBaseObject logdisk)
        {
            IWorkbook wb = new HSSFWorkbook();
            ICreationHelper createHelper = new HSSFCreationHelper((HSSFWorkbook)wb);
            FileStream fileOut = new FileStream("C:\\TEMP\\workbook.xls", FileMode.Create);
            ISheet sheet = wb.CreateSheet("new sheet");
            int rowinc = 0;
            int drowinc = 0;
            Console.WriteLine("System R Props");
            IRow row = sheet.CreateRow(0);
            IRow drow = sheet.CreateRow(1);
            foreach (PropertyData prop in sys.Properties)
            {
                row.CreateCell(rowinc).SetCellValue(
                createHelper.CreateRichTextString(prop.Name));
                drow.CreateCell(drowinc).SetCellValue(
                createHelper.CreateRichTextString(sys[prop.Name].ToString()));
                rowinc++;
                drowinc++;
            }
            foreach (PropertyData prop in os.Properties)
            {
                row.CreateCell(rowinc).SetCellValue(
                createHelper.CreateRichTextString(prop.Name));
                drow.CreateCell(drowinc).SetCellValue(
                createHelper.CreateRichTextString(os[prop.Name].ToString()));
                rowinc++;
                drowinc++;
            }
            foreach (PropertyData prop in compsys.Properties)
            {
                if (compsys[prop.Name] != null)
                {
                    row.CreateCell(rowinc).SetCellValue(
                    createHelper.CreateRichTextString(prop.Name));
                    drow.CreateCell(drowinc).SetCellValue(
                    createHelper.CreateRichTextString(compsys[prop.Name].ToString()));
                    rowinc++;
                    drowinc++;
                }
            }
            foreach (PropertyData prop in comppro.Properties)
            {
                if (comppro[prop.Name] != null)
                {
                    row.CreateCell(rowinc).SetCellValue(
                    createHelper.CreateRichTextString(prop.Name));
                    drow.CreateCell(drowinc).SetCellValue(
                    createHelper.CreateRichTextString(comppro[prop.Name].ToString()));
                    rowinc++;
                    drowinc++;
                }
            }
            foreach (PropertyData prop in logdisk.Properties)
            {
                if (logdisk[prop.Name] != null)
                {
                    row.CreateCell(rowinc).SetCellValue(
                    createHelper.CreateRichTextString(prop.Name));
                    drow.CreateCell(drowinc).SetCellValue(
                    createHelper.CreateRichTextString(logdisk[prop.Name].ToString()));
                    rowinc++;
                    drowinc++;
                }
            }
            wb.Write(fileOut);
            fileOut.Close();
        }
        public void bw_getCompInfo(object sender, DoWorkEventArgs e)
        {

            try
            {
                Action outputt = () => OutputBox.AppendText("Computer Info For "+ ComputerNameBox.Text+": \r\n################################\r\n");
                OutputBox.Invoke(outputt);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();

                wqlQuery = new WqlObjectQuery("select  SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.IPAddresses, SMS_R_System.SystemOUName, SMS_R_System.OperatingSystemNameandVersion, SMS_R_System.MACAddresses, SMS_G_System_COMPUTER_SYSTEM.Manufacturer, SMS_G_System_COMPUTER_SYSTEM.Model, SMS_G_System_COMPUTER_SYSTEM.Description, SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version, SMS_G_System_COMPUTER_SYSTEM.TotalPhysicalMemory, SMS_G_System_LOGICAL_DISK.Size, SMS_G_System_LOGICAL_DISK.FreeSpace, SMS_G_System_OPERATING_SYSTEM.Caption, SMS_G_System_DESKTOP_MONITOR.Name, SMS_G_System_DESKTOP_MONITOR.MonitorType, SMS_G_System_DESKTOP_MONITOR.ScreenWidth, SMS_G_System_DESKTOP_MONITOR.ScreenHeight from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM on SMS_G_System_COMPUTER_SYSTEM.ResourceID = SMS_R_System.ResourceId inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId inner join SMS_G_System_LOGICAL_DISK on SMS_G_System_LOGICAL_DISK.ResourceID = SMS_R_System.ResourceId inner join SMS_G_System_OPERATING_SYSTEM on SMS_G_System_OPERATING_SYSTEM.ResourceID = SMS_R_System.ResourceId inner join SMS_G_System_DESKTOP_MONITOR on SMS_G_System_DESKTOP_MONITOR.ResourceId = SMS_R_System.ResourceId where SMS_R_System.Name =  '" + ComputerNameBox.Text + "'");

                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                 ManagementObjectCollection coll = searcher.Get();
              
                  ManagementObject firstobj = coll.OfType<ManagementObject>().First();
                  ManagementBaseObject sys = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_R_System");
                ManagementBaseObject os = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_G_System_OPERATING_SYSTEM");


                Action output1 = () => OutputBox.AppendText("Name: " + sys["Name"] + "\r\n" + "Last Logged on User: " + sys["LastLogonUserName"] + "\r\n" + "IP Addresses: ");
                                  OutputBox.Invoke(output1);
                          Regex regex = new Regex("^(?:[0-9]{1,3}\\.){3}[0-9]{1,3}$");
                          foreach (string s in (String[])sys["IPAddresses"])
                          {
                              Match match = regex.Match(s);
                              if(match.Success)
                              {
                              Action output2 = () => OutputBox.AppendText(s.ToString()+"\r\n");
                              OutputBox.Invoke(output2);
                              }
                          }
                        Action output9 = () => OutputBox.AppendText("MAC Addresses: ");
                        OutputBox.Invoke(output9);
                        foreach (string s in (String[])sys["MACAddresses"])
                          {

                                Action output2 = () => OutputBox.AppendText(s.ToString() + "\r\n");
                                OutputBox.Invoke(output2);
                            
                        }
                Action output6 = () => OutputBox.AppendText("Operating System: " + os["Caption"].ToString() + "\r\n");
                          OutputBox.Invoke(output6);

                          Action output3 = () => OutputBox.AppendText("OU: ");
                          OutputBox.Invoke(output3);
                          foreach (string s in (String[])sys["SystemOUName"])
                          {
                              Action output4 = () => OutputBox.AppendText(s.ToString() + "\r\n");
                              OutputBox.Invoke(output4);
                          }

                          ManagementBaseObject compsys = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_G_System_COMPUTER_SYSTEM");
                          ManagementBaseObject comppro = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_G_System_COMPUTER_SYSTEM_PRODUCT");
                          ManagementBaseObject logdisk = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_G_System_LOGICAL_DISK");
                          //ManagementBaseObject disk = (ManagementBaseObject)firstobj.GetPropertyValue(" SMS_G_System_DISK");
                        if (logdisk["Size"] != null && logdisk["FreeSpace"] != null)
                          {
                              double total = Convert.ToDouble(logdisk["Size"]);
                              double free = Convert.ToDouble(logdisk["FreeSpace"]);
                              if (total > 1000)
                                total = total / 1024f;
                              if (free > 1000)
                                free = free / 1024f;
                              Action output5 = () => OutputBox.AppendText("Manufacturer: " + compsys["Manufacturer"].ToString() + "\r\n" + "Model: " + compsys["Model"].ToString() + "\r\n" + "Model Friendly Name: " + comppro["Version"] + "\r\n" + "Total Memory: " + compsys["TotalPhysicalMemory"] + "\r\n" + "Disk Size: " + String.Format("{0:0.00}GB", (total)) + "\r\n" + "Disk Free Space: " + String.Format("{0:0.00}GB", (free)) + "\r\n" + "#####################" + "\r\n" + "\r\n");
                              OutputBox.Invoke(output5);
                          }
                          else
                          {
                              double total = Convert.ToDouble(logdisk["Size"]);
                              Action output5 = () => OutputBox.AppendText("Manufacturer: " + compsys["Manufacturer"].ToString() + "\r\n" + "Model: " + compsys["Model"].ToString() + "\r\n" + "Model Friendly Name: " + comppro["Version"] + "\r\n" + "Total Memory: " + compsys["TotalPhysicalMemory"] + "\r\n" + "Disk Size: " + String.Format("{0:0.00}GB", (total)) );
                              OutputBox.Invoke(output5);
                          }

            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void getCompInfobutton_Click(object sender, EventArgs e)
        {
           
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_getCompInfo;
                bw.RunWorkerAsync();
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void bw_getDHCPClients(object sender, DoWorkEventArgs e)
        {
           
            
                BackgroundWorker bw = sender as BackgroundWorker;
                Action wait = () => OutputBox.Text = "Please wait....";
                OutputBox.Invoke(wait);
                try
                {
                    Process p = new Process();
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                    p.StartInfo.Arguments = "/c netsh dhcp server " + Properties.Settings.Default.DHCP + " scope " + subnetBox.Text + " show clients 1";
                    p.Start();

                    var reader = p.StandardOutput;

                    Action output78 = () => OutputBox.Clear();
                    OutputBox.Invoke(output78);
                    while (!reader.EndOfStream)
                    {
                        if (bw.CancellationPending)
                        {
                            e.Cancel = true;
                            bw.Dispose();
                            Action err = () => OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                            OutputBox.Invoke(err);
                            return;
                        }
                        // the point is that the stream does not end until the process has 
                        // finished all of its output.
                        var nextLine = reader.ReadLine();

                        Action output77 = () => OutputBox.AppendText(nextLine.ToString() + "\r\n");
                        OutputBox.Invoke(output77);
                    }
                    Action output99 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                    OutputBox.Invoke(output99);
                    p.Close();
                }
                catch (SystemException err)
                {

                    Action excp1 = () => OutputBox.Text = err.ToString();
                    OutputBox.Invoke(excp1);
                }
            

        }
        BackgroundWorker bwdhcp = new BackgroundWorker();
        private void getDHCPClientsBtn_Click(object sender, EventArgs e)
        {
            
                if (bwdhcp.IsBusy)
                {
                    bwdhcp.CancelAsync();
                }
                else
                {
                    bwdhcp = new BackgroundWorker();
                    bwdhcp.DoWork += bw_getDHCPClients;
                    bwdhcp.WorkerSupportsCancellation = true;
                    bwdhcp.RunWorkerAsync();
                }
           
        }


        public void bw_getDHCPScope(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;

            try
            {
                Process p = new Process();
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                p.StartInfo.Arguments = "/c netsh dhcp server " + Properties.Settings.Default.DHCP + " show scope";
                p.Start();

                var reader = p.StandardOutput;
                p.Close();
                Action wait = () => OutputBox.AppendText("Please wait...\r\n");
                while (!reader.EndOfStream)
                {
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        bw.Dispose();
                        Action err = () => OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                        OutputBox.Invoke(err);
                        return;
                    }
                    // the point is that the stream does not end until the process has 
                    // finished all of its output.
                    var nextLine = reader.ReadLine();
                    Action output1 = () => OutputBox.AppendText(nextLine.ToString() + "\r\n");
                    OutputBox.Invoke(output1);
                }
                Action output3 = () => OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
                OutputBox.Invoke(output3);
            }
            catch (SystemException)
            {

                Action excp1 = () => OutputBox.Text = "Error getting" + subnetBox.Text + ". Please check the IP and try again.";
                OutputBox.Invoke(excp1);
            }
            // Action enable = () => PingButton.Enabled = true; ;
            // PingButton.Invoke(enable);

        }
        BackgroundWorker bwdhcp2 = new BackgroundWorker();
        private void showDHCPScopeBtn_Click(object sender, EventArgs e)
        {
           
                if (bwdhcp.IsBusy)
                {
                    bwdhcp.CancelAsync();
                }
                else
                {
                    bwdhcp = new BackgroundWorker();
                    bwdhcp.DoWork += bw_getDHCPScope;
                    bwdhcp.WorkerSupportsCancellation = true;
                    bwdhcp.RunWorkerAsync();
                }
         
        }
        List<EventRecord> eventRecords = new List<EventRecord>();
        public void bw_getLockEvent(object sender, DoWorkEventArgs e)
        {
            string message = @"Writing events to C:\TEMP..." + "\r\n\r\n";
            Action output1 = () => OutputBox.AppendText(message);
            OutputBox.Invoke(output1);
            //logType can be Application, Security, System or any other Custom Log.
            try
            {
                using (var session = new EventLogSession(ADServer))
                {
                    string query = "*[System/EventID=4740]";
                    EventLogQuery eventsQuery = new EventLogQuery("Security", PathType.LogName, query);
                    eventsQuery.Session = session;
                      
                        EventLogReader logReader = new EventLogReader(eventsQuery);
                   
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        for (EventRecord entry = logReader.ReadEvent(); entry != null; entry = logReader.ReadEvent())
                        {
                 
                            eventRecords.Add(entry);
                        }
                        string date = DateTime.Now.ToString("yyyyMMdd-HHMMsstt");
                        string path = @"C:\TEMP\lockevents_" + date +".txt";
                  
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
                        {
                           
                                foreach (EventRecord entry in eventRecords)
                                {
                                   file.WriteLineAsync("Event ID : " + entry.Id + "\r\nTime : " + entry.TimeCreated + "\r\nMessage :  "+ entry.FormatDescription().ToString() + "\r\n\r\n");
                                }
                                
                                System.Threading.Thread.Sleep(2000);
                                file.Close();
                                file.Dispose();
                                eventsQuery.Session.Dispose();
                               
                        }
                    
                            timer.Stop();
                            Action output2 = () => OutputBox.AppendText(@"\nFinished writing events to C:\TEMP...\r\nPlease check in C:\TEMP for lockevents_"+date+"\r\n\r\n");
                            OutputBox.Invoke(output2);

                            string text = System.IO.File.ReadAllText(path);
                            Action output3 = () => OutputBox.AppendText(text);
                            OutputBox.Invoke(output3);
               
            }
        }
            catch (Exception err)
            {
                Action error = () => OutputBox.AppendText(err.Message);
                OutputBox.Invoke(error);
            }
        }
        public void worker_ProgressChanged(object sender, ProgressChangedEventHandler e)
        {
           
        }
      
      

        BackgroundWorker bw3 = new BackgroundWorker();
      

        public object AppSettings { get; private set; }

        private void getLockEventBtn_Click(object sender, EventArgs e)
        {

            var lockedUsers = new List<UserPrincipal>();
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                GroupPrincipal grp = GroupPrincipal.FindByIdentity(context, IdentityType.SamAccountName, "Domain Users");
                foreach (var userPrincipal in grp.GetMembers(false))
                {
                    var user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, userPrincipal.UserPrincipalName);
                    if (user != null)
                    {
                        if (user.IsAccountLockedOut())
                        {
                            lockedUsers.Add(user);
                        }
                    }
                }
            }
            foreach(var user in lockedUsers)
            {
                OutputBox.AppendText(user.DisplayName);

            }



        }
      

        private void subnetBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(this.subnetBox.Text))
            {
                getDHCPClientsBtn.Enabled = false;
            }
            else
            {
             getDHCPClientsBtn.Enabled = true;
            }
          
        }
   
        private void map_PrinterButton_Click(object sender, EventArgs e)
        {
           
                sendtext = ComputerNameBox.Text;
                domain = getCurrentDomain().ToString();
                The_Admin_Toolbox.MapPrinter frm = new The_Admin_Toolbox.MapPrinter(this);
                frm.Show();
  

        }

       

        private void map_NetDrButton_Click(object sender, EventArgs e)
        {
            
                sendtext = ComputerNameBox.Text;
                domain = getCurrentDomain().ToString();
                The_Admin_Toolbox.MapNetDrives frm = new The_Admin_Toolbox.MapNetDrives(this);
                frm.Show();
           
        }

        private void runAsDifferentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_currentUserStrip;
            bw.RunWorkerAsync();
        }

        private void currentUserToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void DeleteSCCMbutton_Click(object sender, EventArgs e)
        {

            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the computer from SCCM and AD?", "Delete Computer", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    SmsNamedValuesDictionary namedValues = new SmsNamedValuesDictionary();

                    WqlConnectionManager connection = new WqlConnectionManager(namedValues);
                    connection.Connect(SCCMServer);

                    IResultObject computer = connection.QueryProcessor.ExecuteQuery("Select ResourceID From SMS_R_System Where Name ='" + ComputerNameBox.Text + "'");

                    computer.Delete();
                   
                    MessageBox.Show("Deleted " + ComputerNameBox.Text + " from SCCM.");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString() + "\r\n Couldn't be deleted from SCCM");
                }
                //ManagementBaseObject n = (ManagementBaseObject)firstobj.GetPropertyValue("SMS_R_System");

                try
                {
                    PrincipalContext ctx = new PrincipalContext(ContextType.Domain,
                                                  getCurrentDomain().ToString());

                    // find the computer in question
                    ComputerPrincipal computer = ComputerPrincipal.FindByIdentity(ctx, ComputerNameBox.Text);

                    // if found - delete it
                    if (computer != null)
                    {
                        computer.Delete();
                        MessageBox.Show("Deleted " + ComputerNameBox.Text + " from AD.");
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show("Make sure you have the correct domain selected. Otherwise, this computer doesn't exist in AD");
                }
               
            }            

        }

        private void getHistoryButton_Click(object sender, EventArgs e)
        {
            try
            {
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=./app.sqlite;Version=3;");
                m_dbConnection.Open();
                string sql = "select * from history order by created_at desc";
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                OutputBox.AppendText("Previous search history");
                OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                while (reader.Read())
                    OutputBox.AppendText("\r\nComputer: " + reader["name"] + "\r\nUser: " + reader["user"] + "\r\nSearched on: " + DateTime.Parse(reader["created_at"].ToString()) + "\r\n");
                m_dbConnection.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void clearHistoryButton_Click(object sender, EventArgs e)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=./app.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "DELETE FROM History";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteReader();
            m_dbConnection.Close();
            MessageBox.Show("History has been cleared", "History", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void bw_getProfiles(object sender, DoWorkEventArgs e)
        {
            try
            {
                    List<string> profiles = new List<string>();

                    ConnectionOptions connectionOptions = new ConnectionOptions();

                    ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\CIMV2", connectionOptions);
                    scope.Connect();

                    string profileRegLoc = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList";

                    ManagementClass registry = new ManagementClass(scope, new ManagementPath("StdRegProv"), null);
                    ManagementBaseObject inParams = registry.GetMethodParameters("EnumKey");
                    inParams["hDefKey"] = 0x80000002;//HKEY_LOCAL_MACHINE
                    inParams["sSubKeyName"] = profileRegLoc;

                    // Read Registry Key Names 
                    ManagementBaseObject outParams = registry.InvokeMethod("EnumKey", inParams, null);
                    string[] SIDS = outParams["sNames"] as string[];
                    Action output = () => OutputBox.AppendText("\r\nUser profiles on computer: \r\n");
                    OutputBox.Invoke(output);
                    foreach (string sid in SIDS)
                    {
                        StringBuilder name = new StringBuilder();
                        uint cchName = (uint)name.Capacity;
                        StringBuilder referencedDomainName = new StringBuilder();
                        uint cchReferencedDomainName = (uint)referencedDomainName.Capacity;
                        SID_NAME_USE sidUse;
                        // Sid for BUILTIN\Administrators

                        IntPtr SID_ptr = new IntPtr(0);
                        ConvertStringSidToSid(sid, out SID_ptr);
                        int size = (int)GetLengthSid(SID_ptr);
                        byte[] Sid = new byte[size];
                        Marshal.Copy(SID_ptr, Sid, 0, size);
                        Marshal.FreeHGlobal(SID_ptr);

                        int err = NO_ERROR;
                        if (!LookupAccountSid(null, Sid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out sidUse))
                        {
                            err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                            if (err == ERROR_INSUFFICIENT_BUFFER)
                            {
                                name.EnsureCapacity((int)cchName);
                                referencedDomainName.EnsureCapacity((int)cchReferencedDomainName);
                                err = NO_ERROR;
                                if (!LookupAccountSid(null, Sid, name, ref cchName, referencedDomainName, ref cchReferencedDomainName, out sidUse))
                                    err = System.Runtime.InteropServices.Marshal.GetLastWin32Error();
                            }
                        }
                        if (err == 0)
                        {
                            //Console.WriteLine(@"Found account {0} : {1}\{2}", sidUse, referencedDomainName.ToString(), name.ToString());
                            if(sidUse.ToString() == "SidTypeUser")
                            {
                                Action output2 = () => OutputBox.AppendText(referencedDomainName.ToString() + "\\" + name.ToString() + "\r\n");
                                OutputBox.Invoke(output2);
                            }
                        }
                    }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void GetProfilesButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_getProfiles;
            bw.RunWorkerAsync();
        }

        private void installSnagitButton_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installSnagit;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey needs to be installed first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void bw_installSnagit(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                string chocoPath = Properties.Settings.Default.Snagit11Path;
                FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\Snagit11\\", UIOption.AllDialogs);
                var button = sender as Button;
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + ComputerNameBox.Text.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", "Snagit11", @"C:\Snagit11");
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\Snagit11", DeleteDirectoryOption.DeleteAllContents);
                    if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\TechSmith\Snagit 11\"))
                {

                    System.Windows.Forms.MessageBox.Show("Snagit is now installed", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Snagit is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }


        public void bw_installAble2Extract(object sender, EventArgs e)
        {
            string comp = ComputerNameBox.Text;
            try
            {
                string chocoPath = Properties.Settings.Default.Able2ExtractPath;
                FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\Able2Extract\\", UIOption.AllDialogs);
                var button = sender as Button;
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + ComputerNameBox.Text.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", "Able2Extract", @"C:\Able2Extract");
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\Able2Extract", DeleteDirectoryOption.DeleteAllContents);
                if (Directory.Exists("\\\\" + comp + @"\C$\Program Files (x86)\Investintech.com Inc\"))
                {

                    System.Windows.Forms.MessageBox.Show("Able2Extract is now installed", "Installation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Windows.Forms.MessageBox.Show("Please give the user this serial number so that they can activate it. Serial: 7382-9290-1808", "Serial Number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Able2Extract is not installed", "Installation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void installAble2ExtractButton_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_installAble2Extract;
            bw.RunWorkerAsync(bw);
        }
        async private void getAllDataBaseData(FilterDefinition<CompInfo> filter, List<FieldDefinition<CompInfo>> fields = null)
        {
            try
            {
                
                var credential = MongoCredential.CreateCredential("pcdata", "tyjohnson", "");
                var mongoClientSettings = new MongoClientSettings
                {
                    //ConnectionMode = ConnectionMode.ReplicaSet,
                    
                  
                    
                    Credential = credential,
                    /*
                    ClusterConfigurator = builder =>
                    {
                        builder.ConfigureCluster(settings => settings.With(serverSelectionTimeout: TimeSpan.FromSeconds(4)));
                    }
                    */
                };
                var client = new MongoClient(mongoClientSettings);
                IMongoDatabase db = client.GetDatabase("pcdata");
                var collection = db.GetCollection<CompInfo>("compinfo");
                //Get only certain fileds
                List<CompInfo> docs = new List<CompInfo>();
                if (fields == null)
                {
                    docs = await collection.Find(filter).ToListAsync();
                }
                else
                {
                    var projectionBuilder = Builders<CompInfo>.Projection;
                    var projection = projectionBuilder.Combine(fields.Select(field => projectionBuilder.Include(field)));
                    docs = await collection.Find(filter).Project<CompInfo>(projection).ToListAsync();
                }



                if (docs.Count >= 1)
                {
                    foreach (CompInfo doc in docs)
                    {
                        OutputBox.AppendText("========== System Information ========== \r\n");
                        if (doc.ComputerName != null) { OutputBox.AppendText("\r\nComputer Name: " + doc?.ComputerName + "\r\n"); }
                        if (doc.OS != null) { OutputBox.AppendText("Operating System: " + doc.OS + "\r\n"); }
                        if (doc.CurrentUser != null) { OutputBox.AppendText("Users logged into " + doc?.ComputerName + ": "); }
                        if (doc.CurrentUser != null) { doc.CurrentUser?.ForEach(u => { OutputBox.AppendText(u + "\r\n"); }); }
                        if (doc.LastBootUpTime != null) { OutputBox.AppendText("Last Boot Up Time: " + doc.LastBootUpTime + "\r\n"); }
                        if (doc.ModelVersion != null) { OutputBox.AppendText("Friendly Name: " + doc.ModelVersion + "\r\n"); }
                        if (doc.BiosAge != null) { OutputBox.AppendText("Bios Age: " + doc.BiosAge + "\r\n"); }
                        if (doc.BiosVer != null) { OutputBox.AppendText("Bios Version: " + doc.BiosVer + "\r\n"); }
                        if (doc.TotalRAM != null) { OutputBox.AppendText("Total RAM: " + doc.TotalRAM + "\r\n"); }
                        if (doc.Manufacturer != null) { OutputBox.AppendText("Manufacturer: " + doc.Manufacturer + "\r\n"); }
                        if (doc.SN != null) { OutputBox.AppendText("Serial Number: " + doc.SN + "\r\n"); }
                        if (doc.Model != null) { OutputBox.AppendText("Model Number: " + doc.Model + "\r\n"); }
                        if (doc.CreatedAt != DateTime.MinValue) { OutputBox.AppendText("Record Created/Update At: " + doc.CreatedAt.ToString() + "\r\n"); };
                        OutputBox.AppendText("\r\n========== Battery/Power Information ========== \r\n\r\n");
                        if (doc.BatteryStatus != null)
                        {
                            foreach (KeyValuePair<string, string> entry in doc?.BatteryStatus)
                            {
                                OutputBox.AppendText(String.Format("{0} : {1}", entry.Key,
                            entry.Value) + "\r\n");
                            }
                        }
                        OutputBox.AppendText("\r\n========== Netowrk Information ========== \r\n\r\n");
                        if (doc.MACEthernetAddress != null) { OutputBox.AppendText("Ethernet MAC: " + doc.MACEthernetAddress + "\r\n"); }
                        if (doc.WirelessMACAddress != null) { OutputBox.AppendText("Wireless MAC: " + doc.WirelessMACAddress + "\r\n"); }
                      
                        if (doc.IPAddresses != null) { doc.IPAddresses.ForEach(ip => OutputBox.AppendText(ip + "\r\n")); }
                        OutputBox.AppendText("\r\n========== Hard Drive Information ========== \r\n\r\n");
                        if (doc.HDDTotalSize != null)
                        {
                            OutputBox.AppendText(String.Format("Hard Drive Total Space: {0} : {1}", doc.HDDTotalSize?.FirstOrDefault().Key,
                            doc.HDDTotalSize?.FirstOrDefault().Value) + "\r\n");
                        }
                        if (doc.HDDFreeSpace != null)
                        {
                            OutputBox.AppendText(String.Format("Hard Drive Free Space: {0} : {1}", doc.HDDFreeSpace?.FirstOrDefault().Key,
                            doc.HDDFreeSpace?.FirstOrDefault().Value) + "\r\n");
                        }
                        if (doc.HDDUsedSpace != null)
                        {
                            OutputBox.AppendText(String.Format("Hard Drive Used Space: {0} : {1}", doc.HDDUsedSpace?.FirstOrDefault().Key,
                            doc.HDDUsedSpace?.FirstOrDefault().Value) + "\r\n");
                        }
                        if (doc.HDDPercentUsed != null)
                        {
                            OutputBox.AppendText(String.Format("Percentage of Used Space: {0}",
                            doc.HDDPercentUsed?.FirstOrDefault().Value) + "%\r\n");
                        }
                        if (includeNetPrinterscheckBox.Checked == true)
                        {
                            OutputBox.AppendText("\r\n========== Netowork Printer Information ========== \r\n\r\n");
                            if (doc.MappedPrinters != null) { if (doc.CurrentUser.Count > 1) { OutputBox.AppendText("Mapped Printers for first User: \r\n"); } }
                            if (doc.MappedPrinters != null) { if (doc.CurrentUser.Count == 1) { OutputBox.AppendText("Mapped Printers: \r\n"); } }
                            if (doc.MappedPrinters != null) { doc.MappedPrinters?.ForEach(p => { OutputBox.AppendText(p + "\r\n"); }); }
                        }
                        if(includeNetDrivescheckBox.Checked == true)
                        {
                            OutputBox.AppendText("\r\n========== Netowork Drive Information ========== \r\n\r\n");
                            if (doc.NetworkDrives != null) { if (doc.CurrentUser.Count > 1) { OutputBox.AppendText("Network Drives for first User: \r\n"); } }
                            if (doc.NetworkDrives != null) { if (doc.CurrentUser.Count == 1) { OutputBox.AppendText("Network Drives: \r\n"); } }
                            if (doc.NetworkDrives != null)
                            {
                                foreach (KeyValuePair<string, string> entry in doc?.NetworkDrives)
                                {
                                    OutputBox.AppendText(String.Format("{0} : {1}", entry.Key,
                                    entry.Value) + "\r\n");
                                }
                            }
                        }
                        if(includeSoftwarecheckBox.Checked == true)
                        {
                            OutputBox.AppendText("\r\n========== Installed Software Information ========== \r\n\r\n");
                            if (doc.Software != null) { OutputBox.AppendText("Installed Software: \r\n"); };
                            if (doc.Software != null) { doc.Software?.ForEach(s => { OutputBox.AppendText(s + "\r\n"); }); };
                        }
                        
                        OutputBox.AppendText("\r\n" + "#####################" + "\r\n\r\n");

                    }
                }
                else
                {
                    OutputBox.AppendText("\r\nThe database is empty or the item you're searching for could not be found.\r\n");
                    OutputBox.AppendText("\r\n" + "#####################" + "\r\n");
                }
            }
            catch(Exception )
            {
                System.Windows.Forms.MessageBox.Show("Could not connect to database", "Connection Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void getallDataButton_Click(object sender, EventArgs e)
        {
            var filter = FilterDefinition<CompInfo>.Empty;
            getAllDataBaseData(filter);
        }

        private void findByCompNameButton_Click(object sender, EventArgs e)
        {
            if (ComputerNameBox.Text.StartsWith("10."))
            {
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                    int pos = hostEntry.HostName.IndexOf(".");
                    string computer = hostEntry.HostName.Remove(pos);
                    var filter = Builders<CompInfo>.Filter.Where(x => x.ComputerName == computer);
                    getAllDataBaseData(filter);
                }
                catch(Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                var filter = Builders<CompInfo>.Filter.Where(x => x.ComputerName == ComputerNameBox.Text);
                getAllDataBaseData(filter);
            }
                    
        }

        private  void findByUserNameDBButton_Click(object sender, EventArgs e)
        {

            var username = findByUserNameDBtextBox.Text;

            var filter = Builders<CompInfo>.Filter.Regex(x => x.CurrentUser, new BsonRegularExpression(username));
            getAllDataBaseData(filter);
           
        }

 

        private void installServiceCompNameBoxButton_Click(object sender, EventArgs e)
        {
            controlDataService("install", ComputerNameBox.Text);
        }

        private void uninstallFromComputerButton_Click(object sender, EventArgs e)
        {
            controlDataService("uninstall", ComputerNameBox.Text);
        }


        public void controlDataService(string cmd, string comp)
        {
            string installpath = Properties.Settings.Default.ServiceInstallerLocation;
            if (cmd == "install")
            {
                FileSystem.CopyDirectory(installpath, "\\\\" + comp + "\\C$\\ProgramData\\ITDataService", true);
            }
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + comp.ToLower();
            string hh = string.Format("\"{0}\"", path);
            psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c ""C:\ProgramData\ITDataService\TopShelfDataService.exe {0}"" & pause", cmd);
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            if (cmd == "uninstall")
            {
                FileSystem.DeleteDirectory("\\\\" + comp + "\\C$\\ProgramData\\ITDataService", DeleteDirectoryOption.DeleteAllContents);
            }
        }

        private void button1_Click_3(object sender, EventArgs e)
        {

        }

        private void getUserNameOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ComputerNameBox.Text.StartsWith("10."))
            {
                try
                {
                    IPHostEntry hostEntry = Dns.GetHostEntry(ComputerNameBox.Text);
                    int pos = hostEntry.HostName.IndexOf(".");
                    string computer = hostEntry.HostName.Remove(pos);
                    var filter = Builders<CompInfo>.Filter.Where(x => x.ComputerName == computer);
                    List<FieldDefinition<CompInfo>> fields = new List<FieldDefinition<CompInfo>>();
                    FieldDefinition<CompInfo> field = "CurrentUser";
                    fields.Add(field);
                    getAllDataBaseData(filter, fields);
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                var filter = Builders<CompInfo>.Filter.Where(x => x.ComputerName == ComputerNameBox.Text);
                List<FieldDefinition<CompInfo>> fields = new List<FieldDefinition<CompInfo>>();
                FieldDefinition<CompInfo> field = "CurrentUser";
                fields.Add(field);
                getAllDataBaseData(filter, fields);
            }
        }

        private void getComputerNameOnlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string username = findByUserNameDBtextBox.Text;
            var filter = Builders<CompInfo>.Filter.Regex(x => x.CurrentUser, new BsonRegularExpression(username));
            List<FieldDefinition<CompInfo>> fields = new List<FieldDefinition<CompInfo>>();
            FieldDefinition<CompInfo> field = "ComputerName";
            fields.Add(field);
            getAllDataBaseData(filter, fields);
        }

        private void checkServiceStatusButton_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                scope.Connect();
                string service = "DataSenderService";

                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "' AND State LIKE 'Running'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                ManagementObjectCollection n = searcher.Get();
                if (n.Count == 1)
                {
                    foreach (ManagementObject o in n)
                    {
                        ManagementBaseObject outParams = o.InvokeMethod("StopService", null, null);
                        if(Convert.ToInt32(outParams["ReturnValue"]) == 0)
                        {
                            ManagementBaseObject outParams2 = o.InvokeMethod("StartService", null, null);
                            if (Convert.ToInt32(outParams2["ReturnValue"]) == 0)
                            {
                                System.Windows.Forms.MessageBox.Show("Service was restarted.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                        System.Windows.Forms.MessageBox.Show("Service is installed and running.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Service is either not installed or not running.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }catch(Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void includeSoftwarecheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void findByUserNameDBtextBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(findByUserNameDBtextBox.Text))
            {
                findByUserNameDBButton.Enabled = false;
            }
            else
            {
                findByUserNameDBButton.Enabled = true;
            }
        }

        private void IPRangeInstallbutton_Click(object sender, EventArgs e)
        {
            The_Admin_Toolbox.IPRangeInstall frm = new The_Admin_Toolbox.IPRangeInstall();
            // new Thread(() => new The_Admin_Toolbox.IPRangeInstall().Show()).Start();
            if (!frm.IsDisposed)
                frm.Show();
        }

        private void button_HardDrive_Click(object sender, EventArgs e)
        {
            string size = ComputerInfo.GetSize(ComputerNameBox.Text);
            OutputBox.AppendText("Total Space: " + SizeSuffix(Convert.ToInt64(size)) + "\r\n");
            OutputBox.AppendText("Total Free Space: " + SizeSuffix(Convert.ToInt64(size)) + "\r\n");
        }

        private void StartUpBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ManagementScope scope = new ManagementScope("\\\\" + ComputerNameBox.Text + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_StartUpCommand");
                ManagementObjectSearcher searcher0 =
                    new ManagementObjectSearcher(scope, wqlQuery0);
                OutputBox.AppendText("Startup Items: \r\n");
                OutputBox.AppendText("#####################\r\n\r\n");
                foreach (ManagementObject n in searcher0.Get())
                {
                    OutputBox.AppendText("Caption: " + n["Caption"] + "\r\n" + "Command: " + n["Command"] + "\r\n\r\n");
                }
                OutputBox.AppendText("\r\n" + "\r\n" + "#####################" + "\r\n");
            }
            catch (Exception err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SCCMcomboBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Properties.Settings.Default["SCCMServer"] = SCCMcomboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void SCCMcomboBox_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default["SCCMServer"] = SCCMcomboBox.Text;
            Properties.Settings.Default.Save();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + ComputerNameBox.Text.ToLower();
                string hh = string.Format("\"{0}\"", path);
                psi.Arguments = @"/k C:\Windows\System32\psexec.exe -accepteula -s " + hh + " -h cmd /k";
                process.StartInfo = psi;
                process.Start();
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void bw_installCleverAnt(object sender, EventArgs e)
        {
            string chocoPath = Properties.Settings.Default.CleverantPath;
            FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\Cleverant\\", UIOption.AllDialogs);
            var button = sender as Button;
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + ComputerNameBox.Text.ToLower();
            string hh = string.Format("\"{0}\"", path);
            psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", button.Text, @"C:\Cleverant");
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\Cleverant", DeleteDirectoryOption.DeleteAllContents);
        }

        private void installCleverAnt_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installCleverAnt;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey is not installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void bw_installSabre(object sender, EventArgs e)
        {
            string chocoPath = Properties.Settings.Default.SabreHTEPath;
            FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\ASHostPlatform", UIOption.AllDialogs);
            var button = sender as Button;
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + ComputerNameBox.Text.ToLower();
            string hh = string.Format("\"{0}\"", path);
            psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", button.Text, @"C:\ASHostPlatform");
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\ASHostPlatform", DeleteDirectoryOption.DeleteAllContents);
        }

        private void installSabreButton_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installSabre;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey is not installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void bw_installDialPad(object sender, EventArgs e)
        {
            string chocoPath = Properties.Settings.Default.DialPadPath;
            FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\DialPad", UIOption.AllDialogs);
            var button = sender as Button;
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + ComputerNameBox.Text.ToLower();
            string hh = string.Format("\"{0}\"", path);
            psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", button.Text, @"C:\DialPad");
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\DialPad", DeleteDirectoryOption.DeleteAllContents);
        }

        private void installDialPad_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installDialPad;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey is not installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void bw_installSAPGUI730(object sender, EventArgs e)
        {
            string chocoPath = Properties.Settings.Default.SAPGUIPath;
            FileSystem.CopyDirectory(chocoPath, "\\\\" + ComputerNameBox.Text + "\\C$\\SAPGUI730", UIOption.AllDialogs);
            var button = sender as Button;
            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.UseShellExecute = false;
            string path = "\\\\" + ComputerNameBox.Text.ToLower();
            string hh = string.Format("\"{0}\"", path);
            psi.Arguments = @"/c psexec -accepteula -s " + hh + String.Format(@" -h cmd /c choco install {0} -s {1} -y", "SAPGUI730", @"C:\SAPGUI730");
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
            FileSystem.DeleteDirectory("\\\\" + ComputerNameBox.Text + "\\C$\\SAPGUI730", DeleteDirectoryOption.DeleteAllContents);
        }
        private void installSAPGUI730_Click(object sender, EventArgs e)
        {
            bool exists = Directory.Exists("\\\\" + ComputerNameBox.Text + "\\C$\\ProgramData\\chocolatey");
            if (exists)
            {
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += bw_installSAPGUI730;
                bw.RunWorkerAsync();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Chocolatey is not installed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }

    [BsonIgnoreExtraElements]
    public class CompInfo
    {
        [BsonElement("name")]
        public string ComputerName { get; set; }
        [BsonElement("os")]
        public string OS { get; set; }
        [BsonElement("systemtype")]
        public string PCSystemType { get; set; }
        [BsonElement("lastbootuptime")]
        public string LastBootUpTime { get; set; }
        [BsonElement("modelver")]
        public string ModelVersion { get; set; }
        [BsonElement("biosage")]
        public string BiosAge { get; set; }
        [BsonElement("biosver")]
        public string BiosVer { get; set; }
        [BsonElement("buildver")]
        public string BuildVersion { get; set; }
        [BsonElement("ram")]
        public string TotalRAM { get; set; }
        [BsonElement("manufacturer")]
        public string Manufacturer { get; set; }
        [BsonElement("serialnumber")]
        public string SN { get; set; }
        [BsonElement("modelnumber")]
        public string Model { get; set; }
        [BsonElement("batterystatus")]
        public Dictionary<string, string> BatteryStatus { get; set; }
        [BsonElement("ethernetmac")]
        public string MACEthernetAddress { get; set; }
        [BsonElement("wirelessmac")]
        public string WirelessMACAddress { get; set; }
        [BsonElement("hdd_freespace")]
        public Dictionary<string, string> HDDFreeSpace { get; set; }
        [BsonElement("hdd_totalsize")]
        public Dictionary<string, string> HDDTotalSize { get; set; }
        [BsonElement("hdd_usedspace")]
        public Dictionary<string, string> HDDUsedSpace { get; set; }
        [BsonElement("hdd_percusedspace")]
        public Dictionary<string, string> HDDPercentUsed { get; set; }
        [BsonElement("mapped_printers")]
        public List<String> MappedPrinters { get; set; }
        [BsonElement("network_drives")]
        public Dictionary<string, string> NetworkDrives { get; set; }
        [BsonElement("software")]
        public List<String> Software { get; set; }
        [BsonElement("current_user")]
        public List<string> CurrentUser { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("ipaddresses")]
        public List<String> IPAddresses { get; set; }
    }
}

