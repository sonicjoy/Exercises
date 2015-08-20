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

        public Dictionary<KeyValuePair<string, string>, List<Credential>> BuildWebsiteList(string[] textLines)
        {
            var websiteDict = new Dictionary<KeyValuePair<string, string>, List<Credential>>();
            foreach(var line in textLines)
            {
                var stringList = line.Split(' ');
                if (stringList.Length < 2) throw new FormatException();
                var websitePair = new KeyValuePair<string, string>(stringList[0], stringList[1]);
                var credential = new Credential(new List<string>(stringList.Skip(2)));

            }
            return websiteDict;
        }
    }
}
