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

        IAmazonSQS sqs = AWSClientFactory.CreateAmazonSQSClient(@"AKIAJA35TKQ3MW7RPZJA", @"/tipcgUGa4Lqa+p1HxZvUzgwRNfw3sUWZ32pJxVv", RegionEndpoint.USEast1);

        public void Enqueue(string json)
        {
            try
            {
                CreateQueueRequest sqsRequest = new CreateQueueRequest();
                sqsRequest.QueueName = "nosprawl_agents";
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
