using HtmlParser.Core;
using HtmlParser.Core.Habra;
using System;
using System.Windows.Forms;

namespace HtmlParser
{
    public partial class Form1 : Form
    {
        ParserWorker<string[]> parser;

        public Form1()
        {
            InitializeComponent();

            parser = new ParserWorker<string[]>(new HabraParser());

            parser.OneCompleted += Parser_OneCompleted;
            parser.OneNewData += Parser_OneNewData;
        }

        private void Parser_OneNewData(object arg1, string[] arg2)
        {
            ListTitles.Items.AddRange(arg2);
        }

        private void Parser_OneCompleted(object obj)
        {
            MessageBox.Show("All works is done!");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parser.Settings = new HabraSettings((int)NumericStart.Value,(int)NumericEnd.Value);
            parser.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }
    }
}
