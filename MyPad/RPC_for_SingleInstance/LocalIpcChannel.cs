using System;
using System.Collections;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;

//using Unix  = System.Runtime.Remoting.Channels.Ipc.Unix;
//using Win32 = System.Runtime.Remoting.Channels.Ipc.Win32;
using System.Runtime.Remoting.Channels.Ipc;

namespace MyPad
{
    public class LocalIpcChannel : IChannelReceiver, IChannelSender, IChannel
    {
        IChannel _innerChannel;

        public LocalIpcChannel ()
        {
            if (Globals.IsUnix)
                _innerChannel = new LocalUnixIpcChannel ();
            else
                _innerChannel = new IpcChannel ();
        }

        public LocalIpcChannel (string portName)
        {
            if (Globals.IsUnix)
                _innerChannel = new LocalUnixIpcChannel (portName);
            else
                _innerChannel = new IpcChannel (portName);
        }

        public LocalIpcChannel (IDictionary properties,
            IClientChannelSinkProvider clientSinkProvider,
            IServerChannelSinkProvider serverSinkProvider)
        {
            if (Globals.IsUnix)
                _innerChannel = new LocalUnixIpcChannel (properties, clientSinkProvider, serverSinkProvider);
            else
                _innerChannel = new IpcChannel (properties, clientSinkProvider, serverSinkProvider);
        }

        public string ChannelName
        {
            get { return _innerChannel.ChannelName; }
        }

        public int ChannelPriority
        {
            get { return _innerChannel.ChannelPriority; }
        }

        public string Parse (string url, out string objectURI)
        {
            return _innerChannel.Parse (url, out objectURI);
        }

        public IMessageSink CreateMessageSink (string url,
            object remoteChannelData,
            out string objectURI)
        {
            return ((IChannelSender)_innerChannel).CreateMessageSink (url, remoteChannelData, out  objectURI);
        }

        public object ChannelData
        {
            get { return ((IChannelReceiver)_innerChannel).ChannelData; }
        }

        public string[] GetUrlsForUri (string objectURI)
        {
            return ((IChannelReceiver)_innerChannel).GetUrlsForUri (objectURI);
        }

        public void StartListening (object data)
        {
            ((IChannelReceiver)_innerChannel).StartListening (data);
        }

        public void StopListening (object data)
        {
            ((IChannelReceiver)_innerChannel).StopListening (data);
        }

    }
}

