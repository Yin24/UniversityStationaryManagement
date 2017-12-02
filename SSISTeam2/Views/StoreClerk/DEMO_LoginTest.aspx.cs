using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class DEMO_LoginTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Uri u = new Uri("http://localhost:65454/Default.aspx");
            //HttpWebRequest request = WebRequest.Create(u) as HttpWebRequest;
            //NetworkCredential credentials = new NetworkCredential("leo", "leo1234!", "domain");
            //var cache = new CredentialCache { { u, "NTLM", credentials } };
            //request.Credentials = cache;
            //HttpWebResponse response = request.GetResponse() as HttpWebResponse;

            //using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
            //{ pageSource = sr.ReadToEnd(); }
            //status = true;
            //return pageSource;

            bool yes = Membership.ValidateUser("leo", "leo1234!");

            Label1.Text = yes.ToString();
        }

        private static string GetDataFromPHP(string formUrl, string getUrl, string username, string password, out bool status)
        {

            try
            {

                string formParams = string.Format("access_login={0}&access_password={1}", username, password);
                string cookieHeader;
                WebRequest req = WebRequest.Create(formUrl);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                byte[] bytes = Encoding.ASCII.GetBytes(formParams);
                req.ContentLength = bytes.Length;
                using (Stream os = req.GetRequestStream())
                {
                    os.Write(bytes, 0, bytes.Length);
                }

                WebResponse resp = req.GetResponse();
                cookieHeader = resp.Headers["Set-cookie"];
                string pageSource;

                WebRequest getRequest = WebRequest.Create(getUrl);
                getRequest.Headers.Add("Cookie", cookieHeader);
                WebResponse getResponse = getRequest.GetResponse();
                using (StreamReader sr = new StreamReader(getResponse.GetResponseStream()))
                {
                    pageSource = sr.ReadToEnd();
                }
                status = true;

                return pageSource;
            }



            catch (System.Exception ex)
            {
                status = false;
                return string.Empty;
            }

        }
    }
}