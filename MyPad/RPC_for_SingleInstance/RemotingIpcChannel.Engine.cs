// http://stackoverflow.com/questions/5366316/simple-cross-platform-process-to-process-communication-in-mono
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace MyPad.RemotingIpcChannel
{
    public class Engine : MarshalByRefObject, ICommunicationMechanism, IMyPadServer
    {
        IpcChannel serverChannel;
        public void Start()
        {
            serverChannel = new IpcChannel(GetChannelName());
            ChannelServices.RegisterChannel(serverChannel, false);
            RemotingConfiguration.RegisterWellKnownServiceType(
                this.GetType(),
                GetObjectName(),
                WellKnownObjectMode.SingleCall);
        }

        public void Stop()
        {
            ChannelServices.UnregisterChannel(serverChannel);
        }

        string GetChannelName()
        {
            return Environment.UserName;
        }

        string GetObjectName()
        {
            return "object2";
        }

        public bool PassCommand(string filename, string encoding)
        {
            IpcChannel clientChanel = new IpcChannel("myClient");
            ChannelServices.RegisterChannel(clientChanel, false); 
            IMyPadServer obj = (IMyPadServer)Activator.GetObject(
                typeof(IMyPadServer),
                "ipc://" + GetChannelName() + "/" + GetObjectName());
            try
            {
            obj.OpenPage(filename, encoding);
                return true;
            }
            catch (RemotingException)
            {
                // Connection refused
                return false;
            }
            finally
            {
                ChannelServices.UnregisterChannel(clientChanel);
            }
        }

        void IMyPadServer.OpenPage(string pageUrl, string encoding)
        {
            Trace.WriteLine("Request received");
            Console.WriteLine("Request received");
            var form = Program.GetMainForm();
            form.InvokeOpenFile(pageUrl);
        }
    }
    public interface IMyPadServer
    {
        void OpenPage(string pageUrl, string encoding);
    }
}
