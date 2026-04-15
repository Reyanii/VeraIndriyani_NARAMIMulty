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
            this.Size = new Size(300, 250);
            this.StartPosition = FormStartPosition.CenterParent;

            Label l1 = new Label() { Text = "Nama Cabang:", Location = new Point(20, 20), Width = 100 };
            txtNamaCabang = new TextBox() { Location = new Point(120, 20), Width = 140 };
            
            Label l2 = new Label() { Text = "Alamat:", Location = new Point(20, 60), Width = 100 };
            txtAlamat = new TextBox() { Location = new Point(120, 60), Width = 140, Multiline = true, Height = 60 };

            btnTambah = new Button() { Text = "Tambah", Location = new Point(120, 130), Width = 100, BackColor = Color.LightGreen };
            btnTambah.Click += BtnTambah_Click;

            this.Controls.Add(l1);
            this.Controls.Add(txtNamaCabang);
            this.Controls.Add(l2);
            this.Controls.Add(txtAlamat);
            this.Controls.Add(btnTambah);
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                bll.InsertCabang(txtNamaCabang.Text, txtAlamat.Text);
                MessageBox.Show("Cabang berhasil ditambah!");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
