using System.Text.Json;
using backend.src.Models;

namespace backend.src.Services
{
    public class RedeApoioService
    {
        private readonly string _filePath;

        public RedeApoioService()
        {
            var baseDir = AppContext.BaseDirectory;
            var dataFolder = Path.Combine(baseDir, "Data");
            _filePath = Path.Combine(dataFolder, "redeApoio.json");

            if (!Directory.Exists(dataFolder))
                Directory.CreateDirectory(dataFolder);

            if (!File.Exists(_filePath))
            {
                try
                {
                    var defaultRedes = new List<RedeApoio>
                    {
                        new RedeApoio { Id = 1, Nome = "Centro de Apoio Psicológico", Tipo = "Psicólogo", Contato = "psicologo@apoio.com" },
                        new RedeApoio { Id = 2, Nome = "Associação Livre de Jogadores", Tipo = "Grupo de Apoio", Contato = "grupoapoio@ajuda.org" },
                        new RedeApoio { Id = 3, Nome = "ONG Vida Saudável", Tipo = "ONG", Contato = "contato@vidasaudavel.org" }
                    };

                    var json = JsonSerializer.Serialize(defaultRedes, new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    File.WriteAllText(_filePath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao criar arquivo JSON: " + ex.Message);
                }
            }
        }

        public List<RedeApoio> ListarRedes()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<RedeApoio>();

                var json = File.ReadAllText(_filePath);
                var redes = JsonSerializer.Deserialize<List<RedeApoio>>(json, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                return redes ?? new List<RedeApoio>();
            }
            catch
            {
                return new List<RedeApoio>();
            }
        }

        public RedeApoio? BuscarPorId(int id)
        {
            return ListarRedes().FirstOrDefault(r => r.Id == id);
        }
    }
}
