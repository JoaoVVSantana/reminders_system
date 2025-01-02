using Xunit;
using Backend.Models;

public class LembreteTests
{
    [Fact]
    public void Deve_Validar_Lembrete_Com_Dados_Corretos()
    {
        var lembrete = new Lembrete { Titulo = "Estudar"};
        Assert.NotNull(lembrete.Titulo);
        Assert.True(lembrete.DataLembrete > DateTime.Now);
    }

    [Fact]
    public void Nao_Deve_Validar_Lembrete_Sem_Nome()
    {
        var lembrete = new Lembrete { Titulo = ""};
        Assert.False(string.IsNullOrWhiteSpace(lembrete.Titulo));
    }
}
