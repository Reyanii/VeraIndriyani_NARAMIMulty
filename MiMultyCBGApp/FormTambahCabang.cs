using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;

namespace MiMultyCBGApp
{
    public class FormTambahCabang : Form
    {
        private TextBox txtNamaCabang;
        private TextBox txtAlamat;
        private Button btnTambah;
        private BarangBLL bll = new BarangBLL();

        public FormTambahCabang()
        {
            this.Text = "Tambah Cabang";
            this.Size = new Size(320, 230);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label l1 = new Label() { Text = "Nama Cabang:", Location = new Point(20, 25), Width = 100 };
            txtNamaCabang = new TextBox() { Location = new Point(130, 22), Width = 150 };

            Label l2 = new Label() { Text = "Alamat:", Location = new Point(20, 65), Width = 100 };
            txtAlamat = new TextBox() { Location = new Point(130, 62), Width = 150, Multiline = true, Height = 60 };

            btnTambah = new Button()
            {
                Text = "Tambah",
                Location = new Point(130, 140),
                Width = 100,
                BackColor = Color.LightGreen
            };
            btnTambah.Click += BtnTambah_Click;

            this.Controls.Add(l1);
            this.Controls.Add(txtNamaCabang);
            this.Controls.Add(l2);
            this.Controls.Add(txtAlamat);
            this.Controls.Add(btnTambah);
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNamaCabang.Text))
            {
                MessageBox.Show("Nama Cabang harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bll.InsertCabang(txtNamaCabang.Text.Trim(), txtAlamat.Text.Trim());
                MessageBox.Show("Cabang berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saat menambah cabang:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
