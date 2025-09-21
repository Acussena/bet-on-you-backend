using System;
using backend.src.Models;
using Oracle.ManagedDataAccess.Client;

namespace backend.src.Repositories
{
    public class UsuarioRepository : DatabaseRepository
    {
        public bool Cadastrar(Usuario usuario)
        {
            try
            {
                using var conn = new OracleConnection(connectionString);
                conn.Open();

                var checkCmd = conn.CreateCommand();
                checkCmd.CommandText = "SELECT COUNT(*) FROM USUARIOS_GS WHERE Email = :Email";
                checkCmd.Parameters.Add(new OracleParameter("Email", usuario.Email));
                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                    return false;

                var insertCmd = conn.CreateCommand();
                insertCmd.CommandText = "INSERT INTO USUARIOS_GS (Nome, Email, Senha) VALUES (:Nome, :Email, :Senha)";
                insertCmd.Parameters.Add(new OracleParameter("Nome", usuario.Nome));
                insertCmd.Parameters.Add(new OracleParameter("Email", usuario.Email));
                insertCmd.Parameters.Add(new OracleParameter("Senha", usuario.Senha));

                return insertCmd.ExecuteNonQuery() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cadastro: {ex.Message}");
                throw;
            }
        }

        public bool Login(string email, string senha)
        {
            using var conn = new OracleConnection(connectionString);
            conn.Open();
            var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM USUARIOS_GS WHERE Email = :Email AND Senha = :Senha";
            cmd.Parameters.Add(new OracleParameter("Email", email));
            cmd.Parameters.Add(new OracleParameter("Senha", senha));
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }
}
