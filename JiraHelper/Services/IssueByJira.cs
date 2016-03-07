using JiraHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiraHelper.Services
{
    public class IssueByJira
    {
        public JiraCredential credential = new JiraCredential();
        private string urlJira = "http://ao.gmd.com.pe:8085/rest/api/2/issue/";
       

        public IssueByJira(string userName, string password)
        {
            this.credential.UserName = userName;
            this.credential.Password = password;
        }

        public string GetUssueByID(string Id)
        {
            string result = null;
            string newUrlJira = urlJira;
            JiraManager manager = new JiraManager(credential);
            result = manager.RunQuery(newUrlJira += Id, null, "GET");
            return result;
        }

        public string GetAllIssuesByProjectAndStatus(string projectName,string status)
        {
            
            string UrlByProject = string.Format("http://ao.gmd.com.pe:8085/rest/api/2/search?jql=project={0}&maxResults={1}&status{2}",projectName,9000,status);
            string result = null;
            string newUrlJira = urlJira;
            JiraManager manager = new JiraManager(credential);
            result = manager.RunQuery(UrlByProject, null, "GET");
            return result;
        
        }

    }
}
