using FuckingDo.ViewModels.Pages.MDEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuckingDo.Views.Pages.MDEditor
{
    /// <summary>
    /// MDEditor.xaml 的交互逻辑
    /// </summary>
    public partial class MDEditorPage : INavigableView<MDEditorViewModel>
    {

        public MDEditorViewModel ViewModel { get; init; }

        public MDEditorPage(MDEditorViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();

            ViewModel.SetWebView(MDWebView);
        }
    }
}
