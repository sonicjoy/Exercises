using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

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