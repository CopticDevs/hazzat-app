using Hazzat.Service.Data;
using System;
using System.ServiceModel;

namespace Hazzat.Service.Providers.DataProviders.WebServiceProvider
{
    public class WebServiceProvider : DataProvider
    {
        private const string HazzatServiceEndpoint = "http://hazzat.com/DesktopModules/Hymns/WebService/HazzatWebService.asmx";
        private const int HazzatServiceMaxReceivedMessageSize = 2147483647;
        private const int HazzatServiceMaxBufferPoolSize = 2147483647;
        private const int HazzatServiceMaxBufferSize = 2147483647;

        private BasicHttpBinding HazzatServiceBinding
        {
            get
            {
                return new BasicHttpBinding()
                {
                    MaxReceivedMessageSize = HazzatServiceMaxReceivedMessageSize,
                    MaxBufferSize = HazzatServiceMaxBufferSize,
                    MaxBufferPoolSize = HazzatServiceMaxBufferPoolSize
                };
            }
        }

        public override void GetSeasons(bool isDateSpecific, Action<object, GetSeasonsCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonsCompleted += new EventHandler<GetSeasonsCompletedEventArgs>(callback);
                client.GetSeasonsAsync(isDateSpecific);
            });
        }

        public override void GetSeasonServices(int seasonId, Action<object, GetSeasonServicesCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonServicesCompleted += new EventHandler<GetSeasonServicesCompletedEventArgs>(callback);
                client.GetSeasonServicesAsync(seasonId);
            });
        }

        public override void GetSeasonServiceHymns(int structureId, Action<object, GetSeasonServiceHymnsCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonServiceHymnsCompleted += new EventHandler<GetSeasonServiceHymnsCompletedEventArgs>(callback);
                client.GetSeasonServiceHymnsAsync(structureId);
            });
        }

        public override void GetSeasonServiceHymnText(int itemId, Action<object, GetSeasonServiceHymnTextCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonServiceHymnTextCompleted += new EventHandler<GetSeasonServiceHymnTextCompletedEventArgs>(callback);
                client.GetSeasonServiceHymnTextAsync(itemId);
            });            
        }

        public override void GetSeasonServiceHymnHazzat(int itemId, Action<object, GetSeasonServiceHymnHazzatCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonServiceHymnHazzatCompleted += new EventHandler<GetSeasonServiceHymnHazzatCompletedEventArgs>(callback);
                client.GetSeasonServiceHymnHazzatAsync(itemId);
            });
        }

        public override void GetSeasonServiceHymnVerticalHazzat(int itemId, Action<object, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonServiceHymnVerticalHazzatCompleted += new EventHandler<GetSeasonServiceHymnVerticalHazzatCompletedEventArgs>(callback);
                client.GetSeasonServiceHymnVerticalHazzatAsync(itemId);
            });
        }

        public override void GetTypeList(Action<object, GetTypeListCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetTypeListCompleted += new EventHandler<GetTypeListCompletedEventArgs>(callback);
                client.GetTypeListAsync();
            });
        }

        public override void GetSeasonsByTypeId(int typeId, Action<object, GetSeasonsByTypeIDCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonsByTypeIDCompleted += new EventHandler<GetSeasonsByTypeIDCompletedEventArgs>(callback);
                client.GetSeasonsByTypeIDAsync(typeId);
            });
        }

        public override void GetServiceHymnListBySeasonIdAndTypeId(int seasonId, int typeId, Action<object, GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetServiceHymnListBySeasonIdAndTypeIdCompleted += new EventHandler<GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs>(callback);
                client.GetServiceHymnListBySeasonIdAndTypeIdAsync(seasonId, typeId);
            });
        }

        public override void GetServiceHymnListBySeasonIdAndTuneId(int seasonId, int tuneId, Action<object, GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetServiceHymnListBySeasonIdAndTuneIdCompleted += new EventHandler<GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs>(callback);
                client.GetServiceHymnListBySeasonIdAndTuneIdAsync(seasonId, tuneId);
            });
        }

        public override void GetTuneList(Action<object, GetTuneListCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetTuneListCompleted += new EventHandler<GetTuneListCompletedEventArgs>(callback);
                client.GetTuneListAsync();
            });
        }

        public override void GetSeasonsByTuneId(int tuneId, Action<object, GetSeasonsByTuneIDCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetSeasonsByTuneIDCompleted += new EventHandler<GetSeasonsByTuneIDCompletedEventArgs>(callback);
                client.GetSeasonsByTuneIDAsync(tuneId);
            });
        }

        public override void GetTextRowDelimiterToken(Action<object, GetTextRowDelimiterTokenCompletedEventArgs> callback)
        {
            WebServiceHelper.Execute(() =>
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                client.GetTextRowDelimiterTokenCompleted += new EventHandler<GetTextRowDelimiterTokenCompletedEventArgs>(callback);
                client.GetTextRowDelimiterTokenAsync();
            });
        }
    }
}
