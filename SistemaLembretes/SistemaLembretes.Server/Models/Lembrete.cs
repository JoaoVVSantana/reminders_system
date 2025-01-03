using System;
using System.ComponentModel.DataAnnotations;


namespace Backend.Models
{
    public class Lembrete
    {
        [Key]
        public int Id { get; set; }

        public string? Titulo { get; set; }

        public string? Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataLembrete { get; set; }

        public bool Concluido { get; set; } = false;
    }
}