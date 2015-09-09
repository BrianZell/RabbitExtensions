using System;
using System.Linq;
using System.Windows.Forms;
using RabbitMQ.Client.Exceptions;

namespace IDT.ManualShovel
{
    public partial class Form1 : Form
    {
        private const int PageSize = 30;
        private Controller _controller;
        private SortableBindingList<MessageView> _viewList = new SortableBindingList<MessageView>();

        public Form1()
        {
            InitializeComponent();
            this._gdCurrentMessages.DataSource = _viewList;
            this.components.Add(new DisposerComponent(b =>{
                                                              if (b && _controller != null)
                                                              {
                                                                  _controller.Dispose();
                                                                  _controller = null;
                                                              }
                                                          }));
        }

        private void _btnSkipMessage_Click(object sender, EventArgs e)
        {
            if (_controller == null)
            {
                return;
            }

            try
            {
                var allMessages = _viewList.Cast<MessageView>().ToList();
                _viewList.Clear();
                //var allMessages = _dsMessageViews.Cast<MessageView>().ToList();
                //_dsMessageViews.Clear();
                var newMessages = _controller.RequeueItemsAndGetMore(allMessages).ToList();
                //newMessages.ForEach(x => _dsMessageViews.Add(x));
                newMessages.ForEach(x => _viewList.Add(x));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Occured");
                throw;
            }
        }

        private void _btnSendToDestination_Click(object sender, EventArgs e)
        {
            if (_controller == null)
            {
                return;
            }

            try
            {
                var selectedMessages = _viewList.Cast<MessageView>().Where(x => x.Selected).ToList();
                var sendToQueue = selectedMessages.Select(x => x.DLQueue).FirstOrDefault(x => !string.IsNullOrEmpty(x));
                using (var form = new DestinationForm(sendToQueue))
                {
                    form.ShowDialog();
                    if (form.DialogResult != DialogResult.OK)
                    {
                        return;
                    }
                    sendToQueue = form.QueueName;
                }
                var newMessages = _controller.SendToDestination(selectedMessages, sendToQueue).ToList();
                selectedMessages.ForEach(x => _viewList.Remove(x));
                newMessages.ForEach(x => _viewList.Add(x));
            }
            catch (InvalidQueueException ex)
            {
                MessageBox.Show(ex.ToString(),"Invalid Queue");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Occured");
                throw;
            }
        }

        private void sourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                _viewList.Clear();
                if (_controller != null)
                {
                    _controller.Dispose();
                    _controller = null;
                }

                using (var form = new ConnectionForm())
                {
                    form.Name = "Set Connection";
                    var result = form.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _controller = new Controller(form.Factory, form.QueueName);
                        var messages = _controller.GetAvailableMessages(PageSize).ToList();
                        messages.ForEach(x => _viewList.Add(x));
                    }
                }
            }
            catch (BrokerUnreachableException bex)
            {
                MessageBox.Show(bex.ToString(), "Broker Unreachable");
            }
            catch (OperationInterruptedException oex)
            {
                MessageBox.Show(oex.ToString(), "Operation Canceled");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error Occured");
                throw;
            }
        }

        private void _gdCurrentMessages_SelectionChanged(object sender, EventArgs e)
        {
            if (_gdCurrentMessages.SelectedRows.Count <= 0)
            {
                _txtOutput.Text = string.Empty;
                return;
            }

            var view = (MessageView)_gdCurrentMessages.SelectedRows[0].DataBoundItem;
            if (view != null)
            {
                _txtOutput.Text = view.DisplayMessage();
            }
        }

        private void selectAllRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MessageView dsMessageView in _viewList)
            {
                dsMessageView.Selected = true;
            }
        }

        private void unselectAllRowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (MessageView dsMessageView in _viewList)
            {
                dsMessageView.Selected = false;
            }
        }
    }
}
