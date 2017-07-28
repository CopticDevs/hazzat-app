using System;
using System.Diagnostics;
using System.Net.Http;

namespace Hazzat.Service.Providers.DataProviders.WebServiceProvider
{
    public class WebServiceHelper
    {
        public static void Execute(Action action)
        {
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    action();
                }

                testConnection.Dispose();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
