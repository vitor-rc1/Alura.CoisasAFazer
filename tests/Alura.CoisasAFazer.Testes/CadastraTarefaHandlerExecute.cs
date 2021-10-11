using Alura.CoisasAFazer.Core.Commands;
using Alura.CoisasAFazer.Core.Models;
using Alura.CoisasAFazer.Services.Handlers;
using System;
using Xunit;

namespace Alura.CoisasAFazer.Testes
{
    public class CadastraTarefaHandlerExecute
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var comando = new CadastraTarefa(
                "Estudar XUnit", 
                new Categoria("Estudo"), 
                new DateTime(2019, 12, 31));

            var handler = new CadastraTarefaHandler();

            // Act
            handler.Execute(comando); 

            // Assert

        }
    }
}
