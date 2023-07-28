using Newtonsoft.Json;
using servico_avaliacao.Domain.Entities;
using servico_avaliacao.ServicoSQS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace servico_avaliacao.Application.Services
{
    public class AvaliacaoService
    {
        private readonly SqsProducerService _producerService;
        double mediaMinimaAprovacao = 7.0;

        public AvaliacaoService(SqsProducerService producerService)
        {
            _producerService = producerService;
        }

        public async Task ProcessarMensagemAsync(string messageBody)
        {
            try
            {
                AvaliacaoAlunoNotas avaliacaoAluno = JsonConvert.DeserializeObject<AvaliacaoAlunoNotas>(messageBody);

                var situacaoAluno = avaliacaoAluno.VerificarAprovacao(mediaMinimaAprovacao)? "Aprovado":"Reprovado";

                double mediaNotas = avaliacaoAluno.CalcularMediaNotas();

     
                Debug.WriteLine($"Média das notas do aluno ID {avaliacaoAluno.Id_aluno}: {mediaNotas}");

         
                string mediaJson = JsonConvert.SerializeObject(new { id_aluno = avaliacaoAluno.Id_aluno, id_curso = avaliacaoAluno.Id_curso, media = mediaNotas, situacao = situacaoAluno });
                await _producerService.SendAsync(mediaJson);
            }
            catch (Exception ex)
            {

                Console.WriteLine("Erro ao processar mensagem: " + ex.Message);
            }
        }
    }
}