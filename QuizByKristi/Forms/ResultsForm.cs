using QuizByKristi.Managers;
using QuizByKristi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace QuizByKristi.Forms
{
    public partial class ResultsForm : Form
    {
        private static readonly string USERS_PATH = Application.StartupPath + "\\" + "Users";

        public ResultsForm()
        {
            InitializeComponent();
        }


        private void ResultsForm_Load(object sender, EventArgs e)
        {
            string path = USERS_PATH + "\\Results.txt";
           
            string[] lines = File.ReadAllLines(path);
            foreach (var line in lines)
            {
                listBox1.Items.Add(line);
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
