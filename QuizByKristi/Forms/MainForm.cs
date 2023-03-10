using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuizByKristi.Managers;

namespace QuizByKristi.Forms
{
    public partial class MainForm : Form
    {
        private readonly string username;
        public MainForm(string currentUser)
        {
            InitializeComponent();
            Text=username= currentUser;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            QuizManager.StartGame();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Form nlog = new LoginForm();
            nlog.ShowDialog();
            
        }


        /* private void btnLogout_Click(object sender, EventArgs e)
         {
             Form log = new LoginForm();
             log.ShowDialog();
             this.Close();
         }
        */

    }
}
