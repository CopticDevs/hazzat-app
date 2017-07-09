using hazzat.com;
using Hazzat.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hazzat.ViewModels
{
    public class MainViewModel
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

        /// <summary>
        /// A collection of hazzat.com objects
        /// </summary>
        public SeasonInfo[] Seasons { get; private set; }
        public StructureInfo[] HymnsBySeason { get; private set; }
        public ServiceHymnsContentInfo[] TextHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] HazzatHymnContentInfo { get; private set; }
        public ServiceHymnsContentInfo[] VerticalHazzatHymnContent { get; private set; }

        public TypeInfo[] TypeList { get; private set; }
        public SeasonInfo[] TypeSeasons { get; private set; }

        public TuneInfo[] TuneList { get; private set; }
        public SeasonInfo[] TuneSeasons { get; private set; }

        private HazzatController hazzatController = new HazzatController();

        public void GetSeasons(bool isDateSpecific)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasons(isDateSpecific, OnGetSeasonsCompleted);
        }

        public void OnGetSeasonsCompleted(object sender, GetSeasonsCompletedEventArgs e)
        {
            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    item.Name = $"{item.Name} ({item.ServiceHymnsCount})";
                }
            }

            Seasons = e.Result;

            MessagingCenter.Send(this, "Done");
            App.IsLoaded = true;
        }

        public void GetSeasonServices(int seasonId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonServices(seasonId, OnGetSeasonServicesCompleted);
        }

        public void OnGetSeasonServicesCompleted(object sender, GetSeasonServicesCompletedEventArgs e)
        {
            HymnsBySeason = e.Result;
            MessagingCenter.Send(this, "DoneSeason");
            App.IsLoaded = true;
        }

        public void GetSeasonServiceHymns(int structureId, Action<object, GetSeasonServiceHymnsCompletedEventArgs> GetCompletedHymnsBySeason)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonServiceHymns(structureId, GetCompletedHymnsBySeason);
        }

        public void GetHymnContent(int itemId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            hazzatController.GetSeasonServiceHymnText(itemId, OnGetSeasonServiceHymnTextCompleted);
            hazzatController.GetSeasonServiceHymnHazzat(itemId, OnGetSeasonServiceHymnHazzatCompleted);
            hazzatController.GetSeasonServiceHymnVerticalHazzat(itemId, OnGetSeasonServiceHymnVerticalHazzatCompleted);
        }

        public void OnGetSeasonServiceHymnTextCompleted(object sender, GetSeasonServiceHymnTextCompletedEventArgs e)
        {
            TextHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHymnText");
            App.IsLoaded = true;
        }

        public void OnGetSeasonServiceHymnHazzatCompleted(object sender, GetSeasonServiceHymnHazzatCompletedEventArgs e)
        {
            HazzatHymnContentInfo = e.Result;
            MessagingCenter.Send(this, "DoneWithHazzat");
            App.IsLoaded = true;
        }

        private void OnGetSeasonServiceHymnVerticalHazzatCompleted(object sender, GetSeasonServiceHymnVerticalHazzatCompletedEventArgs e)
        {
            VerticalHazzatHymnContent = e.Result;
            MessagingCenter.Send(this, "DoneWithVerticalHazzat");
            App.IsLoaded = true;
        }

        #region byType
        public void GetHymnsByType()
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetTypeListCompleted += new EventHandler<GetTypeListCompletedEventArgs>(GetCompletedTypeList);
                    client.GetTypeListAsync();
                }
                testConnection.Dispose();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GetCompletedTypeList(object sender, GetTypeListCompletedEventArgs e)
        {
            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    item.Name = $"{item.Name} ({item.ServiceHymnsCount})";
                }
            }

            TypeList = e.Result;
            MessagingCenter.Send(this, "DoneWithTypeList");
            App.IsLoaded = true;
        }

        public void GetSeasonsByType(int typeId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetSeasonsByTypeIDCompleted += new EventHandler<GetSeasonsByTypeIDCompletedEventArgs>(client_GetSeasonsByTypeID);
                    client.GetSeasonsByTypeIDAsync(typeId);
                }
                testConnection.Dispose();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void client_GetSeasonsByTypeID(object sender, GetSeasonsByTypeIDCompletedEventArgs e)
        {
            TypeSeasons = e.Result;
            MessagingCenter.Send(this, "DoneWithSeasonsListByType");
            App.IsLoaded = true;
        }

        public void GetServiceHymnListBySeasonIdAndTypeId(int seasonId, int typeId, Action<object, GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs> getCompletedHymnsBySeasonAndType)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetServiceHymnListBySeasonIdAndTypeIdCompleted += new EventHandler<GetServiceHymnListBySeasonIdAndTypeIdCompletedEventArgs>(getCompletedHymnsBySeasonAndType);
                    client.GetServiceHymnListBySeasonIdAndTypeIdAsync(seasonId, typeId);
                }
                testConnection.Dispose();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void GetServiceHymnListBySeasonIdAndTuneId(int seasonId, int tuneId, Action<object, GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs> getCompletedHymnsBySeasonAndTune)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetServiceHymnListBySeasonIdAndTuneIdCompleted += new EventHandler<GetServiceHymnListBySeasonIdAndTuneIdCompletedEventArgs>(getCompletedHymnsBySeasonAndTune);
                    client.GetServiceHymnListBySeasonIdAndTuneIdAsync(seasonId, tuneId);
                }
                testConnection.Dispose();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region byTune
        public void GetHymnsByTune()
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetTuneListCompleted += new EventHandler<GetTuneListCompletedEventArgs>(GetCompletedTuneList);
                    client.GetTuneListAsync();
                }
                testConnection.Dispose();

            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void GetCompletedTuneList(object sender, GetTuneListCompletedEventArgs e)
        {
            foreach (var item in e.Result)
            {
                if (item?.ServiceHymnsCount != null)
                {
                    item.Name = $"{item.Name} ({item.ServiceHymnsCount})";
                }
            }

            TuneList = e.Result;
            MessagingCenter.Send(this, "DoneWithTuneList");
            App.IsLoaded = true;
        }

        public void ByTuneGetSeasons(int tuneId)
        {
            App.IsLoaded = false;
            MessagingCenter.Send(this, "Loading");
            try
            {
                HttpClient testConnection = new HttpClient();

                if (!String.IsNullOrWhiteSpace(testConnection.GetAsync("http://hazzat.com").Result.Content.ToString()))
                {
                    HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(HazzatServiceBinding, new EndpointAddress(HazzatServiceEndpoint));
                    client.GetSeasonsByTuneIDCompleted += new EventHandler<GetSeasonsByTuneIDCompletedEventArgs>(client_ByTuneGetSeasons);
                    client.GetSeasonsByTuneIDAsync(tuneId);
                }
                testConnection.Dispose();
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void client_ByTuneGetSeasons(object sender, GetSeasonsByTuneIDCompletedEventArgs e)
        {
            TuneSeasons = e.Result;
            MessagingCenter.Send(this, "DoneWithSeasonsListByTune");
            App.IsLoaded = true;
        }



        #endregion
    }
}
