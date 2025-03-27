using System.Windows;
using System.ComponentModel.DataAnnotations;

namespace NotasAPI.Models
{
    public class Nota
    {

        public Guid Id { get; set; } // Chave primária

        [Required]
        [MaxLength(100)]
        public string Aluno { get; set; }

        [Required]
        [MaxLength(100)]
        public string Disciplina { get; set; }

        [Range(0, 10)]
        public decimal Valor { get; set; }

        public DateTime DataLancamento { get; set; } = DateTime.UtcNow;
    }
}

