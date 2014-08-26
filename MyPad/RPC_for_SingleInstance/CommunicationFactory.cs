using System;

namespace MyPad
{
    public interface ICommunicationMechanism
    {
        void Start();
        void Stop();
        bool PassCommand(string filename, string encoding);
    }

    public class CommunicationFactory
    {
        //ICommunicationMechanism cm = new MyPad.WcfHttp.Engine();
        ICommunicationMechanism cm = new MyPad.RemotingIpcChannel.Engine();
        public void Start()
        {
            cm.Start();
        }
        public void Stop()
        {
            cm.Stop();
        }
        public bool PassCommand(string filename, string encoding)
        {
            bool res = cm.PassCommand(filename, encoding);
            return res;
        }
    }
}

