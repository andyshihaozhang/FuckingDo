class MarkdownEditor {
    constructor() {
        this.editor = new toastui.Editor({
            el: document.getElementById('editor'),
            initialEditType: 'markdown',
            previewStyle: 'vertical',
            height: '100vh',
            usageStatistics: false,
            hooks: {
                addImageBlobHook: this.uploadImage.bind(this)
            }
        });

        // 监听内容变化（节流处理）
        this.editor.on('change', () => {
            window.chrome.webview.postMessage({
                type: 'contentChanged',
                content: this.editor.getMarkdown()
            });
        });
    }

    uploadImage(blob, callback) {
        // 处理图片上传（可通过C#实现本地保存）
        const reader = new FileReader();
        reader.onload = () => {
            callback(reader.result, 'alt text');
        };
        reader.readAsDataURL(blob);
    }
}

// 初始化编辑器
window.editor = new MarkdownEditor();
