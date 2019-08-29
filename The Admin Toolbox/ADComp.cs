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
using System.Net;

namespace The_Admin_Toolbox
{
    class ADComp
    {
        TheAdminToolBox Admin = new TheAdminToolBox();

        public System.Security.SecureString convertToSecureString(string strPassword)
        {
            var secureStr = new System.Security.SecureString();
            if (strPassword.Length > 0)
            {
                foreach (var c in strPassword.ToCharArray()) secureStr.AppendChar(c);
            }
            return secureStr;
        }

        public int RenameRemotePC(String oldName, String newName, String domain, NetworkCredential localaccount, NetworkCredential domainaccount)
        {
            
            var remoteControlObject = new ManagementPath
            {
                ClassName = "Win32_ComputerSystem",
                Server = oldName,
                Path =
                    oldName + "\\root\\cimv2:Win32_ComputerSystem.Name='" + oldName + "'",
                NamespacePath = "\\\\" + oldName + "\\root\\cimv2"
            };

            var conn = new ConnectionOptions
            {
                Authentication = AuthenticationLevel.PacketPrivacy,
                Impersonation = ImpersonationLevel.Impersonate,
                EnablePrivileges = true,
                Username = oldName + "\\" + localaccount.UserName,
                Password = localaccount.Password
            };

            var remoteScope = new ManagementScope(remoteControlObject, conn);
            remoteScope.Connect();
            var remoteSystem = new ManagementObject(remoteScope, remoteControlObject, null);

            ManagementBaseObject newRemoteSystemName = remoteSystem.GetMethodParameters("Rename");
            var methodOptions = new InvokeMethodOptions();

            newRemoteSystemName.SetPropertyValue("Name", newName);
            newRemoteSystemName.SetPropertyValue("UserName", domainaccount.UserName);
            newRemoteSystemName.SetPropertyValue("Password", domainaccount.Password);

            methodOptions.Timeout = new TimeSpan(0, 10, 0);
            ManagementBaseObject outParams = remoteSystem.InvokeMethod("Rename", newRemoteSystemName, null);
            return (int)outParams.Properties["ReturnValue"].Value;
        }

        public int JoinADRemotePC(String Name, String domain, NetworkCredential localaccount, NetworkCredential domainaccount, String OU)
        {
            int JOIN_DOMAIN = 1;
            int ACCT_CREATE = 2;
            var remoteControlObject = new ManagementPath
            {
                ClassName = "Win32_ComputerSystem",
                Server = Name,
                Path = Name + "\\root\\cimv2:Win32_ComputerSystem",
                NamespacePath = "\\\\" + Name + "\\root\\cimv2:Win32_ComputerSystem"
            };

            var conn = new ConnectionOptions
            {
                
                Authentication = AuthenticationLevel.PacketPrivacy,
               // Impersonation = ImpersonationLevel.Impersonate,
               //EnablePrivileges = true,               
                Username = Name + "\\" + localaccount.UserName,
                Password = localaccount.Password
            };

            var remoteScope = new ManagementScope(remoteControlObject, conn);
            remoteScope.Connect();

            var remoteSystem = new ManagementObject(remoteScope, remoteControlObject, null);

            ManagementBaseObject newRemoteSystemName = remoteSystem.GetMethodParameters("JoinDomainorWorkgroup");
            var methodOptions = new InvokeMethodOptions();
            
            newRemoteSystemName.SetPropertyValue("Name", domain);
            newRemoteSystemName.SetPropertyValue("UserName", domainaccount.UserName);
            newRemoteSystemName.SetPropertyValue("Password", domainaccount.Password);
            newRemoteSystemName.SetPropertyValue("AccountOU", OU);
            newRemoteSystemName.SetPropertyValue("FJoinOptions", JOIN_DOMAIN + ACCT_CREATE);

            methodOptions.Timeout = new TimeSpan(0, 10, 0);
            ManagementBaseObject outParams = remoteSystem.InvokeMethod("JoinDomainorWorkgroup", newRemoteSystemName, null);
            return (int)outParams.Properties["ReturnValue"].Value;
        }

    }
}
