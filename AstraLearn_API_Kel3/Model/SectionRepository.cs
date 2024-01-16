using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace AstraLearn_API_Kel3.Model
{
    public class SectionRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public SectionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<SectionModel> GetAllData()
        {
            List<SectionModel> dataList = new List<SectionModel>();
            try
            {
                string query = "SELECT * FROM tb_section Where status = 1";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SectionModel data = new SectionModel
                            {
                                id_section = Convert.ToInt32(reader["id_section"]),
                                id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                                nama_section = reader["nama_section"].ToString(),
                                video_pembelajaran = reader["video_pembelajaran"].ToString(),
                                modul_pembelajaran = reader["modul_pembelajaran"].ToString(),
                                deskripsi = reader["deskripsi"].ToString(),
                                status = Convert.ToInt32(reader["status"]),
                            };

                            dataList.Add(data);
                        }
                    }
                }
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

        public SectionModel GetData(int id)
        {
            SectionModel data = new SectionModel();
            try
            {
                string query = "SELECT * FROM tb_section WHERE id_section = @p1 AND status = 1";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", id);
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.id_section = Convert.ToInt32(reader["id_section"]);
                            data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                            data.nama_section = reader["nama_section"].ToString();
                            data.video_pembelajaran = reader["video_pembelajaran"].ToString();
                            data.modul_pembelajaran = reader["modul_pembelajaran"].ToString();
                            data.deskripsi = reader["deskripsi"].ToString();
                            data.status = Convert.ToInt32(reader["status"]);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manajemen pengecualian dengan lebih terstruktur
                Console.WriteLine($"SQL Exception: {ex.Message}");
                // Atau, lempar pengecualian kustom atau tangani sesuai kebutuhan
                // throw new CustomDataAccessException("Failed to retrieve data from the database", ex);
            }
            finally
            {
                _connection.Close();
            }
            return data;
        }


        public List<SectionModel> GetDataByPelatihan(int idPelatihan, int status)
        {
            List<SectionModel> dataList = new List<SectionModel>();
            try
            {
                string query = "SELECT * FROM tb_section WHERE id_pelatihan = @p1 and status = @p2";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", idPelatihan);
                    command.Parameters.AddWithValue("@p2", 1);
                    _connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SectionModel data = new SectionModel
                            {
                                id_section = Convert.ToInt32(reader["id_section"]),
                                id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                                nama_section = reader["nama_section"].ToString(),
                                video_pembelajaran = reader["video_pembelajaran"].ToString(),
                                modul_pembelajaran = reader["modul_pembelajaran"].ToString(),
                                deskripsi = reader["deskripsi"].ToString(),
                                status = Convert.ToInt32(reader["status"]),
                            };
                            dataList.Add(data);
                        }
                    }
                }
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


        public void InsertData(SectionModel data)
        {
            try
            {
                string query = "INSERT INTO tb_section (id_pelatihan, nama_section, video_pembelajaran, modul_pembelajaran, deskripsi, status) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                command.Parameters.AddWithValue("@p2", data.nama_section);
                command.Parameters.AddWithValue("@p3", data.video_pembelajaran); // Assuming video_pembelajaran is a file path
                command.Parameters.AddWithValue("@p4", data.modul_pembelajaran);
                command.Parameters.AddWithValue("@p5", data.deskripsi);
                command.Parameters.AddWithValue("@p6", 1); // Assuming status is supposed to be @p6

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

        /*// Helper method to convert file content to byte array
        private byte[] GetFileBytes(string filePath)
        {
            try
            {
                // Read the file content into a byte array
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return fileBytes;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
                return null;
            }
        }*/



        public void DeleteData(int id)
        {
            try
            {
                // Ubah data status menjadi 0 daripada menghapusnya secara fisik
                string query = "UPDATE tb_section SET status = 0 WHERE id_section = @p1";
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

        public void UpdateData(SectionModel data)
        {
            try
            {
                data.status = 1;

                string query = "UPDATE tb_section SET id_pelatihan = @p2, nama_section = @p3, video_pembelajaran = @p4, modul_pembelajaran = @p5, deskripsi = @p6 , status = @p7 WHERE id_section = @p1";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", data.id_section);
                    command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                    command.Parameters.AddWithValue("@p3", data.nama_section);
                    command.Parameters.AddWithValue("@p4", data.video_pembelajaran);
                    command.Parameters.AddWithValue("@p5", data.modul_pembelajaran);
                    command.Parameters.AddWithValue("@p6", data.deskripsi);
                    command.Parameters.AddWithValue("@p7", data.status);
                    _connection.Open();
                    command.ExecuteNonQuery();
                }
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
