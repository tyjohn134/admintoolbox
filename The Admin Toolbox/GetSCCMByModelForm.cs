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
    public partial class GetSCCMByModelForm : Form
    {
        private TheAdminToolBox m_form = null;
        private static string SCCMServer = Properties.Settings.Default["SCCMServer"].ToString();

        public GetSCCMByModelForm(TheAdminToolBox f)
        {
            InitializeComponent();
            m_form = f;
            this.Text = "Get Computer Info By Model";
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        }

        public void bw_getModel(object sender, DoWorkEventArgs e)
        {
            try
            {
                string domain = m_form.getCurrentDomain();
                string query = "";
                string ou = "";
                Action q = null;
                if (domain == "")
                {
                    ou = "";
                    q = () => query = String.Format("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = '{0}' and SMS_R_System.FullDomainName = '{1}'  and SMS_R_System.SystemOUName = '{2}'", modelComboBox.Text, domain, ou);
                    modelComboBox.Invoke(q);
                }
                else if (domain == "")
                {
                    ou = "";
                    q = () => query = String.Format("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = '{0}' and SMS_R_System.FullDomainName = '{1}' and SMS_R_System.SystemOUName = '{2}'", modelComboBox.Text, domain, ou);
                }
                else
                {
                    q = () => query = String.Format("select SMS_R_System.Name, SMS_R_System.LastLogonUserName, SMS_R_System.LastLogonTimestamp from  SMS_R_System inner join SMS_G_System_COMPUTER_SYSTEM_PRODUCT on SMS_G_System_COMPUTER_SYSTEM_PRODUCT.ResourceID = SMS_R_System.ResourceId where SMS_G_System_COMPUTER_SYSTEM_PRODUCT.Version = '{0}' and SMS_R_System.FullDomainName = '{1}'", modelComboBox.Text, domain);
                }
                Action outputt = () => m_form.OutputBox.AppendText(modelComboBox.Text + " computers in the " + domain +  " domain: \r\n################################\r\n");
                m_form.OutputBox.Invoke(outputt);
                ManagementScope scope = new ManagementScope("\\\\" + SCCMServer + "\\root\\sms\\site_GG3");
                scope.Connect();
                modelComboBox.Invoke(q);
               
                WqlObjectQuery wqlQuery = new WqlObjectQuery(query);
                List<string> list = new List<string>();
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher(scope, wqlQuery);
                foreach (ManagementObject n in searcher.Get())
                {
                   
                    list.Add(n["Name"].ToString());
                    if (!(object.ReferenceEquals(null, n["LastLogonTimeStamp"])))
                    {
                        string lastlogon = n["LastLogonTimeStamp"].ToString();
                        DateTime final = DateTime.ParseExact(lastlogon.Split('.')[0], "yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);

                        Action output = () => m_form.OutputBox.AppendText("Computer Name: " + n["Name"] + "\r\n" + "Last Logged On User: " + n["LastLogonUserName"] + "\r\n" + "Last Logon Date: " + final.ToString() + "\r\n" + "\r\n");
                        m_form.OutputBox.Invoke(output);
                    }
                }
                Action output3 = () => m_form.OutputBox.AppendText("\r\n" + "\r\n" + "Count: " + list.Count.ToString() + "\r\n" + "#####################" + "\r\n");
                m_form.OutputBox.Invoke(output3);
            }
            catch (SystemException err)
            {
                System.Windows.Forms.MessageBox.Show(err.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly);
            }
        }

      
        private void GetModelButton_Click(object sender, EventArgs e)
        {
            string value = modelComboBox.Text;


            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += bw_getModel;
            bw.RunWorkerAsync();

        }
    }
}
