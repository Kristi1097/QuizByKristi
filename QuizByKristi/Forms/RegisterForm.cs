using QuizByKristi.Exceptions;
using QuizByKristi.Managers;
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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void TbUserName_Click(object sender, EventArgs e)
        {
            lblUserName.ForeColor = SystemColors.ControlText;
        }

        private void TbPassword_Click(object sender, EventArgs e)
        {
            lblPassword.ForeColor = SystemColors.ControlText;
        }

        private void TbRepeatedPassword_Click(object sender, EventArgs e)
        {
            lblRepeatedPassword.ForeColor = SystemColors.ControlText;
        }

       /* private void BtnRegister_Click(object sender, EventArgs e)
        {
            string username = tbUserName.Text = tbUserName.Text.Trim().Replace("_", "-").Replace("#", "-");
            string password = tbPassword.Text = tbPassword.Text.Trim();
            string repeatedPassword = tbRepeatedPassword.Text = tbRepeatedPassword.Text.Trim();

            bool completed = true;
            if (string.IsNullOrEmpty(username))
            {
                lblUserName.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                lblPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(repeatedPassword))
            {
                lblRepeatedPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (!password.Equals(repeatedPassword))
            {
                lblPassword.ForeColor = lblRepeatedPassword.ForeColor = Color.Red;
                MessageBox.Show("Lozinke se ne poklapaju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Text = tbRepeatedPassword.Text = "";
                completed = false;
            }

            if (completed)
            {
                string hashAlgorythm = "-md5";
               
                try
                {
                    UserManager.Register(username, password, hashAlgorythm, tbEncrypt.ToString());
                    MessageBox.Show("Uspješno ste se registrovali! Možete da se prijavite na sistem pomoću korisničkog imena: " + tbUserName.Text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (QbKException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom registracije na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                    this.Close();
                }

            }
        }*/

        private void BtnRegister_Click_1(object sender, EventArgs e)
        {
            string username = tbUserName.Text = tbUserName.Text.Trim().Replace("_", "-").Replace("#", "-");
            string password = tbPassword.Text = tbPassword.Text.Trim();
            string repeatedPassword = tbRepeatedPassword.Text = tbRepeatedPassword.Text.Trim();

            bool completed = true;
            if (string.IsNullOrEmpty(username))
            {
                lblUserName.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(password))
            {
                lblPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (string.IsNullOrEmpty(repeatedPassword))
            {
                lblRepeatedPassword.ForeColor = Color.Red;
                completed = false;
            }
            if (!password.Equals(repeatedPassword))
            {
                lblPassword.ForeColor = lblRepeatedPassword.ForeColor = Color.Red;
                MessageBox.Show("Lozinke se ne poklapaju!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbPassword.Text = tbRepeatedPassword.Text = "";
                completed = false;
            }

            if (completed)
            {
               // string hashAlgorythm = "md5";

                try
                {
                    UserManager.Register(username, password,"1", "aes-256-ecb");
                    MessageBox.Show("Uspješno ste se registrovali! Možete da se prijavite na sistem pomoću korisničkog imena: " + tbUserName.Text, String.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (QbKException ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Došlo je do greške prilikom registracije na sistem!", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.StackTrace + " : " + ex.Message);
                    this.Close();
                }

            }
        }
    }
 
}
