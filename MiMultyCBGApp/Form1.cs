using System;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public partial class Form1 : Form
    {
        private LoginBLL loginBll = new LoginBLL();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User loggedUser = loginBll.AuthenticateUser(txtUsername.Text, txtPassword.Text);

                if (loggedUser != null)
                {
                    MessageBox.Show("Login Berhasil sebagai " + loggedUser.Role);
                    this.Hide();

                    if (loggedUser.Role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
                    {
                        FormBarang formAdmin = new FormBarang();
                        formAdmin.ShowDialog();
                    }
                    else if (loggedUser.Role.Equals("Staff", StringComparison.OrdinalIgnoreCase))
                    {
                        FormStaff formStaff = new FormStaff(loggedUser);
                        formStaff.ShowDialog();
                    }
                    
                    this.Show(); 
                    txtUsername.Clear();
                    txtPassword.Clear();
                }
                else
                {
                    MessageBox.Show("Username atau Password salah!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
