using Backend.Models;
using Backend.Configs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Data
{
    public interface IRepositorioLembretes
    {
        void Criar(Lembrete lembrete);
        IEnumerable<Lembrete> ObterTodos();
        Lembrete ObterPorId(int id);
        void Atualizar(Lembrete lembrete);
        void Excluir(int id);
    }

    public class RepositorioLembretes(DatabaseContext context) : IRepositorioLembretes
    {
        private readonly DatabaseContext _context = context;

        public void Criar(Lembrete lembrete)
        {
            _context.Lembretes.Add(lembrete);
            _context.SaveChanges();
        }

        public IEnumerable<Lembrete> ObterTodos()
        {
            return _context.Lembretes.AsNoTracking().ToList();
        }

        public Lembrete ObterPorId(int id)
        {
            return _context.Lembretes.AsNoTracking().FirstOrDefault(l => l.Id == id);
        }

        public void Atualizar(Lembrete lembrete)
        {
            _context.Lembretes.Update(lembrete);
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var lembrete = ObterPorId(id);
            if (lembrete != null)
            {
                _context.Lembretes.Remove(lembrete);
                _context.SaveChanges();
            }
        }
    }
}