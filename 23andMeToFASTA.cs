using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NS_23andMe_to_FASTA
{
    public partial class Form1 : Form
    {
        string reference = NS_23andMe_to_FASTA.Properties.Resources.rCRS_ref;
        Dictionary<string, string> dict = new Dictionary<string, string>();
        char[] fasta = null;
        string filename = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                button2.Enabled = false;
                statusLbl.Text = "Working...";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            if(filename!=null)
            {
                fasta = NS_23andMe_to_FASTA.Properties.Resources.rCRS_seq.ToCharArray();
                string[] lines = File.ReadAllLines(filename);
                string[] data = null;
                string position = null;
                foreach(string line in lines)
                {
                    if (line.StartsWith("#"))
                        continue;
                    data = line.Split(new char[] { '\t' });
                    if(data[1] == "MT")
                    {
                        //
                        if(dict.TryGetValue(data[0], out position))
                        {
                            if (data[3] != "-" && data[3] != "--")
                            {
                                fasta[int.Parse(position) - 1] = data[3].ToCharArray()[0];
                            }
                        }
                    }
                }
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button2.Enabled = true;
            statusLbl.Text = "Done.";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringReader reader = new StringReader(reference);
            string line = null;
            string[] data = null;
            while ((line = reader.ReadLine()) != null)
            {
                data = line.Split(new char[] { ',' });
                if(!dict.ContainsKey(data[0]))
                    dict.Add(data[0], data[1]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(">"+Path.GetFileName(saveFileDialog1.FileName)+"\r\n");
                int i = 1;
                foreach(char c in fasta)
                {
                    sb.Append(c);
                    if (i == 50)
                    {
                        i = 0;
                        sb.Append("\r\n");
                    }
                    i++;
                }
                File.WriteAllText(saveFileDialog1.FileName, sb.ToString());
                statusLbl.Text = "File saved.";
            }
        }

        private void Form1_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start("http://www.y-str.org/2014/04/23andme-to-fasta.html");
        }
    }
}
