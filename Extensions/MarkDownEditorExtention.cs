using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Interop;
using Wpf.Ui.Appearance;

namespace FuckingDo.Extensions
{
    public class MarkDownEditorExtention : IDisposable
    {
        private readonly WebView2 _webView;
        private readonly string _editorHtmlPath = Path.Combine(AppContext.BaseDirectory, "Assets/ToastUI/index.html");
        public event EventHandler<string>? OnContentChanged;


        public MarkDownEditorExtention(WebView2 webView)
        {
            _webView = webView;
        }

        public async Task InitializeAsync()
        {
            // 初始化WebView2环境
            var env = await CoreWebView2Environment.CreateAsync();
            await _webView.EnsureCoreWebView2Async(env);

            // 配置通信和导航
            _webView.CoreWebView2.WebMessageReceived += OnWebMessageReceived;
            _webView.CoreWebView2.Navigate(_editorHtmlPath);
        }

        private void OnWebMessageReceived(object? sender, CoreWebView2WebMessageReceivedEventArgs e)
        {
            if(sender is null)
            {
                return;
            }
            var msg = JsonSerializer.Deserialize<EditorMessage>(e.WebMessageAsJson);

            if (msg?.Content != null && msg.Type == "contentChanged")
            {
                OnContentChanged?.Invoke(this, msg.Content);
            }
        }

        // 设置markdown主题
        public async Task SetThemeAsync(ApplicationTheme appApplicationTheme)
        {
            var script = $"MarkdownBridge.setTheme({JsonSerializer.Serialize(appApplicationTheme.ToString())})";
            await _webView.ExecuteScriptAsync(script);
        }

        // 从C#设置Markdown内容
        public async Task SetMarkdownAsync(string content)
        {
            var script = $"MarkdownBridge.setContent({JsonSerializer.Serialize(content)})";
            await _webView.ExecuteScriptAsync(script);
        }
        // 获取当前Markdown内容
        public async Task<string> GetMarkdownAsync()
        {
            return await _webView.ExecuteScriptAsync("MarkdownBridge.getContent()");
        }
        // 销毁事件
        public void Dispose()
        {
            OnContentChanged = null;
            _webView?.Dispose();
        }
    }

    public record EditorMessage(string Type, string Content);

}
