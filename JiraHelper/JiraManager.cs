using JiraHelper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JiraHelper
{
    public enum JiraResource
    {
        project,
        issue
    }
    public class JiraManager
    {
        private string m_Username;
        private string m_Password;

        public JiraManager(JiraCredential credential)
        {
            m_Username = credential.UserName;
            m_Password = credential.Password;
        }

        public string RunQuery(
            //JiraResource resource,
            string url = null,
            //string argument = null,
            string data = null,
            string method = null)
        {
            //string url = string.Format("{0}{1}/", urlbase, resource.ToString());

            //if (argument != null)
            //{
            //    url = string.Format("{0}{1}/", url, argument);
            //}

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = method;

            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                }
            }

            string base64Credentials = GetEncodedCredentials();
            request.Headers.Add("Authorization", "Basic " + base64Credentials);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            string result = string.Empty;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        public void RunUpdate(
            JiraResource resource,
            string urlbase = null,
            string argument = null,
            string data = null,
            string method = null)
        {
            string url = string.Format("{0}{1}/", urlbase, resource.ToString());

            if (argument != null)
            {
                url = string.Format("{0}{1}/", url, argument);
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = method;

            if (data != null)
            {
                using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
                {
                    writer.Write(data);
                }
            }

            string base64Credentials = GetEncodedCredentials();
            request.Headers.Add("Authorization", "Basic " + base64Credentials);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            Console.WriteLine("ACTUALIZADO!!!");
        }

        private string GetEncodedCredentials()
        {
            string mergedCredentials = string.Format("{0}:{1}", m_Username, m_Password);
            byte[] byteCredentials = UTF8Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }
    }
}
