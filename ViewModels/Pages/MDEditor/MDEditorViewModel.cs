using FuckingDo.Extensions;
using FuckingDo.Models.Monaco;
using FuckingDo.Services;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuckingDo.ViewModels.Pages.MDEditor
{
    public partial class MDEditorViewModel
    {
        private MarkDownEditorExtention? _markDownEditorExtention;

        public void SetWebView(WebView2 webView)
        {
            webView.NavigationCompleted += OnWebViewNavigationCompleted;
            webView.SetCurrentValue(FrameworkElement.UseLayoutRoundingProperty, true);
            webView.SetCurrentValue(WebView2.DefaultBackgroundColorProperty, System.Drawing.Color.Transparent);
            webView.SetCurrentValue(
                WebView2.SourceProperty,
                new Uri(
                    System.IO.Path.Combine(
                        System.AppDomain.CurrentDomain.BaseDirectory,
                        @"Assets\Monaco\index.html"
                    )
                )
            );

            _markDownEditorExtention = new MarkDownEditorExtention(webView);
        }

        private async Task InitializeEditorAsync()
        {
            if (_markDownEditorExtention == null)
            {
                return;
            }

            await _markDownEditorExtention.InitializeAsync();
            await _markDownEditorExtention.SetThemeAsync(Wpf.Ui.Appearance.ApplicationThemeManager.GetAppTheme());
        }

        private void OnWebViewNavigationCompleted(
            object? sender,
            Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e
        )
        {
            _ = DispatchAsync(InitializeEditorAsync);
        }

        private static DispatcherOperation<TResult> DispatchAsync<TResult>(Func<TResult> callback)
        {
            return Application.Current.Dispatcher.InvokeAsync(callback);
        }
    }
}
