using Backend.Models;
using Backend.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;

namespace Backend.Services
{
    public interface IGerenciadorLembretes
    {
        void CriarLembrete(Lembrete lembrete);
        IEnumerable<Lembrete> ObterTodosLembretes();
        IEnumerable<Lembrete> ObterLembretesFuturos();
        Lembrete ObterLembretePorId(int id);
        void AtualizarLembrete(int id, Lembrete lembreteAtualizado);
        void ExcluirLembrete(int id);
        
    }

    public class GerenciadorLembretes(IRepositorioLembretes repositorio) : IGerenciadorLembretes
    {
        private IRepositorioLembretes _repositorio = repositorio;

        public void CriarLembrete(Lembrete lembrete)
        {

            ValidarLembrete(lembrete);

            _repositorio.Criar(lembrete);
               
            
            
        }

        private void ValidarLembrete (Lembrete lembrete)
        {

            if (lembrete.Titulo.Length > 50)
            {
                throw new ValidationException("O titulo deve conter no maximo 50 caracteres.");
            }

            if (lembrete.Descricao != null && lembrete.Descricao.Length > 50)
            {
                throw new ValidationException("A descricao deve conter no maximo 50 caracteres.");
            }

            if (lembrete.DataLembrete.Date < DateTime.Today)
            {
                throw new ValidationException("A data nao pode ser no passado.");
            }
        }

        public IEnumerable<Lembrete> ObterTodosLembretes()
        {
            return _repositorio.ObterTodos().OrderBy(l => l.DataLembrete);
        }

        public IEnumerable<Lembrete> ObterLembretesFuturos()
        {
            return _repositorio.ObterTodos().Where(l => l.DataLembrete > DateTime.Now).OrderBy(l => l.DataLembrete);
        }

        public Lembrete ObterLembretePorId(int id)
        {
            var lembrete = _repositorio.ObterPorId(id);
            return lembrete ?? throw new KeyNotFoundException("Lembrete não encontrado.");
        }

        public void AtualizarLembrete(int id, Lembrete lembreteAtualizado)
        {
            var lembreteExistente = _repositorio.ObterPorId(id) ?? throw new KeyNotFoundException("Lembrete não encontrado.");
            lembreteExistente.Titulo = lembreteAtualizado.Titulo;
            lembreteExistente.Descricao = lembreteAtualizado.Descricao;
            lembreteExistente.DataLembrete = lembreteAtualizado.DataLembrete;
            lembreteExistente.Concluido = lembreteAtualizado.Concluido;
            

            _repositorio.Atualizar(lembreteExistente);
        }

        public void ExcluirLembrete(int id)
        {
            Lembrete lembrete = _repositorio.ObterPorId(id) ?? throw new KeyNotFoundException("Lembrete não encontrado.");
            _repositorio.Excluir(id);
        }

        
    }
}