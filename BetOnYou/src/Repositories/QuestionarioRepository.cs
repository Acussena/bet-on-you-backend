using System;
using System.Collections.Generic;
using backend.src.Models;
using Oracle.ManagedDataAccess.Client;

namespace backend.src.Repositories
{
    public class QuestionarioRepository : DatabaseRepository
    {
        public int Inserir(Questionario q)
        {
            try
            {
                using var conn = new OracleConnection(connectionString);
                conn.Open();

                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO QUESTIONARIO 
                        (FREQUENCIA_APOSTAS, VALOR_MENSAL, MOTIVO) 
                        VALUES (:Frequencia, :ValorMensal, :Motivo)
                        RETURNING ID INTO :Id";

                cmd.Parameters.Add(new OracleParameter("Frequencia", q.FrequenciaApostas));
                cmd.Parameters.Add(new OracleParameter("ValorMensal", q.ValorMensal));
                cmd.Parameters.Add(new OracleParameter("Motivo", q.Motivo));

                var idParam = new OracleParameter("Id", OracleDbType.Int32);
                idParam.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(idParam);

                cmd.ExecuteNonQuery();

                return Convert.ToInt32(idParam.Value.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir questionário: {ex.Message}");
                return -1;
            }
        }

        public List<Questionario> ListarTodos()
        {
            var lista = new List<Questionario>();

            using var conn = new OracleConnection(connectionString);
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT ID, FREQUENCIA_APOSTAS, VALOR_MENSAL, MOTIVO FROM QUESTIONARIO";

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lista.Add(new Questionario
                {
                    Id = Convert.ToInt32(reader["ID"]),
                    FrequenciaApostas = Convert.ToInt32(reader["FREQUENCIA_APOSTAS"]),
                    ValorMensal = Convert.ToDecimal(reader["VALOR_MENSAL"]),
                    Motivo = Convert.ToString(reader["MOTIVO"])
                });
            }

            return lista;
        }
    }
}
