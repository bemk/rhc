using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        public const bool DEBUG = true;
<<<<<<< HEAD
        public const string COM_PORT = "COM4"; // This must become an option to select at startup.
=======
        public const string COM_PORT = "COM23"; // This must become an option to select at startup.
>>>>>>> 056f6b745a5d3822f69ff4dbc466957506ff4ec7

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Client());
        }
    }
}
