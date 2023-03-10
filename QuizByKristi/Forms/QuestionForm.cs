using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuizByKristi.Managers;
using QuizByKristi.Model;

namespace QuizByKristi.Forms
{
    public partial class QuestionForm : Form
    {
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";
        private static readonly string QUESTION_PATH = Application.StartupPath + "\\" + "Questions";
        private bool button1Clicked = false;
        private bool button2Clicked = false;
        private bool button3Clicked = false;
        private bool button4Clicked = false;
        private int i = 0;
        private int result;
        Stopwatch timeOfPlaying = new Stopwatch();

        public QuestionForm(int numberOfQuestion)
        {
            InitializeComponent();
            timeOfPlaying.Start();
            QuizManager.ReadQuestion(numberOfQuestion);
            ShowQuestion(numberOfQuestion);
        }
       
        private void button2_Click(object sender, EventArgs e)
        {
            button2Clicked = true;

                button2.BackColor = Color.GreenYellow;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3Clicked = true;
    
                button3.BackColor = Color.GreenYellow;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4Clicked = true;           
                button4.BackColor = Color.GreenYellow;
        }

        public void ShowQuestion(int numberOfQuestion)
        {
            button1Clicked = false;
            button2Clicked = false;
            button3Clicked = false;
            button4Clicked = false;
            button1.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            button2.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            button3.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            button4.BackColor = Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
           
            i++;
            string qPath = QUESTION_PATH + "\\Question" + numberOfQuestion.ToString() + ".txt";
            string[] lines = File.ReadAllLines(qPath);
            File.Delete(qPath);
            int numberOfLines = lines.Count();
            if (numberOfLines < 3)
            {
                lblQuestion.Text = lines[0];
                string correctAnswer = lines[1];
                tbAnswer.Visible = true ;
               
                Console.WriteLine(tbAnswer.Text.Equals(correctAnswer));
                if (tbAnswer.Text.Equals(correctAnswer))
                {
                    
                    result += 5;
                    Console.WriteLine(result+" rezultat");
                }
                tbAnswer.Clear();
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
            }
            else {
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                tbAnswer.Visible = false;
                lblQuestion.Text = lines[0];
                button1.Text = lines[1];
                button2.Text = lines[2];
                button3.Text = lines[3];
                button4.Text = lines[4];
                int correctAnswer = CheckAnswer(lines[5]);
                Console.WriteLine(CheckAnswer(lines[5])+"odg");
                if (button1Clicked && correctAnswer == 1)
                {
                    result += 5;
                }
                else if (button2Clicked && correctAnswer == 2)
                {
                    result += 5;
                }
                else if (button3Clicked && correctAnswer == 3)
                {
                    result += 5;
                }
                else if (button4Clicked && correctAnswer == 4)
                {
                    result += 5;
                }
                else result += 0;
                /*if (button1Clicked && correctAnswer == 1)
                      button1.BackColor = Color.Red;
                  if (button2Clicked && correctAnswer == 2)
                      button2.BackColor = Color.Red;
                  if (button3Clicked && correctAnswer == 3)
                      button3.BackColor = Color.Red;
                  if (button4Clicked && correctAnswer == 4)
                      button4.BackColor = Color.Red;*/

                /*switch (correctAnswer)
                {
                    case 1:
                        if (button1Clicked)
                        {
                            result += 5;
                        }
                        break;
                    case 2:
                        if (button2Clicked)
                        { 
                        result += 5;
                        }
                        break;
                    case 3:
                        if (button3Clicked)
                        {
                            result += 5;
                 
                        }
                            break;
                    case 4:
                        if (button4Clicked)
                        {
                            result += 5;
                          
                        }
                            break;

                }*/
                Console.WriteLine(result);
            }
        }

        private int CheckAnswer(string correct)
        {
            if (button1.Text.Equals(correct))
                return 1;
            if (button2.Text.Equals(correct))
                return 2;
            if (button3.Text.Equals(correct))
                return 3;
            if (button4.Text.Equals(correct))
                return 4;
            return 0;
        }
        private void AddUserResult(User user)
        {
            string path = USERS_PATH + "\\Results.txt"; 
            string content = user.Name + " " + timeOfPlaying.Elapsed + " " +result; 
            File.AppendAllText(path,content +Environment.NewLine);
            
        }
        private void btnNext_Click(object sender, EventArgs e)
        {

            button1Clicked = false;
            button2Clicked = false;
            button3Clicked = false;
            button4Clicked = false;
            bool isFive = false;
            if (i == 5)
            {
                timeOfPlaying.Stop();
                AddUserResult(UserManager.GetCurrentUser());
                new ResultsForm().Show();
                this.Close();
                isFive = true;
            }
            if (isFive == false)
            { 
                Console.WriteLine(QuizManager.numberOfQuestion[i]);
                QuizManager.ReadQuestion(QuizManager.numberOfQuestion[i]);
                ShowQuestion(QuizManager.numberOfQuestion[i]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1Clicked = true;

            button1.BackColor = Color.GreenYellow;

        }
    }
}
    
