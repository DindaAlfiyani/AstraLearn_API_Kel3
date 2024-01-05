using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

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
                string query = "SELECT * FROM tb_section";
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
                                modul_pembelajaran = reader["modul_pembelajaran"].ToString(),
                                deskripsi = reader["deskripsi"].ToString(),
                            };

                            // Baca data varbinary video_pembelajaran
                            if (reader["video_pembelajaran"] != DBNull.Value)
                            {
                                data.video_pembelajaran = (byte[])reader["video_pembelajaran"];
                            }

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
                string query = "SELECT * FROM tb_section WHERE id_section = @p1";
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
                            data.modul_pembelajaran = reader["modul_pembelajaran"].ToString();
                            data.deskripsi = reader["deskripsi"].ToString();

                            // Baca data varbinary video_pembelajaran
                            if (reader["video_pembelajaran"] != DBNull.Value)
                            {
                                data.video_pembelajaran = (byte[])reader["video_pembelajaran"];
                            }
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
            return data;
        }

        public List<SectionModel> GetDataByPelatihan(int idPelatihan)
        {
            List<SectionModel> dataList = new List<SectionModel>();
            try
            {
                string query = "SELECT * FROM tb_section WHERE id_pelatihan = @p1";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", idPelatihan);
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
                                modul_pembelajaran = reader["modul_pembelajaran"].ToString(),
                                deskripsi = reader["deskripsi"].ToString(),
                            };

                            // Baca data varbinary video_pembelajaran
                            if (reader["video_pembelajaran"] != DBNull.Value)
                            {
                                data.video_pembelajaran = (byte[])reader["video_pembelajaran"];
                            }

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
                string query = "INSERT INTO tb_section (id_pelatihan, nama_section, video_pembelajaran, modul_pembelajaran, deskripsi) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5)";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                    command.Parameters.AddWithValue("@p2", data.nama_section);
                    command.Parameters.AddWithValue("@p3", data.video_pembelajaran);
                    command.Parameters.AddWithValue("@p4", data.modul_pembelajaran);
                    command.Parameters.AddWithValue("@p5", data.deskripsi);
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

        public void DeleteData(int id)
        {
            try
            {
                string query = "DELETE FROM tb_section WHERE id_section = @p1";
                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", id);
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

        public void UpdateData(SectionModel data)
        {
            try
            {
                string query = "UPDATE tb_section SET id_pelatihan = @p2, nama_section = @p3, video_pembelajaran = @p4, modul_pembelajaran = @p5, deskripsi = @p6 WHERE id_section = @p1";

                using (SqlCommand command = new SqlCommand(query, _connection))
                {
                    command.Parameters.AddWithValue("@p1", data.id_section);
                    command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                    command.Parameters.AddWithValue("@p3", data.nama_section);
                    command.Parameters.AddWithValue("@p4", data.video_pembelajaran);
                    command.Parameters.AddWithValue("@p5", data.modul_pembelajaran);
                    command.Parameters.AddWithValue("@p6", data.deskripsi);
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
