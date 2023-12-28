using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class ViewPelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ViewPelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ViewPelatihanModel> GetAllData()
        {
            List<ViewPelatihanModel> dataList = new List<ViewPelatihanModel>();
            try
            {
                string query = @"SELECT tb_pengguna.nama_lengkap AS nama_pengguna, 
                                        tb_klasifikasi_pelatihan.nama_klasifikasi, 
                                        tb_pelatihan.nama_pelatihan, 
                                        tb_pelatihan.deskripsi_pelatihan, 
                                        tb_pelatihan.jumlah_peserta,
                                        COUNT(DISTINCT CASE WHEN rs.riwayat_section = tb_pelatihan.jumlah_section THEN rs.id_pengguna END) AS jumlah_peserta_selesai
                                FROM tb_pelatihan
                                JOIN tb_pengguna ON tb_pelatihan.id_pengguna = tb_pengguna.id_pengguna
                                JOIN tb_klasifikasi_pelatihan ON tb_pelatihan.id_klasifikasi = tb_klasifikasi_pelatihan.id_klasifikasi
                                LEFT JOIN tb_mengikuti_pelatihan rs ON tb_pelatihan.id_pelatihan = rs.id_pelatihan
                                GROUP BY 
                                    tb_pengguna.nama_lengkap, 
                                    tb_klasifikasi_pelatihan.nama_klasifikasi, 
                                    tb_pelatihan.nama_pelatihan, 
                                    tb_pelatihan.deskripsi_pelatihan, 
                                    tb_pelatihan.jumlah_peserta";

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ViewPelatihanModel data = new ViewPelatihanModel
                    {
                        nama_pengguna = Convert.ToString(reader["nama_pengguna"]),
                        nama_klasifikasi = Convert.ToString(reader["nama_klasifikasi"]),
                        nama_pelatihan = Convert.ToString(reader["nama_pelatihan"]),
                        deskripsi_pelatihan = Convert.ToString(reader["deskripsi_pelatihan"]),
                        jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]),
                        jumlah_peserta_selesai = Convert.ToInt32(reader["jumlah_peserta_selesai"]),
                    };
                    dataList.Add(data);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
            return dataList;
        }
    }
}
