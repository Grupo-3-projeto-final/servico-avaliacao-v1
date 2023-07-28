using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace servico_avaliacao.ServicoSQS
{
    public class SqsProducerService
    {

        private readonly AmazonSQSClient _sqsClient;
        private readonly string _queueUrl;

        public SqsProducerService()
        {
            var awsAccessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
            var awsSecretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
            var awsRegion = ConfigurationManager.AppSettings["AWSRegion"];
            _queueUrl = ConfigurationManager.AppSettings["QueueUrlProducer"];

            if (string.IsNullOrEmpty(awsAccessKey) || string.IsNullOrEmpty(awsSecretKey) || string.IsNullOrEmpty(awsRegion))
            {
                throw new Exception("Missing AWS environment variables");
            }

            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(awsAccessKey, awsSecretKey);
            var config = new AmazonSQSConfig { RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion) };
            _sqsClient = new AmazonSQSClient(awsCredentials, config);
        }

        public async Task SendAsync(string message)
        {
            var sendRequest = new SendMessageRequest
            {
                QueueUrl = _queueUrl,
                MessageBody = message
            };

            await _sqsClient.SendMessageAsync(sendRequest);
        }

    }
}