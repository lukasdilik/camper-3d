using System;
using System.Windows.Forms;

namespace ApplicationUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static MainForm ActiveMainForm;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ActiveMainForm = new MainForm();
            Application.Run(ActiveMainForm);
        }
    }
}
