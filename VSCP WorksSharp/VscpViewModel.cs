using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using VscpHelperLibWrapper;
using VscpHelperLibWrapper.Enums;
using VscpHelperLibWrapper.EventArguments;
using VscpHelperLibWrapper.Interfaces;
using VscpHelperLibWrapper.DataTypes;
using VscpWorksSharp.Settings;
using VscpSimulator;

namespace VscpWorksSharp
{
    class VscpViewModel : BindableBase, IDisposable
    {
        #region Commands
        public DelegateCommand<object> ConnectionCommand { get; set; }
        public DelegateCommand<object> SettingsCommand { get; set; }
        public DelegateCommand<object> ClearLogCommand { get; set; }

        
        #endregion

        #region Fields
        private IVscp _vscp = null;
        private readonly StringBuilder _log = new StringBuilder();
        private readonly ObservableCollection<VscpEventStruct> _vscpReceivedEvents = new ObservableCollection<VscpEventStruct>();
        private readonly object _receiveEventsLock = new object();
        ProjectSettings _settings = ProjectSettings.GetSettings();
        #endregion

        public VscpViewModel()
        {
            ConnectionCommand = new DelegateCommand<object>(ChangeConnection);
            ClearLogCommand = new DelegateCommand<object>(ClearLog);
            SettingsCommand = new DelegateCommand<object>(ShowSettings);

            _vscp = new Simulator();
            //_vscp = new Vscp();
            _vscp.LogMessageEvent += VscpLogMessageEvent;
            _vscp.Connection.ConnectionStateChangedEvent += VscpConnectionStateChangedEvent;
            _vscp.Receiver.VscpMsgReceivedEvent += VscpMsgReceivedEvent;
			_vscp.Receiver.UseBlockingReceiver = true;

            //_vscp.OpenSession("127.0.0.1:9598", "admin", "secret");
            //Username = "admin";
            //Password = "secret";
            //ConnectionString = "127.0.0.1:9598";
            BindingOperations.EnableCollectionSynchronization(_vscpReceivedEvents, _receiveEventsLock); // Enables acces to ObservableCollection through worker threads.
        }

        #region Received VSCP Events
        private void VscpMsgReceivedEvent(object sender, VscpMsgReceivedEventArgs e)
        {
            _vscpReceivedEvents.Add(e.Data);
            OnPropertyChanged("ReceivedEvents");
        }

        public ObservableCollection<VscpEventStruct> ReceivedEvents
        {
            get
            {
                return _vscpReceivedEvents;
            }
        }

        #endregion

        #region Log
        private void VscpLogMessageEvent(object sender, LogMessageEventArgs e)
        {
            _log.AppendLine(string.Format("{0} - {1}", DateTime.Now.ToString("HH:mm:ss"), e.Message));
            OnPropertyChanged("LogMessages");
        }

        public string LogMessages
        {
            get
            {
                return _log.ToString();
            }
        }

        public void ClearLog(object param)
        {
            _log.Clear();
            OnPropertyChanged("LogMessages");
        }
        #endregion

        #region Connection
        public string Username
        {
            get
            {
                return _settings.UserName;
            }
            set
            {
                _settings.UserName = value;
            }
        }
        public string Password
        {
            get
            {
                return _settings.Password;
            }
            set
            {
                _settings.Password= value;
            }
        }
        public string ConnectionString
        {
            get
            {
                return _settings.ConnectionString;
            }
            set
            {
                _settings.ConnectionString = value;
            }
        }

        private void VscpConnectionStateChangedEvent(object sender, ConnectionStateEventArgs e)
        {
            OnPropertyChanged("ConnectionState");
            if (e.State == ConnectionStateEnum.Started)
            {
                _vscp.Receiver.StartReceiving();
            }
            else
            {
                _vscp.Receiver.StopReceiving();
            }
        }

        public ConnectionStateEnum ConnectionState
        {
            get
            {
                if (_vscp == null) { return ConnectionStateEnum.Idle; }
                return _vscp.Connection.ConnectionState;
            }
        }

        public void ChangeConnection(object param)
        {
            switch (ConnectionState)
            {
                case ConnectionStateEnum.Idle:
                case ConnectionStateEnum.Stopped:
                    SaveSettings(); // Persist all settings.
                    _vscp.Connection.OpenConnection(ConnectionString, Username, Password);
                    break;
                case ConnectionStateEnum.Started:
                    _vscp.Connection.CloseConnection();
                    break;
                case ConnectionStateEnum.Connecting:
                    _vscp.Connection.AbortOpenConnection();
                    break;
            }
        }
        #endregion

        #region Settings
        public void ShowSettings(object param)
        {

        }

        private void SaveSettings()
        {
            // TODO: If autosave is true ...
            _settings.Save();
        }
        #endregion

        #region IDisposable Support
        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_vscp != null)
                {
                    _vscp.Dispose();
                    _vscp = null;
                }
            }
        }
        #endregion
    }
}
