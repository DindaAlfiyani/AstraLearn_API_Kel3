using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using AstraLearn_API_Kel3.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace AstraLearn_API_Kel3.Model
{
    public class KlasifikasiPelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
        ResponseModel response = new ResponseModel();

        public KlasifikasiPelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<KlasifikasiPelatihanModel> GetAllData()
        {
            List<KlasifikasiPelatihanModel> dataList = new List<KlasifikasiPelatihanModel>();
            try
            {
                string query = "SELECT * FROM tb_klasifikasi_pelatihan WHERE status = 1";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    KlasifikasiPelatihanModel data = new KlasifikasiPelatihanModel
                    {
                        id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]),
                        nama_klasifikasi = reader["nama_klasifikasi"].ToString(),
                        deskripsi = reader["deskripsi"].ToString(),
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

        public KlasifikasiPelatihanModel GetData(int id)
        {
            KlasifikasiPelatihanModel data = new KlasifikasiPelatihanModel();
            try
            {
                string query = "SELECT * FROM tb_klasifikasi_pelatihan WHERE id_klasifikasi = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_klasifikasi = Convert.ToInt32(reader["id_klasifikasi"]);
                    data.nama_klasifikasi = reader["nama_klasifikasi"].ToString();
                    data.deskripsi = reader["deskripsi"].ToString();
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

        public bool CheckKlasifikasi(string nama_klasifikasi)
        {
            try
            {
                // Query SQL untuk memeriksa keberadaan jenis paket dengan nama tertentu
                string query = "SELECT COUNT(*) FROM tb_klasifikasi_pelatihan WHERE nama_klasifikasi = @nama_klasifikasi AND status = 1";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@nama_klasifikasi", nama_klasifikasi);
                    _connection.Open();

                    // Eksekusi query dan periksa hasilnya
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                _connection?.Close();
            }
        }
        public ResponseModel InsertData(KlasifikasiPelatihanModel data)
        {
            try
            {
                // Cek apakah nama sudah ada sebelum menambahkannya
                if (CheckKlasifikasi(data.nama_klasifikasi))
                {
                    Console.WriteLine("Nama Klasifikasi tersebut sudah ada.");
                    response.status = 400; // Ubah status sesuai kebutuhan, misalnya 400 untuk Bad Request
                    response.message = "Nama Klasifikasi tersebut sudah ada.";
                    response.data = null;
                    return response;
                }

                SqlCommand command = new SqlCommand("sp_InsertKlasifikasiPelatihan", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@nama_klasifikasi", data.nama_klasifikasi);
                command.Parameters.AddWithValue("@deskripsi", data.deskripsi);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();

                response.status = 200;
                response.message = "klasifikasi pelatihan berhasil dibuat";
                response.data = data;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                response.status = 500;
                response.message = "Terjadi kesalahan saat membuat Kelompok = " + ex.Message;
                response.data = null;
            }
            finally
            {
                _connection.Close();
            }
            return response;
        }

        public void UpdateData(KlasifikasiPelatihanModel data)
        {
            try
            {
                using SqlCommand command = new SqlCommand("sp_UpdateKlasifikasiPelatihan", _connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_klasifikasi", data.id_klasifikasi);
                command.Parameters.AddWithValue("@nama_klasifikasi", data.nama_klasifikasi);
                command.Parameters.AddWithValue("@deskripsi", data.deskripsi);

                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void DeleteData(int id)
        {
            try
            {
                using SqlCommand command = new SqlCommand("sp_DeleteKlasifikasiPelatihan", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_klasifikasi", id);

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
