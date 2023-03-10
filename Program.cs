using QuizByKristi.Forms;
using QuizByKristi.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizByKristi
{
    static class Program
    {
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";
        private static readonly string CERTS_PATH = System.IO.Directory.GetCurrentDirectory() + "\\" + "Certificates";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MakeDirectories();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
        private static void MakeDirectories()
        {
            if (!Directory.Exists(CERTS_PATH))
                throw new Exception("Za pokretanje aplikacije potrebno je da postoji okruzenje za CA tijelo smjesteno u folderu \"Certificates\"");
            if (!Directory.Exists(USERS_PATH))
                Directory.CreateDirectory(USERS_PATH);
            if (!Directory.Exists(CERTS_PATH + "\\certs\\ca1.crt"))
                UserManager.MakeCA("ca1");
            if (!Directory.Exists(CERTS_PATH + "\\certs\\ca2.crt" ))
                UserManager.MakeCA("ca2");
        }
    }
}
