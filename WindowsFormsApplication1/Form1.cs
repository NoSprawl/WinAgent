using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private TcpListener tcpListener;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //IPAddress ip = IPAddress.Parse("10.100.0.215");
                //IPAddress ip = IPAddress.Parse("127.0.0.1");
                //tcpListener = new TcpListener(ip, 80);
                //tcpListener.Start();
                //tcpListener.BeginAcceptTcpClient(this.OnAcceptConnection, tcpListener);
                //MessageBox.Show("Listening");

                MobileDDRequest x = new MobileDDRequest();
                x.searchTerm = "Nestle";
                x.includeImages = false;
                x.checkType = "MobileDD";

                string test = JsonConvert.SerializeObject(x);

                //string URI = "http://127.0.0.1:34343";
                byte[] myParameters = Encoding.UTF8.GetBytes(test);

                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 34343);
                NetworkStream networkStream = client.GetStream();

                networkStream.Write(myParameters, 0, myParameters.Length);
                byte[] buffer = new byte[1024];
                int result = 0;
                string data = string.Empty;

                while(!networkStream.DataAvailable)
                {
                    Application.DoEvents();
                }


                while (networkStream.DataAvailable && client.Connected)
                {
                    result = networkStream.Read(buffer, 0, 1024);
                    data += Encoding.UTF8.GetString(buffer, 0, result);
                }

                MessageBox.Show(data);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void OnAcceptConnection(IAsyncResult asyn)
        {
            try
            {
                // Get the listener that handles the client request.
                TcpListener listener = (TcpListener)asyn.AsyncState;

                // Get the newly connected TcpClient
                TcpClient client = listener.EndAcceptTcpClient(asyn);

                MessageBox.Show("Connect");

                /*
                byte[] buffer = new byte[1024];
                int result = 1;
                string response = string.Empty;

                NetworkStream networkStream = client.GetStream();
                while (result > 0 && client.Connected)
                {
                    result = networkStream.Read(buffer, 0, 1024);
                    response += Encoding.UTF8.GetString(buffer, 0, result);
                }

                client.Close();
                response = response.TrimEnd('\r', '\n');
                ParseXML(response);

                // Issue another connect, only do this if you want to handle multiple clients
                listener.BeginAcceptTcpClient(this.OnAcceptConnection, listener);
                 */
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                //MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            SSLCheckRequest x = new SSLCheckRequest();
            //x.hosts = new List<string>() { "bestbridalprices.com" };
            x.hosts = new List<string>() { "hp.com" };

            x.ports = new List<int>() { 443 };
            x.checkType = "SSLCheck";

            string test = JsonConvert.SerializeObject(x);
            byte[] myParameters = Encoding.UTF8.GetBytes(test);

            TcpClient client = new TcpClient();
            client.Connect("127.0.0.1", 34343);
            NetworkStream networkStream = client.GetStream();

            networkStream.Write(myParameters, 0, myParameters.Length);
            byte[] buffer = new byte[1024];
            int result = 0;
            string data = string.Empty;

            while (!networkStream.DataAvailable)
            {
                Application.DoEvents();
            }


            while (networkStream.DataAvailable && client.Connected)
            {
                result = networkStream.Read(buffer, 0, 1024);
                data += Encoding.UTF8.GetString(buffer, 0, result);
            }

            MessageBox.Show(data);
        }
    }

    public class MobileDDRequest
    {
        public string checkType { get; set; }
        public string searchTerm { get; set; }
        public bool includeImages { get; set; }
    }

    public class SSLCheckRequest
    {
        public string checkType { get; set; }
        public List<string> hosts { get; set; }
        public List<int> ports { get; set; }
    }

}
