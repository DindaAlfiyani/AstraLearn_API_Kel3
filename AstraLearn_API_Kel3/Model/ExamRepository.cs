using System;
using System.Collections.Generic;
using System.Data;
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
                string query = "SELECT * FROM tb_soal_exam WHERE id_pelatihan = @p1 AND status = 1";
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
                string query = "SELECT * FROM tb_soal_exam WHERE id_exam = @p1 AND status = 1";
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
                SqlCommand command = new SqlCommand("sp_InsertSoalExam", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_pelatihan", data.id_pelatihan);
                command.Parameters.AddWithValue("@soal", data.soal);
                command.Parameters.AddWithValue("@pilgan1", data.pilgan1);
                command.Parameters.AddWithValue("@pilgan2", data.pilgan2);
                command.Parameters.AddWithValue("@pilgan3", data.pilgan3);
                command.Parameters.AddWithValue("@pilgan4", data.pilgan4);
                command.Parameters.AddWithValue("@pilgan5", data.pilgan5);
                command.Parameters.AddWithValue("@kunci_jawaban", data.kunci_jawaban);
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
                SqlCommand command = new SqlCommand("sp_UpdateSoalExam", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_exam", data.id_exam);
                command.Parameters.AddWithValue("@id_pelatihan", data.id_pelatihan);
                command.Parameters.AddWithValue("@soal", data.soal);
                command.Parameters.AddWithValue("@pilgan1", data.pilgan1);
                command.Parameters.AddWithValue("@pilgan2", data.pilgan2);
                command.Parameters.AddWithValue("@pilgan3", data.pilgan3);
                command.Parameters.AddWithValue("@pilgan4", data.pilgan4);
                command.Parameters.AddWithValue("@pilgan5", data.pilgan5);
                command.Parameters.AddWithValue("@kunci_jawaban", data.kunci_jawaban);
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
                using SqlCommand command = new SqlCommand("sp_DeleteSoalExam", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_exam", id);

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

    }
}