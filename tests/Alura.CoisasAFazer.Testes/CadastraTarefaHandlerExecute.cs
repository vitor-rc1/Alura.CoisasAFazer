using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Infrastructure;
using Alura.CoisasAFazer.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;

namespace Alura.CoisasAFazer.Testes
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void DataTarefaComInfoValidasDeveIncluirNoBanco()
        {
            // Arrange
            var comando = new CadastraTarefa(
                "Estudar XUnit", 
                new Categoria("Estudo"), 
                new DateTime(2019, 12, 31));

            var options = new DbContextOptionsBuilder<DbTarefasContext>()
                .UseInMemoryDatabase("DbTarefasContext2")
                .Options;
            var context = new DbTarefasContext(options);
            var repo = new RepositorioTarefa(context);

            var handler = new CadastraTarefaHandler(repo);

            // Act
            handler.Execute(comando);

            // Assert
            var tarefa = repo.ObtemTarefas(t => t.Titulo == "Estudar XUnit").FirstOrDefault();
            Assert.NotNull(tarefa);
        }

        [Fact]
        public void QuandoExceptionForLancadaResultadoIsSuccessDeveSerFalse()
        {
            // Arrange
            var comando = new CadastraTarefa(
                "Estudar XUnit",
                new Categoria("Estudo"),
                new DateTime(2019, 12, 31));

            var mock = new Mock<IRepositorioTarefas>();

            mock.Setup(r => r.IncluirTarefas(It.IsAny<Tarefa[]>()))
                .Throws(new Exception("Houve um erro na inclusão de tarefas"));

            var repo = mock.Object;

            var handler = new CadastraTarefaHandler(repo);

            // Act
            CommandResult resultado = handler.Execute(comando);

            // Assert
            Assert.False(resultado.IsSuccess);
        }
    }
}
