using backend.src.Models;
using backend.src.Repositories;

namespace backend.src.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repository = new();

        public bool Cadastrar(Usuario usuario) => _repository.Cadastrar(usuario);

        public bool Login(string email, string senha) => _repository.Login(email, senha);
    }
}
