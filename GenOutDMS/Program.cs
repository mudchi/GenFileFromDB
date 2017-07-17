using COMMON;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GenOutDMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo(AppConfig.DEFAULTCULTURE);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            //"11/05/2017" "DMSFTU" "WEBCALL" "GENINBT" "C:\Users\chirayut.w\Desktop\Test" "DMSFTU_*_{0}.txt"
            if (args.Length > 0)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new formMain(args[0], args[1], args[2].ToUpper(), args[3], args[4], args[5]));
            }
        }
    }
}
