using Microsoft.AspNetCore.Mvc;

namespace AstraLearn_API_Kel3.Model
{
    public class PelatihanModel
    {
        public int id_pelatihan { get; set; }
        public int id_pengguna { get; set; }
        public int id_klasifikasi { get; set; }
        public int id_sertifikat { get; set; }
        public string nama_pelatihan { get; set; }
        public DateTime tanggal_mulai { get; set; }
        public DateTime tanggal_selesai { get; set; }
        public string deskripsi_pelatihan { get; set; }
        public int nilai { get; set; }
        public int status { get; set; }
    }
}
