using System;
using System.Net;
using System.Threading;

namespace Services
{
    internal static class WebResponse
    {

        public static ErrorDetail CheckHost(string url, int intervalReq)
        {
            var req1 = NetWebException(url);
            Thread.Sleep(intervalReq*1000);
            var req2 = NetWebException(url);

            if (req1 != null && req1.StatusCode == req2.StatusCode)
            {
                return new ErrorDetail {StatusCode = req1.StatusCode, StatusDescription = req1.StatusDescription};
            }
            return null;
        }


        private static ErrorDetail NetWebException(string url)
        {
            try
            {
                var myHttpWebRequest = (HttpWebRequest) WebRequest.Create(url);
                var myHttpWebResponse = (HttpWebResponse) myHttpWebRequest.GetResponse();

                myHttpWebResponse.Close();
                return null;
            }
            catch (WebException e)
            {
                return new ErrorDetail
                {
                    StatusCode = (int) ((HttpWebResponse) e.Response).StatusCode,
                    StatusDescription =
                        e.Message + "\n\nSzczegóły błędu: " + ((HttpWebResponse) e.Response).StatusDescription
                };
            }
            catch (Exception e)
            {
                return new ErrorDetail
                {
                    StatusDescription = e.Message,
                    StatusCode = -1
                };
            }
        }
    }
}