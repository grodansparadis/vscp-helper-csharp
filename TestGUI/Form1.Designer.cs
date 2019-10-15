namespace TestGUI
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
            if (disposing)
            {
                if (_vscp != null)
                {
                    _vscp.Dispose();
                    _vscp = null;
                }
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxSend = new System.Windows.Forms.GroupBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtBxType = new System.Windows.Forms.TextBox();
            this.lblClass = new System.Windows.Forms.Label();
            this.txtBxClass = new System.Windows.Forms.TextBox();
            this.bttnSendEvent = new System.Windows.Forms.Button();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.bttnVersionInfo = new System.Windows.Forms.Button();
            this.bttnClearDaemonEventQueue = new System.Windows.Forms.Button();
            this.lblConnectionState = new System.Windows.Forms.Label();
            this.bttnStartStopConnection = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtBxPassword = new System.Windows.Forms.TextBox();
            this.txtBxUserName = new System.Windows.Forms.TextBox();
            this.txtBxAddress = new System.Windows.Forms.TextBox();
            this.groupBoxReceive = new System.Windows.Forms.GroupBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.bttnStartStopRec = new System.Windows.Forms.Button();
            this.lblPolTime = new System.Windows.Forms.Label();
            this.bttnReceiveEvent = new System.Windows.Forms.Button();
            this.bttnSendEventEx = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBoxSend.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.groupBoxReceive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.DetectUrls = false;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 217);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(809, 222);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.groupBoxSend);
            this.panel1.Controls.Add(this.groupBoxConnection);
            this.panel1.Controls.Add(this.groupBoxReceive);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(809, 217);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(671, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBoxSend
            // 
            this.groupBoxSend.Controls.Add(this.bttnSendEventEx);
            this.groupBoxSend.Controls.Add(this.lblType);
            this.groupBoxSend.Controls.Add(this.txtBxType);
            this.groupBoxSend.Controls.Add(this.lblClass);
            this.groupBoxSend.Controls.Add(this.txtBxClass);
            this.groupBoxSend.Controls.Add(this.bttnSendEvent);
            this.groupBoxSend.Location = new System.Drawing.Point(448, 12);
            this.groupBoxSend.Name = "groupBoxSend";
            this.groupBoxSend.Size = new System.Drawing.Size(142, 171);
            this.groupBoxSend.TabIndex = 8;
            this.groupBoxSend.TabStop = false;
            this.groupBoxSend.Text = "Send Event";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(6, 49);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(34, 13);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "Type:";
            // 
            // txtBxType
            // 
            this.txtBxType.Location = new System.Drawing.Point(47, 46);
            this.txtBxType.Name = "txtBxType";
            this.txtBxType.Size = new System.Drawing.Size(62, 20);
            this.txtBxType.TabIndex = 10;
            this.txtBxType.Text = "1";
            // 
            // lblClass
            // 
            this.lblClass.AutoSize = true;
            this.lblClass.Location = new System.Drawing.Point(6, 23);
            this.lblClass.Name = "lblClass";
            this.lblClass.Size = new System.Drawing.Size(35, 13);
            this.lblClass.TabIndex = 9;
            this.lblClass.Text = "Class:";
            // 
            // txtBxClass
            // 
            this.txtBxClass.Location = new System.Drawing.Point(47, 20);
            this.txtBxClass.Name = "txtBxClass";
            this.txtBxClass.Size = new System.Drawing.Size(62, 20);
            this.txtBxClass.TabIndex = 8;
            this.txtBxClass.Text = "20";
            // 
            // bttnSendEvent
            // 
            this.bttnSendEvent.Location = new System.Drawing.Point(6, 114);
            this.bttnSendEvent.Name = "bttnSendEvent";
            this.bttnSendEvent.Size = new System.Drawing.Size(85, 23);
            this.bttnSendEvent.TabIndex = 7;
            this.bttnSendEvent.Text = "Send Event";
            this.bttnSendEvent.UseVisualStyleBackColor = true;
            this.bttnSendEvent.Click += new System.EventHandler(this.bttnSendEvent_Click);
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.bttnVersionInfo);
            this.groupBoxConnection.Controls.Add(this.bttnClearDaemonEventQueue);
            this.groupBoxConnection.Controls.Add(this.lblConnectionState);
            this.groupBoxConnection.Controls.Add(this.bttnStartStopConnection);
            this.groupBoxConnection.Controls.Add(this.lblPassword);
            this.groupBoxConnection.Controls.Add(this.lblUser);
            this.groupBoxConnection.Controls.Add(this.lblAddress);
            this.groupBoxConnection.Controls.Add(this.txtBxPassword);
            this.groupBoxConnection.Controls.Add(this.txtBxUserName);
            this.groupBoxConnection.Controls.Add(this.txtBxAddress);
            this.groupBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(224, 199);
            this.groupBoxConnection.TabIndex = 6;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Connection";
            // 
            // bttnVersionInfo
            // 
            this.bttnVersionInfo.Location = new System.Drawing.Point(144, 149);
            this.bttnVersionInfo.Name = "bttnVersionInfo";
            this.bttnVersionInfo.Size = new System.Drawing.Size(74, 44);
            this.bttnVersionInfo.TabIndex = 10;
            this.bttnVersionInfo.Text = "Get Version Info";
            this.bttnVersionInfo.UseVisualStyleBackColor = true;
            this.bttnVersionInfo.Click += new System.EventHandler(this.bttnVersionInfo_Click);
            // 
            // bttnClearDaemonEventQueue
            // 
            this.bttnClearDaemonEventQueue.Location = new System.Drawing.Point(11, 149);
            this.bttnClearDaemonEventQueue.Name = "bttnClearDaemonEventQueue";
            this.bttnClearDaemonEventQueue.Size = new System.Drawing.Size(109, 44);
            this.bttnClearDaemonEventQueue.TabIndex = 8;
            this.bttnClearDaemonEventQueue.Text = "Clear Daemon Event Queue";
            this.bttnClearDaemonEventQueue.UseVisualStyleBackColor = true;
            this.bttnClearDaemonEventQueue.Click += new System.EventHandler(this.bttnClearDaemonEventQueue_Click);
            // 
            // lblConnectionState
            // 
            this.lblConnectionState.AutoSize = true;
            this.lblConnectionState.Location = new System.Drawing.Point(92, 114);
            this.lblConnectionState.Name = "lblConnectionState";
            this.lblConnectionState.Size = new System.Drawing.Size(109, 13);
            this.lblConnectionState.TabIndex = 9;
            this.lblConnectionState.Text = "State: Not connected";
            // 
            // bttnStartStopConnection
            // 
            this.bttnStartStopConnection.Location = new System.Drawing.Point(11, 98);
            this.bttnStartStopConnection.Name = "bttnStartStopConnection";
            this.bttnStartStopConnection.Size = new System.Drawing.Size(75, 44);
            this.bttnStartStopConnection.TabIndex = 8;
            this.bttnStartStopConnection.Text = "Start Connection";
            this.bttnStartStopConnection.UseVisualStyleBackColor = true;
            this.bttnStartStopConnection.Click += new System.EventHandler(this.bttnStartStopConnection_Click);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(8, 71);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Location = new System.Drawing.Point(8, 45);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(32, 13);
            this.lblUser.TabIndex = 6;
            this.lblUser.Text = "User:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(8, 23);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(48, 13);
            this.lblAddress.TabIndex = 5;
            this.lblAddress.Text = "Address:";
            // 
            // txtBxPassword
            // 
            this.txtBxPassword.Location = new System.Drawing.Point(80, 68);
            this.txtBxPassword.Name = "txtBxPassword";
            this.txtBxPassword.Size = new System.Drawing.Size(138, 20);
            this.txtBxPassword.TabIndex = 2;
            this.txtBxPassword.Text = "secret";
            // 
            // txtBxUserName
            // 
            this.txtBxUserName.Location = new System.Drawing.Point(80, 42);
            this.txtBxUserName.Name = "txtBxUserName";
            this.txtBxUserName.Size = new System.Drawing.Size(138, 20);
            this.txtBxUserName.TabIndex = 1;
            this.txtBxUserName.Text = "admin";
            // 
            // txtBxAddress
            // 
            this.txtBxAddress.Location = new System.Drawing.Point(80, 16);
            this.txtBxAddress.Name = "txtBxAddress";
            this.txtBxAddress.Size = new System.Drawing.Size(138, 20);
            this.txtBxAddress.TabIndex = 0;
            this.txtBxAddress.Text = "127.0.0.1:9598";
            // 
            // groupBoxReceive
            // 
            this.groupBoxReceive.Controls.Add(this.numericUpDown1);
            this.groupBoxReceive.Controls.Add(this.bttnStartStopRec);
            this.groupBoxReceive.Controls.Add(this.lblPolTime);
            this.groupBoxReceive.Controls.Add(this.bttnReceiveEvent);
            this.groupBoxReceive.Location = new System.Drawing.Point(242, 12);
            this.groupBoxReceive.Name = "groupBoxReceive";
            this.groupBoxReceive.Size = new System.Drawing.Size(200, 171);
            this.groupBoxReceive.TabIndex = 5;
            this.groupBoxReceive.TabStop = false;
            this.groupBoxReceive.Text = "Receive events";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(98, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(64, 20);
            this.numericUpDown1.TabIndex = 3;
            this.numericUpDown1.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // bttnStartStopRec
            // 
            this.bttnStartStopRec.Location = new System.Drawing.Point(6, 58);
            this.bttnStartStopRec.Name = "bttnStartStopRec";
            this.bttnStartStopRec.Size = new System.Drawing.Size(75, 39);
            this.bttnStartStopRec.TabIndex = 4;
            this.bttnStartStopRec.Text = "Start receiver";
            this.bttnStartStopRec.Click += new System.EventHandler(this.bttnStartStopRec_Click);
            // 
            // lblPolTime
            // 
            this.lblPolTime.AutoSize = true;
            this.lblPolTime.Location = new System.Drawing.Point(22, 25);
            this.lblPolTime.Name = "lblPolTime";
            this.lblPolTime.Size = new System.Drawing.Size(66, 13);
            this.lblPolTime.TabIndex = 4;
            this.lblPolTime.Text = "Pol time [ms]";
            // 
            // bttnReceiveEvent
            // 
            this.bttnReceiveEvent.Location = new System.Drawing.Point(6, 142);
            this.bttnReceiveEvent.Name = "bttnReceiveEvent";
            this.bttnReceiveEvent.Size = new System.Drawing.Size(166, 23);
            this.bttnReceiveEvent.TabIndex = 0;
            this.bttnReceiveEvent.Text = "Receive Event Manual";
            this.bttnReceiveEvent.UseVisualStyleBackColor = true;
            this.bttnReceiveEvent.Click += new System.EventHandler(this.bttnReceiveEvent_Click);
            // 
            // bttnSendEventEx
            // 
            this.bttnSendEventEx.Location = new System.Drawing.Point(6, 142);
            this.bttnSendEventEx.Name = "bttnSendEventEx";
            this.bttnSendEventEx.Size = new System.Drawing.Size(85, 23);
            this.bttnSendEventEx.TabIndex = 12;
            this.bttnSendEventEx.Text = "Send EventEx";
            this.bttnSendEventEx.UseVisualStyleBackColor = true;
            this.bttnSendEventEx.Click += new System.EventHandler(this.bttnSendEvent_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 439);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.panel1.ResumeLayout(false);
            this.groupBoxSend.ResumeLayout(false);
            this.groupBoxSend.PerformLayout();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.groupBoxReceive.ResumeLayout(false);
            this.groupBoxReceive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button bttnReceiveEvent;
        private System.Windows.Forms.Button bttnStartStopRec;
        private System.Windows.Forms.GroupBox groupBoxReceive;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblPolTime;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.TextBox txtBxPassword;
        private System.Windows.Forms.TextBox txtBxUserName;
        private System.Windows.Forms.TextBox txtBxAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblConnectionState;
        private System.Windows.Forms.Button bttnStartStopConnection;
        private System.Windows.Forms.Button bttnSendEvent;
        private System.Windows.Forms.GroupBox groupBoxSend;
        private System.Windows.Forms.Button bttnClearDaemonEventQueue;
        private System.Windows.Forms.Button bttnVersionInfo;
        private System.Windows.Forms.Label lblClass;
        private System.Windows.Forms.TextBox txtBxClass;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.TextBox txtBxType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button bttnSendEventEx;
    }
}

