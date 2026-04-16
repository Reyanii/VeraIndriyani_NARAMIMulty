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

        private void btnProses_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKode.Text) || string.IsNullOrEmpty(txtQty.Text))
            {
                MessageBox.Show("Pilih barang dan isi Qty terlebih dahulu!");
                return;
            }

            string kodeBarang = txtKode.Text;
            int qtyReq = 0;
            if (!int.TryParse(txtQty.Text, out qtyReq) || qtyReq <= 0)
            {
                MessageBox.Show("Qty harus berupa angka lebih besar dari 0!");
                return;
            }

            using (SqlConnection conn = DbConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction sqlTrans = conn.BeginTransaction();
                try
                {
                    // 1. Cek Stok di DB
                    string queryCheck = "SELECT Stok FROM Barang WHERE KodeBarang=@Kode";
                    int stokReal = 0;
                    using (SqlCommand cmdCheck = new SqlCommand(queryCheck, conn, sqlTrans))
                    {
                        cmdCheck.Parameters.AddWithValue("@Kode", kodeBarang);
                        object result = cmdCheck.ExecuteScalar();
                        if (result != null)
                        {
                            stokReal = Convert.ToInt32(result);
                        }
                    }

                    if (stokReal < qtyReq)
                    {
                        MessageBox.Show("Stok Tidak Cukup!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        sqlTrans.Rollback();
                        return;
                    }

                   
