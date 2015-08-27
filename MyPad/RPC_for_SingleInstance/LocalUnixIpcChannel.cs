using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels;
//using System.Runtime.Remoting.Channels.Ipc;

namespace MyPad
{
    internal class LocalUnixIpcChannel : IChannelReceiver, IChannelSender, IChannel
    {
        IChannelSender _clientChannel;
        IChannelReceiver _serverChannel;

        string _portName;
        string _name = "ipc";
        int  _priority = 1;

        internal static IDictionary GetDefaultProperties (string portName)
        {
            Hashtable h = new Hashtable ();
            if (portName != null)
                h ["portName"] = portName;
            h ["name"] = "ipc";
            h ["priority"] = "1";
            return h;
        }

        public LocalUnixIpcChannel () : this (null)
        {
        }

        public LocalUnixIpcChannel (string portName)
            : this (GetDefaultProperties (portName), null, null)
        {
        }

        public LocalUnixIpcChannel (IDictionary properties,
            IClientChannelSinkProvider clientSinkProvider,
            IServerChannelSinkProvider serverSinkProvider)
        {
            if (properties != null) {
                _portName = properties ["portName"] as string;
                if (properties ["name"] != null)
                    _name = properties ["name"] as string;
                else
                    properties ["name"] = _name;
                if (properties ["priority"] != null)
                    _priority = Convert.ToInt32 (properties ["priority"]);
            }

            if (_portName != null)
                _serverChannel = new LocalUnixIpcServerChannel (properties, serverSinkProvider);

            _clientChannel = new LocalUnixIpcClientChannel (properties, clientSinkProvider);
        }

        public string ChannelName
        {
            get { return _name; }
        }

        public int ChannelPriority
        {
            get { return _priority; }
        }

        public string Parse (string url, out string objectUri)
        {
            if (_serverChannel != null)
                return _serverChannel.Parse (url, out objectUri);
            else
                return _clientChannel.Parse (url, out objectUri);

        }

        public IMessageSink CreateMessageSink(string url,
            object remoteChannelData,
            out string objectUri)
        {
            return _clientChannel.CreateMessageSink (url, remoteChannelData, out objectUri);
        }

        public object ChannelData
        {
            get {
                if (_serverChannel != null)
                    return _serverChannel.ChannelData;
                else
                    return null;
            }
        }

        public string[] GetUrlsForUri(string objectUri)
        {
            if (_serverChannel != null)
                return _serverChannel.GetUrlsForUri (objectUri);
            else
                return null;
        }

        public void StartListening (object data)
        {
            if (_serverChannel != null)
                _serverChannel.StartListening (data);
        }

        public void StopListening(object data)
        {
            if (_serverChannel != null)
                _serverChannel.StopListening (data);
        }
    }
}

