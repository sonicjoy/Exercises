using WebLoginViewer;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace WebLoginViewer.Tests
{
    [TestClass()]
    public class MainFunctionTest
    {
        string[] textLines;
        WebLoginViewerForm form;
        [TestInitialize]
        public void InitTest()
        {
            form = new WebLoginViewerForm();
            textLines = new string[]{"Googlemail http://mail.google.com/ Username Password", 
                            "Westlaw http://web2.westlaw.com/ Password", 
                            "Factiva http://global.factiva.com/ UserId Password NameSpace",
                            "Googlemail http://mail.google.com/ UserId",
                            "Googlemail http://www.googlemail.com/"};
        }

        [TestMethod()]
        public void BuildWebsiteList_Should_Return_List_Of_Websites()
        {
            var websiteList = form.BuildWebsiteList(textLines);
            Assert.IsInstanceOfType(websiteList, typeof(Dictionary<KeyValuePair<string, string>, List<Credential>>));
            Assert.IsTrue(websiteList.Any());
        }

    }
}

