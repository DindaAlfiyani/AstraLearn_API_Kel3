namespace AstraLearn_API_Kel3.Model
{
    public class PenggunaModel
    {
        public int id_pengguna { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nama_lengkap { get; set; }
        public string email { get; set; }
        public string tanggal_lahir { get; set; }
        public string alamat { get; set; }
        public string hak_akses { get; set; }
    }
}
