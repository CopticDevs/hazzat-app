using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using hazzat.com;
using System.Diagnostics;
using GalaSoft.MvvmLight.Messaging;
using System.ServiceModel;
using Xamarin.Forms;

namespace HazzatService
{
    public class HymnStructNameViewModel : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        private string _season;
        public string Season
        {
            get
            {
                return _season;
            }
            set
            {
                if (value != _season)
                {
                    _season = value;
                    RaisePropertyChanged("Season");
                }
            }
        }

        private string _content;
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value != _content)
                {
                    _content = value;
                    RaisePropertyChanged("Content");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ByNameMainViewModel
    {
        /// <summary>
        /// A collection for SeasonName objects
        /// </summary>
        public SeasonInfo[] Seasons { get; private set; }
        public HymnStructNameViewModel[] Hymns { get; private set; }
        public StructureInfo[] HymnsBySeason { get; private set; }
        public ServiceHymnInfo[] HazzatHymns { get; private set; }
        public ServiceHymnsContentInfo[] HymnContentInfo { get; private set; }

        public void createSeasonsViewModel(bool isDateSpecific)
        {
            MessagingCenter.Send(this, "Loading");
            try
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient(new BasicHttpBinding(), new EndpointAddress("http://hazzat.com/DesktopModules/Hymns/WebService/HazzatWebService.asmx"));
                client.GetSeasonsCompleted += new EventHandler<GetSeasonsCompletedEventArgs>(client_GetCompleted);
                client.GetSeasonsAsync(isDateSpecific);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void client_GetCompleted(object sender, GetSeasonsCompletedEventArgs e)
        {
            Seasons = e.Result;
            IsDataLoaded = true;
            MessagingCenter.Send(this, "Done");
        }



        public bool IsDataLoaded { get; set; }

        public void createViewModelBySeason(int Season)
        {
            Messenger.Default.Send(new NotificationMessage("Loading"));
            try
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient();
                client.GetSeasonServicesCompleted += new EventHandler<GetSeasonServicesCompletedEventArgs>(GetCompletedStructBySeason);
                client.GetSeasonServicesAsync(Season);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void GetCompletedStructBySeason(object sender, GetSeasonServicesCompletedEventArgs e)
        {
            HymnsBySeason = e.Result;
            Messenger.Default.Send(new NotificationMessage("Done"));
        }

        public void createViewModelHymns(int StructId)
        {
            Messenger.Default.Send(new NotificationMessage("Loading"));
            try
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient();
                client.GetSeasonServiceHymnsCompleted += new EventHandler<GetSeasonServiceHymnsCompletedEventArgs>(GetCompletedHymnsBySeason);
                client.GetSeasonServiceHymnsAsync(StructId);
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void GetCompletedHymnsBySeason(object sender, GetSeasonServiceHymnsCompletedEventArgs e)
        {
            HazzatHymns = e.Result;
            Messenger.Default.Send(new NotificationMessage("Done"));
        }


        public void createHymnTextViewModel(int itemId)
        {
            Messenger.Default.Send(new NotificationMessage("Loading"));
            try
            {
                HazzatWebServiceSoapClient client = new HazzatWebServiceSoapClient();
                client.GetSeasonServiceHymnTextCompleted += new EventHandler<GetSeasonServiceHymnTextCompletedEventArgs>(client_GetCompletedHymnInfo);
                client.GetSeasonServiceHymnTextAsync(itemId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void client_GetCompletedHymnInfo(object sender, GetSeasonServiceHymnTextCompletedEventArgs e)
        {
            HymnContentInfo = e.Result;
            IsDataLoaded = true;
            Messenger.Default.Send(new NotificationMessage("DoneWithContent"));
        }
    }
}
