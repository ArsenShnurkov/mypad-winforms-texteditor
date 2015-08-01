using System;
using System.Reflection;
//using System.Runtime.Remoting.Channels.Ipc.Unix;

namespace MyPad
{
    public class LocalUnixChannelLoader
    {

        LocalUnixChannelLoader ()
        {
        }

        static object _asmLock = new object ();
        static Assembly _asm;

        static Assembly channelAssembly
        {
            get {
                lock (_asmLock) {
                    if (_asm == null)
                        _asm = Assembly.Load (LocalConsts.AssemblyMono_Posix);
                }
                return _asm;
            }
        }

        static Type Load (string className)
        {
            return channelAssembly.GetType ("Mono.Remoting.Channels.Unix." + className, true);
        }

        public static Type LoadChannel ()
        {
            return Load ("UnixChannel");
        }

        public static Type LoadClientChannel ()
        {
            return Load ("UnixClientChannel");
        }

        public static Type LoadServerChannel ()
        {
            return Load ("UnixServerChannel");
        }
    }
}


