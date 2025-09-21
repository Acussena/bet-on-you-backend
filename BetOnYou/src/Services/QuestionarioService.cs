using backend.src.Models;
using backend.src.Repositories;
using System;

namespace backend.src.Services
{
    public class QuestionarioService
    {
        private readonly QuestionarioRepository _repo = new();

        public int RegistrarRespostas(Questionario q)
        {
            if (q.FrequenciaApostas < 1 || q.FrequenciaApostas > 5)
                throw new ArgumentException("A frequência deve estar entre 1 e 5.");
            if (q.ValorMensal <= 0)
                throw new ArgumentException("O valor mensal deve ser maior que 0.");

            return _repo.Inserir(q);
        }

        public Questionario? ObterPorId(int id)
        {
            var todos = _repo.ListarTodos();
            return todos.Find(q => q.Id == id);
        }
    }
}
