using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WaterSystem
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login login = new Login();
            login.ShowDialog();
            if (login.getResult())
            {
                Welcome welcome = new Welcome();
                welcome.ShowDialog();
                Application.Run(new Main(login.GetUser()));
            }
        }
    }
}
