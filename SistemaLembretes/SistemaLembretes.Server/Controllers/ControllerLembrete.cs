// Controllers/LembreteController.cs
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

        [HttpGet]
        public IActionResult ObterTodosLembretes()
        {
            try
            {
                var lembretes = _gerenciadorLembretes.ObterTodosLembretes();
                return Ok(lembretes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterLembretePorId(int id)
        {
            try
            {
                var lembrete = _gerenciadorLembretes.ObterLembretePorId(id);
                return Ok(lembrete);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Lembrete não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CriarLembrete([FromBody] Lembrete lembrete)
        {
            try
            {
                _gerenciadorLembretes.CriarLembrete(lembrete);
                return CreatedAtAction(nameof(ObterLembretePorId), new { id = lembrete.Id }, lembrete);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult AtualizarLembrete(int id, [FromBody] Lembrete lembrete)
        {
            try
            {
                _gerenciadorLembretes.AtualizarLembrete(id, lembrete);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Lembrete não encontrado.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ExcluirLembrete(int id)
        {
            try
            {
                _gerenciadorLembretes.ExcluirLembrete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Lembrete não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}/concluir")]
        public IActionResult MarcarComoConcluido(int id)
        {
            try
            {
                _gerenciadorLembretes.MarcarComoConcluido(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Lembrete não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
