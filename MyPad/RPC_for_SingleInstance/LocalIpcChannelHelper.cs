using System;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Runtime.Remoting.Channels.Ipc.Win32;

namespace MyPad
{
    /// <summary>
    /// Provides helper services to the IpcChannel implementation.
    /// </summary>
    public class LocalIpcChannelHelper
    {
        public const string Scheme = "ipc";
        public const string SchemeStart = "ipc://";

        LocalIpcChannelHelper ()
        {
        }

        static readonly char[] InvalidPipeNameChars =
            new char[] { '\\', '/' };

        /// <summary>
        /// Validates a pipe name.
        /// </summary>
        /// <param name="pipeName">The pipe name.</param>
        /// <returns></returns>
        public static bool IsValidPipeName (string pipeName)
        {
            if (pipeName == null || pipeName.Trim () == "")
                return false;

            var chars = Path.GetInvalidPathChars ();
            if (pipeName.IndexOfAny (chars) >= 0)
                return false;

            if (pipeName.IndexOfAny (InvalidPipeNameChars) >= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Parses an url against IpcChannel's rules.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="pipeName">The pipe name.</param>
        /// <param name="objectUri">The uri of the object.</param>
        /// <returns>All but the object uri.</returns>
        public static string Parse (string url, out string pipeName, out string objectUri)
        {
            if (url.StartsWith (SchemeStart)) 
                {
                int i = url.LastIndexOf('/');
                if (i >= 0) 
                    {
                    pipeName = url.Substring (SchemeStart.Length, i - SchemeStart.Length);
                    objectUri = url.Substring (i+1);
                    return SchemeStart + pipeName;
                    } else 
                    {
                    pipeName = url.Substring (SchemeStart.Length);
                    objectUri = null;
                    return SchemeStart + pipeName;
                    }
                }

            pipeName = null;
            objectUri = null;
            return null;
        }

        /// <summary>
        /// Parses an url against IpcChannel's rules.
        /// </summary>
        /// <param name="url">The url.</param>
        /// <param name="objectUri">The uri of the object.</param>
        /// <returns>All but the object uri.</returns>
        public static string Parse (string url, out string objectUri)
        {
            string pipeName;
            return Parse (url, out pipeName, out objectUri);
        }

        /// <summary>
        /// Copies a stream.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public static void Copy (Stream input, Stream output)
        {
            MemoryStream ms = input as MemoryStream;
            if (ms != null)
                {
                ms.WriteTo (output);
                return;
                }

            // TODO: find out the optimal chunk size.
            const int size = 1024 * 1024;
            byte[] buffer = new byte[size];

            int count;
            while ((count = input.Read (buffer, 0, size)) > 0)
                {
                output.Write (buffer, 0, count);
                }
        }

    }
}
