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
using Microsoft.Win32;



namespace The_Admin_Toolbox
{
    public partial class Services : Form
    {
        public void bw_initialization(object sender, DoWorkEventArgs e)
        {
            try
            {
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                    listBoxServices.Invoke(outputt);
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public Services()
        {
            InitializeComponent();
            this.Text = "Services Manager";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_initialization;
            bw.RunWorkerAsync();
        }

        

        string computername = The_Admin_Toolbox.TheAdminToolBox.sendtext;
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void bw_Running(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action clear = () => listBoxServices.Items.Clear();
                listBoxServices.Invoke(clear);
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    if (n["State"].ToString() == "Running")
                    {
                        Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                        listBoxServices.Invoke(outputt);
                    }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void buttonRunning_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Running;
            bw.RunWorkerAsync();
        }


        public void bw_ShowAll(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action clear = () => listBoxServices.Items.Clear();
                listBoxServices.Invoke(clear);
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                    listBoxServices.Invoke(outputt);
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_ShowAll;
            bw.RunWorkerAsync();
        }

        public void bw_Disabled(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action clear = () => listBoxServices.Items.Clear();
                listBoxServices.Invoke(clear);
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    if (n["StartMode"].ToString() == "Disabled")
                    {
                        Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                        listBoxServices.Invoke(outputt);
                    }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }


        private void buttonDisabled_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Disabled;
            bw.RunWorkerAsync();
        }


        public void bw_Stopped(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action clear = () => listBoxServices.Items.Clear();
                listBoxServices.Invoke(clear);
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    if (n["State"].ToString() == "Stopped")
                    {
                        Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                        listBoxServices.Invoke(outputt);
                    }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void buttonStopped_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Disabled;
            bw.RunWorkerAsync();
        }

        public void bw_Auto(object sender, DoWorkEventArgs e)
        {
            try
            {
                Action clear = () => listBoxServices.Items.Clear();
                listBoxServices.Invoke(clear);
                listBoxServices.Sorted = true;
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                    if (n["StartMode"].ToString() == "Auto")
                    {
                        Action outputt = () => listBoxServices.Items.Add(n["DisplayName"]);
                        listBoxServices.Invoke(outputt);
                    }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly); ;
            }
        }

        private void buttonAuto_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Auto;
            bw.RunWorkerAsync();
        }


        public void bw_Start(object sender, EventArgs e)
        {
            try
            {

                string service = "";
                Action o = () => { service = listBoxServices.SelectedItem.ToString(); };
                listBoxServices.Invoke(o);
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject outParams = n.InvokeMethod("StartService", null, null);
                    if (outParams["ReturnValue"].ToString() == "0")
                    {
                        System.Windows.Forms.MessageBox.Show("The service has successfully started!");

                    }
                    else { System.Windows.Forms.MessageBox.Show("The service has not successfully started."); }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Start;
            bw.RunWorkerAsync();
        }


        public void bw_Delete(object sender, DoWorkEventArgs e)
        {
            try
            {
                string service = "";
                Action o = () => { service = listBoxServices.SelectedItem.ToString(); };
                listBoxServices.Invoke(o);
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject inputArgs = n.GetMethodParameters("ChangeStartMode");
                    inputArgs["startmode"] = "Disabled";
                    ManagementBaseObject outParams = n.InvokeMethod("ChangeStartMode", inputArgs, null);
                    if (outParams["ReturnValue"].ToString() == "0")
                    {
                        System.Windows.Forms.MessageBox.Show("The service start mode has been changed to manual!");
                    }
                    else { System.Windows.Forms.MessageBox.Show("The service start mode was not changed."); }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Delete;
            bw.RunWorkerAsync();
        }

        public void bw_Stop(object sender, DoWorkEventArgs e)
        {
            
            try
            {
                string service = "";
                Action o = () => { service = listBoxServices.SelectedItem.ToString(); };
                listBoxServices.Invoke(o);
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject outParams = n.InvokeMethod("StopService", null, null);
                    if (outParams["ReturnValue"].ToString() == "0")
                    {
                        System.Windows.Forms.MessageBox.Show("The service has successfully stopped!");
                    }
                    else { System.Windows.Forms.MessageBox.Show("The service has not successfully stopped."); }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly); ;
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Stop;
            bw.RunWorkerAsync();
        }

        public void bw_Enable(object sender, DoWorkEventArgs e)
        {
            try
            {
                string service = "";
                Action o = () => { service = listBoxServices.SelectedItem.ToString(); };
                listBoxServices.Invoke(o);
                ManagementScope scope = new ManagementScope("\\\\" + computername + "\\root\\cimv2");
                scope.Connect();
                WqlObjectQuery wqlQuery =
                new WqlObjectQuery("SELECT * FROM Win32_Service WHERE DisplayName LIKE '" + service + "'");
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);

                foreach (ManagementObject n in searcher.Get())
                {
                    ManagementBaseObject inputArgs = n.GetMethodParameters("ChangeStartMode");
                    inputArgs["startmode"] = "Manual";
                    ManagementBaseObject outParams = n.InvokeMethod("ChangeStartMode", inputArgs, null);
                    if (outParams["ReturnValue"].ToString() == "0")
                    {
                        System.Windows.Forms.MessageBox.Show("The service start mode has been changed to manual!");
                    }
                    else { System.Windows.Forms.MessageBox.Show("The service start mode was not changed."); }
                }
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);

            }
        }

        private void buttonEnable_Click(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_Enable;
            bw.RunWorkerAsync();
        } 
    }
}
