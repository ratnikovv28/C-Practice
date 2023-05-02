using Practice_6.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace Practice_6
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowVM mainVM = new MainWindowVM();
        public MainWindow()
        {
            InitializeComponent();

            DataContext = mainVM;
        }

        //Возможность зажатием левой кнопки мыши двигать приложение
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
