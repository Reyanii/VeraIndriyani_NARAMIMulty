using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;

namespace MiMultyCBGApp
{
    public class FormHapusCabang : Form
    {
        private TextBox txtIdCabang;
        private Button btnHapus;
        private BarangBLL bll = new BarangBLL();

        public FormHapusCabang()
        {
            this.Text = "Hapus Cabang";
            this.Size = new Size(300, 150);
            this.StartPosition = FormStartPosition.CenterParent;

            Label l1 = new Label() { Text = "ID Cabang:", Location = new Point(20, 30), Width = 100 };
            txtIdCabang = new TextBox() { Location = new Point(120, 30), Width = 120 };

            btnHapus = new Button() { Text = "Hapus", Location = new Point(120, 70), Width = 100, BackColor = Color.LightCoral };
            btnHapus.Click += BtnHapus_Click;

            this.Controls.Add(l1);
            this.Controls.Add(txtIdCabang);
            this.Controls.Add(btnHapus);
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtIdCabang.Text))
            {
                MessageBox.Show("ID Cabang harus diisi!");
                return;
            }

            var confirm = MessageBox.Show("Menghapus cabang akan menghapus SEMUA data Staff, Barang, dan Transaksi di cabang tersebut secara permanen. Lanjutkan?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    bll.DeleteCabang(int.Parse(txtIdCabang.Text));
                    MessageBox.Show("Cabang dan semua data terkait berhasil dihapus!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal Hapus: " + ex.Message);
                }
            }
        }
    }
}
