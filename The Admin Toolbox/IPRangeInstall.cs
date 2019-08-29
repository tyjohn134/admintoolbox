using NetTools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Timers;
using System.Windows.Forms;

namespace The_Admin_Toolbox
{
    public partial class IPRangeInstall : Form
    {
        public IPRangeInstall()
        {
            InitializeComponent();
        }

        private void ipRangeInstallbutton_Click(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += bw_Run;
            bgw.RunWorkerAsync();
            
        }


        public void bw_Run(object source, EventArgs e)
        {
            TheAdminToolBox adm = new TheAdminToolBox();
            string strange = ipStartRangetextBox.Text;
            string endrange = endRangetextBox.Text;
            foreach (var ip in IPAddressRange.Parse(strange + "-" + endrange))
            {
               if(ipListView.InvokeRequired)
                {
                    ipListView.Invoke(new MethodInvoker(delegate
                    {
                       ipListView.Items.Add(ip.ToString());
                        Application.DoEvents();
                        foreach (ListViewItem i in ipListView.Items)
                        {
                            adm.controlDataService("install", ip.ToString());
                            i.SubItems.Add("Finished installing");
                            Action oo = () => ipListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                            ipListView.Invoke(oo);
                        }
                    }));
                }
            }
            Action oo2 = () => ipListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            ipListView.Invoke(oo2);
        }


  

    }
}
