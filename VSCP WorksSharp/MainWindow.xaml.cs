using System.ComponentModel;
using System.Windows;

namespace VscpWorksSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Closing += MainWindowClosing;
            DataContext = new VscpViewModel();
        }

        private void MainWindowClosing(object sender, CancelEventArgs e)
        {
            var viewModel = DataContext as VscpViewModel;
            if (viewModel != null)
            {
                viewModel.Dispose();
            }
        }
    }
}
