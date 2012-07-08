using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;

namespace GestureRecognition
{
    class Program
    {

        public static Logger logger = LogManager.GetCurrentClassLogger();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()); 
        }
    }
}

//logger.Trace("Sample trace message");
//logger.Debug("Sample debug message");
//logger.Info("Sample informational message");
//logger.Warn("Sample warning message");
//logger.Error("Sample error message");
//logger.Fatal("Sample fatal error message");   
