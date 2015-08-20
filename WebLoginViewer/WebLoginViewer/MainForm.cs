using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WebLoginViewer
{
    public partial class WebLoginViewerForm : System.Windows.Forms.Form
    {
        public WebLoginViewerForm()
        {
            InitializeComponent();
            listView.View = View.Details;
            listView.Columns.Add("Website", 300);
            listView.Columns.Add("Credential Count", 180);
            listView.Columns.Add("Is Online", 90);
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
                        foreach (var website in websiteList)
                        {
                            var listViewItem = new ListViewItem(new string[] { website.Key, website.Value.Credentials.Count.ToString(), website.Value.IsOnline.ToString() });
                            listView.Items.Add(listViewItem);
                        }
                        totalLoginLabel.Text = websiteList.Sum(w => w.Value.Credentials.Count).ToString();
                        fileNameLabel.Text = file;
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                }
            }
        }

        public SortedList<string, Website> BuildWebsiteList(string[] textLines)
        {
            var websiteList = new SortedList<string, Website>();
            foreach(var line in textLines)
            {
                var stringList = line.Split(' ');
                if (stringList.Length < 2) continue;

                var websiteName = stringList[0];
                var websiteUrl = stringList[1];
                var credentialValues = new List<Credential>();
                foreach (var value in stringList.Skip(2))
                    credentialValues.Add(new Credential(value));

                if (!websiteList.ContainsKey(websiteName))
                    websiteList[websiteName] = new Website(websiteName, websiteUrl);
                websiteList[websiteName].Credentials.AddRange(credentialValues);
            }
            return websiteList;
        }
    }
}
