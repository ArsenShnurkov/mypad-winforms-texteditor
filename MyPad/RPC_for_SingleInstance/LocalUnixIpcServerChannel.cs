using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
//using Unix = System.Runtime.Remoting.Channels.Ipc.Unix;
//using Win32 = System.Runtime.Remoting.Channels.Ipc.Win32;

namespace MyPad
{
    public class LocalUnixIpcServerChannel : IChannelReceiver, IChannel
    {
        object _innerChannel;
        string _portName;
        string _path;

        internal static string BuildPathFromPortName (string portName)
        {
            //return Path.Combine (Path.GetTempPath (), portName);
            var path = Path.Combine(portName);
            return path;
        }

        internal static IDictionary MapProperties (IDictionary props)
        {
            if (props == null) return null;
            Hashtable h = new Hashtable ();

            foreach (DictionaryEntry e in props) {
                h [e.Key] = e.Value;

                switch (e.Key as string) {
                case "portName":
                    h ["path"] = BuildPathFromPortName ((string)e.Value);
                    break;
                }
            }
            return h;
        }

public LocalUnixIpcServerChannel (string portName)
        {
            _portName = portName;
            _path = BuildPathFromPortName (portName);

            _innerChannel = Activator.CreateInstance(LocalUnixChannelLoader.LoadServerChannel (), new object [] {_path});
        }

public LocalUnixIpcServerChannel (string name, string portName,
            IServerChannelSinkProvider serverSinkProvider)
        {
            _portName = portName;
            _path = portName = BuildPathFromPortName (portName);

            _innerChannel = Activator.CreateInstance(LocalUnixChannelLoader.LoadServerChannel (), new object [] {name, _path, serverSinkProvider});
        }

        public LocalUnixIpcServerChannel (string name, string portName)
        {
            _portName = portName;
            _path = BuildPathFromPortName (portName);

            _innerChannel = Activator.CreateInstance(LocalUnixChannelLoader.LoadServerChannel (), new object [] {name, _path});
        }

        public LocalUnixIpcServerChannel (IDictionary properties,
            IServerChannelSinkProvider serverSinkProvider)
        {
            properties = MapProperties (properties);
            if (properties != null) {
                _portName = properties ["portName"] as string;
                _path = properties ["path"] as string;
            }

            _innerChannel = Activator.CreateInstance(LocalUnixChannelLoader.LoadServerChannel (), new object [] {properties, serverSinkProvider});
        }

        public string ChannelName
        {
            get { return ((IChannel)_innerChannel).ChannelName; }
        }

        public int ChannelPriority
        {
            get { return ((IChannel)_innerChannel).ChannelPriority; }
        }

        public string Parse (string url, out string objectUri)
        {
            return LocalIpcChannelHelper.Parse (url, out objectUri);
        }

        public object ChannelData
        {
            get { return ((IChannelReceiver)_innerChannel).ChannelData; }
        }

        public string[] GetUrlsForUri(string objectUri)
        {
            string[] res = ((IChannelReceiver)_innerChannel).GetUrlsForUri (objectUri);
            if (res != null) {
                string[] urls = new string [res.Length + 1];

                for (int i = 0; i < res.Length; i++)
                    urls [i] = res [i];

                if (!objectUri.StartsWith ("/"))
                    objectUri = "/" + objectUri;
                urls [res.Length] = "ipc://" + _portName + objectUri;
                return urls;
            }
            return res;
        }

        public void StartListening (object data)
        {
            ((IChannelReceiver)_innerChannel).StartListening (data);
        }

        public void StopListening(object data)
        {
            ((IChannelReceiver)_innerChannel).StopListening (data);
        }
    }
}

