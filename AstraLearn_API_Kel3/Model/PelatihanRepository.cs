using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class PelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public PelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<PelatihanModel> GetAllData()
        {
            List<PelatihanModel> dataList = new List<PelatihanModel>();
            try
            {
                string query = "select* from tb_pelatihan WHERE status = 1";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PelatihanModel data = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]),
                        id_sertifikat = Convert.ToInt32(reader["id_sertifikat"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        tanggal_mulai = Convert.ToDateTime(reader["tanggal_mulai"]),
                        tanggal_selesai = Convert.ToDateTime(reader["tanggal_selesai"]),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        nilai = Convert.ToInt32(reader["nilai"]),
                        status = Convert.ToInt32(reader["status"])
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

        public List<PelatihanModel> GetAllData2(int id)
        {
            List<PelatihanModel> dataList = new List<PelatihanModel>();
            try
            {
                string query = "select* from View_Pelatihan WHERE id_pengguna = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PelatihanModel data = new PelatihanModel
                    {
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]),
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        nilai = Convert.ToInt32(reader["nilai"])
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

        public PelatihanModel GetData(int id)
        {
            PelatihanModel data = new PelatihanModel();
            try
            {
                string query = "select * from tb_pelatihan where id_pelatihan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]);
                    data.id_sertifikat = Convert.ToInt32(reader["id_sertifikat"]);
                    data.nama_pelatihan = reader["nama_pelatihan"].ToString();
                    data.tanggal_mulai = Convert.ToDateTime(reader["tanggal_mulai"]);
                    data.tanggal_selesai = Convert.ToDateTime(reader["tanggal_selesai"]);
                    data.deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString();
                    data.nilai = Convert.ToInt32(reader["nilai"]);
                    data.status = Convert.ToInt32(reader["status"]);
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
            return data;
        }

        public void InsertData(PelatihanModel data)
        {
            try
            {
                string query = "INSERT INTO tb_pelatihan (id_pengguna, id_klasifikasi, id_sertifikat, nama_pelatihan, tanggal_mulai, tanggal_selesai, deskripsi_pelatihan, nilai, status) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.id_sertifikat);
                command.Parameters.AddWithValue("@p3", data.id_klasifikasi);
                command.Parameters.AddWithValue("@p4", data.nama_pelatihan);
                command.Parameters.AddWithValue("@p5", data.tanggal_mulai);
                command.Parameters.AddWithValue("@p6", data.tanggal_selesai);
                command.Parameters.AddWithValue("@p7", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@p8", data.nilai);
                command.Parameters.AddWithValue("@p9", 1 );
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateData(PelatihanModel data)
        {
            try
            {
                data.status = 1; 

                string query = "UPDATE tb_pelatihan " +
                               "SET id_pengguna = @p2, id_klasifikasi = @p3, id_sertifikat = @p4, nama_pelatihan = @p5, tanggal_mulai = @p6, tanggal_selesai = @p7, deskripsi_pelatihan = @p8, nilai = @p9, status = @p10 " +
                               "WHERE id_pelatihan = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                command.Parameters.AddWithValue("@p2", data.id_pengguna);
                command.Parameters.AddWithValue("@p3", data.id_klasifikasi);
                command.Parameters.AddWithValue("@p4", null);
                command.Parameters.AddWithValue("@p5", data.nama_pelatihan);
                command.Parameters.AddWithValue("@p6", null);
                command.Parameters.AddWithValue("@p7", null);
                command.Parameters.AddWithValue("@p8", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@p9", null);
                command.Parameters.AddWithValue("@p10", data.status);
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateNilai(int id, int nilai)
        {
            try
            {
                string query = "UPDATE tb_pelatihan " +
                               "SET nilai = @p2 " +
                               "WHERE id_pelatihan = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", nilai);
                _connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public void DeleteData(int id)
        {
            try
            {
                // Ubah data status menjadi 0 daripada menghapusnya secara fisik
                string query = "UPDATE tb_pelatihan SET status = 0 WHERE id_pelatihan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
    