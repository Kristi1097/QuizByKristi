using QuizByKristi.Exceptions;
using QuizByKristi.Model;
using QuizByKristi.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizByKristi.Managers
{
    class UserManager
    {
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";
        private static readonly string CERTS_PATH = Application.StartupPath + "\\" + "Certificates";
        private static User currentUser = null;
        private static int caNumber = 1;
        public static void MakeCA(string ca)
        {
            string makeKeysCAcommand = CERTS_PATH + "\\keysScript.bat"+" " + ca;  
            CommandPrompt.ExecuteCommand(makeKeysCAcommand);

            string certsCommand = CERTS_PATH + "\\certsScript.bat" + " " + ca;
            CommandPrompt.ExecuteCommand(certsCommand);

            //string makeCertsCAcommand = CERTS_PATH + "\\certs" + ca + "Script.bat" + " " + ca;
            //CommandPrompt.ExecuteCommand(makeCertsCAcommand);
        }

        public static User GetCurrentUser()
        {
            return currentUser;
        }
        public static void Register(string userName,string password,string hashAlgorythm,string encAlgorythm)
        {
            if (CheckNameExists(userName))
                throw new QbKException("Unijeli ste vec postojece korisnicko ime! Ponovite unos!");
            else
            {
                User user = new User(userName, Passwd(password, hashAlgorythm), hashAlgorythm, encAlgorythm,0);

                string keysCommand = CERTS_PATH + "\\keysScript.bat" + " " + user.Name;
                CommandPrompt.ExecuteCommand(keysCommand);

                //string certsCommand = CERTS_PATH + "\\certsScript.bat" + " " + user.Name;
                //CommandPrompt.ExecuteCommand(certsCommand);

                string makeCertsCAcommand = CERTS_PATH + "\\certsca" + caNumber + "Script.bat" + " " + user.Name;
                CommandPrompt.ExecuteCommand(makeCertsCAcommand);
                if (caNumber == 1)
                    caNumber = 2;
                else
                    caNumber = 1;

                if (File.Exists(CERTS_PATH+"\\private\\"+user.Name+"_private.key")&&
                    File.Exists(CERTS_PATH+"\\certs\\"+user.Name+".crt"))
                {
                    user.WriteInfo();
                }
                else
                {
                    throw new QbKException("Doslo je do greske prilikom registracije!");
                }
            }
        }
        public static User Login(string userName,string password)
        {
            if (!CheckNameExists(userName))
                throw new QbKException("Unijeli ste nepostojece korisnicko ime!");
            else
            {
                User user = ReadUserInfo(userName);
                currentUser = user;
                if (user != null)
                {
                    ValidateCertificate(userName);

                    var passwordHash = Passwd(password, user.HashAlgorythm);
                    if (!passwordHash.Equals(user.Password))
                        throw new QbKException("Pogresna lozinka!");
                    else
                        return user;
                }
                else
                {
                    throw new QbKException("Doslo je do greske prillikom prijave na quiz!");
                }
            }
        }
        public static User ReadUserInfo(string name)
        {
            User user = null;
            string decryptCommand = "openssl aria-256-ecb -d -in Users/" + name + "#.txt -out Users/" + name + ".txt -pbkdf2 -k kriptografija -nosalt -base64";
            CommandPrompt.ExecuteCommand(decryptCommand);
            try
            {
                var path = USERS_PATH + "\\" + name+".txt";
                string[] lines = File.ReadAllLines(path);
                File.Delete(path);
                user = new User(lines[0], lines[1], lines[2], lines[3],Int32.Parse(lines[4]));

            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace+":"+e.Message);
            }
            return user;
        }
        /*public static List<string> GetUsers()
        {
     
            string[] fileEntries = Directory.GetFiles(USERS_PATH);
                List<string> users = new List<string>();
            foreach (var fileName in fileEntries)
                users.Add(fileName);
            return users;
        }*/
        public static void ValidateCertificate(string userName)
        {
            string path = CERTS_PATH + "\\certs\\" + userName + ".crt";
            if (!File.Exists(path))
                throw new QbKException("Nije pronadjen digitalni sertifikat korisnika" + userName + "!");
            else
            {
                var verify1CrtCommand = "openssl verify -CAfile " + CERTS_PATH + "\\certs\\rootca.crt " + path;
                //var verified1 = CommandPrompt.ExecuteCommandWithResponse(verify1CrtCommand);

                var verify2CrtCommand = "openssl verify -CAfile " + CERTS_PATH + "\\certs\\rootca.crt " + path;
                //var verified2 = CommandPrompt.ExecuteCommandWithResponse(verify2CrtCommand);
                //Console.WriteLine(verified1.Contains("OK") + " " + verified2.Contains("OK"));

                //if (!verified1.Contains("OK") && !verified2.Contains("OK"))
                //    throw new QbKException("Digitalni sertifikat korisnika" + userName + "nije izdat od tijela kome se vjeruje!");

                var datesCommand = "openssl x509 -in " + path + " -noout -dates";
                var dates = CommandPrompt.ExecuteCommandWithResponse(datesCommand);
                if (!CheckDates(dates))
                    throw new QbKException("Digitaln isertifikat korisnika" + userName + "nije vazeci!");
                var serialNumberCommand = "openssl x509 -in " + path + " -noout -serial";
                    var serialNumber = CommandPrompt.ExecuteCommandWithResponse(serialNumberCommand).Trim().Substring(7);
                if (IsRevoked(serialNumber))
                    throw new QbKException("Digitalni sertifikat korisnika" + userName + "trenutni je van upotrebe!");
                

            }
        }
        #region
        private static bool CheckNameExists(string name)
        {
            /*bool exists = false;
            string[] fileEntries = Directory.GetFiles(USERS_PATH);
            foreach (string fileName in fileEntries)
                if (Path.GetFileNameWithoutExtension(fileName).Equals(name))
                    exists=true;
            return exists;*/
            DirectoryInfo root = new DirectoryInfo(USERS_PATH);
            var homeDirs = root.GetDirectories();

            bool exists = false;
            foreach (var home in homeDirs)
                if (home.Name.Equals(name))
                    exists = true;

            return exists;
        }
        private static string Passwd(string password,string hashAlgorythm)
        {
            string command = "openssl passwd -" + hashAlgorythm + " -salt password " + password;
            return CommandPrompt.ExecuteCommandWithResponse(command).Trim();

        }
        private static bool CheckDates(string dates)
        {
            string notBefore =dates.Split('\n')[0].Substring(10).Trim().Replace("  ", " ");
            string notAfter = dates.Split('\n')[1].Substring(9).Trim().Replace("  ", " ");
            var partsNotBefore = notBefore.Split(' ');
            var partsNotAfter = notAfter.Split(' ');

            var notBeforeTime = partsNotBefore[2].Split(':');
            var notAfterTime = partsNotAfter[2].Split(':');

            DateTime dateTimeNotBef = new DateTime(int.Parse(partsNotBefore[3]), GetMonth(partsNotBefore[0]), int.Parse(partsNotBefore[1]), int.Parse(notBeforeTime[0]), int.Parse(notBeforeTime[1]), int.Parse(notBeforeTime[2]));
            DateTime dateTimeNotAft = new DateTime(int.Parse(partsNotAfter[3]), GetMonth(partsNotAfter[0]), int.Parse(partsNotAfter[1]), int.Parse(notAfterTime[0]), int.Parse(notAfterTime[1]), int.Parse(notAfterTime[2]));

            return (dateTimeNotBef < DateTime.Now) && (dateTimeNotAft > DateTime.Now);
        }

        private static bool IsRevoked(string serial)
        {
            string indexPath = CERTS_PATH + "\\index.txt";
            var lines = File.ReadAllLines(indexPath);
            foreach (var line in lines)
                if (line.Contains("\t" + serial + "\t") && line.StartsWith("R\t"))
                    return true;

            return false;
        }

        private static int GetMonth(string month)
        {
            int monthNumber = 1;
            switch (month)
            {
                case "Jan":
                    monthNumber = 1;
                    break;
                case "Feb":
                    monthNumber = 2;
                    break;
                case "Mar":
                    monthNumber = 3;
                    break;
                case "Apr":
                    monthNumber = 4;
                    break;
                case "May":
                    monthNumber = 5;
                    break;
                case "Jun":
                    monthNumber = 6;
                    break;
                case "Jul":
                    monthNumber = 7;
                    break;
                case "Aug":
                    monthNumber = 8;
                    break;
                case "Sep":
                    monthNumber = 9;
                    break;
                case "Oct":
                    monthNumber = 10;
                    break;
                case "Nov":
                    monthNumber = 11;
                    break;
                case "Dec":
                    monthNumber = 12;
                    break;
            }

            return monthNumber;
        }

        #endregion
    }
}

