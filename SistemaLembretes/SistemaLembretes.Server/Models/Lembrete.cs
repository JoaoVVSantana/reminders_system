using System;
using System.ComponentModel.DataAnnotations;


namespace Backend.Models
{
    public class Lembrete
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o título do lembrete")]
        [StringLength(50, ErrorMessage = "O título deve conter no máximo 50 caracteres")]
        public string? Titulo { get; set; }

        [StringLength(100, ErrorMessage = "A descrição deve conter no máximo 100 caracteres")]
        public string? Descricao { get; set; }

        [Required(ErrorMessage = "Insira a data e a hora")]
        [DataType(DataType.DateTime)]
        public DateTime DataLembrete { get; set; }

        public bool Concluido { get; set; } = false;
    }
}