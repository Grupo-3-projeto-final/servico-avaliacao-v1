using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace servico_avaliacao.Domain.Entities
{
    public class AvaliacaoAlunoNotas
    {
        public int Id_aluno { get; set; }
        public int Id_curso { get; set; }
        public NotaAvaliacao AvaliacaoNotas { get; set; }

        public double CalcularMediaNotas()
        {
            double media = (AvaliacaoNotas.NotaA1 + AvaliacaoNotas.NotaA2 + AvaliacaoNotas.NotaA3) / 3.0;
            return media;
        }

        public bool VerificarAprovacao(double mediaMinimaAprovacao)
        {
            double media = CalcularMediaNotas();
            return media >= mediaMinimaAprovacao;
        }

    }
}