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
                return Ok(lembretes); //http 200
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao obter lembretes. ", details = ex.Message });//server error
            }
        }

        [HttpGet("{id}/obterLembrete")]
        public IActionResult ObterLembretePorId(int id)
        {
            try
            {
                var lembrete = _gerenciadorLembretes.ObterLembretePorId(id);
                return Ok(lembrete);//http 200
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Lembrete não encontrado. " });//http 404
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao obter lembrete. ", details = ex.Message });//server error
            }
        }

        [HttpPost("criarLembrete")]
        public IActionResult CriarLembrete([FromBody] Lembrete lembrete)
        {
            try
            {
                _gerenciadorLembretes.CriarLembrete(lembrete);
                return CreatedAtAction(nameof(ObterLembretePorId), new { id = lembrete.Id }, lembrete);//http 201
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = "Erro ao criar: ", details = ex.Message });//http 400
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao criar lembrete: ", details = ex.Message });//server error
            }
        }

        [HttpDelete("{id}/apagarLembrete")]
        public IActionResult ExcluirLembrete(int id)
        {
            try
            {
                _gerenciadorLembretes.ExcluirLembrete(id);
                return NoContent();//http 204
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { message = "Lembrete não encontrado. " });//http 404
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Erro ao excluir lembrete. ", details = ex.Message });//server error
            }
        }
    }
}
