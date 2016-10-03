using Hazzat.Abstract;
using HazzatService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Hazzat.Views
{
    public partial class HymnPage : TabbedPage
    {
        public HymnPage(string breadcrumbName, string HymnName, int HymnId)
        {
            InitializeComponent();

            Title = $"{breadcrumbName} - {HymnName}";

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
                    });
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithHazzat", (sender) =>
            {
                if (App.NameViewModel?.HazzatHymnContentInfo != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();
                    source.BaseUrl = DependencyService.Get<IWebAssets>().Get();
                    //Append Coptic Font css
                    html.Append($@"<html><body>
                                    <style>
                                        @font-face {{
                                            font-family:Hazzat;
                                            src:url('Fonts/hazzat1_10a.ttf');
                                        }}
                                        @font-face{{
                                            font-family:'CS Copt';
                                            src:url('Fonts/CSCopt.ttf');
                                        }}
                                        .CopticFont
                                        {{
                                           font-family: 'CS Copt';
                                           color: orange;
                                        }}
                                        .HazzatFont
                                        {{
                                           font-family: Hazzat;
                                           color: green;
                                        }}
                                   </style>");
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
                    source.Html = html.Append("</body></html>").ToString();
                    HazzatWebView.Source = source;
                }
            });

            MessagingCenter.Subscribe<ByNameMainViewModel>(this, "DoneWithVerticalHazzat", (sender) =>
            {
                if (App.NameViewModel?.VerticalHazzatHymnContent != null)
                {
                    HtmlWebViewSource source = new HtmlWebViewSource();

                    var html = new StringBuilder();

                    foreach (var hymnContent in App.NameViewModel.VerticalHazzatHymnContent)
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
