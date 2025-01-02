using Xunit;
using Moq;
using Backend.Models;
using Backend.Services;
using Backend.Data;
using System.ComponentModel.DataAnnotations;

namespace Backend.Tests
{
    public class GerenciadorLembretesTests
    {
        [Fact]
        public void Deve_Criar_Lembrete_Valido()
        {

            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembrete = new Lembrete
            {
                Titulo = "Daily da dti",
                DataLembrete = DateTime.Now.AddDays(1)
            };


            gerenciador.CriarLembrete(lembrete);


            mockRepositorio.Verify(r => r.Criar(It.Is<Lembrete>(l => l.Titulo == lembrete.Titulo)), Times.Once);
        }

        [Fact]
        public void Nao_Deve_Criar_Lembrete_Com_Data_Passada()
        {
            
            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembrete = new Lembrete
            {
                Titulo = "Reunião antiga",
                DataLembrete = DateTime.Now.AddDays(-1)
            };

        
            Assert.Throws<ValidationException>(() => gerenciador.CriarLembrete(lembrete));
            mockRepositorio.Verify(r => r.Criar(It.IsAny<Lembrete>()), Times.Never);
        }

        [Fact]
        public void Deve_Deletar_Lembrete_Existente()
        {
            
            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembreteId = 1;

            mockRepositorio.Setup(r => r.ObterPorId(lembreteId))
                .Returns(new Lembrete { Id = lembreteId, Titulo = "Teste" });

            
            gerenciador.ExcluirLembrete(lembreteId);

            
            mockRepositorio.Verify(r => r.Excluir(lembreteId), Times.Once);
        }

        [Fact]
        public void Nao_Deve_Deletar_Lembrete_Inexistente()
        {
            
            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembreteId = 1;

            mockRepositorio.Setup(r => r.ObterPorId(lembreteId))
                .Returns((Lembrete?)null);

            
            Assert.Throws<KeyNotFoundException>(() => gerenciador.ExcluirLembrete(lembreteId));
            mockRepositorio.Verify(r => r.Excluir(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Deve_Alterar_Lembrete_Existente()
        {
            
            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembreteId = 1;
            var lembreteAtualizado = new Lembrete
            {
                Id = lembreteId,
                Titulo = "Lembrete Atualizado",
                DataLembrete = DateTime.Now.AddDays(2),
                Concluido = true
            };

            mockRepositorio.Setup(r => r.ObterPorId(lembreteId))
                .Returns(new Lembrete { Id = lembreteId, Titulo = "Antigo", DataLembrete = DateTime.Now.AddDays(1) });

            
            gerenciador.AtualizarLembrete(lembreteId, lembreteAtualizado);

            
            mockRepositorio.Verify(r => r.Atualizar(It.Is<Lembrete>(l =>
                l.Id == lembreteId &&
                l.Titulo == "Lembrete Atualizado" &&
                l.DataLembrete == lembreteAtualizado.DataLembrete &&
                l.Concluido)), Times.Once);
        }

        [Fact]
        public void Nao_Deve_Alterar_Lembrete_Inexistente()
        {
            
            var mockRepositorio = new Mock<IRepositorioLembretes>();
            var gerenciador = new GerenciadorLembretes(mockRepositorio.Object);
            var lembreteId = 1;
            var lembreteAtualizado = new Lembrete
            {
                Id = lembreteId,
                Titulo = "Lembrete Atualizado",
                DataLembrete = DateTime.Now.AddDays(2),
                Concluido = true
            };

            mockRepositorio.Setup(r => r.ObterPorId(lembreteId))
                .Returns((Lembrete?)null);

            
            Assert.Throws<KeyNotFoundException>(() => gerenciador.AtualizarLembrete(lembreteId, lembreteAtualizado));
            mockRepositorio.Verify(r => r.Atualizar(It.IsAny<Lembrete>()), Times.Never);
        }

    }

}
