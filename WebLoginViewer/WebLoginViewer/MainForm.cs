using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebLoginViewer
{
    public partial class WebLoginViewerForm : System.Windows.Forms.Form
    {
        public WebLoginViewerForm()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog.ShowDialog(); 
            if (result == DialogResult.OK) 
            {
                string file = openFileDialog.FileName;
                try
                {
                    var textLines = File.ReadAllLines(file);
                    if (textLines != null)
                    {
                        var websiteList = BuildWebsiteList(textLines);

                    }
                }
                catch (IOException)
                {
                }
            }
        }

        public Dictionary<string, Website> BuildWebsiteList(string[] textLines)
        {
            var websiteDict = new Dictionary<string, Website>();
            foreach(var line in textLines)
            {
                var stringList = line.Split(' ');
                if (stringList.Length < 2) throw new FormatException();

                var websiteName = stringList[0];
                var websiteUrl = stringList[1];
                var credentialValues = new List<Credential>();
                foreach (var value in stringList.Skip(2))
                    credentialValues.Add(new Credential(value));

                if (!websiteDict.Keys.Contains(websiteName))
                    websiteDict[websiteName] = new Website(websiteName, websiteUrl);
                websiteDict[websiteName].Credentials.AddRange(credentialValues);
            }
            return websiteDict;
        }
    }
}
