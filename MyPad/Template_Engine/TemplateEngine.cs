using System;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.Runtime.Remoting;
using System.Globalization;
using System.Diagnostics;
using System.Text;
using System.Reflection;

namespace MyPad
{
    public class MyExeHost : MarshalByRefObject
    {
        public string ProcessRequest(String page, String query)
        {
            try
            {
                Trace.WriteLine(string.Format("page is '{0}'", page), "TemplateEngine");
                var target = new StringBuilder(4096);
                using (var stream = new StringWriter(target))
                {
                    HttpWorkerRequest hwr = new SimpleWorkerRequest(page, query, stream);
                    HttpRuntime.ProcessRequest(hwr);
                    stream.Flush();
                } 
                string targetString = target.ToString();
                Trace.WriteLine(targetString);
                return targetString; 
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString(), "TemplateEngine");
                return ex.ToString();
            }
        }

        /// <returns>null = object creates once and stays forever</returns>
        public override object InitializeLifetimeService()
        {
            return null;
        }
    }

    class DomainHolder : IDisposable
    {
        protected AppDomain ad;

        public DomainHolder(string virtualDir, string physicalDir)
        {
            string slash = Path.DirectorySeparatorChar.ToString();
            if (physicalDir.EndsWith(slash) == false)
            {
                physicalDir = physicalDir + slash;
            }
            Trace.WriteLine(string.Format("Physical Dir is '{0}'", physicalDir), "TemplateEngine");

            AppDomainSetup setup = new AppDomainSetup();

            string appName = (virtualDir + physicalDir).GetHashCode().ToString("x");
            setup.ApplicationName = appName;
            Trace.WriteLine(string.Format("appName is '{0}'", appName), "TemplateEngine");

            //setup.ConfigurationFile = "web.config"; // not necessary except for debugging

            string domainId = DateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo).GetHashCode().ToString("x");
            Trace.WriteLine(string.Format("domainID is '{0}'", domainId), "TemplateEngine");

            ad = AppDomain.CreateDomain(domainId, /*System.Security.Policy.Evidence*/null, /* AppDomainSetup */setup);
            ad.SetData(".appDomain", "*");
            ad.SetData(".appPath", physicalDir);
            ad.SetData(".appVPath", virtualDir);

            ad.SetData(".domainId", domainId);

            ad.SetData(".hostingVirtualPath", virtualDir);
            Trace.WriteLine(string.Format("virtualDir is '{0}'", virtualDir), "TemplateEngine");

            string hostingInstallDir = HttpRuntime.AspInstallDirectory;
            ad.SetData(".hostingInstallDir", hostingInstallDir);
            Trace.WriteLine(string.Format("aspDir is '{0}'", hostingInstallDir), "TemplateEngine");
        }

        void IDisposable.Dispose()
        {
            AppDomain.Unload(ad);
        }

        public object CreateProxy(Type hostType)
        {
            ObjectHandle oh = ad.CreateInstance(hostType.Module.Assembly.FullName, hostType.FullName);
            return oh.Unwrap();
        }

    }

    abstract class TemplateEngine
    {
        public static  string GetPhysicalRoot()
        {
            string loc = Assembly.GetEntryAssembly().Location;
            var fi = new FileInfo(loc);
            return fi.DirectoryName;
        }
        public static string GetVirtualRoot()
        {
            return Path.DirectorySeparatorChar.ToString();
        }
    }

    class TemplateEngine1 : TemplateEngine
    {
        public static string ProcessRequest(string fileName, string query)
        {
            using (var te = new DomainHolder(GetVirtualRoot(), GetPhysicalRoot()))
            {
                MyExeHost myHost = (MyExeHost)te.CreateProxy(typeof(MyExeHost));
                string res = myHost.ProcessRequest(fileName, query);
                return res;
            }
        }
    }

    class TemplateEngine2 : TemplateEngine
    {
        static MyExeHost myHost = null;

        public static MyExeHost MyExeHost {
            get { 
                if (myHost == null)
                {
                    myHost = (MyExeHost)ApplicationHost.CreateApplicationHost(typeof(MyExeHost), GetVirtualRoot(), GetPhysicalRoot());
                } 
                return myHost;
            }
        }

        public static string ProcessRequest(string fileName, string query)
        {
            string res = MyExeHost.ProcessRequest(fileName, query);
            return res;
        }
    }
}
