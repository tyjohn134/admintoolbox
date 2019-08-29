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


namespace The_Admin_Toolbox
{
    class ComputerInfo
    {

        public static string GetModel(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_ComputerSystemProduct");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string model = "";
            foreach (ManagementObject n in searcher.Get())
            {
                model = n["Version"].ToString();
                
            }
            return model;
        }//end of getmodel

        public static string GetSN(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string sn = "";
            foreach (ManagementObject n in searcher.Get())
            {
                sn = n["SerialNumber"].ToString();

            }
            return sn;
        }//end of getsn

        public static string GetOS(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string os = "";
            foreach (ManagementObject n in searcher.Get())
            {
                os = n["Caption"].ToString();

            }
            return os;
        }//end of getos

        public static string GetServicePack(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string service = "";
            foreach (ManagementObject n in searcher.Get())
            {
                service = n["servicepackmajorversion"].ToString();

            }
            return service;
        }//end of getsn

        public static string GetLastBootUp(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string boottime = ""; 
            foreach (ManagementObject n in searcher.Get())
            {
                boottime = n["LastBootUpTime"].ToString();

            }
            return boottime;
        }//end of getsn

        public static string GetFreeSpace(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("select FreeSpace,Size,Name from Win32_LogicalDisk where DriveType=3");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string freespace = "";
            foreach (ManagementObject n in searcher.Get())
            {
                freespace = n["FreeSpace"].ToString();

            }
            return freespace;
        }//end of getsn

        public static string GetSize(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("select FreeSpace,Size,Name from Win32_LogicalDisk where DriveType=3");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string size = "";
            foreach (ManagementObject n in searcher.Get())
            {
                size = n["Size"].ToString();

            }
            return size;
        }//end of getsize

        public static string GetManufacturer(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string man = "";
            foreach (ManagementObject n in searcher.Get())
            {
                man = n["Manufacturer"].ToString();

            }
            return man;
        }//end of getsn


        public static string GetL2Cache(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string cache = "";
            foreach (ManagementObject n in searcher.Get())
            {
                cache = n["L2CacheSize"].ToString();

            }
            return cache;
        }//end of getsn

        public static string GetName(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string name = "";
            foreach (ManagementObject n in searcher.Get())
            {
                name = n["Name"].ToString();

            }
            return name;
        }//end of gets

        public static string GetCores(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string cores = "";
            foreach (ManagementObject n in searcher.Get())
            {
                cores = n["NumberOfCores"].ToString();

            }
            return cores;
        }//end of gets

        public static string GetStepping(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string step = "";
            foreach (ManagementObject n in searcher.Get())
            {
                step = n["Stepping"].ToString();

            }
            return step;
        }//end of gets

        public static string GetProcessMan(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string manuf = "";
            foreach (ManagementObject n in searcher.Get())
            {
                manuf = n["Manufacturer"].ToString();

            }
            return manuf;
        }//end of gets

        public static string GetProcSpeed(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string speed = "";
            foreach (ManagementObject n in searcher.Get())
            {
                speed = n["CurrentClockSpeed"].ToString();

            }
            return speed;
        }//end of gets

        public static string GetMaxSpeed(string computer)
        {
            ManagementScope scope = new ManagementScope("\\\\" + computer + "\\root\\cimv2");
            scope.Connect();
            WqlObjectQuery wqlQuery =
            new WqlObjectQuery("SELECT * FROM Win32_Processor");
            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(scope, wqlQuery);
            string speed = "";
            foreach (ManagementObject n in searcher.Get())
            {
                speed = n["MaxClockSpeed"].ToString();

            }
            return speed;
        }//end of gets

       
  

    }
}
