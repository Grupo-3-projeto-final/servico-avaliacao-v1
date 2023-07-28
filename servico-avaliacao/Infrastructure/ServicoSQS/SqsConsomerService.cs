using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Newtonsoft.Json;
using servico_avaliacao.Application.Services;
using servico_avaliacao.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;


namespace servico_avaliacao.ServicoSQS
{
    public class SqsConsumerService
    {
        private readonly AmazonSQSClient _sqsClient;
        private readonly string _queueUrl;
        private readonly AvaliacaoService _avaliacaoService;
        private readonly SqsProducerService _producerService;

        public SqsConsumerService()
        {

            var awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            var awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            var awsRegion = ConfigurationManager.AppSettings["AWSRegion"];
            _queueUrl = ConfigurationManager.AppSettings["QueueUrlConsomer"];

            if (string.IsNullOrEmpty(awsAccessKey) || string.IsNullOrEmpty(awsSecretKey) || string.IsNullOrEmpty(awsRegion))
            {
                throw new Exception("Missing AWS environment variables");
            }

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            var config = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion) };
            _sqsClient = new AmazonSQSClient(awsCredentials, config);
            _producerService = new SqsProducerService();
            _avaliacaoService = new AvaliacaoService(_producerService);
        }

        public async Task StartListeningAsync()
        {
            var receiveMessageRequest = new ReceiveMessageRequest
            {
                QueueUrl = _queueUrl,
                MaxNumberOfMessages = 10, 
                WaitTimeSeconds = 20, 
                VisibilityTimeout = 60 
            };

            while (true)
            {
                var receiveMessageResponse = await _sqsClient.ReceiveMessageAsync(receiveMessageRequest);
                Debug.WriteLine("Buscando mensagens na fila...");

                if (receiveMessageResponse.Messages.Count == 0)
                {
                   
                    continue;
                }

                foreach (var message in receiveMessageResponse.Messages)
                {
                    await _avaliacaoService.ProcessarMensagemAsync(message.Body);
                    Debug.WriteLine($"Mensagem recebida: {message.Body}");
                   

                   
                    await DeleteMessageAsync(message);
                }
            }
        }

        private async Task DeleteMessageAsync(Message message)
        {
            var deleteMessageRequest = new DeleteMessageRequest
            {
                QueueUrl = _queueUrl,
                ReceiptHandle = message.ReceiptHandle
            };

            await _sqsClient.DeleteMessageAsync(deleteMessageRequest);
        }


    }
}