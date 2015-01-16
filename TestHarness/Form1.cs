using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

using Newtonsoft.Json;

namespace TestHarness
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /*
             * {
  customer_id: 123,
  hostname: “”,
  last_update_ran: “”,
  installed: [{name: ‘’, version: ‘’, service: true/false, running: true/false/null}]
}

             */

            //InstalledApplications ias = new InstalledApplications();
            //List<InstalledApplication> apps = ias.GetInstalledApplications();

            //Product product = new Product();
            //product.Name = "Apple";
            //product.Expiry = new DateTime(2008, 12, 28);
            //product.Sizes = new string[] { "Small" };

            Profile x = new Profile();
            string json = JsonConvert.SerializeObject(x);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
 * AWS Login Credentials (IAM)
arn: arn:aws:iam::480589117377:user/nosprawl_agent
username: nosprawl_agent
access key id: AKIAJA35TKQ3MW7RPZJA
secret access key: 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

SQS name: nosprawl_agents
SQS arn: arn:aws:sqs:us-east-1:480589117377:nosprawl_agents

 */
            try
            {
                IAmazonSQS sqs = AWSClientFactory.CreateAmazonSQSClient(@"AKIAJA35TKQ3MW7RPZJA", @"/tipcgUGa4Lqa+p1HxZvUzgwRNfw3sUWZ32pJxVv", RegionEndpoint.USEast1);

                CreateQueueRequest sqsRequest = new CreateQueueRequest();
                sqsRequest.QueueName = "nosprawl_agents";

                CreateQueueResponse createQueueResponse = sqs.CreateQueue(sqsRequest);

                String myQueueUrl;
                myQueueUrl = createQueueResponse.QueueUrl;

                //Confirming the queue exists
                ListQueuesRequest listQueuesRequest = new ListQueuesRequest();
                ListQueuesResponse listQueuesResponse = sqs.ListQueues(listQueuesRequest);

                Console.WriteLine("Printing list of Amazon SQS queues.\n");
                foreach (String queueUrl in listQueuesResponse.QueueUrls)
                {
                    Console.WriteLine("  QueueUrl: {0}", queueUrl);
                }
                Console.WriteLine();

                //Sending a message
                Console.WriteLine("Sending a message to MyQueue.\n");
                SendMessageRequest sendMessageRequest = new SendMessageRequest();
                sendMessageRequest.QueueUrl = myQueueUrl; //URL from initial queue creation
                sendMessageRequest.MessageBody = "This is my message text.";
                sqs.SendMessage(sendMessageRequest);

                //Receiving a message
                ReceiveMessageRequest receiveMessageRequest = new ReceiveMessageRequest();
                receiveMessageRequest.QueueUrl = myQueueUrl;
                ReceiveMessageResponse receiveMessageResponse = sqs.ReceiveMessage(receiveMessageRequest);

                Console.WriteLine("Printing received message.\n");
                foreach (Amazon.SQS.Model.Message message in receiveMessageResponse.Messages)
                {
                    Console.WriteLine("  Message");
                    Console.WriteLine("    MessageId: {0}", message.MessageId);
                    Console.WriteLine("    ReceiptHandle: {0}", message.ReceiptHandle);
                    Console.WriteLine("    MD5OfBody: {0}", message.MD5OfBody);
                    Console.WriteLine("    Body: {0}", message.Body);

                    foreach (KeyValuePair<string, string> entry in message.Attributes)
                    {
                        Console.WriteLine("  Attribute");
                        Console.WriteLine("    Name: {0}", entry.Key);
                        Console.WriteLine("    Value: {0}", entry.Value);
                    }
                }
                String messageRecieptHandle = receiveMessageResponse.Messages[0].ReceiptHandle;

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
