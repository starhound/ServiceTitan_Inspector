using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Windows.Forms;

namespace ST_Inspector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static IRestResponse GetJobData(int jobNum)
        {
            string requestUrlStart = "jobs/";
            string API_KEY = "YOUR_ST_API_KEY";
            string requestUrlEnd = "?serviceTitanApiKey=" + API_KEY;
            string requestUrlFull = requestUrlStart + jobNum + requestUrlEnd;
            string ST_USER = "USER";
            string ST_PWD = "PASSWORD";
            //new rest client for the ST API
            var client = new RestClient("https://api.servicetitan.com/v1");

            //auth factor
            client.Authenticator = new HttpBasicAuthenticator(ST_USER, ST_PWD);

            //initial request
            var request = new RestRequest(requestUrlFull, DataFormat.Json);

            //server response
            IRestResponse response = client.Get(request);

            return response;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            outputTextBox.Text = "";
            int jobNum = Int32.Parse(inputTextBox.Text);
            IRestResponse jobData = GetJobData(jobNum);
            string content = jobData.Content;
            if(content.Contains("does not exist"))
            {
                outputTextBox.Text = "Job does not exist.";
                return;
            }

            dynamic jData = JObject.Parse(content);
            outputTextBox.Text = jData.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
