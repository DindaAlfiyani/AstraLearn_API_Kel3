using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class ExerciseRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public ExerciseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        // ...

        public List<ExerciseModel> GetAllData(int sectionId)
        {
            List<ExerciseModel> dataList = new List<ExerciseModel>();
            try
            {
                string query = "SELECT * FROM tb_exercise WHERE id_section = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", sectionId);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ExerciseModel data = new ExerciseModel
                    {
                        id_exercise = Convert.ToInt32(reader["id_exercise"]),
                        id_section = Convert.ToInt32(reader["id_section"]),
                        soal = Convert.ToString(reader["soal"]),
                        pilgan1 = Convert.ToString(reader["pilgan1"]),
                        pilgan2 = Convert.ToString(reader["pilgan2"]),
                        pilgan3 = Convert.ToString(reader["pilgan3"]),
                        pilgan4 = Convert.ToString(reader["pilgan4"]),
                        pilgan5 = Convert.ToString(reader["pilgan5"]),
                        kunci_jawaban = Convert.ToString(reader["kunci_jawaban"]),
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

        public ExerciseModel GetData(int id)
        {
            ExerciseModel data = new ExerciseModel();
            try
            {
                string query = "SELECT * FROM tb_exercise WHERE id_exercise = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_exercise = Convert.ToInt32(reader["id_exercise"]);
                    data.id_section = Convert.ToInt32(reader["id_section"]);
                    data.soal = Convert.ToString(reader["soal"]);
                    data.pilgan1 = Convert.ToString(reader["pilgan1"]);
                    data.pilgan2 = Convert.ToString(reader["pilgan2"]);
                    data.pilgan3 = Convert.ToString(reader["pilgan3"]);
                    data.pilgan4 = Convert.ToString(reader["pilgan4"]);
                    data.pilgan5 = Convert.ToString(reader["pilgan5"]);
                    data.kunci_jawaban = Convert.ToString(reader["kunci_jawaban"]);
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

        public void InsertData(ExerciseModel data)
        {
            try
            {
                string query = "INSERT INTO tb_exercise (id_section, soal, pilgan1, pilgan2, pilgan3, pilgan4, pilgan5, kunci_jawaban) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_section);
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

        public void UpdateData(ExerciseModel data)
        {
            try
            {
                string query = "UPDATE tb_exercise " +
                               "SET id_section = @p2, soal = @p3, pilgan1 = @p4, pilgan2 = @p5, pilgan3 = @p6, pilgan4 = @p7, pilgan5 = @p8, kunci_jawaban = @p9 " +
                               "WHERE id_exercise = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_exercise);
                command.Parameters.AddWithValue("@p2", data.id_section);
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
                string query = "DELETE FROM tb_exercise WHERE id_exercise = @p1";
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
