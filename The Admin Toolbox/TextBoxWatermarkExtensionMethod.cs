using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace The_Admin_Toolbox
{
    
        public static class TextBoxWatermarkExtensionMethod
        {
            private const uint ECM_FIRST = 0x1500;
            private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
            private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, uint wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

            public static void SetWatermark(this TextBox textBox, string watermarkText)
            {
                SendMessage(textBox.Handle, EM_SETCUEBANNER, 0, watermarkText);
            }
    }
}
