using System.Net;
using TimerSK.TextRes;
using System.IO;
using Newtonsoft.Json.Linq;

namespace TimerSK.Tools
{
    class HTTPRequest
    {

        public bool CheckToken(string token)
        {
            string hashedToken = new Security().GetHashString(token);
            bool result = true;
            string postedToken = "tokenCheck=" + hashedToken;
            WebRequest webRequest = WebRequest.Create(URLS.CHECK_TOKEN_URL);

            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());

            streamWriter.Write(postedToken);
            streamWriter.Flush();

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

            JObject json = JObject.Parse(streamReader.ReadToEnd());

            if((int)json["request"] == 0)
            {
                result = false;
            }

            return result;
        }

        public string GetJsonFromAPI(string token)
        {
            token = "token=" + token;
            WebRequest webRequest = WebRequest.Create(URLS.GET_JSON_URL);

            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "POST";

            StreamWriter streamWriter = new StreamWriter(webRequest.GetRequestStream());

            streamWriter.Write(token);
            streamWriter.Flush();

            var httpResponse = (HttpWebResponse)webRequest.GetResponse();

            StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream());

            return streamReader.ReadToEnd();

        }

    }
}
