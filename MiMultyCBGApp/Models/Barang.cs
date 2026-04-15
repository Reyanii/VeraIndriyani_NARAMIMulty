namespace MiMultyCBGApp.Models
{
    public class Barang
    {
        public string KodeBarang { get; set; }
        public int ID_Cabang { get; set; }
        public string NamaBarang { get; set; }
        public string Kategori { get; set; }
        public decimal Harga { get; set; }
        public int Stok { get; set; }
    }
}
