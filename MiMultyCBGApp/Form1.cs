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
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                User loggedUser = loginBll.AuthenticateUser(txtUsername.Text, txtPassword.Text);

                if (loggedUser != null)
                {
                    MessageBox.Show("Login Berhasil sebagai " + loggedUser.Role, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("Username atau Password salah!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
