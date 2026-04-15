using System;
using System.Drawing;
using System.Windows.Forms;
using MiMultyCBGApp.BLL;

namespace MiMultyCBGApp
{
    public partial class FormBarang : Form
    {
        private BarangBLL bll = new BarangBLL();
        private DataGridView gridDashboard;
        private Button btnRefresh;
        private Button btnRestock;
        private Button btnTambahCabang;
        private Button btnHapusCabang;
        private Button btnTambahStaff;
        private Button btnLogout;

        private Button btnTambahBarang;

        public FormBarang()
        {
            InitializeComponent();
            SetupDashboardUI();
            LoadData();
        }

        private void SetupDashboardUI()
        {
            this.Size = new Size(1000, 500);

            gridDashboard = new DataGridView();
            gridDashboard.Location = new Point(20, 20);
            gridDashboard.Size = new Size(940, 350);
            gridDashboard.ReadOnly = true;
            gridDashboard.AllowUserToAddRows = false;
            gridDashboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridDashboard.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            btnRefresh = new Button() { Text = "Refresh Data", Location = new Point(20, 390), Width = 110, BackColor = Color.LightSkyBlue };
            btnRefresh.Click += (s, e) => LoadData();

            btnTambahBarang = new Button() { Text = "Barang Baru", Location = new Point(140, 390), Width = 100, BackColor = Color.Plum };
            btnTambahBarang.Click += BtnTambahBarang_Click;
            
            btnRestock = new Button() { Text = "Restock", Location = new Point(250, 390), Width = 100, BackColor = Color.LightYellow };
            btnRestock.Click += BtnRestock_Click;

            btnTambahCabang = new Button() { Text = "Tambah Cabang", Location = new Point(360, 390), Width = 120, BackColor = Color.LightBlue };
            btnTambahCabang.Click += BtnTambahCabang_Click;

            btnHapusCabang = new Button() { Text = "Hapus Cabang", Location = new Point(490, 390), Width = 110, BackColor = Color.LightCoral };
            btnHapusCabang.Click += BtnHapusCabang_Click;

            btnTambahStaff = new Button() { Text = "Tambah Staff", Location = new Point(610, 390), Width = 110, BackColor = Color.LightGreen };
            btnTambahStaff.Click += BtnTambahStaff_Click;

            btnLogout = new Button() { Text = "Keluar", Location = new Point(860, 390), Width = 100, BackColor = Color.IndianRed, ForeColor = Color.White };
            btnLogout.Click += (s, e) => this.Close();

            this.Controls.Add(gridDashboard);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnTambahBarang);
            this.Controls.Add(btnRestock);
            this.Controls.Add(btnTambahCabang);
            this.Controls.Add(btnHapusCabang);
            this.Controls.Add(btnTambahStaff);
            this.Controls.Add(btnLogout);
        }

        private void BtnTambahBarang_Click(object sender, EventArgs e)
        {
            new FormTambahBarang().ShowDialog();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                gridDashboard.DataSource = bll.GetDashboardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data dashboard: " + ex.Message);
            }
        }

        private void BtnRestock_Click(object sender, EventArgs e)
        {
            new FormRestock().ShowDialog();
            LoadData();
        }

        private void BtnTambahCabang_Click(object sender, EventArgs e)
        {
            new FormTambahCabang().ShowDialog();
            LoadData();
        }

        private void BtnHapusCabang_Click(object sender, EventArgs e)
        {
            new FormHapusCabang().ShowDialog();
            LoadData();
        }

        private void BtnTambahStaff_Click(object sender, EventArgs e)
        {
            new FormTambahStaff().ShowDialog();
            LoadData();
        }
    }
}
