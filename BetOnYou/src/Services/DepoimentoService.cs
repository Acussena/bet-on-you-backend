using System;
using System.Collections.Generic;
using backend.src.Models;
using backend.src.Repositories;

namespace backend.src.Services
{
    public class DepoimentoService
    {
        private readonly DepoimentoRepository _repo = new();

        public int Criar(Depoimento d)
        {
            if (string.IsNullOrWhiteSpace(d.Titulo))
                throw new ArgumentException("O título é obrigatório.");
            if (string.IsNullOrWhiteSpace(d.Texto))
                throw new ArgumentException("O texto do depoimento é obrigatório.");
            if (d.Texto.Length < 10)
                throw new ArgumentException("O depoimento deve ter pelo menos 10 caracteres.");

            return _repo.Inserir(d); // DataCriacao automático no Oracle
        }

        public List<Depoimento> ListarTodos()
        {
            return _repo.ListarTodos();
        }

        public Depoimento? ObterPorId(int id)
        {
            return _repo.ObterPorId(id);
        }

        public void Atualizar(int id, Depoimento atualizado)
        {
            var existente = _repo.ObterPorId(id);
            if (existente == null)
                throw new ArgumentException("Depoimento não encontrado.");

            if (string.IsNullOrWhiteSpace(atualizado.Titulo))
                throw new ArgumentException("O título é obrigatório.");
            if (string.IsNullOrWhiteSpace(atualizado.Texto))
                throw new ArgumentException("O texto do depoimento é obrigatório.");
            if (atualizado.Texto.Length < 10)
                throw new ArgumentException("O depoimento deve ter pelo menos 10 caracteres.");

            atualizado.Id = id;
            atualizado.DataCriacao = existente.DataCriacao;

            _repo.Atualizar(atualizado);
        }

        public void Deletar(int id)
        {
            var existente = _repo.ObterPorId(id);
            if (existente == null)
                throw new ArgumentException("Depoimento não encontrado.");

            _repo.Deletar(id);
        }
    }
}
