using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;

namespace MiMultyCBGApp
{
    public class FormRestock : Form
    {
        private TextBox txtIdBarang;
        private Label lblNamaBarang;
        private Button btnCekNama;
        private TextBox txtIdCabang;
        private TextBox txtJumlah;
        private Button btnSimpan;

        private BarangBLL bll = new BarangBLL();

        public FormRestock()
        {
            this.Text = "Restock Barang";
            this.Size = new Size(350, 300);
            this.StartPosition = FormStartPosition.CenterParent;

            Label l1 = new Label() { Text = "ID Barang:", Location = new Point(20, 20) };
            txtIdBarang = new TextBox() { Location = new Point(120, 20), Width = 150, MaxLength = 50 };

            btnCekNama = new Button() { Text = "Cek Nama", Location = new Point(120, 50), Width = 70 };
            btnCekNama.Click += BtnCekNama_Click;

            lblNamaBarang = new Label() { Text = "Nama: -", Location = new Point(20, 80), Width = 250, ForeColor = Color.Blue };

            Label l2 = new Label() { Text = "ID Cabang:", Location = new Point(20, 110) };
            txtIdCabang = new TextBox() { Location = new Point(120, 110), Width = 150 };
            txtIdCabang.KeyPress += NumericOnly_KeyPress;

            Label l3 = new Label() { Text = "Jumlah:", Location = new Point(20, 140) };
            txtJumlah = new TextBox() { Location = new Point(120, 140), Width = 150 };
            txtJumlah.KeyPress += NumericOnly_KeyPress;

            btnSimpan = new Button() { Text = "Simpan", Location = new Point(120, 180), Width = 100, BackColor = Color.LightGreen };
            btnSimpan.Click += BtnSimpan_Click;

            this.Controls.Add(l1);
            this.Controls.Add(txtIdBarang);
            this.Controls.Add(btnCekNama);
            this.Controls.Add(lblNamaBarang);
            this.Controls.Add(l2);
            this.Controls.Add(txtIdCabang);
            this.Controls.Add(l3);
            this.Controls.Add(txtJumlah);
            this.Controls.Add(btnSimpan);
        }

        private void NumericOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void BtnCekNama_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdBarang.Text))
            {
                MessageBox.Show("ID Barang harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string nama = bll.GetNamaBarang(txtIdBarang.Text);
                lblNamaBarang.Text = "Nama: " + (string.IsNullOrEmpty(nama) ? "Tidak ditemukan" : nama);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdBarang.Text) || string.IsNullOrWhiteSpace(txtIdCabang.Text) || string.IsNullOrWhiteSpace(txtJumlah.Text))
            {
                MessageBox.Show("Semua data harus diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtJumlah.Text, out int jumlahRestock))
            {
                MessageBox.Show("Input string was not in a correct format untuk Jumlah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (jumlahRestock <= 0)
            {
                MessageBox.Show("Jumlah stok atau restock harus minimal 1!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                bll.RestockBarangDashboard(txtIdBarang.Text, int.Parse(txtIdCabang.Text), jumlahRestock);
                MessageBox.Show("Restock Berhasil!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
