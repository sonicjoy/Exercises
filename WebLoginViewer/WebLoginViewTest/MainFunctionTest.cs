using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebLoginViewer.Tests
{
    [TestClass()]
    public class MainFunctionTest
    {
        string[] textLines;
        WebLoginViewerForm form;
        SortedList<string, Website> websiteList;
        [TestInitialize]
        public void InitTest()
        {
            form = new WebLoginViewerForm();
            textLines = new string[]{"Googlemail http://mail.google.com/ Username Password", 
                            "Westlaw http://web2.westlaw.com/ Password", 
                            "Factiva http://global.factiva.com/ UserId Password NameSpace",
                            "Googlemail http://mail.google.com/ UserId",
                            "Googlemail http://www.googlemail.com/",
                            "NowOnAir"};
            websiteList = form.BuildWebsiteList(textLines);
        }

        [TestMethod()]
        public void BuildWebsiteList_Should_Return_List_Of_Websites()
        {
            Assert.IsInstanceOfType(websiteList, typeof(SortedList<string, Website>));
            Assert.IsTrue(websiteList.Any());
        }

        [TestMethod]
        public void Website_Should_Be_Identify_By_Its_Name_Only()
        {
            Assert.AreEqual(3, websiteList.Count);
        }

        [TestMethod]
        public void Website_Credentials_Should_Be_Grouped_By_Website_Names()
        {
            Assert.AreEqual(3, websiteList["Googlemail"].Credentials.Count);
        }

        [TestMethod]
        public void Website_List_Should_Be_In_Alphabetical_Order()
        {
            Assert.IsTrue(websiteList.IndexOfKey("Westlaw") > websiteList.IndexOfKey("Factiva"));
        }

        [TestMethod]
        public void If_Anyline_Contains_Less_Than_2_Strings_Shall_Be_Ignored()
        {
            Assert.IsFalse(websiteList.ContainsKey("NowOnAir"));
        }

    }
}

