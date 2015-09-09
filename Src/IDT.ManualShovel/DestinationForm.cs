using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDT.ManualShovel
{
    public partial class DestinationForm : Form
    {
        public DestinationForm(string returnToQueueName)
        {
            InitializeComponent();
            _txtQueueName.Text = returnToQueueName;
        }

        public string QueueName { get; private set; }

        private void _btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void _btnSend_Click(object sender, EventArgs e)
        {
            _errorProvider.Clear();

            var queueName = _txtQueueName.Text;
            if (string.IsNullOrEmpty(queueName))
            {
                _errorProvider.SetError(_txtQueueName,"Queue Name is required");
                return;
            }
            
            QueueName = _txtQueueName.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
