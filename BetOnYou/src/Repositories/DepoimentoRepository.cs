using System;
using System.Collections.Generic;
using backend.src.Models;
using Oracle.ManagedDataAccess.Client;

namespace backend.src.Repositories
{
    public class DepoimentoRepository : DatabaseRepository
    {
        public int Inserir(Depoimento d)
        {
            try
            {
                using var conn = new OracleConnection(connectionString);
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO DEPOIMENTOS (NOME, TITULO, TEXTO)
                    VALUES (:Nome, :Titulo, :Texto)
                    RETURNING ID INTO :Id";

                cmd.Parameters.Add(new OracleParameter("Nome", (object?)d.Nome ?? DBNull.Value));
                cmd.Parameters.Add(new OracleParameter("Titulo", d.Titulo));
                cmd.Parameters.Add(new OracleParameter("Texto", d.Texto));

                var idParam = new OracleParameter("Id", Oracle.ManagedDataAccess.Client.OracleDbType.Int32)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                cmd.Parameters.Add(idParam);

                cmd.ExecuteNonQuery();

                return idParam.Value == DBNull.Value ? -1 : (int)idParam.Value;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir depoimento: {ex.Message}");
                return -1;
            }
        }

        public List<Depoimento> ListarTodos()
        {
            var lista = new List<Depoimento>();

            using var conn = new OracleConnection(connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, NOME, TITULO, TEXTO, DATA_CRIACAO FROM DEPOIMENTOS ORDER BY DATA_CRIACAO DESC";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var d = new Depoimento
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Nome = reader["NOME"] == DBNull.Value ? null : reader["NOME"].ToString(),
                    Titulo = Convert.ToString(reader["TITULO"]) ?? string.Empty,
                    Texto = Convert.ToString(reader["TEXTO"]) ?? string.Empty,
                    DataCriacao = reader["DATA_CRIACAO"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(reader["DATA_CRIACAO"])
                };
                lista.Add(d);
            }

            return lista;
        }

        public Depoimento? ObterPorId(int id)
        {
            using var conn = new OracleConnection(connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, NOME, TITULO, TEXTO, DATA_CRIACAO FROM DEPOIMENTOS WHERE ID = :Id";
            cmd.Parameters.Add(new OracleParameter("Id", id));

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Depoimento
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    Nome = reader["NOME"] == DBNull.Value ? null : reader["NOME"].ToString(),
                    Titulo = Convert.ToString(reader["TITULO"]) ?? string.Empty,
                    Texto = Convert.ToString(reader["TEXTO"]) ?? string.Empty,
                    DataCriacao = reader["DATA_CRIACAO"] == DBNull.Value
                        ? DateTime.MinValue
                        : Convert.ToDateTime(reader["DATA_CRIACAO"])
                };
            }

            return null;
        }

        public void Atualizar(Depoimento d)
        {
            using var conn = new OracleConnection(connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                UPDATE DEPOIMENTOS
                SET NOME = :Nome,
                    TITULO = :Titulo,
                    TEXTO = :Texto
                WHERE ID = :Id";

            cmd.Parameters.Add(new OracleParameter("Nome", (object?)d.Nome ?? DBNull.Value));
            cmd.Parameters.Add(new OracleParameter("Titulo", d.Titulo));
            cmd.Parameters.Add(new OracleParameter("Texto", d.Texto));
            cmd.Parameters.Add(new OracleParameter("Id", d.Id));

            cmd.ExecuteNonQuery();
        }

        public void Deletar(int id)
        {
            using var conn = new OracleConnection(connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM DEPOIMENTOS WHERE ID = :Id";
            cmd.Parameters.Add(new OracleParameter("Id", id));
            cmd.ExecuteNonQuery();
        }
    }
}
