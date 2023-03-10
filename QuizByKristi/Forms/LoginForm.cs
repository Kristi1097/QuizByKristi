using QuizByKristi.Exceptions;
using QuizByKristi.Managers;
using QuizByKristi.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizByKristi.Forms
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void TbUserName_Click(object sender, EventArgs e)
        {
            lblUserName.ForeColor = SystemColors.ControlText;
        }

        private void TbPasssword_Click(object sender, EventArgs e)
        {
            lblUserName.ForeColor = SystemColors.ControlText;

        }
 

        private void LblRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form my = new RegisterForm();
            my.ShowDialog();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text = tbUserName.Text.Trim();
            string password = tbPassword.Text = tbPassword.Text.Trim();

            if (string.IsNullOrEmpty(username))
            {
                lblUserName.ForeColor = Color.Red;
            }
            if (string.IsNullOrEmpty(password))
            {
                lblPassword.ForeColor = Color.Red;
            }

            if (!(string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)))
            {
                try
                {
                    User currentUser = UserManager.Login(username, password);
                    new MainForm(currentUser.Name).Show();
                    this.Hide();
                   
                }
                catch (QbKException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom prijave na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                }
            }
        }
    }
}
