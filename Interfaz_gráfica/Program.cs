//Program.cs
using System;
using System.Windows.Forms;

namespace Interfaz_gráfica
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Para personalizar la configuración de la aplicación (DPI, fuente, etc.)
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
