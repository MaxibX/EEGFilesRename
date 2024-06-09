using System.Windows;

namespace EEGFilesRename
{
    /// <summary>
    /// Логика взаимодействия для WindowErrorVhdr.xaml
    /// </summary>
    public partial class WindowErrorVhdr : Window
    {
        public WindowErrorVhdr()
        {
            InitializeComponent();
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
