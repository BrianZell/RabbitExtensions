using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDT.RabbitMQ.Extensions;
using IDT.RabbitMQ.Extensions.Internals;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing.Impl;

namespace IDT.ManualShovel
{
    public partial class ConnectionForm : Form
    {
        public ConnectionForm()
        {
            InitializeComponent();
        }

        public ConnectionFactory Factory { get; private set; }

        public string QueueName { get; private set; }

        private void _btnConnect_Click(object sender, EventArgs e)
        {
            if (!ValidateFields())
            {
                return;
            }

            Factory = new ConnectionFactory
            {
                HostName = _cboSourceBroker.Text,
                VirtualHost = _cboSourceVirtualHost.Text,
                UserName = _sourceUserName.Text,
                Password = _txtPassword.Text,
                RequestedHeartbeat = 5,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5),
            };
            QueueName = _txtQueueName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateFields()
        {
            _errorProvider.Clear();

            return HasRequiredText(_errorProvider, _cboSourceBroker, "Broker")
                   & HasRequiredText(_errorProvider, _cboSourceVirtualHost, "Virtual Host")
                   & HasRequiredText(_errorProvider, _sourceUserName, "User Name")
                   & HasRequiredText(_errorProvider, _txtPassword, "Password")
                   & HasRequiredText(_errorProvider, _txtQueueName, "Queue Name");
        }

        private bool HasRequiredText(ErrorProvider provider, Control control, string name)
        {
            if (string.IsNullOrEmpty(control.Text))
            {
                provider.SetError(control,string.Format("{0} is a required field",name));
                return false;
            }
            return true;
        }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
