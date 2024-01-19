namespace AstraLearn_API_Kel3.Model
{
    public class MengikutiPelatihanModel
    {
        public int id_mengikuti_pelatihan { get; set; }
        public int id_pengguna { get; set; }
        public int id_pelatihan { get; set; }
        public int riwayat_section { get; set; }
        public DateTime tanggal_mulai { get; set; }
        public DateTime tanggal_selesai { get; set; }
        public int status { get; set; }
    }
}
