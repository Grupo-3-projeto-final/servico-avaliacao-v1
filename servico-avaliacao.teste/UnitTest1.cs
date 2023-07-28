using Microsoft.VisualStudio.TestTools.UnitTesting;
using servico_avaliacao.Domain.Entities;

namespace servico_avaliacao.teste
{
    [TestClass]
    public class AvaliacaoAlunoNotasTests
    {
        [TestMethod]
        public void TestCalcularMediaNotas_CenarioValido()
        {
            // Arrange
            AvaliacaoAlunoNotas avaliacao = new AvaliacaoAlunoNotas
            {
                AvaliacaoNotas = new NotaAvaliacao
                {
                    NotaA1 = 8.5,
                    NotaA2 = 7.0,
                    NotaA3 = 6.0
                }
            };

            // Act
            double media = avaliacao.CalcularMediaNotas();

            // Assert
            Assert.AreEqual(7.166666666666667, media, 0.00001);
        }

        [TestMethod]
        public void TestCalcularMediaNotas_TodasNotasZeradas()
        {
            // Arrange
            AvaliacaoAlunoNotas avaliacao = new AvaliacaoAlunoNotas
            {
                AvaliacaoNotas = new NotaAvaliacao
                {
                    NotaA1 = 0.0,
                    NotaA2 = 0.0,
                    NotaA3 = 0.0
                }
            };

            // Act
            double media = avaliacao.CalcularMediaNotas();

            // Assert
            Assert.AreEqual(0.0, media, 0.00001);
        }

        [TestMethod]
        public void TestCalcularMediaNotas_NotasNegativas()
        {
            // Arrange
            AvaliacaoAlunoNotas avaliacao = new AvaliacaoAlunoNotas
            {
                AvaliacaoNotas = new NotaAvaliacao
                {
                    NotaA1 = -5.0,
                    NotaA2 = -3.0,
                    NotaA3 = -2.0
                }
            };

            // Act
            double media = avaliacao.CalcularMediaNotas();

            // Assert
            Assert.AreEqual(-3.3333333333333, media, 0.00001);
        }

        [TestMethod]
        public void TesteVerificarAprovacao_AlunoAprovado()
        {
            // Arrange
            double notaA1 = 7.0;
            double notaA2 = 8.0;
            double notaA3 = 6.5;
            double mediaEsperada = (notaA1 + notaA2 + notaA3) / 3.0;

            NotaAvaliacao avaliacaoNotas = new NotaAvaliacao
            {
                NotaA1 = notaA1,
                NotaA2 = notaA2,
                NotaA3 = notaA3
            };

            AvaliacaoAlunoNotas avaliacaoAluno = new AvaliacaoAlunoNotas
            {
                Id_aluno = 1,
                Id_curso = 101,
                AvaliacaoNotas = avaliacaoNotas
            };

            double mediaMinimaAprovacao = 6.0;

            // Act
            bool alunoAprovado = avaliacaoAluno.VerificarAprovacao(mediaMinimaAprovacao);

            // Assert
            Assert.IsTrue(alunoAprovado, $"Aluno deveria ter sido aprovado com média {mediaEsperada}.");
        }

        [TestMethod]
        public void TesteVerificarAprovacao_AlunoReprovado()
        {
            // Arrange
            double notaA1 = 5.0;
            double notaA2 = 4.5;
            double notaA3 = 5.5;
            double mediaEsperada = (notaA1 + notaA2 + notaA3) / 3.0;

            NotaAvaliacao avaliacaoNotas = new NotaAvaliacao
            {
                NotaA1 = notaA1,
                NotaA2 = notaA2,
                NotaA3 = notaA3
            };

            AvaliacaoAlunoNotas avaliacaoAluno = new AvaliacaoAlunoNotas
            {
                Id_aluno = 2,
                Id_curso = 102,
                AvaliacaoNotas = avaliacaoNotas
            };

            double mediaMinimaAprovacao = 6.0;

            // Act
            bool alunoAprovado = avaliacaoAluno.VerificarAprovacao(mediaMinimaAprovacao);

            // Assert
            Assert.IsFalse(alunoAprovado, $"Aluno deveria ter sido reprovado com média {mediaEsperada}.");
        }
    }
}

