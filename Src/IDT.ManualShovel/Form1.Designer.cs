namespace IDT.ManualShovel
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this._txtOutput = new System.Windows.Forms.TextBox();
            this._statusStrip = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllRowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this._btnSendToDestination = new System.Windows.Forms.Button();
            this._btnSkipMessage = new System.Windows.Forms.Button();
            this._gdCurrentMessages = new System.Windows.Forms.DataGridView();
            this._dsMessageViews = new System.Windows.Forms.BindingSource(this.components);
            this.Selected = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.MessageId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RoutingKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Expiration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReplyTo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exchangeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLQueue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLRoutingKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLExchange = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DLReason = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gdCurrentMessages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._dsMessageViews)).BeginInit();
            this.SuspendLayout();
            // 
            // _txtOutput
            // 
            this._txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._txtOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._txtOutput.Location = new System.Drawing.Point(12, 322);
            this._txtOutput.Multiline = true;
            this._txtOutput.Name = "_txtOutput";
            this._txtOutput.ReadOnly = true;
            this._txtOutput.Size = new System.Drawing.Size(1030, 352);
            this._txtOutput.TabIndex = 0;
            // 
            // _statusStrip
            // 
            this._statusStrip.Location = new System.Drawing.Point(0, 744);
            this._statusStrip.Name = "_statusStrip";
            this._statusStrip.Size = new System.Drawing.Size(1054, 22);
            this._statusStrip.TabIndex = 1;
            this._statusStrip.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1054, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sourceToolStripMenuItem
            // 
            this.sourceToolStripMenuItem.Name = "sourceToolStripMenuItem";
            this.sourceToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.sourceToolStripMenuItem.Text = "Connect...";
            this.sourceToolStripMenuItem.Click += new System.EventHandler(this.sourceToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllRowsToolStripMenuItem,
            this.unselectAllRowsToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // selectAllRowsToolStripMenuItem
            // 
            this.selectAllRowsToolStripMenuItem.Name = "selectAllRowsToolStripMenuItem";
            this.selectAllRowsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.selectAllRowsToolStripMenuItem.Text = "Select All Rows";
            this.selectAllRowsToolStripMenuItem.Click += new System.EventHandler(this.selectAllRowsToolStripMenuItem_Click);
            // 
            // unselectAllRowsToolStripMenuItem
            // 
            this.unselectAllRowsToolStripMenuItem.Name = "unselectAllRowsToolStripMenuItem";
            this.unselectAllRowsToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.unselectAllRowsToolStripMenuItem.Text = "Unselect All Rows";
            this.unselectAllRowsToolStripMenuItem.Click += new System.EventHandler(this.unselectAllRowsToolStripMenuItem_Click);
            // 
            // _btnSendToDestination
            // 
            this._btnSendToDestination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSendToDestination.Location = new System.Drawing.Point(928, 718);
            this._btnSendToDestination.Name = "_btnSendToDestination";
            this._btnSendToDestination.Size = new System.Drawing.Size(114, 23);
            this._btnSendToDestination.TabIndex = 8;
            this._btnSendToDestination.Text = "Send to Destination";
            this._btnSendToDestination.UseVisualStyleBackColor = true;
            this._btnSendToDestination.Click += new System.EventHandler(this._btnSendToDestination_Click);
            // 
            // _btnSkipMessage
            // 
            this._btnSkipMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._btnSkipMessage.Location = new System.Drawing.Point(810, 718);
            this._btnSkipMessage.Name = "_btnSkipMessage";
            this._btnSkipMessage.Size = new System.Drawing.Size(112, 23);
            this._btnSkipMessage.TabIndex = 9;
            this._btnSkipMessage.Text = "Next Page";
            this._btnSkipMessage.UseVisualStyleBackColor = true;
            this._btnSkipMessage.Click += new System.EventHandler(this._btnSkipMessage_Click);
            // 
            // _gdCurrentMessages
            // 
            this._gdCurrentMessages.AllowUserToAddRows = false;
            this._gdCurrentMessages.AllowUserToDeleteRows = false;
            this._gdCurrentMessages.AllowUserToOrderColumns = true;
            this._gdCurrentMessages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._gdCurrentMessages.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._gdCurrentMessages.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this._gdCurrentMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._gdCurrentMessages.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.MessageId,
            this.Timestamp,
            this.Type,
            this.RoutingKey,
            this.Expiration,
            this.ReplyTo,
            this.exchangeDataGridViewTextBoxColumn,
            this.DLQueue,
            this.DLTime,
            this.DLRoutingKeys,
            this.DLExchange,
            this.DLReason});
            this._gdCurrentMessages.DataSource = this._dsMessageViews;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this._gdCurrentMessages.DefaultCellStyle = dataGridViewCellStyle2;
            this._gdCurrentMessages.Location = new System.Drawing.Point(12, 27);
            this._gdCurrentMessages.Name = "_gdCurrentMessages";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._gdCurrentMessages.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this._gdCurrentMessages.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this._gdCurrentMessages.Size = new System.Drawing.Size(1030, 289);
            this._gdCurrentMessages.TabIndex = 10;
            this._gdCurrentMessages.SelectionChanged += new System.EventHandler(this._gdCurrentMessages_SelectionChanged);
            // 
            // _dsMessageViews
            // 
            this._dsMessageViews.DataSource = typeof(IDT.ManualShovel.MessageView);
            // 
            // Selected
            // 
            this.Selected.DataPropertyName = "Selected";
            this.Selected.HeaderText = "Selected";
            this.Selected.Name = "Selected";
            this.Selected.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // MessageId
            // 
            this.MessageId.DataPropertyName = "MessageId";
            this.MessageId.HeaderText = "MessageId";
            this.MessageId.Name = "MessageId";
            this.MessageId.ReadOnly = true;
            // 
            // Timestamp
            // 
            this.Timestamp.DataPropertyName = "Timestamp";
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.DataPropertyName = "Type";
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // RoutingKey
            // 
            this.RoutingKey.DataPropertyName = "RoutingKey";
            this.RoutingKey.HeaderText = "RoutingKey";
            this.RoutingKey.Name = "RoutingKey";
            this.RoutingKey.ReadOnly = true;
            // 
            // Expiration
            // 
            this.Expiration.DataPropertyName = "Expiration";
            this.Expiration.HeaderText = "Expiration";
            this.Expiration.Name = "Expiration";
            this.Expiration.ReadOnly = true;
            // 
            // ReplyTo
            // 
            this.ReplyTo.DataPropertyName = "ReplyTo";
            this.ReplyTo.HeaderText = "ReplyTo";
            this.ReplyTo.Name = "ReplyTo";
            this.ReplyTo.ReadOnly = true;
            // 
            // exchangeDataGridViewTextBoxColumn
            // 
            this.exchangeDataGridViewTextBoxColumn.DataPropertyName = "Exchange";
            this.exchangeDataGridViewTextBoxColumn.HeaderText = "Exchange";
            this.exchangeDataGridViewTextBoxColumn.Name = "exchangeDataGridViewTextBoxColumn";
            this.exchangeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // DLQueue
            // 
            this.DLQueue.DataPropertyName = "DLQueue";
            this.DLQueue.HeaderText = "DLQueue";
            this.DLQueue.Name = "DLQueue";
            this.DLQueue.ReadOnly = true;
            // 
            // DLTime
            // 
            this.DLTime.DataPropertyName = "DLTime";
            this.DLTime.HeaderText = "DLTime";
            this.DLTime.Name = "DLTime";
            this.DLTime.ReadOnly = true;
            // 
            // DLRoutingKeys
            // 
            this.DLRoutingKeys.DataPropertyName = "DLRoutingKeys";
            this.DLRoutingKeys.HeaderText = "DLRoutingKeys";
            this.DLRoutingKeys.Name = "DLRoutingKeys";
            this.DLRoutingKeys.ReadOnly = true;
            // 
            // DLExchange
            // 
            this.DLExchange.DataPropertyName = "DLExchange";
            this.DLExchange.HeaderText = "DLExchange";
            this.DLExchange.Name = "DLExchange";
            this.DLExchange.ReadOnly = true;
            // 
            // DLReason
            // 
            this.DLReason.DataPropertyName = "DLReason";
            this.DLReason.HeaderText = "DLReason";
            this.DLReason.Name = "DLReason";
            this.DLReason.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 766);
            this.Controls.Add(this._gdCurrentMessages);
            this.Controls.Add(this._btnSkipMessage);
            this.Controls.Add(this._btnSendToDestination);
            this.Controls.Add(this._statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this._txtOutput);
            this.Name = "Form1";
            this.Text = "Rabbit Shovel";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gdCurrentMessages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._dsMessageViews)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _txtOutput;
        private System.Windows.Forms.StatusStrip _statusStrip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button _btnSendToDestination;
        private System.Windows.Forms.Button _btnSkipMessage;
        private System.Windows.Forms.DataGridView _gdCurrentMessages;
        private System.Windows.Forms.BindingSource _dsMessageViews;
        private System.Windows.Forms.ToolStripMenuItem sourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllRowsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllRowsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Selected;
        private System.Windows.Forms.DataGridViewTextBoxColumn MessageId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn RoutingKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn Expiration;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReplyTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn exchangeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLRoutingKeys;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLExchange;
        private System.Windows.Forms.DataGridViewTextBoxColumn DLReason;
    }
}

