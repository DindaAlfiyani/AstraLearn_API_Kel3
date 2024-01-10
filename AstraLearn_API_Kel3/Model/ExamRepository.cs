using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class ExamRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ExamRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<ExamModel> GetAllData(int trainingId)
        {
            List<ExamModel> dataList = new List<ExamModel>();
            try
            {
                string query = "SELECT * FROM tb_soal_exam WHERE id_exam = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", trainingId);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExamModel data = new ExamModel
                    {
                        id_exam = Convert.ToInt32(reader["id_exam"]),
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        soal = Convert.ToString(reader["soal"]),
                        pilgan1 = Convert.ToString(reader["pilgan1"]),
                        pilgan2 = Convert.ToString(reader["pilgan2"]),
                        pilgan3 = Convert.ToString(reader["pilgan3"]),
                        pilgan4 = Convert.ToString(reader["pilgan4"]),
                        pilgan5 = Convert.ToString(reader["pilgan5"]),
                        kunci_jawaban = Convert.ToString(reader["kunci_jawaban"]),
                        status = Convert.ToInt32(reader["status"]),
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

        public ExamModel GetData(int id)
        {
            ExamModel data = new ExamModel();
            try
            {
                string query = "SELECT * FROM tb_soal_exam WHERE id_exam = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_exam = Convert.ToInt32(reader["id_exam"]);
                    data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    data.soal = Convert.ToString(reader["soal"]);
                    data.pilgan1 = Convert.ToString(reader["pilgan1"]);
                    data.pilgan2 = Convert.ToString(reader["pilgan2"]);
                    data.pilgan3 = Convert.ToString(reader["pilgan3"]);
                    data.pilgan4 = Convert.ToString(reader["pilgan4"]);
                    data.pilgan5 = Convert.ToString(reader["pilgan5"]);
                    data.kunci_jawaban = Convert.ToString(reader["kunci_jawaban"]);
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

        public void InsertData(ExamModel data)
        {
            try
            {
                string query = "INSERT INTO tb_soal_exam (id_pelatihan, soal, pilgan1, pilgan2, pilgan3, pilgan4, pilgan5, kunci_jawaban,status) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
                command.Parameters.AddWithValue("@p2", data.soal);
                command.Parameters.AddWithValue("@p3", data.pilgan1);
                command.Parameters.AddWithValue("@p4", data.pilgan2);
                command.Parameters.AddWithValue("@p5", data.pilgan3);
                command.Parameters.AddWithValue("@p6", data.pilgan4);
                command.Parameters.AddWithValue("@p7", data.pilgan5);
                command.Parameters.AddWithValue("@p8", data.kunci_jawaban);
                command.Parameters.AddWithValue("@p9", 1); // Set status to 1 for new data
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

        public void UpdateData(ExamModel data)
        {
            try
            {
                string query = "UPDATE tb_soal_exam " +
                               "SET id_pelatihan = @p2, soal = @p3, pilgan1 = @p4, pilgan2 = @p5, pilgan3 = @p6, pilgan4 = @p7, pilgan5 = @p8, kunci_jawaban = @p9 " +
                               "WHERE id_exam = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_exam);
                command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                command.Parameters.AddWithValue("@p3", data.soal);
                command.Parameters.AddWithValue("@p4", data.pilgan1);
                command.Parameters.AddWithValue("@p5", data.pilgan2);
                command.Parameters.AddWithValue("@p6", data.pilgan3);
                command.Parameters.AddWithValue("@p7", data.pilgan4);
                command.Parameters.AddWithValue("@p8", data.pilgan5);
                command.Parameters.AddWithValue("@p9", data.kunci_jawaban);
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

        public void DeleteData(int id)
        {
            try
            {
                string query = "UPDATE tb_soal_exam set status = 0 where id_exam = @p1";
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

        internal object GetAllData()
        {
            throw new NotImplementedException();
        }
    }
}
