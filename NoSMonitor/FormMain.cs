using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoSMonitor
{
    public partial class FormMain : Form
    {
        private NoSAgentService.Settings settings = null;

        public FormMain()
        {
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            InitializeComponent();
            GetSettings();
        }


        private void GetSettings()
        {
            NoSAgentService.AgentServiceClient agent = new NoSAgentService.AgentServiceClient();
            settings = agent.GetSettings();
            textBoxCustomerID.Text = settings.CustomerId;
            textBoxInterval.Text = settings.Interval.ToString();
        }

        private void StartService()
        {

        }


        private void StopService()
        {
            NoSAgentService.AgentServiceClient agent = new NoSAgentService.AgentServiceClient();
            agent.StopAgent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                NoSAgentService.AgentServiceClient agent = new NoSAgentService.AgentServiceClient();
                settings.CustomerId = textBoxCustomerID.Text;
                settings.Interval = Convert.ToInt16(textBoxInterval.Text);
                agent.SaveSettings(settings);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            MessageBox.Show("Settings saved");

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void configurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void buttonSync_Click(object sender, EventArgs e)
        {
            try
            {
                NoSAgentService.AgentServiceClient agent = new NoSAgentService.AgentServiceClient();
                agent.Send();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
