using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AstraLearn_API_Kel3.Model
{
    public class PenggunaRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public string formattingDate(string inputTimestamp)
        {
            DateTime date = DateTime.Parse(inputTimestamp);
            string formattedDate = date.ToString("yyyy-MM-dd");
            return formattedDate;
        }

        public PenggunaRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
        }

        public List<PenggunaModel> GetAllData()
        {
            List<PenggunaModel> penggunaList = new List<PenggunaModel>();
            try
            {
                string query = "SELECT * FROM tb_pengguna";
                SqlCommand command = new SqlCommand(query, _connection);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PenggunaModel pengguna = new PenggunaModel
                    {
                        id_pengguna = Convert.ToInt32(reader["id_pengguna"]),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        nama_lengkap = reader["nama_lengkap"].ToString(),
                        email = reader["email"].ToString(),
                        alamat = reader["alamat"].ToString(),
                        tanggal_lahir = formattingDate(reader["tanggal_lahir"].ToString()),
                        hak_akses = reader["hak_akses"].ToString(),
                    };
                    penggunaList.Add(pengguna);
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
            return penggunaList;
        }

        public PenggunaModel GetData(int id)
        {
            PenggunaModel pengguna = new PenggunaModel();
            try
            {
                string query = "SELECT * FROM tb_pengguna WHERE id_pengguna = @p1";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                _connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    pengguna.id_pengguna = Convert.ToInt32(reader["id_pengguna"]);
                    pengguna.username = reader["username"].ToString();
                    pengguna.password = reader["password"].ToString();
                    pengguna.nama_lengkap = reader["nama_lengkap"].ToString();
                    pengguna.email = reader["email"].ToString();
                    pengguna.alamat = reader["alamat"].ToString();
                    pengguna.tanggal_lahir = formattingDate(reader["tanggal_lahir"].ToString());
                    pengguna.hak_akses = reader["hak_akses"].ToString();
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
            return pengguna;
        }

        public void InsertPengguna(PenggunaModel pengguna)
        {
            try
            {
                string query = "INSERT INTO tb_pengguna (username, password, nama_lengkap, email, alamat, tanggal_lahir, hak_akses) " +
                               "VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7)";
                SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", pengguna.username);
                command.Parameters.AddWithValue("@p2", pengguna.password);
                command.Parameters.AddWithValue("@p3", pengguna.nama_lengkap);
                command.Parameters.AddWithValue("@p4", pengguna.email);
                command.Parameters.AddWithValue("@p5", pengguna.alamat);
                command.Parameters.AddWithValue("@p6", pengguna.tanggal_lahir);
                command.Parameters.AddWithValue("@p7", pengguna.hak_akses);
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

        public void UpdatePengguna(PenggunaModel pengguna)
        {
            try
            {
                string query = "UPDATE tb_pengguna " +
                               "SET username = @p2, password = @p3, nama_lengkap = @p4, email = @p5, alamat = @p6, tanggal_lahir = @p7, hak_akses = @p8 " +
                               "WHERE id_pengguna = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", pengguna.id_pengguna);
                command.Parameters.AddWithValue("@p2", pengguna.username);
                command.Parameters.AddWithValue("@p3", pengguna.password);
                command.Parameters.AddWithValue("@p4", pengguna.nama_lengkap);
                command.Parameters.AddWithValue("@p5", pengguna.email);
                command.Parameters.AddWithValue("@p6", pengguna.alamat);
                command.Parameters.AddWithValue("@p7", pengguna.tanggal_lahir);
                command.Parameters.AddWithValue("@p8", pengguna.hak_akses);
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

        public void UpdateHakAkses(int id, string newHakAkses)
        {
            try
            {
                string query = "UPDATE tb_pengguna " +
                               "SET hak_akses = @p2 " +
                               "WHERE id_pengguna = @p1";
                using SqlCommand command = new SqlCommand(query, _connection);
                command.Parameters.AddWithValue("@p1", id);
                command.Parameters.AddWithValue("@p2", newHakAkses);
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
