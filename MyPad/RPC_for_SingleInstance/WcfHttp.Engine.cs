// http://stackoverflow.com/questions/5366316/simple-cross-platform-process-to-process-communication-in-mono
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace MyPad.WcfHttp
{
    /// <summary>
    /// This works, but requires a port number, which is inapropriate for multiuser environment
    /// </summary>
    public class Engine : ICommunicationMechanism, IMyPadServer
    {
        protected readonly string pipeName = "http:" + Path.DirectorySeparatorChar + Path.DirectorySeparatorChar  + "localhost:8674"
            + Path.DirectorySeparatorChar + Environment.UserName;
        ServiceHost serviceHost = null;
        public void Start()
        {
            serviceHost = new ServiceHost
                (typeof(Engine), new Uri[] { new Uri(pipeName) });
            serviceHost.AddServiceEndpoint(typeof(IMyPadServer), new BasicHttpBinding(), pipeName);

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

        public IMyPadServer Connect()
        {
            var factory = new ChannelFactory<IMyPadServer>(new BasicHttpBinding(), new EndpointAddress(pipeName));
            // Since this endpoint uses sessions, we have to allow sessions to prevent an exception.
            // http://stackoverflow.com/questions/13237766/wcf-channelfactory-iduplexsessionchannel
            //factory.Endpoint.Contract.SessionMode = SessionMode.Allowed;
            var pipeProxy = factory.CreateChannel();
            return pipeProxy;
        }

        bool IMyPadServer.OpenPage(string pageUrl, string encoding)
        {
            Trace.WriteLine("Request received");
            var form = Program.GetMainForm();
            form.InvokeOpenFile(pageUrl);
            return true;
        }

        public bool PassCommand(string filename, string encoding)
        {
            var proxy = this.Connect();
            try
            {
                proxy.OpenPage(filename, encoding);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    [ServiceContract]
    public interface IMyPadServer
    {
        [OperationContract]
        bool OpenPage(string pageUrl, string encoding);
    }
}
