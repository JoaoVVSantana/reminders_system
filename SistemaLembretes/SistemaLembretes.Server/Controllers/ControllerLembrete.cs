using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LembreteController(IGerenciadorLembretes gerenciadorLembretes) : ControllerBase
    {
        private readonly IGerenciadorLembretes _gerenciadorLembretes = gerenciadorLembretes;

        [HttpGet("todos")]
        public IActionResult ObterTodosLembretes()
        {
            try
            {
                var lembretes = _gerenciadorLembretes.ObterTodosLembretes();
                return Ok(lembretes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao obter lembretes.", details = ex.Message });
            }
        }

        [HttpGet("{id}/obterLembrete")]
        public IActionResult ObterLembretePorId(int id)
        {
            try
            {
                var lembrete = _gerenciadorLembretes.ObterLembretePorId(id);
                return Ok(lembrete);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Lembrete não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao obter lembrete.", details = ex.Message });
            }
        }

        [HttpPost("criarLembrete")]
        public IActionResult CriarLembrete([FromBody] Lembrete lembrete)
        {
            try
            {
                if (lembrete.DataLembrete <= DateTime.Now) throw new ValidationException("A data deve ser futura");

                _gerenciadorLembretes.CriarLembrete(lembrete);
                return CreatedAtAction(nameof(ObterLembretePorId), new { id = lembrete.Id }, lembrete);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = "Erro de validação.", details = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar lembrete.", details = ex.Message });
            }
        }

        [HttpDelete("{id}/apagarLembrete")]
        public IActionResult ExcluirLembrete(int id)
        {
            try
            {
                _gerenciadorLembretes.ExcluirLembrete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Lembrete não encontrado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao excluir lembrete.", details = ex.Message });
            }
        }
    }
}
