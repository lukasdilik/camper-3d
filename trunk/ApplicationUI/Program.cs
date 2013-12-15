using System;

namespace ApplicationUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (MainForm form = new MainForm())
            {
                form.Start();
            }
        }
    }
}
