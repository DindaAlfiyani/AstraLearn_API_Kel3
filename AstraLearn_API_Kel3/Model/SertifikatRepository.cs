using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class SertifikatRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public string formatingDate(string inputTimestamp)
        {
            DateTime date = DateTime.Parse(inputTimestamp);
            string formattedDate = date.ToString("dd-MM-yyyy");
            return formattedDate;
        }

        public SertifikatRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<SertifikatModel> GetAllData()
        {
            List<SertifikatModel> dataList = new List<SertifikatModel>();
            try
            {
                string query = "SELECT * FROM tb_sertifikat";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SertifikatModel data = new SertifikatModel
                    {
                        id_sertifikat = Convert.ToInt32(reader["id_sertifikat"]),
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        nilai_exam = Convert.ToInt32(reader["nilai_exam"]),
                        tanggal_selesai = formatingDate(reader["tanggal_selesai"].ToString()),
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

        public List<SertifikatModel> GetAllData2(int id)
        {
            List<SertifikatModel> dataList = new List<SertifikatModel>();
            try
            {
                string query = "SELECT * FROM tb_sertifikat where id_pengguna = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SertifikatModel data = new SertifikatModel
                    {
                        id_sertifikat = Convert.ToInt32(reader["id_sertifikat"]),
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        nilai_exam = Convert.ToInt32(reader["nilai_exam"]),
                        tanggal_selesai = formatingDate(reader["tanggal_selesai"].ToString()),
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

        public SertifikatModel GetData(int id)
        {
            SertifikatModel data = new SertifikatModel();
            try
            {
                string query = "SELECT * FROM tb_sertifikat WHERE id_sertifikat = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_sertifikat = Convert.ToInt32(reader["id_sertifikat"]);
                    data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.nilai_exam = Convert.ToInt32(reader["nilai_exam"]);
                    data.tanggal_selesai = formatingDate(reader["tanggal_selesai"].ToString());
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

        public void InsertData(SertifikatModel data)
        {
            try
            {
                string query = "INSERT INTO tb_sertifikat (id_pelatihan, id_pengguna, nilai_exam, tanggal_selesai) " +
                               "VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                command.Parameters.AddWithValue("@p2", data.id_pengguna);
                command.Parameters.AddWithValue("@p3", data.nilai_exam);
                command.Parameters.AddWithValue("@p4", data.tanggal_selesai);
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

        public void UpdateData(SertifikatModel data)
        {
            try
            {
                string query = "UPDATE tb_sertifikat " +
                               "SET id_pelatihan = @p2, id_pengguna = @p3, nilai_exam = @p4, tanggal_selesai = @p5 " +
                               "WHERE id_sertifikat = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_sertifikat);
                command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                command.Parameters.AddWithValue("@p3", data.id_pengguna);
                command.Parameters.AddWithValue("@p4", data.nilai_exam);
                command.Parameters.AddWithValue("@p5", data.tanggal_selesai);
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
                string query = "DELETE FROM tb_sertifikat WHERE id_sertifikat = @p1";
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
