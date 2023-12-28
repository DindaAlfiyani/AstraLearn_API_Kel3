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
                string query = "select* from PelatihanView";
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
                        nama_pelatihan = reader["nama_pelatihan"].ToString(),
                        deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString(),
                        jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]),
                        nama_klasifikasi = reader["nama_klasifikasi"].ToString(),
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

        public List<PelatihanModel> GetAllData2(int id)
        {
            List<PelatihanModel> dataList = new List<PelatihanModel>();
            try
            {
                string query = "select* from PelatihanView WHERE id_pengguna = @p1";
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
                        jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]),
                        nama_klasifikasi = reader["nama_klasifikasi"].ToString(),
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
                string query = "select * from PelatihanView where id_pelatihan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]);
                    data.nama_pelatihan = reader["nama_pelatihan"].ToString();
                    data.deskripsi_pelatihan = reader["deskripsi_pelatihan"].ToString();
                    data.jumlah_peserta = Convert.ToInt32(reader["jumlah_peserta"]);
                    data.nama_klasifikasi = reader["nama_klasifikasi"].ToString();
                    data.nilai = Convert.ToInt32(reader["nilai"]);
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
                string query = "INSERT INTO tb_pelatihan (id_pengguna, id_klasifikasi, nama_pelatihan, deskripsi_pelatihan, jumlah_peserta, nilai) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.id_klasifikasi);
                command.Parameters.AddWithValue("@p3", data.nama_pelatihan);
                command.Parameters.AddWithValue("@p4", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@p5", data.jumlah_peserta);
                command.Parameters.AddWithValue("@p6", data.nilai);
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
                string query = "UPDATE tb_pelatihan " +
                               "SET id_pengguna = @p2, id_klasifikasi = @p3, nama_pelatihan = @p4, deskripsi_pelatihan = @p5, jumlah_peserta = @p6 " +
                               "WHERE id_pelatihan = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                command.Parameters.AddWithValue("@p2", data.id_pengguna);
                command.Parameters.AddWithValue("@p3", data.id_klasifikasi);
                command.Parameters.AddWithValue("@p4", data.nama_pelatihan);
                command.Parameters.AddWithValue("@p5", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@p6", data.jumlah_peserta);
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
                string query = "DELETE FROM tb_pelatihan WHERE id_pelatihan = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
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
    }
}
    