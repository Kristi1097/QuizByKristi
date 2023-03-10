using QuizByKristi.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizByKristi.Model
{
    public class User
    {
        private readonly string name;
        private readonly string password;
        private readonly string hashAlgorythm;
        private readonly string encAlgorythm;
        private readonly int numberOfPlaying;

        public User(string name,string password,string hashAlgorythm,string encAlgorythm,int numberOfPlaying)
        {
            this.name = name;
            this.password = password;
            this.hashAlgorythm = hashAlgorythm;
            this.encAlgorythm = encAlgorythm;
            this.numberOfPlaying = numberOfPlaying;

        }
        public override bool Equals(object obj)
        {
            return obj is User user &&
                Name == user.Name &&
                Password == user.Password &&
                HashAlgorythm == user.HashAlgorythm &&
                EncAlgorythm == user.EncAlgorythm &&
                numberOfPlaying.ToString() == user.NumberOfPlaying;
        }
        public void WriteInfo()
        {
            CreateHome();
        
            DirectoryInfo users = new DirectoryInfo(Application.StartupPath + "\\Users");
            string[] lines = {Name,Password,hashAlgorythm,EncAlgorythm,NumberOfPlaying.ToString()};
            try
            {
                File.WriteAllLines(users.FullName + "\\" + Name + ".txt", lines);
                string encryptCommand = "openssl aria-256-ecb -in Users/" + Name + ".txt -out Users/" + Name + "#.txt -pbkdf2 -k kriptografija -nosalt -base64";
                CommandPrompt.ExecuteCommand(encryptCommand);

                File.Delete(users.FullName + "\\" + Name + ".txt");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace + ":" + e.Message);
            }
        }
        private void CreateHome()
        {
            DirectoryInfo home = new DirectoryInfo(Application.StartupPath + "\\Users\\" + Name);
            try
            {
                if (!home.Exists)
                { 
                    home.Create();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace + ":" + e.Message);
            }
        }
        public override int GetHashCode()
        {
            int hashCode = -286654916;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + HashAlgorythm.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(EncAlgorythm);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NumberOfPlaying);
            return hashCode;
        }
        public string Name => name;
        public string Password => password;
        public string HashAlgorythm => hashAlgorythm;
        public string EncAlgorythm => encAlgorythm;
        public string NumberOfPlaying => numberOfPlaying.ToString();

    }
}
