using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.DAL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public partial class FormTransaksi : Form
    {
        private User currentUser;
        private BarangBLL barangBll = new BarangBLL();
        
        // Simpan harga satuan barang saat dipilih
        private decimal hargaSatuan = 0;

        public FormTransaksi(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            lblInfo.Text = $"Login Staff: {user.Username} | ID Cabang: {user.ID_Cabang}";
            LoadDataBarang();
        }

        private void LoadDataBarang()
        {
            dgvBarang.DataSource = barangBll.GetAllBarang();
        }

        private void dgvBarang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dgvBarang.Rows[e.RowIndex];
                txtKode.Text = row.Cells["KodeBarang"].Value.ToString();
                hargaSatuan = Convert.ToDecimal(row.Cells["Harga"].Value);
            }
        }

        
