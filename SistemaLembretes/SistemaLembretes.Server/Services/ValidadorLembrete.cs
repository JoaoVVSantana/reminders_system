using Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace Backend.Validation
{
    public class ValidadorLembrete
    {
        public static void Validar(Lembrete lembrete)
        {
            var contexto = new ValidationContext(lembrete);
            Validator.ValidateObject(lembrete, contexto, validateAllProperties: true);

            if (lembrete.DataLembrete <= DateTime.Now)
            {
                throw new ValidationException("A data do lembrete deve estar no futuro.");
            }
        }
    }
}