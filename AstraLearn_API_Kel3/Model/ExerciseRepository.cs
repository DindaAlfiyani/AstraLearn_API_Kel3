using System;
using System.Collections.Generic;
using System.Data;
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
                string query = "SELECT * FROM tb_soal_exercise WHERE id_section = @p1 AND status = 1";
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

        public ExerciseModel GetData(int id)
        {
            ExerciseModel data = new ExerciseModel();
            try
            {
                string query = "SELECT * FROM tb_soal_exercise WHERE id_exercise = @p1 AND status = 1";
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
                    data.status  = Convert.ToInt32(reader["status"]);
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
                SqlCommand command = new SqlCommand("sp_InsertSoalExercise", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_section", data.id_section);
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

        public void UpdateData(ExerciseModel data)
        {
            try
            {
                SqlCommand command = new SqlCommand("sp_UpdateSoalExercise", _connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@id_exercise", data.id_exercise);
                command.Parameters.AddWithValue("@id_section", data.id_section);
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
                using SqlCommand command = new SqlCommand("sp_DeleteSoalExercise", _connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id_exercise", id);

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

        internal object GetAllData()
        {
            throw new NotImplementedException();
        }
    }
}
