using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLoginViewer
{
    public class Website
    {
        public Website(string name, string url)
        {
            Name = name;
            Url = url;
            Credentials = new List<Credential>();
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public List<Credential> Credentials { get; set; }

        public bool IsOnline
        {
            get
            {
                return WebSiteChecker.IsOnline(Url);
            }
        }
    }

    public interface ICredential<T>
    {
        T Values { set; get; }
    }

    public class Credential : ICredential<string>
    {
        public Credential(string value)
        {
            Values = value;
        }

        public string Values { get; set; }
    }

    public static class WebSiteChecker
    {
        public static bool IsOnline(string url)
        {
            return true;
        }
    }
}
