using System;
using System.Collections.Generic;
using System.Data;
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
                string query = "select* from PelatihanView where status = 1";
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
                using SqlCommand command = new SqlCommand("sp_InsertPelatihan", _connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_pengguna", data.id_pengguna);
                command.Parameters.AddWithValue("@id_klasifikasi", data.id_klasifikasi);
                command.Parameters.AddWithValue("@id_sertifikat", data.id_sertifikat);
                command.Parameters.AddWithValue("@nama_pelatihan", data.nama_pelatihan);
                command.Parameters.AddWithValue("@tanggal_mulai", data.tanggal_mulai);
                command.Parameters.AddWithValue("@tanggal_selesai", data.tanggal_selesai);
                command.Parameters.AddWithValue("@deskripsi_pelatihan", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@nilai", data.nilai);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
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
                using SqlCommand command = new SqlCommand("sp_UpdatePelatihan", _connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_pelatihan", data.id_pelatihan);
                command.Parameters.AddWithValue("@id_pengguna", data.id_pengguna);
                command.Parameters.AddWithValue("@id_klasifikasi", data.id_klasifikasi);
                command.Parameters.AddWithValue("@id_sertifikat", data.id_sertifikat);
                command.Parameters.AddWithValue("@nama_pelatihan", data.nama_pelatihan);
                command.Parameters.AddWithValue("@tanggal_mulai", data.tanggal_mulai);
                command.Parameters.AddWithValue("@tanggal_selesai", data.tanggal_selesai);
                command.Parameters.AddWithValue("@deskripsi_pelatihan", data.deskripsi_pelatihan);
                command.Parameters.AddWithValue("@nilai", data.nilai);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
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
                using SqlCommand command = new SqlCommand("sp_DeletePelatihan", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_pelatihan", id);

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
    