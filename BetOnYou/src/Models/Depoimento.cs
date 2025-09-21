using System;

namespace backend.src.Models
{
    public class Depoimento
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Texto { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
    }
}
