using System.Data.SqlClient;

namespace AstraLearn_API_Kel3.Model
{
    public class DetailExerciseRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public DetailExerciseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<DetailExerciseModel> GetAllData()
        {
            List<DetailExerciseModel> detailList = new List<DetailExerciseModel>();
            try
            {
                string query = "SELECT * FROM tb_exercise";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DetailExerciseModel data = new DetailExerciseModel
                    {
                        id_exercise = Convert.ToInt32(reader["id_exercise"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        jawaban_peserta = reader["jawaban_peserta"].ToString(),
                        nilai_exercise = Convert.ToInt32(reader["nilai_exercise"]),
                        status = Convert.ToInt32(reader["status"]),
                    };
                    detailList.Add(data);
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
            return detailList;
        }

        public DetailExerciseModel GetData(int id)
        {
            DetailExerciseModel data = new DetailExerciseModel();
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
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.jawaban_peserta = reader["jawaban_peserta"].ToString();
                    data.nilai_exercise = Convert.ToInt32(reader["nilai_exercise"]);
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

        public void InsertData(DetailExerciseModel data)
        {
            try
            {
                string query = "INSERT INTO tb_exercise (id_pengguna, jawaban_peserta, nilai_exercise, status) " +
                               "VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.jawaban_peserta);
                command.Parameters.AddWithValue("@p3", data.nilai_exercise);
                command.Parameters.AddWithValue("@p4", 1);
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

        public void UpdateData(DetailExerciseModel data)
        {
            try
            {
                data.status = 1;

                string query = "UPDATE tb_exercise " +
                               "SET id_pengguna = @p2, jawaban_peserta = @p3, nilai_exercise = @p4, status = @p5" +
                               "WHERE id_exercise = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_exercise);
                command.Parameters.AddWithValue("@p2", data.id_pengguna);
                command.Parameters.AddWithValue("@p3", data.jawaban_peserta);
                command.Parameters.AddWithValue("@p4", data.nilai_exercise);
                command.Parameters.AddWithValue("@p5", data.status);
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
                // Ubah data status menjadi 0 daripada menghapusnya secara fisik
                string query = "UPDATE tb_exercise SET status = 0 WHERE id_exercise = @p1";
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
    }
}
