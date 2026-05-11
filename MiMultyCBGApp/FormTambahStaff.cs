using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;

namespace MiMultyCBGApp
{
    public class FormTambahStaff : Form
    {
        private TextBox txtUser;
        private TextBox txtPass;
        private TextBox txtIdCabang;
        private Button btnSimpan;
        private BarangBLL bll = new BarangBLL();

        public FormTambahStaff()
        {
            this.Text = "Tambah Staff";
            this.Size = new Size(300, 220);
            this.StartPosition = FormStartPosition.CenterParent;

            Label l1 = new Label() { Text = "Username:", Location = new Point(20, 20), Width = 80 };
            txtUser = new TextBox() { Location = new Point(110, 20), Width = 140, MaxLength = 50 };

            Label l2 = new Label() { Text = "Password:", Location = new Point(20, 60), Width = 80 };
            txtPass = new TextBox() { Location = new Point(110, 60), Width = 140, UseSystemPasswordChar = true, MaxLength = 100 };

            Label l3 = new Label() { Text = "ID Cabang:", Location = new Point(20, 100), Width = 80 };
            txtIdCabang = new TextBox() { Location = new Point(110, 100), Width = 140 };
            txtIdCabang.KeyPress += NumericOnly_KeyPress;

            btnSimpan = new Button() { Text = "Simpan", Location = new Point(110, 140), Width = 100, BackColor = Color.LightBlue };
            btnSimpan.Click += BtnSimpan_Click;

            this.Controls.Add(l1);
            this.Controls.Add(txtUser);
            this.Controls.Add(l2);
            this.Controls.Add(txtPass);
            this.Controls.Add(l3);
            this.Controls.Add(txtIdCabang);
            this.Controls.Add(btnSimpan);
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text) || string.IsNullOrWhiteSpace(txtIdCabang.Text))
            {
                MessageBox.Show("Semua data harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bll.InsertStaff(txtUser.Text, txtPass.Text, int.Parse(txtIdCabang.Text));
                MessageBox.Show("Staff berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
