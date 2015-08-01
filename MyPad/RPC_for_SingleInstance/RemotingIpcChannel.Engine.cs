// http://stackoverflow.com/questions/5366316/simple-cross-platform-process-to-process-communication-in-mono
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;

namespace MyPad.RemotingIpcChannel
{
    public class Engine : MarshalByRefObject, ICommunicationMechanism, IMyPadServer
    {
        LocalIpcChannel serverChannel;
        public void Start()
        {
            var name = GetChannelName ();
            serverChannel = new LocalIpcChannel(name);
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

        //static bool useLocalWrapper = false;
        static bool useLocalWrapper = true;

        string GetChannelName()
        {
            if (useLocalWrapper)
            {
                var homeFolder = GetHomeFolder ();
                var res = Path.Combine (homeFolder, ".mypad_channel");
                return res;

            } else
            {
                return Environment.UserName;
            }
        }

        string GetClientChannelName()
        {
            var spid = Process.GetCurrentProcess ().Id.ToString ();
            if (useLocalWrapper)
            {
                var homeFolder = GetHomeFolder ();
                var res = Path.Combine (homeFolder, spid);
                return res;
            } else
            {
                return spid;
            }
        }

        string GetObjectName()
        {
            return "object3";
        }


        public bool PassCommand(string filename, string encoding)
        {
            var name = GetClientChannelName();
            var clientChanel = new LocalIpcChannel(name);
            ChannelServices.RegisterChannel(clientChanel, false); 
            var u = "ipc://" + GetChannelName() + "/" + GetObjectName();
            IMyPadServer obj = (IMyPadServer)Activator.GetObject(typeof(IMyPadServer),u);
            try
            {
                obj.OpenPage(filename, encoding);
                return true;
            }
            catch (RemotingException ex)
            {
                // Connection refused
                // error code -2146233087 = 0x80131501 (Failed to connect to server)
                Trace.WriteLine(ex.ToString());
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
            var form = Globals.GetMainForm();
            form.InvokeOpenFile(pageUrl);
        }

        string GetHomeFolder()
        {
            if (Globals.IsLinux)
            {
                var res = Environment.GetEnvironmentVariable ("HOME");
                return res;
            } else
            {
                var res = Environment.GetEnvironmentVariable ("HOMEDRIVE") + Environment.GetEnvironmentVariable ("HOMEPATH");
                return res;
            }
        }
    }
    public interface IMyPadServer
    {
        void OpenPage(string pageUrl, string encoding);
    }
}
