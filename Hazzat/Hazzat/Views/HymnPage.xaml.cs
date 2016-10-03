using HazzatService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnPage : TabbedPage
    {
        private string navigationTitle { get; set; }

        public HymnPage(string breadcrumbName, string HymnName, int HymnId)
        {
            InitializeComponent();

            navigationTitle = breadcrumbName;
            Title = HymnName;

            SubscribeMessage();

            App.NameViewModel.CreateHymnTextViewModel(HymnId);
        }

        private void SubscribeMessage()
        {
            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHymnText", (sender) =>
            {
                if (App.NameViewModel?.HymnContentInfo != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var fullText = new StringBuilder();
                         
                        foreach (var hymnContent in App.NameViewModel.HymnContentInfo)
                        {
                            if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                            {
                                fullText.Append(hymnContent.Content_English);
                                fullText.Append(Environment.NewLine);
                                fullText.Append(Environment.NewLine);
                            }

                            if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                            {
                                fullText.Append(hymnContent.Content_Coptic);
                                fullText.Append(Environment.NewLine);
                                fullText.Append(Environment.NewLine);
                            }

                            if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                            {
                                fullText.Append(hymnContent.Content_Arabic);
                                fullText.Append(Environment.NewLine);
                                fullText.Append(Environment.NewLine);
                            }

                            // hymn content separator
                            fullText.Append(Environment.NewLine);
                            fullText.Append(Environment.NewLine);
                            fullText.Append("------------------------" + Environment.NewLine);
                        }

                        HymnText.Text = fullText.ToString();
                        Title = $"{navigationTitle} - {Title}";
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHazzat", (sender) =>
            {
                if (App.NameViewModel?.HazzatHymnContentInfo != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();

                    foreach(var hymnContent in App.NameViewModel.HazzatHymnContentInfo)
                    {
                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                        {
                            html.Append(hymnContent.Content_English);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                        {
                            html.Append(hymnContent.Content_Coptic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                        {
                            html.Append(hymnContent.Content_Arabic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        // hymn content separator
                        html.Append("<br /><br />");
                        html.Append("<br /><br />");
                        html.Append("<hr /><br />");
                    }

                    source.Html = html.ToString();
                    HazzatWebView.Source = source;
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithVerticalHazzat", (sender) =>
            {
                if (App.NameViewModel?.VerticalHazzatHymnContent != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();

                    foreach (var hymnContent in App.NameViewModel.HazzatHymnContentInfo)
                    {
                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_English))
                        {
                            html.Append(hymnContent.Content_English);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Coptic))
                        {
                            html.Append(hymnContent.Content_Coptic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        if (!string.IsNullOrWhiteSpace(hymnContent.Content_Arabic))
                        {
                            html.Append(hymnContent.Content_Arabic);
                            html.Append("<br /><br />");
                            html.Append("<br /><br />");
                        }

                        // hymn content separator
                        html.Append("<br /><br />");
                        html.Append("<br /><br />");
                        html.Append("<hr /><br />");
                    }

                    source.Html = html.ToString();
                    VerticalHazzatWebView.Source = source;
                }
            });
        }
    }
}
