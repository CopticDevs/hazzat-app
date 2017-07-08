using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hazzat.Service.Providers.DataProviders.WebServiceProvider
{
    public class WebServiceHelper
    {
        public static void ExecuteActionAsync()
        {
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    //client.GetSeasonsAsync(isDateSpecific);
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
