using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class ViewPesertaRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ViewPesertaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ViewPesertaModel> GetAllData()
        {
            List<ViewPesertaModel> dataList = new List<ViewPesertaModel>();
            try
            {
                string query = @"SELECT
                                    tb_pengguna.nama_lengkap AS nama_pengguna,
                                    tb_pelatihan.nama_pelatihan,
                                    tb_mengikuti_pelatihan.riwayat_section,
                                    CASE 
                                        WHEN tb_mengikuti_pelatihan.riwayat_section = tb_pelatihan.jumlah_section + 1THEN 'Selesai'
                                        ELSE 'Belum Selesai'
                                    END AS status,
                                    ((CAST(tb_mengikuti_pelatihan.riwayat_section AS FLOAT) / (tb_pelatihan.jumlah_section + 1)) * 100) AS presentase
                                FROM
                                    tb_mengikuti_pelatihan
                                    JOIN tb_pengguna ON tb_mengikuti_pelatihan.id_pengguna = tb_pengguna.id_pengguna
                                    JOIN tb_pelatihan ON tb_mengikuti_pelatihan.id_pelatihan = tb_pelatihan.id_pelatihan";

                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ViewPesertaModel data = new ViewPesertaModel
                    {
                        nama_pengguna = Convert.ToString(reader["nama_pengguna"]),
                        nama_pelatihan = Convert.ToString(reader["nama_pelatihan"]),
                        riwayat_section = Convert.ToString(reader["riwayat_section"]),
                        status = Convert.ToString(reader["status"]),
                        presentase = Convert.ToSingle(reader["presentase"])
                    };

                    // Format presentase with two decimal places
                    data.presentase = (float)Math.Round(data.presentase, 2);

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
