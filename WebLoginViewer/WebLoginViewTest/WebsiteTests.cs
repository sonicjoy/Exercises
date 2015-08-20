using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebLoginViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLoginViewer.Tests
{
    [TestClass()]
    public class WebsiteTests
    {
        [TestMethod()]
        public void New_Declared_Websie_Object_Must_Have_Instance_Of_Credential_List()
        {
            var website = new Website("test", "http://google.com");
            Assert.IsNotNull(website.Credentials);
            Assert.IsInstanceOfType(website.Credentials, typeof(IEnumerable<ICredential<string>>));
        }
    }
}