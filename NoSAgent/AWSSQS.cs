using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace NoSAgent
{
    public class AWSSQS
    {

        IAmazonSQS sqs = AWSClientFactory.CreateAmazonSQSClient(@"AKIAJ6Y5GWYOVHARJN3A", @"YdEB8MBNkbE2DdJIXYbAakRiKL45vlHlhLUx1PLs", RegionEndpoint.USEast1);
        //IAmazonSQS sqs = AWSClientFactory.CreateAmazonSQSClient(RegionEndpoint.USEast1);
        private const string QUEUE_NAME = "nosprawl-sqs-va";

        public void Enqueue(string json)
        {
            try
            {
                CreateQueueRequest sqsRequest = new CreateQueueRequest();
                sqsRequest.QueueName = QUEUE_NAME;
                CreateQueueResponse createQueueResponse = sqs.CreateQueue(sqsRequest);


                String myQueueUrl;
                myQueueUrl = createQueueResponse.QueueUrl;

                //Sending a message
                SendMessageRequest sendMessageRequest = new SendMessageRequest();
                sendMessageRequest.QueueUrl = myQueueUrl; //URL from initial queue creation
                sendMessageRequest.MessageBody = json;
                sqs.SendMessage(sendMessageRequest);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
