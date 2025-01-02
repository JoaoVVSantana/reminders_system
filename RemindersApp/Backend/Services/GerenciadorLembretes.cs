using Backend.Models;
using Backend.Data;
using Backend.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

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
        void MarcarComoConcluido(int id);
    }

    public class GerenciadorLembretes(IRepositorioLembretes repositorio) : IGerenciadorLembretes
    {
        private readonly IRepositorioLembretes _repositorio = repositorio;

        public void CriarLembrete(Lembrete lembrete)
        {
            ValidadorLembrete.Validar(lembrete);
            _repositorio.Criar(lembrete);
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
            lembreteExistente.DataAtualizacao = DateTime.Now;

            _repositorio.Atualizar(lembreteExistente);
        }

        public void ExcluirLembrete(int id)
        {
            Lembrete lembrete = _repositorio.ObterPorId(id) ?? throw new KeyNotFoundException("Lembrete não encontrado.");
            _repositorio.Excluir(id);
        }

        public void MarcarComoConcluido(int id)
        {
            var lembrete = _repositorio.ObterPorId(id) ?? throw new KeyNotFoundException("Lembrete não encontrado.");
            lembrete.Concluido = true;
            lembrete.DataAtualizacao = DateTime.Now;
            _repositorio.Atualizar(lembrete);
        }
    }
}