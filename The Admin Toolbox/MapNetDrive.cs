using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Management;
using System.DirectoryServices.AccountManagement;
using Microsoft.Win32;

namespace The_Admin_Toolbox
{
    public partial class MapNetDrives : Form
    {
        string comp = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        BackgroundWorker bw = new BackgroundWorker();
        private TheAdminToolBox mainForm = null;
        public MapNetDrives(Form callingform)
        {
            InitializeComponent();
            mainForm = callingform as TheAdminToolBox;
            bw.DoWork += initNetDr;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += RunWorkerCompletedHandler;
            bw.RunWorkerAsync();
            this.mainForm.ControlMapNetDrIsVisible = false;
            MapNetDrives.ActiveForm.Text = "Please wait...";
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {

                this.mainForm.ControlMapNetDrIsVisible = true;
                MessageBox.Show(e.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Access e.Result only if no error occurred.
            }
            else
            {
                this.mainForm.ControlMapNetDrIsVisible = true;
                MapNetDrives.ActiveForm.Text = "Map Network Drives";
            }
        }

        public void initNetDr(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action o = () => listView1.View = View.Details;
                if(listView1.InvokeRequired)
                {
                    listView1.Invoke(o);
                }
                else
                {
                    listView1.View = View.Details;
                }
                ManagementScope scope = new ManagementScope("\\\\" + comp + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name LIKE 'explorer.exe'");
                ManagementObjectSearcher searcher0 =
                    new ManagementObjectSearcher(scope, wqlQuery0);
                string user = "";
                int usercount = searcher0.Get().Count;
                if (usercount > 1)
                {
                    user = Microsoft.VisualBasic.Interaction.InputBox("It looks like there's multiple users logged in. What user are you mapping this to?", "Pick a user", "Please type username");
                }
                else
                {
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
                }
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  The_Admin_Toolbox.TheAdminToolBox.domain);
                UserPrincipal sid = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, user);
                string usersid = sid.Sid.ToString();

                string service = "Remote Registry";

                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject inputArgs = n.GetMethodParameters("ChangeStartMode");
                    inputArgs["startmode"] = "Manual";
                    ManagementBaseObject outParams = n.InvokeMethod("ChangeStartMode", inputArgs, null);
                    ManagementBaseObject outParams2 = n.InvokeMethod("StartService", null, null);

                }

                string remoteName = comp;
                RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                RegistryKey connections = environmentKey.OpenSubKey("Network");
                string[] lists = connections.GetSubKeyNames();


                foreach (string n in lists)
                {
                    //listBox1.Items.Add("Drive Letter: " + n + "\tPath: " + connections.OpenSubKey(n).GetValue("RemotePath").ToString());
                    //Add items in the listview
                    string[] arr = new string[2];
                    ListViewItem itm;

                    //Add first item
                    arr[0] = n.ToUpper();
                    arr[1] = connections.OpenSubKey(n).GetValue("RemotePath").ToString();

                    itm = new ListViewItem(arr);
                    Action o2 = () => listView1.Items.Add(itm);
                    Action o3 = () => listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    Action o4 = () => listView1.Columns[0].Width = 100;
                    if (listView1.InvokeRequired)
                    {
                        listView1.Invoke(o2);
                        listView1.Invoke(o3);
                        listView1.Invoke(o4);

                    }
                    else
                    {
                        listView1.Items.Add(itm);
                        listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        listView1.Columns[0].Width = 100;
                    }
                    Application.DoEvents();
                }

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject outParams2 = n.InvokeMethod("StopService", null, null);
                    ManagementBaseObject inputArgs = n.GetMethodParameters("ChangeStartMode");
                    inputArgs["startmode"] = "Disabled";
                    ManagementBaseObject outParams = n.InvokeMethod("ChangeStartMode", inputArgs, null);

                }
            }
            catch (SystemException err)
            {
                throw new Exception(err.Message);
            }
        }

        private void map_Button_Click(object sender, EventArgs e)
        {
            if (driveLetter_TextBox.Text != "" && path_textBox.Text != "")
            {
                if (!FileSystem.FileExists("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe"))
                {
                    FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\NIRCMD AND RUNASCURRENTUSER\RunAsCurrentUser.exe", "\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
                }
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + comp.ToLower();
                string hh = string.Format("\"{0}\"", path);
                string drive_letter = this.driveLetter_TextBox.Text;
                string drive_path = string.Format("\"{0}\"", this.path_textBox.Text); 
                psi.Arguments = @"/c C:\Windows\System32\psexec.exe -accepteula -s " + hh + @" -h cmd /c RunAsCurrentUser.exe --w --q net use " + drive_letter + @": " + drive_path + @" /Persistent:yes";
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
              
            }
            else
            {
                MessageBox.Show("Drive letter or path cannot be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             
            }
        }

        private void remove_Button_Click(object sender, EventArgs e)
        {
            if (this.driveLetter_TextBox.Text != "")
            {
                if (!FileSystem.FileExists("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe"))
                {
                    FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\NIRCMD AND RUNASCURRENTUSER\RunAsCurrentUser.exe", "\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
                }
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + comp.ToLower();
                string hh = string.Format("\"{0}\"", path);
                string drive_letter = this.driveLetter_TextBox.Text;
                string drive_path = string.Format("\"{0}\"", this.path_textBox.Text);
                psi.Arguments = @"/c C:\Windows\System32\psexec.exe -accepteula -s " + hh + @" -h cmd /c RunAsCurrentUser.exe --w --q net use " + drive_letter + @": /DELETE";
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");

            }
            else
            {
                MessageBox.Show("Drive letter must not be blank", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MapNetDrives_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw.CancellationPending == false)
            {
                this.bw.CancelAsync();
            }
        }
    }
}
