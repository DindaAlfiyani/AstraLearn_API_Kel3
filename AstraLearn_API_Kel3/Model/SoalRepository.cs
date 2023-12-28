using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class SoalRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public SoalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<SoalModel> GetAllData()
        {
            List<SoalModel> dataList = new List<SoalModel>();
            try
            {
                string query = "SELECT * FROM tb_soal";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    SoalModel data = new SoalModel
                    {
                        id_soal = Convert.ToInt32(reader["id_soal"]),
                        id_exam = Convert.ToInt32(reader["id_exam"]),
                        soal = reader["soal"].ToString(),
                        pilgan1 = reader["pilgan1"].ToString(),
                        pilgan2 = reader["pilgan2"].ToString(),
                        pilgan3 = reader["pilgan3"].ToString(),
                        pilgan4 = reader["pilgan4"].ToString(),
                        pilgan5 = reader["pilgan5"].ToString(),
                        kunci_jawaban = reader["kunci_jawaban"].ToString(),
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

        public SoalModel GetData(int id)
        {
            SoalModel data = new SoalModel();
            try
            {
                string query = "SELECT * FROM tb_soal WHERE id_soal = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_soal = Convert.ToInt32(reader["id_soal"]);
                    data.id_exam = Convert.ToInt32(reader["id_exam"]);
                    data.soal = reader["soal"].ToString();
                    data.pilgan1 = reader["pilgan1"].ToString();
                    data.pilgan2 = reader["pilgan2"].ToString();
                    data.pilgan3 = reader["pilgan3"].ToString();
                    data.pilgan4 = reader["pilgan4"].ToString();
                    data.pilgan5 = reader["pilgan5"].ToString();
                    data.kunci_jawaban = reader["kunci_jawaban"].ToString();
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

        public void InsertData(SoalModel data)
        {
            try
            {
                string query = "INSERT INTO tb_soal (id_exam, soal, pilgan1, pilgan2, pilgan3, pilgan4, pilgan5, kunci_jawaban) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_exam);
                command.Parameters.AddWithValue("@p2", data.soal);
                command.Parameters.AddWithValue("@p3", data.pilgan1);
                command.Parameters.AddWithValue("@p4", data.pilgan2);
                command.Parameters.AddWithValue("@p5", data.pilgan3);
                command.Parameters.AddWithValue("@p6", data.pilgan4);
                command.Parameters.AddWithValue("@p7", data.pilgan5);
                command.Parameters.AddWithValue("@p8", data.kunci_jawaban);
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

        public void UpdateData(SoalModel data)
        {
            try
            {
                string query = "UPDATE tb_soal " +
                               "SET id_exam = @p2, soal = @p3, pilgan1 = @p4, pilgan2 = @p5, pilgan3 = @p6, pilgan4 = @p7, pilgan5 = @p8, kunci_jawaban = @p9 " +
                               "WHERE id_soal = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_soal);
                command.Parameters.AddWithValue("@p2", data.id_exam);
                command.Parameters.AddWithValue("@p3", data.soal);
                command.Parameters.AddWithValue("@p4", data.pilgan1);
                command.Parameters.AddWithValue("@p5", data.pilgan2);
                command.Parameters.AddWithValue("@p6", data.pilgan3);
                command.Parameters.AddWithValue("@p7", data.pilgan4);
                command.Parameters.AddWithValue("@p8", data.pilgan5);
                command.Parameters.AddWithValue("@p9", data.kunci_jawaban);
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
                string query = "DELETE FROM tb_soal WHERE id_soal = @p1";
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
