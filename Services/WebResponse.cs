using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Services
{
    class WebResponse
    {

        public static ErrorDetail CheckHost(string url, int intervalReq)
        {
            var req1 = NetWebException(url);
            Thread.Sleep(intervalReq * 1000);
            var req2 = NetWebException(url);

            if (req1 != null && req1.StatusCode == req2.StatusCode)
            {
                return new ErrorDetail { StatusCode = req1.StatusCode, StatusDescription = req1.StatusDescription };
            }
            return null;
        }


        private static ErrorDetail NetWebException(string URL)
        {
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                myHttpWebResponse.Close();
                return null;
            }
            catch (WebException e)
            {
                return new ErrorDetail
                {
                    StatusCode = (int)((HttpWebResponse)e.Response).StatusCode,
                    StatusDescription = e.Message + "\n\nSzczegóły błędu: " + ((HttpWebResponse)e.Response).StatusDescription
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return new ErrorDetail
                {
                    StatusDescription = e.Message,
                    StatusCode = -1
                };
            }
        }
    }
}
