using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using System.Management;
using System.DirectoryServices.AccountManagement;
using Microsoft.Win32;

namespace The_Admin_Toolbox
{
    /*
      RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                    RegistryKey printers = environmentKey.OpenSubKey("Printers");
                    RegistryKey connections = printers.OpenSubKey("Connections");
                    string[] lists = connections.GetSubKeyNames();
                    Action output1 = () => OutputBox.AppendText("\r\nNetwork printers on remote computer:\r\n");
                    OutputBox.Invoke(output1);

                    foreach (string n in lists)
                    {
                        Action output2 = () => OutputBox.AppendText("" + n + "" + "\r\n");
                        OutputBox.Invoke(output2);
                    }
     */


    public partial class MapPrinter : Form
    {
        string comp = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        BackgroundWorker bw = new BackgroundWorker();
        BackgroundWorker bw2 = new BackgroundWorker();
        private TheAdminToolBox mainForm = null;
        
        public MapPrinter(Form callingform)
        {
            MapPrinter.ActiveForm.Text = "Please wait....";
            mainForm = callingform as TheAdminToolBox;
            InitializeComponent();
            bw.DoWork += initMapPrinters;
            bw.WorkerSupportsCancellation = true;
            bw.RunWorkerCompleted += RunWorkerCompletedHandler;
            bw.RunWorkerAsync();
           
            this.mainForm.ControlMapPrinterIsVisible = false;
        }

        private void RunWorkerCompletedHandler(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
               
                this.mainForm.ControlMapPrinterIsVisible = true;

                MessageBox.Show(e.Error.ToString()); // Access e.Result only if no error occurred.
            }
            else
            {
                MapPrinter.ActiveForm.Text = "Map Network Printers";
                this.mainForm.ControlMapPrinterIsVisible = true;
            }
        }

        private void map_Button_Click(object sender, EventArgs e)
        {
            if (path_textBox.Text != "")
            {
                if (!FileSystem.FileExists("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe")){
                    FileSystem.CopyFile(@"\\iad1srvfs1\IT Common\Powershell\NIRCMD AND RUNASCURRENTUSER\RunAsCurrentUser.exe", "\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
                }
                
                Process process = new Process();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "cmd.exe";
                psi.UseShellExecute = false;
                string path = "\\\\" + comp.ToLower();
                string hh = string.Format("\"{0}\"", path);
                string printer_path = string.Format("\"{0}\"", this.path_textBox.Text);
                MessageBox.Show(printer_path);
                if (!prtr_checkBox.Checked)
                {
                    psi.Arguments = @"/c C:\Windows\System32\psexec.exe -accepteula -s " + hh + @" -h cmd /c RunAsCurrentUser.exe --w --q RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /in /Gw /u /z /n " + printer_path;
                }
                else
                {
                    psi.Arguments = @"/c C:\Windows\System32\psexec.exe -accepteula -s " + hh + @" -h cmd /c RunAsCurrentUser.exe --w --q RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /in /Gw /u /z /n /y" + printer_path;
                }
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
                
            }
            else
            {
                MessageBox.Show("Path cannot be blank");
            }
        }


        private void remove_Button_Click(object sender, EventArgs e)
        {
            if (path_textBox.Text != "")
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
                MessageBox.Show(hh);
                string printer_path = string.Format("\"{0}\"", this.path_textBox.Text);
                MessageBox.Show(printer_path);
                psi.Arguments = @"/c C:\Windows\System32\psexec.exe -accepteula -s " + hh + @" -h cmd /c RunAsCurrentUser.exe --w --q RUNDLL32.EXE PRINTUI.DLL,PrintUIEntry /dn /Gw /n " + printer_path;
                process.StartInfo = psi;
                process.Start();
                process.WaitForExit();
                FileSystem.DeleteFile("\\\\" + comp + "\\C$\\Windows\\System32\\RunAsCurrentUser.exe");
            }
            else
            {
                MessageBox.Show("Path cannot be blank");
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            bw2.DoWork += bw_refresh;
            bw2.RunWorkerAsync();
           
        }

        public void bw_refresh(object sender, EventArgs e)
        {
            try
            {
                Action a = () => listView1.View = View.Details;
                if(listView1.InvokeRequired)
                {
                    listView1.Invoke(a);
                }
                else
                {
                    listView1.View = View.Details;
                }



                ManagementScope scope = new ManagementScope("\\\\" + comp + "\\root\\cimv2");
                scope.Connect();
             

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

                HashSet<string> u = UserWithDomain();
                string user = u.ElementAt(0);
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  The_Admin_Toolbox.TheAdminToolBox.domain);
                UserPrincipal sid = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, user);
                string usersid = sid.Sid.ToString();

                string remoteName = comp;
                RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                RegistryKey printers = environmentKey.OpenSubKey("Printers");
                RegistryKey connections = printers.OpenSubKey("Connections");

                if (connections != null)
                {
                    string[] lists = connections.GetSubKeyNames();
                    Action a2 = () => listView1.Clear();
                    if (listView1.InvokeRequired)
                    {
                        listView1.Invoke(a2);
                    }
                    else
                    {
                        listView1.Clear();
                    }
                    foreach (string n in lists)
                    {
                        //listBox1.Items.Add("Drive Letter: " + n + "\tPath: " + connections.OpenSubKey(n).GetValue("RemotePath").ToString());
                        //Add items in the listview
                        string[] arr = new string[1];
                        ListViewItem itm;

                        //Add first item

                        arr[0] = n.Replace(",", @"\").ToString();

                        itm = new ListViewItem(arr);
                        Action a3 = () => listView1.Items.Add(itm);
                        Action a4 = () => listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (listView1.InvokeRequired)
                        {
                            listView1.Invoke(a3);
                            listView1.Invoke(a4);
                        }
                        else
                        {
                            listView1.Items.Add(itm);
                            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        }

                    }
                   
                }
                else
                {
                    ListViewItem itm;
                    itm = new ListViewItem("No printers");
                    Action a5 = () => listView1.Items.Add(itm);
                   
                    Action a6 = () => listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                 
                    if (listView1.InvokeRequired)
                    {
                        listView1.Invoke(a5);
                        listView1.Invoke(a6);
                    }
                    else
                    {
                        listView1.Items.Add(itm);
                        listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    }
                }

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject outParams2 = n.InvokeMethod("StopService", null, null);
                    ManagementBaseObject inputArgs = n.GetMethodParameters("ChangeStartMode");
                    inputArgs["startmode"] = "Disabled";
                    ManagementBaseObject outParams = n.InvokeMethod("ChangeStartMode", inputArgs, null);
                }

            }
            catch(Exception err)
            {
                if (bw.CancellationPending == false)
                {
                    this.bw.CancelAsync();
                }

                throw new Exception(err.Message);
            }
        }

        public void initMapPrinters(object sender, EventArgs e)
        {
            try
            {

                Action a = () => { listView1.View = View.Details; };
                if (listView1.InvokeRequired)
                {
                    listView1.Invoke(a);
                }
                else
                {
                    listView1.View = View.Details;
                }
                ManagementScope scope = new ManagementScope("\\\\" + comp + "\\root\\cimv2");
                scope.Connect();
                /*
                ManagementScope scope = new ManagementScope("\\\\" + comp + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery0 =
                new WqlObjectQuery("SELECT * FROM Win32_Process WHERE Name LIKE 'explorer.exe'");
                ManagementObjectSearcher searcher0 =
                    new ManagementObjectSearcher(scope, wqlQuery0);
                string user = "";
                List<string> users = new List<string>();
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
                */
              


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


      
                HashSet<string> u = UserWithDomain();
                string user = u.ElementAt(0);
                PrincipalContext domainContext = new PrincipalContext(ContextType.Domain,
                                                                  The_Admin_Toolbox.TheAdminToolBox.domain);
                UserPrincipal sid = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, user);
                string usersid = sid.Sid.ToString();


                string remoteName = comp;
                RegistryKey environmentKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, remoteName).OpenSubKey(usersid);
                RegistryKey printers = environmentKey.OpenSubKey("Printers");
                RegistryKey connections = printers.OpenSubKey("Connections");

                if (connections != null)
                {
                    string[] lists = connections.GetSubKeyNames();

                    foreach (string n in lists)
                    {
                        //listBox1.Items.Add("Drive Letter: " + n + "\tPath: " + connections.OpenSubKey(n).GetValue("RemotePath").ToString());
                        //Add items in the listview
                        string[] arr = new string[1];
                        ListViewItem itm;

                        //Add first item

                        arr[0] = n.Replace(",", @"\").ToString();

                        itm = new ListViewItem(arr);
                       Action a2 = () => listView1.Items.Add(itm);
                        Action a3 = () => listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        if (listView1.InvokeRequired)
                        {
                            listView1.Invoke(a2);
                            listView1.Invoke(a3);
                        }
                        else
                        {
                            listView1.Items.Add(itm);
                            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                        }
                        Application.DoEvents();
                     
                    }
                }
                else
                {
                    ListViewItem itm;
                    itm = new ListViewItem("No printers");
                    Action a4 = () => listView1.Items.Add(itm);
                    listView1.Invoke(a4);
                    Action a5 = () => listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                    listView1.Invoke(a5);
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
                //MessageBox.Show(err.ToString());
                if(bw.CancellationPending == false)
                {
                    this.bw.CancelAsync();
                }
               
                throw new Exception(err.Message);
            }
        }

        private void MapPrinter_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bw.CancellationPending == false)
            {
                this.bw.CancelAsync();
            }
        }

        public HashSet<string> UserWithDomain()
        {
            HashSet<String> user = new HashSet<string>();
            string path = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Authentication\LogonUI\SessionData";
            using (RegistryKey regKey = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, comp,  RegistryView.Registry64).OpenSubKey(path))
            {
                if (regKey != null)
                {
                    string[] valueNames = regKey.GetSubKeyNames();
                    for (int i = 0; i < valueNames.Length; i++)
                    {
                        using (RegistryKey key = regKey.OpenSubKey(valueNames[i], true))
                        {
                            string[] names = key.GetValueNames();
                            for (int e = 0; e < names.Length; e++)
                            {
                                if (names[e] == "LoggedOnSAMUser")
                                {
                                    user.Add(key.GetValue("LoggedOnSAMUser").ToString());
                                }
                            }
                        }
                    }
                }
            }
            return user;
        }
    }
}
