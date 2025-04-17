// 提供给C#调用的API
window.MarkdownBridge = {
    setContent: (content) => {
        window.editor.editor.setMarkdown(content);
    },
    getContent: () => {
        return window.editor.editor.getMarkdown();
    },
    setTheme: (theme) => {
        document.documentElement.setAttribute('data-theme', theme);
    }
};

// 接收来自C#的消息
window.chrome.webview.addEventListener('message', (event) => {
    if (event.data.type === 'loadContent') {
        window.MarkdownBridge.setContent(event.data.content);
    }
});
