using System;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;
using MiMultyCBGApp.Models;

namespace MiMultyCBGApp
{
    public partial class FormStaff : Form
    {
        private User currentUser;
        private BarangBLL barangBll = new BarangBLL();
        private BindingSource bindingSource;
        private BindingNavigator bindingNavigator;

        public FormStaff(User user)
        {
            InitializeComponent();
            this.currentUser = user;
            lblInfo.Text = $"Login Staff: {user.Username} | ID Cabang: {user.ID_Cabang}";
            
            bindingSource = new BindingSource();
            bindingNavigator = new BindingNavigator(true);
            bindingNavigator.BindingSource = bindingSource;
            this.Controls.Add(bindingNavigator);
            
            if (dgvBarangStaff != null)
            {
                dgvBarangStaff.Top = bindingNavigator.Bottom + 10;
            }

            // Tambahkan event handler untuk klik ganda pada Grid
            this.dgvBarangStaff.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarangStaff_CellDoubleClick);
            
            LoadDashboardData();
        }

        private void dgvBarangStaff_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Pastikan tidak klik pada header
            if (e.RowIndex >= 0)
            {
                // Ambil kode barang dari baris yang di-klik ganda
                DataGridViewRow row = this.dgvBarangStaff.Rows[e.RowIndex];
                string kodeBarang = row.Cells["ID Barang"].Value.ToString(); 

                // Oper kode barang ke popup penjualan
                FormInputPenjualan popup = new FormInputPenjualan(currentUser, kodeBarang);
                popup.ShowDialog();
                
                // Refresh data setelah popup ditutup
                LoadDashboardData();
            }
        }

        private void LoadDashboardData()
        {
            bindingSource.DataSource = barangBll.GetDashboardDataStaff(currentUser.ID_Cabang);
            dgvBarangStaff.DataSource = bindingSource;
            dgvBarangStaff.ReadOnly = true;
            dgvBarangStaff.AllowUserToAddRows = false;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void btnInputPenjualan_Click(object sender, EventArgs e)
        {
            FormInputPenjualan popup = new FormInputPenjualan(currentUser);
            popup.ShowDialog();
            LoadDashboardData();
        }
    }
}
