using System;

namespace MiMultyCBGApp.Models
{

    public class Transaksi
    {
        public int ID_Transaksi { get; set; }
        public int ID_Cabang { get; set; }
        public int UserID { get; set; }
        public string KodeBarang { get; set; }
        public int QtyTerjual { get; set; }
        public decimal TotalHarga { get; set; }
        public DateTime TanggalTransaksi { get; set; }
    }
}
