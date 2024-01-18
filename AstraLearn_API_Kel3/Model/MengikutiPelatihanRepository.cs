using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class MengikutiPelatihanRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public MengikutiPelatihanRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<MengikutiPelatihanModel> GetAllData()
        {
            List<MengikutiPelatihanModel> dataList = new List<MengikutiPelatihanModel>();
            try
            {
                string query = "SELECT * FROM tb_mengikuti_pelatihan";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MengikutiPelatihanModel data = new MengikutiPelatihanModel
                    {
                        id_mengikuti_pelatihan = Convert.ToInt32(reader["id_mengikuti_pelatihan"]),
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]),
                        riwayat_section = Convert.ToInt32(reader["riwayat_section"]),
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

        public MengikutiPelatihanModel GetData(int id, int id2)
        {
            MengikutiPelatihanModel data = new MengikutiPelatihanModel();
            try
            {
                string query = "SELECT * FROM tb_mengikuti_pelatihan WHERE id_pengguna = @p1 AND id_pelatihan = @p2";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", id2);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    data.id_mengikuti_pelatihan = Convert.ToInt32(reader["id_mengikuti_pelatihan"]);
                    data.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    data.id_pelatihan = Convert.ToInt32(reader["id_pelatihan"]);
                    data.riwayat_section = Convert.ToInt32(reader["riwayat_section"]);
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

        public void InsertData(MengikutiPelatihanModel data)
        {
            try
            {
                string query = "INSERT INTO tb_mengikuti_pelatihan (id_pengguna, id_pelatihan, riwayat_section, status) " +
                               "VALUES (@p1, @p2, @p3, @p4)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                command.Parameters.AddWithValue("@p3", data.riwayat_section);
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

            try
            {
                string query = "UPDATE PelatihanView SET jumlah_peserta = jumlah_peserta + 1 WHERE id_pelatihan = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pelatihan);
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

        public void UpdateData(MengikutiPelatihanModel data)
        {
            try
            {
                string query = "UPDATE tb_mengikuti_pelatihan " +
                               "SET riwayat_section = riwayat_section + 1 " +
                               "WHERE id_pengguna = @p1 AND id_pelatihan = @p2";

                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", data.id_pengguna);
                command.Parameters.AddWithValue("@p2", data.id_pelatihan);
                _connection.Open();
                command.ExecuteNonQuery();
                _connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void DeleteData(int id)
        {
            try
            {
                string query = "DELETE FROM tb_mengikuti_pelatihan id_mengikuti_pelatihan = @p1";
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
