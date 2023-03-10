using QuizByKristi.Exceptions;
using QuizByKristi.Forms;
using QuizByKristi.Model;
using QuizByKristi.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizByKristi.Managers
{
    class QuizManager
    {
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";
        private static readonly string CERTS_PATH = Application.StartupPath + "\\" + "Certificates";
        private static readonly string QUESTION_PATH = Application.StartupPath + "\\" + "Questions";
        private static readonly string IMAGE_PATH = Application.StartupPath + "\\" + "Images";


        public static List<int> numberOfQuestion=new List<int>();
        #region Questions
        private static void EncryptQuestion(int numberOfQuestion,string []question)
        {
            string inFile = QUESTION_PATH + "\\Question" + numberOfQuestion.ToString() + ".txt";
            File.WriteAllLines(inFile, question);
            string outFile = QUESTION_PATH + "\\Question###" + numberOfQuestion.ToString() + ".txt";
            //string outFile = inFile.Insert(inFile.LastIndexOf("\\") + 1, "###");
            string key = "kristina";
            string cryptQuestionCommand = "openssl enc -aes-256-cbc -e -in "+inFile+" -out "+outFile+" -nosalt -k "+key+" -base64";
            CommandPrompt.ExecuteCommandWithResponse(cryptQuestionCommand);

            //File.Delete(inFile);

            try { HideQuestion(numberOfQuestion, new FileInfo(outFile)); }
            catch(QbKException ex)
            {
                MessageBox.Show(ex.Message, "Nije skriveno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
        private static void HideQuestion(int numberOfQuestion,FileInfo question)
        {
            string image =IMAGE_PATH + "\\Image" + numberOfQuestion.ToString() + ".jpg";
            //skrivanje pitanja u sliku
            
            string hideCommand = "steghide embed -cf " + image + " -ef " + question.FullName+ " -p kristina";
            
            CommandPrompt.ExecuteCommand(hideCommand);
            
            //question.Delete();
        }
        public static void ReadQuestion(int numberOfQuestion)
        {

            //citanje pitanja iz slike
            Console.WriteLine(numberOfQuestion);
            string decryptedFile =QUESTION_PATH + "\\#Question" + numberOfQuestion.ToString()+".txt";
            //decryptedFile.Insert(decryptedFile.LastIndexOf("\\") + 1, "#");
            string imagePath = IMAGE_PATH + "\\Image" + numberOfQuestion.ToString() + ".jpg";
            string extractFromPictureCommand = "steghide extract -sf " + imagePath + " -xf "+decryptedFile +" -p kristina";
            Console.WriteLine(extractFromPictureCommand);
            CommandPrompt.ExecuteCommand(extractFromPictureCommand);
            Console.WriteLine(extractFromPictureCommand);
            //dekripcija pitanja
            string qPath =QUESTION_PATH+"\\Question"+numberOfQuestion.ToString()+".txt";
            string key = "kristina";
            string decryptQuestionCommand = "openssl enc -aes-256-cbc -d -in " + decryptedFile+ " -out " + qPath + " -nosalt -k " + key+" -base64";
            CommandPrompt.ExecuteCommandWithResponse(decryptQuestionCommand);
            File.Delete(decryptedFile);
        }
        private static void ChooseQuestions()
        {
            Random rand = new Random();
            for(int ctrl=0;ctrl<=4;ctrl++)
            {
                int temp = rand.Next(20)+1;
                if (!numberOfQuestion.Contains(temp))
                {
                    numberOfQuestion.Add(temp);
                }
                else
                    ctrl--;
                //bool same = false;
                 /*   if (numberOfQuestion.Contains(temp))
                    {
                    temp = rand.Next(21);
                    numberOfQuestion[ctrl] = temp; ctrl++;
                }
               else
                {
                    numberOfQuestion[ctrl] = temp;
                }*/

            }      
        }

        #endregion
        public static void EncryptingQuestions()
        {
            List<string[]> questions = new List<string[]> { };
            //string[][] questions;
            questions.Add(new string[6] { "Koliko je dug trenutak?", "60s", "90s", "120s", "180s", "90s" });
            questions.Add(new string[6] { "Koliko je trajao stogodisnji rat?", "50", "100", "101", "116", "116" });
            questions.Add(new string[6] { "Koja ptica moze letjeti unazad?", "papiga", "kolibric", "progutati", "caplja", "kolibric" });
            questions.Add(new string[6] { "Koje popularno morsko stvorenje ima moc kloniranja?", "meduza", "hobotnica", "morski konj", "lignje", "meduza" });
            questions.Add(new string[6] { "Koliko kostiju imaju morski psi?", "1000", "10", "2", "0", "0" });
            questions.Add(new string[6] { "Gdje se najvise pije koka kola?", "SAD", "Kanada", "Island", "Holandija", "Island" });
            questions.Add(new string[2] { "Što ide gore-dolje, ali uvijek ostaje na istom mjestu?", "Stepenice" });
            questions.Add(new string[2] { "Gdje je ocean bez vode?", "Na karti" });
            questions.Add(new string[2] { "Gdje se nalazi najmanja kost kod covjeka?", "Uho" });
            questions.Add(new string[6] { "Gdje se nalazi muzej Prado?", "Prag", "Pariz", "Madrid", "Lisabon", "Madrid" });
            questions.Add(new string[2] { "Po kojoj je vrsti alkohola Rusija poznata?", "Votka" });
            questions.Add(new string[6] { "Gdje se nalazi muzej Prado?", "Prag", "Pariz", "Madrid", "Lisabon", "Madrid" });
            questions.Add(new string[2] { "Pulque je pivo od čega se pravi?", "Kaktusa" });
            questions.Add(new string[6] { "Koje godine je igra prijestolja premijerno prikazana na HBO-u?", "2010", "2011", "2012", "2009", "2011" });
            questions.Add(new string[2] { "Koji je tehnički naziv simbola hashtaga?", "Oktotorp" });
            questions.Add(new string[2] { "Koji se sport igrao na Mjesecu?", "Golf" });
            questions.Add(new string[2] { "Koja država ima najviše tornada po površini?", "Engleska" });
            questions.Add(new string[2] { "Koja se životinja može vidjeti na Porscheovom logotipu?", "Konj" });
            questions.Add(new string[2] { "U kojem je gradu potpisan mirovni ugovor kojim je okončan Vijetnamski rat?", "Pariz" });
            questions.Add(new string[6] { "Koliko dana prosečna osoba provede spavajući u toku jedne godine?", "97", "99", "104", "122", "122" });

            for (int q = 1; q <= 20; q++)
            {
                //foreach(string question in questions[q])
                EncryptQuestion(q, questions[q - 1]);
            }
            string path = QUESTION_PATH + "\\Encrypted.txt";
            if (!File.Exists(path))
                File.WriteAllText(path, "e");
                }
        public static void StartGame()
        {
            string path = QUESTION_PATH + "\\Encrypted.txt";
            string enc = File.ReadAllText(path);
           // User usr = UserManager.GetCurrentUser();
           // if (usr.NumberOfPlaying==3)

            if (!enc.Equals("e"))
            { EncryptingQuestions(); }
            else
            {
                ChooseQuestions();
                new QuestionForm(numberOfQuestion[0]).Show();
            }
        }
    }
}
