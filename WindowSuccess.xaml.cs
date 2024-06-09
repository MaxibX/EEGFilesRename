using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowSuccess.xaml
    /// </summary>
    public partial class WindowSuccess : Window
    {
        public WindowSuccess()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
