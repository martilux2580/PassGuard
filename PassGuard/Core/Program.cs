using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Threading;
using System.Runtime.Versioning;

namespace PassGuard
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
		[SupportedOSPlatform("windows")]
		static void Main()
        {
			//Just one instance of the app running...
			using Mutex mutex = new(true, "UniqueMutex", out bool createdNew);
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
