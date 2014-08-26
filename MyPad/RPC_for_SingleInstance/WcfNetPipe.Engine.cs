// http://stackoverflow.com/questions/5366316/simple-cross-platform-process-to-process-communication-in-mono
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.Threading;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace MyPad.WcfNetPipe
{
    /// <summary>
    /// Netpipes doesn't require port number, thus
    /// this allows to run several program instances for different users on the same machine
    /// </summary>
    public class Engine : ICommunicationMechanism, IMyPadServer
    {
        ServiceHost serviceHost = null;

        public void Start()
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            var pipeName = GetPipeName(binding.Scheme);
            binding.TransferMode = TransferMode.Buffered;//.Streamed;

            serviceHost = new ServiceHost
                (typeof(Engine), new Uri[] { new Uri(pipeName) });
            serviceHost.AddServiceEndpoint(typeof(IMyPadServer), binding, pipeName);

            try
            {
                serviceHost.Open();
            }
            catch (Exception ex)
            {
                Trace.WriteLine("already opened?");
                Trace.WriteLine(ex.ToString());
            }

            Trace.WriteLine("Service started. Available in following endpoints:");
            foreach (var serviceEndpoint in serviceHost.Description.Endpoints)
            {
                Trace.WriteLine(serviceEndpoint.ListenUri.AbsoluteUri);
            }
        }

        public void Stop()
        {
            serviceHost.Close();
        }

        string GetPipeName(string schemeName)
        {
            var pipeName = schemeName + ":" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar  + "127.0.0.1"
                + Path.DirectorySeparatorChar + Environment.UserName;
            return pipeName;
        }

        public IMyPadServer Connect()
        {
            var binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            binding.TransferMode = TransferMode.Buffered;//TransferMode.Streamed;
            binding.OpenTimeout = new TimeSpan(500);

            var address = new EndpointAddress(GetPipeName(binding.Scheme));

            var factory = new ChannelFactory<IMyPadServer>( binding, address );

            //var factory = new ChannelFactory<IMyPadServer>(, );
            //factory.Endpoint.Contract.SessionMode = SessionMode.NotAllowed;
            var pipeProxy = factory.CreateChannel();
            return pipeProxy;
        }

        void IMyPadServer.OpenPage(string pageUrl)
        {
            Trace.WriteLine("Request received");
            Console.WriteLine("Request received");
            var form = Program.GetMainForm();
            form.InvokeOpenFile(pageUrl);
        }

        public bool PassCommand(string filename, string encoding)
        {
            var proxy = this.Connect();
            try
            {
                proxy.OpenPage(filename);
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }
    }
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IMyPadServer
    {
        //[OperationContract(IsOneWay=true)]
        [OperationContract]
        void OpenPage(string pageUrl);
    }
}
