using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;

namespace PassGuard
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {

			bool createdNew;

			//Just one instance of the app running...
			using (Mutex mutex = new Mutex(true, "YourUniqueMutexName", out createdNew))
			{
				if (createdNew)
				{
					Application.EnableVisualStyles();
					Application.SetHighDpiMode(HighDpiMode.SystemAware);
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new mainWindow());
				}
			}


			
        }
    }
}
