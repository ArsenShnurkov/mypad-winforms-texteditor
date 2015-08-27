using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels;
//using Win32 = System.Runtime.Remoting.Channels.Ipc.Win32;

namespace MyPad
{
    public class LocalUnixIpcClientChannel : IChannelSender, IChannel
    {

        object _innerChannel;

        public LocalUnixIpcClientChannel ()
        {
            _innerChannel = Activator.CreateInstance (LocalUnixChannelLoader.LoadClientChannel ());
        }

        public LocalUnixIpcClientChannel (IDictionary properties,
                                          IClientChannelSinkProvider sinkProvider)
        {
            _innerChannel = Activator.CreateInstance (LocalUnixChannelLoader.LoadClientChannel (), new object [] {
                properties,
                sinkProvider
            });
        }

        public LocalUnixIpcClientChannel (string name,
                                          IClientChannelSinkProvider sinkProvider)
        {
            _innerChannel = Activator.CreateInstance (LocalUnixChannelLoader.LoadClientChannel (), new object [] {
                name,
                sinkProvider
            });
        }

        public string ChannelName {
            get { return ((IChannel)_innerChannel).ChannelName; }
        }

        public int ChannelPriority {
            get { return ((IChannel)_innerChannel).ChannelPriority; }
        }

        public string Parse (string url, out string objectUri)
        {
            return LocalIpcChannelHelper.Parse (url, out objectUri);
        }

        //
        // Converts an ipc URL to a unix URL.
        // Returns the URL unchanged if it was not ipc.
        //
        internal static string IpcToUnix (string url)
        {
            if (url == null)
                return null;

            string portName;
            string objectUri;
            LocalIpcChannelHelper.Parse (url, out portName, out objectUri);
            if (objectUri != null)
                url = "unix://" + Path.Combine (Path.GetTempPath (), portName) + "?" + objectUri;
            return url;
        }

        public IMessageSink CreateMessageSink (string url,
                                                      object remoteChannelData,
                                                      out string objectUri)
        {
            url = IpcToUnix (url);
            IMessageSink sink = ((IChannelSender)_innerChannel).CreateMessageSink (url, remoteChannelData, out objectUri);

            if (sink != null)
                return new UrlMapperSink (sink);
            else
                return null;
        }
    }


    //
    // Simple message sink that changes ipc URLs to unix URLs.
    //
    sealed class UrlMapperSink : IMessageSink
    {
        readonly IMessageSink _sink;

        public UrlMapperSink (IMessageSink sink)
        {
            _sink = sink;
        }

        public IMessageSink NextSink {
            get { return _sink.NextSink; }
        }

        static void ChangeUri (IMessage msg)
        {
            string uri = msg.Properties ["__Uri"] as string;
            if (uri != null) {
                msg.Properties ["__Uri"] = LocalUnixIpcClientChannel.IpcToUnix (uri);
                    }
        }

        public IMessage SyncProcessMessage (IMessage msg)
        {
            ChangeUri (msg);
            return _sink.SyncProcessMessage (msg);
        }

        public IMessageCtrl AsyncProcessMessage (IMessage msg,
                                                        IMessageSink replySink)
        {
            ChangeUri (msg);
            return _sink.AsyncProcessMessage (msg, replySink);
        }

    }
}
