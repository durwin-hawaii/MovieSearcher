using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MoviesAPI
{
    public class WebService
    {
        public static string Search(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                HttpWebResponse response = null;
                using (response = (HttpWebResponse)request.GetResponse())
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var encoding = ASCIIEncoding.ASCII;
                        using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                        {
                            string json = reader.ReadToEnd();
                            return json;
                        }
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
                return null;
            }
            return null;
        }
    }
}
