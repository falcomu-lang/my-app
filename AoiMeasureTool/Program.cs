using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AoiMeasureTool
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            ConfigureOpenCvNativePath();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void ConfigureOpenCvNativePath()
        {
            var nativePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dll", "x64");
            if (Directory.Exists(nativePath))
            {
                SetDllDirectory(nativePath);
            }
        }

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SetDllDirectory(string lpPathName);
    }
}
