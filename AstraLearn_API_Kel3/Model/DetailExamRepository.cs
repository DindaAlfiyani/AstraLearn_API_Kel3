using System.Data.SqlClient;

namespace AstraLearn_API_Kel3.Model
{
    public class DetailExamRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public DetailExamRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<DetailExamModel> GetAllData()
        {
            List<DetailExamModel> detailExam = new List<DetailExamModel>();
            try
            {
                string query = "SELECT * FROM tb_exam";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    DetailExamModel data = new DetailExamModel
                    {
                        id_exam = Convert.ToInt32(reader["id_exam"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        jawaban_peserta = reader["jawaban_peserta"].ToString(),
                        nilai_exam = Convert.ToInt32(reader["nilai_exam"]),
                        status = Convert.ToInt32(reader["status"]),
                    };
                    detailExam.Add(data);
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
            return detailExam;
        }

        public DetailExamModel GetData(int id)
        {
            DetailExamModel data = new DetailExamModel();
            try
            {
                string query = "SELECT * FROM tb_exam WHERE id_exam = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_exam = Convert.ToInt32(reader["id_exam"]);
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.jawaban_peserta = reader["jawaban_peserta"].ToString();
                    data.nilai_exam = Convert.ToInt32(reader["nilai_exam"]);
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

        public void InsertData(DetailExamModel data)
        {
            try
            {
                string query = "INSERT INTO tb_exam (id_pengguna, jawaban_peserta, nilai_exam, status) " +
                               "VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.jawaban_peserta);
                command.Parameters.AddWithValue("@p3", data.nilai_exam);
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

        public void UpdateData(DetailExamModel data)
        {
            try
            {
                data.status = 1;

                string query = "UPDATE tb_exam " +
                               "SET id_pengguna = @p2, jawaban_peserta = @p3, nilai_exam = @p4, status = @p5" +
                               "WHERE id_exam = @p1";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_exam);
                command.Parameters.AddWithValue("@p2", data.id_pengguna);
                command.Parameters.AddWithValue("@p3", data.jawaban_peserta);
                command.Parameters.AddWithValue("@p4", data.nilai_exam);
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
                string query = "UPDATE tb_exam SET status = 0 WHERE id_exam = @p1";
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
