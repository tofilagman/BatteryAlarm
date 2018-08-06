using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BatteryAlarm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                RegistryKey currentUserRegistry = Registry.CurrentUser;
                RegistryKey runRegistryKey = currentUserRegistry.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);

                if (runRegistryKey != null)
                {
                    if (runRegistryKey.GetValue(Application.ProductName) == null)
                    //{
                    //    runRegistryKey.DeleteValue(Application.ProductName, false);
                    //    MessageBox.Show("Shortcut has been deleted");
                    //}
                    //else
                    {
                        runRegistryKey.SetValue(Application.ProductName, string.Format("{0} -s", Application.ExecutablePath));
                        MessageBox.Show("Shortcut has been set, please sign out and sign to take effect");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

          
        }
    }
}
